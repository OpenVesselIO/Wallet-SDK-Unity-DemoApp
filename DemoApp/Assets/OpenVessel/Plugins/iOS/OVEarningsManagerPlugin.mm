// Copyright © 2021 OpenVessel. All rights reserved.

#import <OpenVessel/OpenVessel.h>

#import "OVSdkPluginUtils.h"

extern "C" {

    static const char * const kCallbacksObjectName = "OVEarningsManagerCallbacks";

    static void earnings_send_message(const char *method, NSDictionary *json)
    {
        ov_unity_send_message(kCallbacksObjectName, method, json);
    }

    static void earnings_send_auth_error(NSString *msg)
    {
        UnitySendMessage(kCallbacksObjectName, "ForwardOnAuthFailure", msg.UTF8String);
    }

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

    void _OVTrackImpression(const char * triggerName)
    {
        [OVLSdk.sharedInstance.earningsManager trackImpressionWithTriggerName:NSSTRING(triggerName)];
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

    void _OVGenerateAuthCodeForPhoneNumber(const char * cPhoneNumber)
    {
        NSString *phoneNumber = NSSTRING(cPhoneNumber);

        [OVLSdk.sharedInstance.earningsManager generateAuthCodeWithPhoneNumber:phoneNumber resultHandler:
         ^(OVLEarningsAuthCodeMetadata * _Nullable authCodeMetadata, NSError * _Nullable error) {
            if (authCodeMetadata) {
                earnings_send_message("ForwardOnAuthCodeMetadataEvent", @{
                    @"phoneNumber": phoneNumber,
                    @"createdAt": @(authCodeMetadata.createdAt),
                    @"expiresAt": @(authCodeMetadata.expiresAt),
                    @"ttl": @(authCodeMetadata.ttl),
                });
            } else {
                earnings_send_auth_error(error.localizedDescription);
            }
        }];
    }

    void _OVLoginByPhoneAuthCode(const char * json)
    {
        NSData *jsonData = [NSSTRING(json) dataUsingEncoding:NSUTF8StringEncoding];

        NSError *error;
        NSDictionary *dict = [NSJSONSerialization JSONObjectWithData: jsonData
                                                             options: kNilOptions
                                                               error: &error];

        if (dict == nil) {
            return;
        }

        [OVLSdk.sharedInstance.earningsManager
         loginWithPhoneNumber:dict[@"phoneNumber"]
         code:dict[@"code"]
         codeCreatedAt:[dict[@"codeCreatedAt"] longLongValue]
         userId:dict[@"userId"]
         completionHandler:^(NSError * _Nullable error) {
            if (error) {
                earnings_send_auth_error(error.localizedDescription);
            }
        }];
    }
    
}
