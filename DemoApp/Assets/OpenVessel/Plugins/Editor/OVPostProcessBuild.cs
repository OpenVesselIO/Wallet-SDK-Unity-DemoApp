using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace OVSdk
{
    public class OVPostProcessBuildiOS
    {

        private const string CocoapodsPluginName = "cocoapods-openvessel";

        [PostProcessBuild(int.MaxValue)]
        public static void OvPostProcessPlist(BuildTarget buildTarget, string path)
        {
            if (BuildTarget.iOS != buildTarget)
            {
                return;
            }

            var plistPath = Path.Combine(path, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            AddApplicationQueriesSchemesIfNeeded(plist);
            AddCustomUrlSchemesIfNeeded(plist);

            plist.WriteToFile(plistPath);
        }

        // must be between 40 and 50 to ensure that it's not overriden by Podfile generation (40) and that it's added before "pod install" (50)
        [PostProcessBuild(45)]
        private static void OvPostProcessPodfile(BuildTarget target, string buildPath)
        {
            if (BuildTarget.iOS != target)
            {
                return;
            }

            using (StreamWriter sw = File.AppendText(Path.Combine(buildPath, "Podfile")))
            {
                sw.WriteLine($"plugin '{CocoapodsPluginName}'");
                sw.WriteLine(@"
                    post_install do |installer|
                        deployment_target_key = 'IPHONEOS_DEPLOYMENT_TARGET'

                        installer.pods_project.targets.each do |pt|
                            next if not pt.name.start_with?('Capacitor')

                            pt.build_configurations.each do |config|
                                if config.build_settings[deployment_target_key] < '13'
                                    config.build_settings[deployment_target_key] = '13.0'
                                end
                            end
                        end
                    end
                ");
            }

            using (var process = Process.Start("bash", $"-l -c 'gem install {CocoapodsPluginName}'"))
            {
                process.WaitForExit();
            }

            if (!Regex.IsMatch(GetInstalledCocoapodsPlugins(), $"\\b{CocoapodsPluginName}\\b"))
            {
                var tmpFilePath = Path.GetTempFileName();

                File.WriteAllText(
                    tmpFilePath,
                    "#!/bin/bash\n" +
                    "ROOT_PPID=$(ps -o ppid= -p $PPID)\n" +
                    "set -x\n" +
                    $"sudo gem install {CocoapodsPluginName}\n" +
                    "kill $(ps -o ppid= -p $ROOT_PPID)"
                );

                using (var process = Process.Start("chmod", $"+x {tmpFilePath}"))
                {
                    process.WaitForExit();
                }

                var startInfo = new ProcessStartInfo("open");
                startInfo.Arguments = $"-W -F -n -a Terminal.app {tmpFilePath}";
                startInfo.UseShellExecute = false;

                using (var process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                }
            }
        }

        [PostProcessBuild(int.MaxValue)]
        public static void OvPostProcessAndroidManifest(BuildTarget buildTarget, string path)
        {
            if (BuildTarget.Android != buildTarget)
            {
                return;
            }

            const string activityElementName = "activity";
            const string dataElementName = "data";
            const string intentFilterElementName = "intent-filter";

            const string nameAttributeName = "name";
            const string schemeAttributeName = "scheme";

            const string deepLinkHandlerActivityName = "io.openvessel.wallet.sdk.activities.DeeplinkActivity";

            const string androidNamespace = "android";
            const string androidNamespaceUri = "http://schemas.android.com/apk/res/android";

            var callbackUrlScheme = Callback.GetDefaultCallbackUrlScheme(BuildTargetGroup.Android);

            var manifestPath = Path.Combine(path, "unityLibrary", "src", "main", "AndroidManifest.xml");

            var manifest = new XmlDocument();
            manifest.PreserveWhitespace = true;
            manifest.Load(manifestPath);

            var namespaceManager = new XmlNamespaceManager(manifest.NameTable);
            namespaceManager.AddNamespace(androidNamespace, androidNamespaceUri);

            var applicationElement = (XmlElement)manifest.SelectSingleNode("/manifest/application");

            var deepLinkHandlerActivityElement = (XmlElement)applicationElement.SelectSingleNode(
                $"{activityElementName}[@{androidNamespace}:{nameAttributeName} = '{deepLinkHandlerActivityName}']",
                namespaceManager
            );

            if (deepLinkHandlerActivityElement == null)
            {
                deepLinkHandlerActivityElement = manifest.CreateElement(activityElementName);

                applicationElement.AppendChild(deepLinkHandlerActivityElement);

                deepLinkHandlerActivityElement.SetAttribute(
                    nameAttributeName,
                    androidNamespaceUri,
                    deepLinkHandlerActivityName
                );
                deepLinkHandlerActivityElement.SetAttribute("exported", androidNamespaceUri, "true");
            }

            var intentFilterDataXPath = (
                $"{intentFilterElementName}/{dataElementName}[@{androidNamespace}:{schemeAttributeName} = '{callbackUrlScheme}']"
            );

            if (deepLinkHandlerActivityElement.SelectSingleNode(intentFilterDataXPath, namespaceManager) == null)
            {
                var intentFilterElement = manifest.CreateElement(intentFilterElementName);

                deepLinkHandlerActivityElement.AppendChild(intentFilterElement);

                var actionElement = manifest.CreateElement("action");
                actionElement.SetAttribute(nameAttributeName, androidNamespaceUri, "android.intent.action.VIEW");

                string[] categories = { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" };

                foreach (var category in categories)
                {
                    var categoryElement = manifest.CreateElement("category");
                    categoryElement.SetAttribute(nameAttributeName, androidNamespaceUri, category);

                    intentFilterElement.AppendChild(categoryElement);
                }

                var dataElement = manifest.CreateElement(dataElementName);
                dataElement.SetAttribute(schemeAttributeName, androidNamespaceUri, callbackUrlScheme);
                dataElement.SetAttribute("host", androidNamespaceUri, Callback.DefaultCallbackUrlHost);

                intentFilterElement.AppendChild(dataElement);
                intentFilterElement.AppendChild(actionElement);

                manifest.Save(manifestPath);
            }
        }

        private static string GetInstalledCocoapodsPlugins()
        {
            var startInfo = new ProcessStartInfo("zsh");
            startInfo.Arguments = "-l -c 'pod plugins installed'";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            using (var process = Process.Start(startInfo))
            {
                try
                {
                    return process.StandardOutput.ReadToEnd();
                }
                finally
                {
                    process.WaitForExit();
                }
            }
        }

        private static void AddApplicationQueriesSchemesIfNeeded(PlistDocument plist)
        {
            plist.root.values.TryGetValue("LSApplicationQueriesSchemes", out var applicationQueriesSchemesItems);

            var existingApplicationQueryScheme = new HashSet<string>();
            if (applicationQueriesSchemesItems != null &&
                applicationQueriesSchemesItems.GetType() == typeof(PlistElementArray))
            {
                var plistElementDictionaries = applicationQueriesSchemesItems.AsArray().values;
                foreach (var plistElement in plistElementDictionaries)
                {
                    existingApplicationQueryScheme.Add(plistElement.AsString());
                }
            }
            // Else, create an array of LSApplicationQueriesSchemes into which we will add our deeplink.
            else
            {
                applicationQueriesSchemesItems = plist.root.CreateArray("LSApplicationQueriesSchemes");
            }

            if (!existingApplicationQueryScheme.Contains("vesselwa"))
            {
                applicationQueriesSchemesItems.AsArray().AddString("vesselwa");
            }
        }

        private static void AddCustomUrlSchemesIfNeeded(PlistDocument plist)
        {
            const string urlTypesKey = "CFBundleURLTypes";
            const string schemesKey = "CFBundleURLSchemes";

            plist.root.values.TryGetValue(urlTypesKey, out var urlTypes);

            var existingSchemes = new HashSet<string>();

            if (urlTypes != null && urlTypes.GetType() == typeof(PlistElementArray))
            {
                var plistElementDictionaries = urlTypes.AsArray().values;

                foreach (var plistElementDict in plistElementDictionaries)
                {
                    if (plistElementDict.GetType() == typeof(PlistElementDict))
                    {
                        var schemes = plistElementDict.AsDict().values[schemesKey];

                        if (schemes != null && schemes.GetType() == typeof(PlistElementArray))
                        {
                            foreach (var existingScheme in schemes.AsArray().values)
                            {
                                if (existingScheme.GetType() == typeof(PlistElementString))
                                {
                                    existingSchemes.Add(existingScheme.AsString());
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                urlTypes = plist.root.CreateArray(urlTypesKey);
            }

            var scheme = Callback.GetDefaultCallbackUrlScheme(BuildTargetGroup.iOS);

            if (!existingSchemes.Contains(scheme))
            {
                var schemeDict = urlTypes.AsArray().AddDict();
                schemeDict.SetString("CFBundleURLName", "");
                schemeDict.CreateArray(schemesKey).AddString(scheme);
            }
        }
    }
}
