using System;
using System.Runtime.InteropServices;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class PopupUtils
{
#if UNITY_ANDROID
    private static readonly AndroidJavaClass ToastPluginClass =
            new AndroidJavaClass("io.openvessel.unity.ToastPlugin");
#endif

#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _OVUShowAlert(string text);
#endif

    public static void ShowPopup(string text)
    {
#if UNITY_ANDROID
        ToastPluginClass.CallStatic("showTextShort", text);
#elif UNITY_IOS
        _OVUShowAlert(text);
#else
        EditorUtility.DisplayDialog("", text, "OK");
#endif
    }

}