using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

#if UNITY_IOS || UNITY_IPHONE

namespace OVSdk
{
    public class OVPostProcessBuildiOS
    {
        [PostProcessBuild(int.MaxValue)]
        public static void OvPostProcessPlist(BuildTarget buildTarget, string path)
        {
            var plistPath = Path.Combine(path, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            AddApplicationQueriesSchemesIfNeeded(plist);
            
            plist.WriteToFile(plistPath);
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