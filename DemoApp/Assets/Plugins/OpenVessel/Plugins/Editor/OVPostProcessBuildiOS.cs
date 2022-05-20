using System.Collections.Generic;
using System.IO;
using UnityEditor;
using System.Diagnostics;
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
        [PostProcessBuildAttribute(45)] 
        private static void OvPostProcessPodfile(BuildTarget target, string buildPath)
        {
            using (StreamWriter sw = File.AppendText(buildPath + "/Podfile"))
            {
                sw.WriteLine($"plugin '{CocoapodsPluginName}'");
            }
 
            using (var process = Process.Start("gem", $"install {CocoapodsPluginName}"))
            {
                process.WaitForExit();
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