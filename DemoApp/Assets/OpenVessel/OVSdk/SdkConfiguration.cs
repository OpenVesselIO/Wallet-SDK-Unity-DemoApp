using System;
using UnityEngine;

namespace OVSdk
{
    [Serializable]
    public class SdkConfiguration
    {
        [SerializeField] public SdkLogLevel MinLogLevel = SdkLogLevel.Error;

        [SerializeField] public string CallbackUrl;
    }
}