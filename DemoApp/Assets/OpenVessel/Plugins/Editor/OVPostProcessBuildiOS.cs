using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

#if UNITY_IOS || UNITY_IPHONE

namespace OVSdk
{
    public class OVPostProcessBuildiOS
    {
        private const string CocoapodsPluginName = "cocoapods-openvessel";

        [PostProcessBuild(int.MaxValue)]
        public static void OvPostProcessPlist(BuildTarget buildTarget, string path)
        {
            var plistPath = Path.Combine(path, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            AddApplicationQueriesSchemesIfNeeded(plist);

            plist.WriteToFile(plistPath);
        }

        // must be between 40 and 50 to ensure that it's not overriden by Podfile generation (40) and that it's added before "pod install" (50)
        [PostProcessBuild(45)] 
        private static void OvPostProcessPodfile(BuildTarget target, string buildPath)
        {
            using (StreamWriter sw = File.AppendText(buildPath + "/Podfile"))
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

        private static string GetInstalledCocoapodsPlugins()
        {
            var startInfo = new ProcessStartInfo("bash");
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
    }
}

#endif