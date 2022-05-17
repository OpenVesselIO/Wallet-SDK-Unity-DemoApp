using System.Runtime.InteropServices;

namespace OVSdk
{
#if UNITY_IOS
    public class AppConnectManageriOs : AppConnectManagerBase
    {
        [DllImport("__Internal")]
        private static extern void _OVLoadConnectWalletView(string userId);

        /// <summary>
        /// Connect wallet to the current application. This function will display custom UI that renders connect progress.
        /// Use <c>OnAppConnected</c> and <c>OnAppConnectFailed</c> to listen for connection status.
        /// </summary>
        /// <param name="userId">
        /// In-app user ID
        /// </param>
        public void LoadConnectWalletView(string userId)
        {
            _OVLoadConnectWalletView(userId);
        }

        [DllImport("__Internal")]
        private static extern void _OVConnectWallet(string userId);

        /// <summary>
        /// Connect wallet to the current application _without_ displaying UI.();
        /// Use <c>OnAppConnected</c> and <c>OnAppConnectFailed</c> to listen for connection status. Use
        /// <c>cancelConnect()</c> to cancel the operation. 
        /// </summary>
        /// <param name="userId">
        /// In-app user ID
        /// </param>
        public void ConnectWallet(string userId)
        {
            _OVConnectWallet(userId);
        }


        [DllImport("__Internal")]
        private static extern void _OVCancelConnect();

        /// <summary>
        /// Cancel current connect wallet operation. Once connect is canceled  <c>OnAppConnectFailed</c> will be
        /// invoked with 'AppConnectFailure.ErrorCanceled'.  
        /// </summary>
        public void CancelConnect()
        {
            _OVCancelConnect();
        }

        [DllImport("__Internal")]
        private static extern void _OVDisconnectCurrentSession();

        /// <summary>
        /// Disconnect wallet from the current session  
        /// </summary>
        public void DisconnectCurrentSession()
        {
            _OVDisconnectCurrentSession();
        }

        [DllImport("__Internal")]
        private static extern void _OVDisconnectAllSessions();

        /// <summary>
        /// Disconnect wallet from all of the sessions  
        /// </summary>
        public void DisconnectAllSessions()
        {
            _OVDisconnectAllSessions();
        }


        [DllImport("__Internal")]
        private static extern bool _OVHandleConnectDeeplink(string deeplink);

        /// <summary>
        /// Handle a deeplink that returns into the app connect flow
        /// Returns <c>true</c> if OpenVessel connect was able to recognize and handle the link
        /// </summary>
        public bool HandleDeeplink(string deeplink)
        {
            return _OVHandleConnectDeeplink(deeplink);
        }
    }
#endif
}