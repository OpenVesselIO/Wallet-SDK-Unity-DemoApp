using System;
using System.Collections;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class AppConnectManagerUnityEditor : AppConnectManagerBase
    {
        /// <summary>
        /// Connect wallet to the current application _without_ displaying UI.
        /// Use <c>OnAppConnected</c> and <c>OnAppConnectFailed</c> to listen for connection status. Use
        /// <c>cancelConnect()</c> to cancel the operation. 
        /// </summary>
        public void ConnectWallet(string userId)
        {
            ExecuteWithDelay(1f,
                () =>
                {
                    AppConnectManagerCallbacks.Instance.ForwardOnStateUpdatedEvent(
                        "{\"status\": \"Connected\", \"walletAddress\": \"0x000TEST00WALLET000\"}");
                });
        }

        /// <summary>
        /// Connect wallet to the current application. This function will display custom UI that renders connect progress.
        /// Use <c>OnAppConnected</c> and <c>OnAppConnectFailed</c> to listen for connection status.
        /// </summary>
        public void LoadConnectWalletView(string userId)
        {
            ExecuteWithDelay(3f,
                () =>
                {
                    AppConnectManagerCallbacks.Instance.ForwardOnStateUpdatedEvent(
                        "{\"status\": \"Connected\", \"walletAddress\": \"0x000TEST00WALLET0000\"}");
                });
        }

        /// <summary>
        /// Cancel current connect wallet operation. Once connect is canceled  <c>OnAppConnectFailed</c> will be
        /// invoked with 'AppConnectFailure.ErrorCanceled'.  
        /// </summary>
        public void CancelConnect()
        {
            AppConnectManagerCallbacks.Instance.ForwardOnStateUpdatedEvent("{\"status\": \"ErrorCanceled\"}");
        }

        /// <summary>
        /// Disconnect wallet from the current session  
        /// </summary>
        public void DisconnectCurrentSession()
        {
            Logger.UserDebug("Wallet disconnected");

            AppConnectManagerCallbacks.Instance.ForwardOnStateUpdatedEvent("{\"status\": \"Disconnected\"}");
        }

        /// <summary>
        /// Disconnect wallet from all of the sessions  
        /// </summary>
        public void DisconnectAllSessions()
        {
            Logger.UserDebug("Wallet disconnected from all sessions");

            AppConnectManagerCallbacks.Instance.ForwardOnStateUpdatedEvent("{\"status\": \"Disconnected\"}");
        }


        private static void ExecuteWithDelay(float seconds, Action action)
        {
            AppConnectManagerCallbacks.Instance.StartCoroutine(ExecuteAction(seconds, action));
        }

        private static IEnumerator ExecuteAction(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);

            action();
        }
    }
}