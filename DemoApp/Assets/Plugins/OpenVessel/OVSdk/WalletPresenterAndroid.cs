using UnityEngine;

namespace OVSdk
{
#if UNITY_ANDROID
    public class WalletPresenterAndroid : WalletPresenterBase
    {
        private static readonly AndroidJavaClass OvPluginClass =
            new AndroidJavaClass("io.openvessel.sdk.unity.WalletPresenterPlugin");


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
        /// Open a wallet application
        /// </summary>
        public void OpenWalletApplication()
        {
            OvPluginClass.CallStatic("openWalletApplication");
        }

        /// <summary>
        /// Open a wallet application and navigate to the page of a given token
        /// </summary>
        public void OpenTokenInWalletApplication(string fqtn)
        {
            OvPluginClass.CallStatic("openTokenInWalletApplication", fqtn);
        }
    }
#endif
}