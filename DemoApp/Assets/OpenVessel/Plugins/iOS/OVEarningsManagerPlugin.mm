// Copyright © 2021 OpenVessel. All rights reserved.

#import <OpenVessel/OpenVessel.h>

#import "OVSdkPluginUtils.h"

extern "C" {

    void _OVTrackRevenuedAd(const char * adTypeCString)
    {
        OVLAdType adType;

        if (strcmp("APPOPEN", adTypeCString) == 0) {
            adType = OVLAdTypeAppOpen;
        }
        else if (strcmp("BANNER", adTypeCString) == 0) {
            adType = OVLAdTypeBanner;
        }
        else if (strcmp("INTERSTITIAL", adTypeCString) == 0) {
            adType = OVLAdTypeInterstitial;
        }
        else if (strcmp("MREC", adTypeCString) == 0) {
            adType = OVLAdTypeMREC;
        }
        else if (strcmp("REWARDED", adTypeCString) == 0) {
            adType = OVLAdTypeRewarded;
        }
        else {
            return;
        }

        [OVLSdk.sharedInstance.earningsManager trackRevenuedAd:adType];
    }

    void _OVShowEarnings(const char * userId, const char * promoTypeCString)
    {
        OVLEarningsPromoType promoType;

        if (strcmp("STATIC", promoTypeCString)) {
            promoType = OVLEarningsPromoTypeStatic;
        }
        else if (strcmp("VIDEO", promoTypeCString)) {
            promoType = OVLEarningsPromoTypeVideo;
        }
        else {
            return;
        }

        OVLEarningsPresentationSettings *settings;
        settings = [OVLEarningsPresentationSettings settingsWithUserId:NSSTRING(userId)];
        settings.promoType = promoType;

        [OVLSdk.sharedInstance.earningsManager presentEarningsFromViewController: UNITY_VIEW_CONTROLLER
                                                                    withSettings: settings
                                                                        animated: YES];
    }
    
}