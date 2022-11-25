using System;
using UnityEngine;

namespace OVSdk
{
#if UNITY_ANDROID
    public class SdkAndroid : SdkBase
    {
        private static readonly AndroidJavaClass OvSdkPluginClass =
            new AndroidJavaClass("io.openvessel.sdk.unity.SdkPlugin");


        private static SdkConfiguration _configuration;

        public static SdkConfiguration Configuration
        {
            get => _configuration;
            set
            {
                _configuration = value;

                var configurationStr = JsonUtility.ToJson(value);
                OvSdkPluginClass.CallStatic("setConfiguration", configurationStr);
            }
        }

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
        /// An object that can present various wallet views
        /// </summary>
        /// <returns>Wallet presenter</returns>
        public static WalletPresenterAndroid WalletPresenter => new WalletPresenterAndroid();

        /// <summary>
        /// Initialize Open Vessel SDK
        /// </summary>
        public static void Initialize(string userId)
        {
            OvSdkPluginClass.CallStatic("initialize", userId);
        }

        /// <summary>
        /// Assign the environment that should be used for this SDK.
        /// <b>Please note</b>: the environment should be set before calling <code>Initialize()</code>
        /// </summary>
        /// <param name="Environment">
        /// Environment to set. Must not be null. PRODUCTION by default.
        /// </param>
        public static VesselEnvironment Environment
        {
            get
            {
                var envString = OvSdkPluginClass.CallStatic<string>("getEnvironment");
                var envParsed = Enum.TryParse(envString, true, out VesselEnvironment parsedEnv);
                if (envParsed)
                {
                    return parsedEnv;
                }
                else
                {
                    return VesselEnvironment.Production;
                }
            }

            set { OvSdkPluginClass.CallStatic("setEnvironment", value.ToString().ToUpperInvariant()); }
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
        
        /// <summary>
        /// Handle a deeplink that returns into the app connect flow
        /// Returns <c>true</c> if OpenVessel connect was able to recognize and handle the link
        /// </summary>
        public bool HandleDeeplink(string deeplink)
        {
            return false;
        }
    }
#endif
}