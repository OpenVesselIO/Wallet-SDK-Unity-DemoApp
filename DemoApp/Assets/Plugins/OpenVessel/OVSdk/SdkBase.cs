using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class SdkBase
    {
        protected static void InitCallbacks()
        {
            var sdkCallbackObject =
                new GameObject("OVSdkCallbacks", typeof(SdkCallbacks))
                    .GetComponent<SdkCallbacks>(); // Its Awake() method sets Instance.

            if (SdkCallbacks.Instance != sdkCallbackObject)
            {
                Logger.UserWarning("It looks like you have the " + sdkCallbackObject.name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
            }

            var appManagerCallbackObject =
                new GameObject("OVAppConnectManagerCallbacks", typeof(AppConnectManagerCallbacks))
                    .GetComponent<AppConnectManagerCallbacks>(); // Its Awake() method sets Instance.

            if (AppConnectManagerCallbacks.Instance != appManagerCallbackObject)
            {
                Logger.UserWarning("It looks like you have the " + appManagerCallbackObject.name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
            }
        }
    }
}