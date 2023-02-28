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

        public void TrackRevenuedAd(AdType adType)
        {
            throw new NotImplementedException();
        }

        public void ShowEarnings(string userId)
        {
            throw new NotImplementedException();
        }

        public void ShowEarnings(EarningsPresentationSettings settings)
        {
            throw new NotImplementedException();
        }

    }
#endif
}