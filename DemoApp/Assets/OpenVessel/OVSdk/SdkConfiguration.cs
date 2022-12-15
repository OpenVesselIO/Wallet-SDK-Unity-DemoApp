using System;
using UnityEngine;
using OVSdk.Utils;

namespace OVSdk
{
    [Serializable]
    public class SdkConfiguration
    {

        [SerializeField] public SdkLogLevel MinLogLevel = SdkLogLevel.Error;

        [SerializeField] public string CallbackUrl;

#if !UNITY_EDITOR

        public SdkConfiguration()
        {
            CallbackUrl = Callback.GetDefaultCallbackUrl();
        }

#endif
    }
}