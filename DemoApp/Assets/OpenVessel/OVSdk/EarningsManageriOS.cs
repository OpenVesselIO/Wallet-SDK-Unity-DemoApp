using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
#if UNITY_IOS
    public class EarningsManageriOs: EarningsManagerBase
    {

        [DllImport("__Internal")]
        private static extern void _OVTrackRevenuedAd(string adType);

        public void TrackRevenuedAd(AdType adType)
        {
            _OVTrackRevenuedAd(adType.ToString().ToUpperInvariant());
        }

        [DllImport("__Internal")]
        private static extern void _OVTrackImpression(string triggerName);

        public void TrackImpression(string triggerName)
        {
            _OVTrackImpression(triggerName);
        }

        [DllImport("__Internal")]
        private static extern void _OVShowEarnings(string settingsJson);

        public void ShowEarnings(string userId)
        {
            ShowEarnings(new EarningsPresentationSettings(userId));
        }

        public void ShowEarnings(EarningsPresentationSettings settings)
        {
            _OVShowEarnings(JsonUtility.ToJson(new EarningsPresentationSettingsJson(settings)));
        }

    }
#endif
}