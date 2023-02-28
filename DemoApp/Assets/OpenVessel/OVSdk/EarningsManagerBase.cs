using System;
using OVSdk.Utils;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{

    public enum AdType
    {

        AppOpen,

        Banner,

        Interstitial,

        MREC,

        Rewarded,

    }

    public enum EarningsPromoType
    {

        Static,

        Video,

    }

    public class EarningsPresentationSettings
    {

        public string UserId { get; }
        public EarningsPromoType PromoType = EarningsPromoType.Static;
        public string TriggerName;

        public EarningsPresentationSettings(string userId)
        {
            this.UserId = userId;
        }

    }

    [Serializable]
    internal class EarningsPresentationSettingsJson
    {

        [SerializeField]
        string userId;

        [SerializeField]
        string promoType;

        [SerializeField]
        string triggerName;

        internal EarningsPresentationSettingsJson(EarningsPresentationSettings settings)
        {
            userId = settings.UserId;
            promoType = settings.PromoType.ToString().ToUpperInvariant();
            triggerName = settings.TriggerName;
        }

    }

    public class EarningsManagerBase
    {
    }

}