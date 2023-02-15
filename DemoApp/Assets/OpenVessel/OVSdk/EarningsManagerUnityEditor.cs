using System;
using System.Collections;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class EarningsManagerUnityEditor : EarningsManagerBase
    {

        public void TrackRevenuedAd(AdType adType)
        {
        }

        public void ShowEarnings(string userId)
        {
            Logger.UserDebug("Showing in-app page for earnings of '" + userId + "'...");
        }

    }
}