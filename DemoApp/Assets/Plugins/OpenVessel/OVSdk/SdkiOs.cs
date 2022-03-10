using System.Runtime.InteropServices;

namespace OVSdk
{
#if UNITY_IOS
    public class SdkiOs : SdkBase
    {
        /// <summary>
        /// App connect manager enabling connecting this app to the central OpenWallet Vessel.
        /// </summary>
        /// <returns>Connection manager</returns>
        public static AppConnectManageriOs AppConnectManager => new AppConnectManageriOs();

        
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
        }


        [DllImport("__Internal")]
        private static extern void _OVLoadWalletView();


        /// <summary>
        /// Show wallet user wallet activity.
        /// <b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <b>Please note</b>: use {@link AppConnectManager} to connect wallet to this app
        ///
        /// </summary>
        public static void LoadWalletView()
        {
            _OVLoadWalletView();
        }
    }
#endif    
}