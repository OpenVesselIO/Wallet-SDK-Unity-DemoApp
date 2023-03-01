using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
#if UNITY_ANDROID
    public class EarningsManagerAndroid: EarningsManagerBase
    {

        private static readonly AndroidJavaClass PluginClass =
            new AndroidJavaClass("io.openvessel.sdk.unity.EarningsManagerPlugin");

        public void TrackRevenuedAd(AdType adType)
        {
            PluginClass.CallStatic("trackRevenuedAd", adType.ToString().ToUpperInvariant());
        }

        public void ShowEarnings(string userId)
        {
            ShowEarnings(new EarningsPresentationSettings(userId));
        }

        public void ShowEarnings(EarningsPresentationSettings settings)
        {
            PluginClass.CallStatic("showEarnings", JsonUtility.ToJson(new EarningsPresentationSettingsJson(settings)));
        }

    }
#endif
}