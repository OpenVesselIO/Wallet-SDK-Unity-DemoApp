using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace OVSdk
{
#if UNITY_IOS
    public class SdkiOs : SdkBase
    {
        [DllImport("__Internal")]
        private static extern void _OVSetConfiguration(string configurationJson);

        private static SdkConfiguration _configuration;

        public static SdkConfiguration Configuration
        {
            get => _configuration;
            set
            {
                _configuration= value;

                var configurationStr = JsonUtility.ToJson(value);
                _OVSetConfiguration(configurationStr);
            }
        }

        /// <summary>
        /// App connect manager enabling connecting this app to the central OpenWallet Vessel.
        /// </summary>
        /// <returns>Connection manager</returns>
        public static AppConnectManageriOs AppConnectManager => new AppConnectManageriOs();

        /// <summary>
        /// An object that can present various wallet views
        /// </summary>
        /// <returns>Wallet presenter</returns>
        public static WalletPresenteriOs WalletPresenter => new WalletPresenteriOs();

        /// <summary>
        /// IAP manager allows to load balance by IAPs.
        /// </summary>
        /// <returns>IAP manager</returns>
        public static IapManageriOs IapManager => new IapManageriOs();

        static SdkiOs()
        {
            InitCallbacks();
        }

        [DllImport("__Internal")]
        private static extern void _OVInitialize(string userId);


        /// <summary>
        /// Initialize Open Vessel SDK
        /// </summary>
        /// <param name="userId">
        /// In-app user ID
        /// </param>
        public static void Initialize(string userId)
        {
            _OVInitialize(userId);

            Application.deepLinkActivated += OnDeepLinkActivated;

            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                OnDeepLinkActivated(Application.absoluteURL);
            }
        }

        [DllImport("__Internal")]
        private static extern void _OVSetEnvironment(string environment);

        [DllImport("__Internal")]
        private static extern string _OVGetEnvironment();

        [DllImport("__Internal")]
        private static extern bool _OVHandleDeeplink(string deeplink);

        /// <summary>
        /// Handle a deeplink that returns into the app connect flow
        /// Returns <c>true</c> if OpenVessel connect was able to recognize and handle the link
        /// </summary>
        public bool HandleDeeplink(string deeplink)
        {
            return false;
        }

        private static void OnDeepLinkActivated(string url)
        {
            _OVHandleDeeplink(url);
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
                var envString = _OVGetEnvironment();
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

            set { _OVSetEnvironment(value.ToString().ToUpperInvariant()); }
        }
    }
#endif
}