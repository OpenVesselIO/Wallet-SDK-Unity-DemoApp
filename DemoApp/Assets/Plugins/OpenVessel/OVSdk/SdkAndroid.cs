using UnityEngine;

namespace OVSdk
{
#if UNITY_ANDROID
    public class SdkAndroid : SdkBase
    {
        private static readonly AndroidJavaClass OvSdkPluginClass =
            new AndroidJavaClass("io.openvessel.sdk.unity.SdkPlugin");

        static SdkAndroid()
        {
            InitCallbacks();
        }

        /// <summary>
        /// App connect manager enabling connecting this app to the central OpenWallet Vessel.
        /// </summary>
        /// <returns>Connection manager</returns>
        public static AppConnectManagerAndroid AppConnectManager => new AppConnectManagerAndroid();

        /// <summary>
        /// Initialize Open Vessel SDK
        /// </summary>
        public static void Initialize(string userId)
        {
            OvSdkPluginClass.CallStatic("initialize", userId);
        }


        /// <summary>
        /// Show wallet user wallet activity.
        /// <b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <b>Please note</b>: use {@link AppConnectManager} to connect wallet to this app
        ///
        /// </summary>
        public static void LoadWalletView()
        {
            OvSdkPluginClass.CallStatic("loadWalletView");
        }
    }
#endif
}