using OVSdk.Utils;

namespace OVSdk
{
    public class SdkUnityEditor : SdkBase
    {
        /// <summary>
        /// App connect manager enabling connecting this app to the central OpenWallet Vessel.
        /// </summary>
        /// <returns>Connection manager</returns>
        public static AppConnectManagerUnityEditor AppConnectManager => new AppConnectManagerUnityEditor();

        static SdkUnityEditor()
        {
            InitCallbacks();
        }

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


        /// <summary>
        /// Show wallet user wallet activity.
        /// <b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <b>Please note</b>: use {@link AppConnectManager} to connect wallet to this app
        ///
        /// </summary>
        public static void LoadWalletView()
        {
            Logger.UserDebug("Showing User View");
        }
    }
}