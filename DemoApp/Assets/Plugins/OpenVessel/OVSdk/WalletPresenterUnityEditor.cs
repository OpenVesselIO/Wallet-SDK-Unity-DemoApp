using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class WalletPresenterUnityEditor : WalletPresenterBase
    {
        /// <summary>
        /// Show wallet user wallet view inside of the current application
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowWallet()
        {
            Logger.UserDebug("Showing in-app wallet...");
        }

        /// <summary>
        /// Show a token page inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowToken(string fqtn)
        {
            Logger.UserDebug("Showing in-app page for token '" + fqtn + "'");
        }

        /// <summary>
        /// Open a wallet application
        /// </summary>
        public void OpenWalletApplication()
        {
            Logger.UserDebug("Opening wallet application...");
        }

        /// <summary>
        /// Open a wallet application and navigate to the page of a given token
        /// </summary>
        public void OpenTokenInWalletApplication(string fqtn)
        {
            Logger.UserDebug("Opening wallet application with '" + fqtn + "'");
        }
    }
}