using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class SdkUnityEditor : SdkBase
    {
        private static SdkConfiguration _configuration;

        public static SdkConfiguration Configuration
        {
            get => _configuration;

            set
            {
                _configuration = value;
                Logger.UserDebug("Setting vessel config: " + JsonUtility.ToJson(value));
            }
        }

        /// <summary>
        /// App connect manager enabling connecting this app to the central OpenWallet Vessel.
        /// </summary>
        /// <returns>Connection manager</returns>
        public static AppConnectManagerUnityEditor AppConnectManager => new AppConnectManagerUnityEditor();

        /// <summary>
        /// An object that can present various wallet views
        /// </summary>
        /// <returns>Wallet presenter</returns>
        public static WalletPresenterUnityEditor WalletPresenter => new WalletPresenterUnityEditor();

        static SdkUnityEditor()
        {
            InitCallbacks();
        }

        /// <summary>
        /// Assign the environment that should be used for this SDK.
        /// <b>Please note</b>: the environment should be set before calling <code>Initialize()</code>
        /// </summary>
        /// <param name="Environment">
        /// Environment to set. Must not be null. PRODUCTION by default.
        /// </param>
        public static VesselEnvironment Environment { get; set; }

        /// <summary>
        /// Initialize Open Vessel SDK
        /// </summary>
        /// <param name="userId">
        /// In-app user ID
        /// </param>
        public static void Initialize(string userId)
        {
            AppConnectManagerCallbacks.Instance.ForwardOnStateUpdatedEvent("{\"status\": \"Disconnected\"}");
            SdkCallbacks.Instance.ForwardOnSdkInitializedEvent("");
        }
    }
}