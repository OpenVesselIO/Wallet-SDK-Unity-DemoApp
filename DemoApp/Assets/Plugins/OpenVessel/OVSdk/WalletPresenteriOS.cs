using UnityEngine;
using System.Runtime.InteropServices;

namespace OVSdk
{
#if UNITY_IOS
    public class WalletPresenteriOs : WalletPresenterBase
    {
        [DllImport("__Internal")]
        private static extern void _OVWalletPresenterShowToken(string fqtn);

        /// <summary>
        /// Show a token page inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowToken(string fqtn)
        {
            _OVWalletPresenterShowToken(fqtn);
        }

        [DllImport("__Internal")]
        private static extern void _OVWalletPresenterShowCollection(string fqcn);

        /// <summary>
        /// Show a collection page inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowCollection(string fqcn)
        {
            _OVWalletPresenterShowCollection(fqcn);
        }

        [DllImport("__Internal")]
        private static extern void _OVWalletPresenterShowGame(string fqgn);

        /// <summary>
        /// Show a game page inside of the current application.
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowGame(string fqgn)
        {
            _OVWalletPresenterShowGame(fqgn);
        }

        [DllImport("__Internal")]
        private static extern void _OVWalletPresenterShowWallet();

        /// <summary>
        /// Show wallet user wallet view inside of the current application
        /// 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void ShowWallet()
        {
            _OVWalletPresenterShowWallet();
        }

        [DllImport("__Internal")]
        private static extern void _OVWalletPresenterOpenTokenInWalletApplication(string fqtn);

        /// <summary>
        /// Open a wallet application and navigate to the page of a given token
        /// </summary>
        public void OpenTokenInWalletApplication(string fqtn)
        {
            _OVWalletPresenterOpenTokenInWalletApplication(fqtn);
        }

        [DllImport("__Internal")]
        private static extern void _OVWalletPresenterOpenCollectionInWalletApplication(string fqtn);

        /// <summary>
        /// Open a wallet application and navigate to the page of a given collection
        /// </summary>
        public void OpenCollectionInWalletApplication(string fqcn)
        {
            _OVWalletPresenterOpenCollectionInWalletApplication(fqcn);
        }

        [DllImport("__Internal")]
        private static extern void _OVWalletPresenterOpenGameInWalletApplication(string fqgn);

        /// <summary>
        /// Open a wallet application and navigate to the page of a given token
        /// </summary>
        public void OpenGameInWalletApplication(string fqgn)
        {
            _OVWalletPresenterOpenGameInWalletApplication(fqgn);
        }

        [DllImport("__Internal")]
        private static extern void _OVWalletPresenterOpenWalletApplication();

        /// <summary>
        /// Open a wallet application
        /// </summary>
        public void OpenWalletApplication()
        {
            _OVWalletPresenterOpenWalletApplication();
        }

        [DllImport("__Internal")]
        private static extern void _OVWalletVerifyWalletAddressInWalletApplication(string walletAddress);

        /// <summary>
        /// Open wallet application to check if the user is logged in with the same address as provided.
        ///
        /// <p><b>Please note:</b> if verification fails current wallet will be disconnected</p> 
        /// <p><b>Please note</b>: wallet activity will display only if user has connected their wallet to the app.
        /// <p><b>Please note</b>: use <c>AppConnectManager</c> to connect wallet to this app
        /// </summary>
        public void VerifyWalletAddressInWalletApplication(string walletAddress)
        {
            _OVWalletVerifyWalletAddressInWalletApplication(walletAddress);
        }
    }
#endif
}