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

    public class EarningsManagerBase
    {

        [Serializable]
        protected class PresentationSettingsJson
        {

            [SerializeField]
            string userId;

            [SerializeField]
            string promoType;

            [SerializeField]
            string triggerName;

            internal PresentationSettingsJson(EarningsPresentationSettings settings)
            {
                userId = settings.UserId;
                promoType = settings.PromoType.ToString().ToUpperInvariant();
                triggerName = settings.TriggerName;
            }

        }

        [Serializable]
        protected class LoginJson
        {

            [SerializeField] string phoneNumber;
            [SerializeField] string code;
            [SerializeField] Int64 codeCreatedAt;
            [SerializeField] string userId;

            public LoginJson(string phoneNumber, string code, Int64 codeCreatedAt, string userId)
            {
                this.phoneNumber = phoneNumber;
                this.code = code;
                this.codeCreatedAt = codeCreatedAt;
                this.userId = userId;
            }

        }

        [Serializable]
        protected class VerificationJson
        {

            [SerializeField] string email;
            [SerializeField] string code;
            [SerializeField] Int64 codeCreatedAt;

            public VerificationJson(string email, string code, Int64 codeCreatedAt)
            {
                this.email = email;
                this.code = code;
                this.codeCreatedAt = codeCreatedAt;
            }

        }

    }

}