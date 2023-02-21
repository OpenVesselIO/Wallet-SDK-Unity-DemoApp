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
        private static extern void _OVShowEarnings(string userId, string promoType);

        public void ShowEarnings(string userId)
        {
            ShowEarnings(userId, EarningsPromoType.Static);
        }

        public void ShowEarnings(string userId, EarningsPromoType promoType)
        {
            _OVShowEarnings(userId, promoType.ToString().ToUpperInvariant());
        }

    }
#endif
}