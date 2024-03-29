using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class WalletPresenterUnityEditor : WalletPresenterBase
    {

        /// <summary>
        /// Check if the wallet application is installed.
        /// </summary>
        public bool IsWalletApplicationInstalled()
        {
            Logger.UserDebug("Checking if the wallet app is installed");
            return false;
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
        /// Show a collection page inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowCollection(string fqcn)
        {
            Logger.UserDebug("Showing in-app page for collection '" + fqcn + "'");
        }

        /// <summary>
        /// Show a gamepage inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowGame(string fqgn)
        {
            Logger.UserDebug("Showing in-app page for game '" + fqgn + "'");
        }

        /// <summary>
        /// Show wallet user wallet view inside of the current application
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowWallet()
        {
            Logger.UserDebug("Showing in-app wallet page...");
        }

        /// <summary>
        /// Show a wallet user's profile activity inside of the current application
        ///
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowProfile()
        {
            Logger.UserDebug("Showing in-app profile page...");
        }

        /// <summary>
        /// Open a wallet application and navigate to the page of a given token
        /// </summary>
        public void OpenTokenInWalletApplication(string fqtn)
        {
            Logger.UserDebug("Opening wallet application with token '" + fqtn + "'");
        }

        /// <summary>
        /// Open a wallet application and navigate to the page of a given collection
        /// </summary>
        public void OpenCollectionInWalletApplication(string fqcn)
        {
            Logger.UserDebug("Opening wallet application with collection '" + fqcn + "'");
        }

        /// <summary>
        /// Open a wallet application and navigate to the page of a given game
        /// </summary>
        public void OpenGameInWalletApplication(string fqgn)
        {
            Logger.UserDebug("Opening wallet application with game '" + fqgn + "'");
        }


        /// <summary>
        /// Open a wallet application
        /// </summary>
        public void OpenWalletApplication()
        {
            Logger.UserDebug("Opening wallet application...");
        }

        /// <summary>
        /// Open wallet application to check if the user is logged in with the same address as provided.
        ///
        /// <p><b>Please note:</b> if verification fails current wallet will be disconnected</p> 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void VerifyWalletAddressInWalletApplication(string walletAddress)
        {
            Logger.UserDebug("Validating address '" + walletAddress + "'");
        }

        /// <summary>
        /// Open a wallet application on the load balance page.
        ///
        /// <p><b>Please note:</b> if verification fails balance will not be loaded</p>
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void LoadBalanceInWalletApplication(string walletAddress)
        {
            Logger.UserDebug("Loading balance for '" + walletAddress + "'");
        }

        /// <summary>
        /// Open a wallet application on the load balance by specified amount page.
        ///
        /// <p><b>Please note:</b> if verification fails balance will not be loaded</p>
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void LoadBalanceInWalletApplication(string walletAddress, int amount)
        {
            Logger.UserDebug("Loading balance for '" + walletAddress + "' by " + amount);
        }

        /// <summary>
        /// Open a wallet application and present a transaction to confirm.
        /// </summary>
        public void ConfirmTransactionInWalletApplication(string transactionId)
        {
            Logger.UserDebug("Opening wallet application with transaction id '" + transactionId + "'");
        }

        /// <summary>
        /// Present a KYC page inside of the current application.
        /// </summary>
        public void ShowKyc()
        {
            Logger.UserDebug("Showing in-app KYC page...");
        }
    }
}