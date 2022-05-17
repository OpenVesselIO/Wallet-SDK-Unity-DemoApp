using System;
using UnityEngine;

namespace OVSdk
{
#if UNITY_ANDROID
    public class AppConnectManagerAndroid : AppConnectManagerBase
    {
        private static readonly AndroidJavaClass OvSdkPluginClass =
            new AndroidJavaClass("io.openvessel.sdk.unity.AppConnectManagerPlugin");


        /// <summary>
        /// Connect wallet to the current application. This function will display custom UI that renders connect progress.
        /// Use <c>OnAppConnected</c> and <c>OnAppConnectFailed</c> to listen for connection status.
        /// </summary>
        public void LoadConnectWalletView(string userId)
        {
            OvSdkPluginClass.CallStatic("loadConnectWalletView", userId);
        }

        /// <summary>
        /// Connect wallet to the current application _without_ displaying UI.
        /// Use <c>OnAppConnected</c> and <c>OnAppConnectFailed</c> to listen for connection status. Use
        /// <c>cancelConnect()</c> to cancel the operation. 
        /// </summary>
        public void ConnectWallet(string userId)
        {
            OvSdkPluginClass.CallStatic("connectWallet", userId);
        }

        /// <summary>
        /// Cancel current connect wallet operation. Once connect is canceled  <c>OnAppConnectFailed</c> will be
        /// invoked with 'AppConnectFailure.ErrorCanceled'.  
        /// </summary>
        public void CancelConnect()
        {
            OvSdkPluginClass.CallStatic("cancelConnect");
        }

        /// <summary>
        /// Disconnect wallet from the current session  
        /// </summary>
        public void DisconnectCurrentSession()
        {
            OvSdkPluginClass.CallStatic("disconnectCurrentSession");
        }

        /// <summary>
        /// Disconnect wallet from all of the sessions  
        /// </summary>
        public void DisconnectAllSessions()
        {
            OvSdkPluginClass.CallStatic("disconnectAllSessions");
        }

        /// <summary>
        /// Handle a deeplink that returns into the app connect flow
        /// Returns <c>true</c> if OpenVessel connect was able to recognize and handle the link
        /// </summary>
        public bool HandleDeeplink(String deeplink)
        {
            return OvSdkPluginClass.CallStatic<bool>("handleDeeplink", deeplink);
        }
    }
#endif
}