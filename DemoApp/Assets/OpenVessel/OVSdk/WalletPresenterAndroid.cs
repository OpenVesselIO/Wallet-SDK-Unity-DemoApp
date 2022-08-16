using UnityEngine;

namespace OVSdk
{
#if UNITY_ANDROID
    public class WalletPresenterAndroid : WalletPresenterBase
    {
        private static readonly AndroidJavaClass OvPluginClass =
            new AndroidJavaClass("io.openvessel.sdk.unity.WalletPresenterPlugin");

        /// <summary>
        /// Show a token page inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowToken(string fqtn)
        {
            OvPluginClass.CallStatic("showToken", fqtn);
        }
        
        
        /// <summary>
        /// Show a collection page inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowCollection(string fqcn)
        {
            OvPluginClass.CallStatic("showCollection", fqcn);
        }
        
        
        /// <summary>
        /// Show a gamepage inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowGame(string fqgn)
        {
            OvPluginClass.CallStatic("showGame", fqgn);
        }
        
        /// <summary>
        /// Show wallet user wallet view inside of the current application
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowWallet()
        {
            OvPluginClass.CallStatic("showWallet");
        }

        /// <summary>
        /// Open a wallet application and navigate to the page of a given token
        /// </summary>
        public void OpenTokenInWalletApplication(string fqtn)
        {
            OvPluginClass.CallStatic("openTokenInWalletApplication", fqtn);
        }
        
        /// <summary>
        /// Open a wallet application and navigate to the page of a given collection
        /// </summary>
        public void OpenCollectionInWalletApplication(string fqcn)
        {
            OvPluginClass.CallStatic("openCollectionInWalletApplication", fqcn);
        }
        
        /// <summary>
        /// Open a wallet application and navigate to the page of a given game
        /// </summary>
        public void OpenGameInWalletApplication(string fqgn)
        {
            OvPluginClass.CallStatic("openGameInWalletApplication", fqgn);
        }
        
        /// <summary>
        /// Open a wallet application
        /// </summary>
        public void OpenWalletApplication()
        {
            OvPluginClass.CallStatic("openWalletApplication");
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
            OvPluginClass.CallStatic("verifyWalletAddressInWalletApplication", walletAddress);
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
            OvPluginClass.CallStatic("loadBalanceInWalletApplication", walletAddress);
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
            OvPluginClass.CallStatic("loadBalanceByAmountInWalletApplication", walletAddress, amount);
        }
    }
#endif
}