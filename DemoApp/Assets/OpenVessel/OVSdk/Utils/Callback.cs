using System;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace OVSdk.Utils
{

    public static class Callback
    {

        public const string DefaultCallbackUrlHost = "callback";

#if !UNITY_EDITOR

        public static string GetDefaultCallbackUrl()
        {
            return $"{GetDefaultCallbackUrlScheme()}://{DefaultCallbackUrlHost}";
        }

#endif

        public static string GetDefaultCallbackUrlScheme(
#if UNITY_EDITOR
            BuildTargetGroup targetGroup
#endif
        )
        {
            string appId;
#if UNITY_EDITOR
            appId = PlayerSettings.GetApplicationIdentifier(targetGroup);
#else
            appId = Application.identifier;
#endif
            return $"io.openvessel.{appId}";
        }

    }
}