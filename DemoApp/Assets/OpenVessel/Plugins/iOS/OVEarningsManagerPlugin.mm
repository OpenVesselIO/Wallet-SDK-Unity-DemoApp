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

    void _OVShowEarnings(const char * settingsJson)
    {
        NSData *settingsJsonData = [NSSTRING(settingsJson) dataUsingEncoding:NSUTF8StringEncoding];

        NSError *error;
        NSDictionary *settingsDict = [NSJSONSerialization JSONObjectWithData: settingsJsonData
                                                                     options: kNilOptions
                                                                       error: &error];

        if (settingsDict == nil) {
            return;
        }

        NSString *promoTypeString = settingsDict[@"promoType"];

        OVLEarningsPromoType promoType;

        if ([@"STATIC" isEqualToString:promoTypeString]) {
            promoType = OVLEarningsPromoTypeStatic;
        }
        else if ([@"VIDEO" isEqualToString:promoTypeString]) {
            promoType = OVLEarningsPromoTypeVideo;
        }
        else {
            return;
        }

        OVLEarningsPresentationSettings *settings;
        settings = [OVLEarningsPresentationSettings settingsWithUserId:settingsDict[@"userId"]];
        settings.promoType = promoType;
        settings.triggerName = settingsDict[@"triggerName"];

        [OVLSdk.sharedInstance.earningsManager presentEarningsFromViewController: UNITY_VIEW_CONTROLLER
                                                                    withSettings: settings
                                                                        animated: YES];
    }
    
}
