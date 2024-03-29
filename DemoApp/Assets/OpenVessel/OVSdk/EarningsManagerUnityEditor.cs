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

        public void TrackImpression(string triggerName)
        {
        }

        public void ShowEarnings(string userId)
        {
            ShowEarnings(new EarningsPresentationSettings(userId));
        }

        public void ShowEarnings(EarningsPresentationSettings settings)
        {
            Logger.UserDebug("Showing in-app page for earnings of '" + settings.UserId + "' with promo type '" + settings.PromoType.ToString() + "'...");
        }

        public void GenerateAuthCodeForPhoneNumber(string phoneNumber)
        {
        }

        public void LoginByPhoneAuthCode(string phoneNumber, string authCode, Int64 codeCreatedAt, string userId)
        {
        }

        public void GenerateVerificationCodeForEmail(string email)
        {
        }

        public void VerifyEmail(string email, string verificationCode, Int64 codeCreatedAt)
        {
        }

    }
}