using System;
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
        private static extern void _OVSetEnvironment(string environment);

        [DllImport("__Internal")]
        private static extern string _OVGetEnvironment();

        /// <summary>
        /// Assign the environment that should be used for this SDK.
        /// <b>Please note</b>: the environment should be set before calling <code>Initialize()</code>
        /// </summary>
        /// <param name="Environment">
        /// Environment to set. Must not be null. PRODUCTION by default.
        /// </param>
        public static VesselEnvironment Environment
        {
            get
            {
                var envString = _OVGetEnvironment();
                var envParsed = Enum.TryParse(envString, true, out VesselEnvironment parsedEnv);
                if (envParsed)
                {
                    return parsedEnv;
                }
                else
                {
                    return VesselEnvironment.Production;
                }
            }

            set { _OVSetEnvironment(value.ToString().ToUpper()); }
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