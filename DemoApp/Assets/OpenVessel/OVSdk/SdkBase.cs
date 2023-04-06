using System;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class SdkBase
    {
        protected static void InitSingletons()
        {
            var sdkCallbackObject =
                new GameObject("OVSdkCallbacks", typeof(SdkCallbacks))
                    .GetComponent<SdkCallbacks>(); // Its Awake() method sets Instance.

            if (SdkCallbacks.Instance != sdkCallbackObject)
            {
                Logger.UserWarning("It looks like you have the " + sdkCallbackObject.name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
            }

            var appManagerCallbackObject =
                new GameObject("OVAppConnectManagerCallbacks", typeof(AppConnectManagerCallbacks))
                    .GetComponent<AppConnectManagerCallbacks>(); // Its Awake() method sets Instance.

            if (AppConnectManagerCallbacks.Instance != appManagerCallbackObject)
            {
                Logger.UserWarning("It looks like you have the " + appManagerCallbackObject.name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
            }

            var walletPresenterCallbackObject =
                new GameObject("OVWalletPresenterCallbacks", typeof(WalletPresenterCallbacks))
                    .GetComponent<WalletPresenterCallbacks>(); // Its Awake() method sets Instance.

            if (WalletPresenterCallbacks.Instance != walletPresenterCallbackObject)
            {
                Logger.UserWarning("It looks like you have the " + walletPresenterCallbackObject.name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
            }

            var iapManagerCallbackObject =
                new GameObject("OVIapManagerCallbacks", typeof(IapManagerCallbacks))
                    .GetComponent<IapManagerCallbacks>(); // Its Awake() method sets Instance.

            if (IapManagerCallbacks.Instance != iapManagerCallbackObject)
            {
                Logger.UserWarning("It looks like you have the " + iapManagerCallbackObject.name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
            }

            var customPresenterObject =
                new GameObject("OVCustomPresenter", typeof(CustomPresenter))
                    .GetComponent<CustomPresenter>(); // Its Awake() method sets Instance.

            if (CustomPresenter.Instance != customPresenterObject)
            {
                Logger.UserWarning("It looks like you have the " + customPresenterObject.name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
            }

            var earningsManagerCallbackObject =
                new GameObject("OVEarningsManagerCallbacks", typeof(EarningsManagerCallbacks))
                    .GetComponent<EarningsManagerCallbacks>(); // Its Awake() method sets Instance.

            if (EarningsManagerCallbacks.Instance != earningsManagerCallbackObject)
            {
                Logger.UserWarning("It looks like you have the " + earningsManagerCallbackObject.name +
                                   " on a GameObject in your scene. Please remove the script from your scene.");
            }
        }
    }
}