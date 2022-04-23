using UnityEngine;
using System.Runtime.InteropServices;

namespace OVSdk
{
#if UNITY_IOS
    public class WalletPresenteriOs : WalletPresenterBase
    {
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
        private static extern void _OVWalletPresenterOpenWalletApplication();

        /// <summary>
        /// Open a wallet application
        /// </summary>
        public void OpenWalletApplication()
        {
            _OVWalletPresenterOpenWalletApplication();
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
    }
#endif
}