//
//  OVIapManagerDelegateForwarder.m
//  Unity-iPhone
//

#import "OVIapManagerDelegateForwarder.h"


#import "OVSdkPluginUtils.h"
#import <OpenVessel/OpenVessel.h>

#ifdef __cplusplus
extern "C" {
#endif
    // UnityAppController.mm
    UIViewController* UnityGetGLViewController(void);
    UIWindow* UnityGetMainWindow(void);
    
    // life cycle management
    void UnityPause(int pause);
    void UnitySendMessage(const char* obj, const char* method, const char* msg);
#ifdef __cplusplus
}
#endif

@interface OVIapManagerDelegateForwarder() <OVLIapManagerDelegate>

@end

@implementation OVIapManagerDelegateForwarder

- (instancetype)init
{
    if ( self = [super init] )
    {
        // do nothing
    }
    
    return self;
}

- (void)attachDelegate
{
    [OVLSdk.sharedInstance.iapManager setDelegate: self
                                    delegateQueue: dispatch_get_main_queue()];
}

- (void)iapManager:(OVLIapManager *)iapManager didCompletePurchaseWithProductIdentifier:(NSString *)productId
           receipt:(NSData *)receipt
{
    NSString *json = [OVIapManagerDelegateForwarder jsonStringFromDictionary:@{
        @"productId": productId,
        @"receipt": [receipt base64EncodedStringWithOptions:kNilOptions],
        @"receiptSignature": @""
    }];

    if (json != nil) {
        UnitySendMessage("OVIapManagerCallbacks", "ForwardOnPurchaseSuccessEvent", json.UTF8String);
    }
}

- (void)iapManager:(OVLIapManager *)iapManager didFailPurchaseWithProductIdentifier:(NSString *)productId
             error:(nullable NSError *)error
{
    NSString *json = [OVIapManagerDelegateForwarder jsonStringFromDictionary:@{
        @"productId": productId,
        @"error": @{
            @"message": error.localizedDescription ?: error.description ?: NSNull.null,
            @"detailedMessage": error.debugDescription ?: NSNull.null
        }
    }];

    if (json != nil) {
        UnitySendMessage("OVIapManagerCallbacks", "ForwardOnPurchaseFailureEvent", json.UTF8String);
    }
}

- (void)iapManager:(OVLIapManager *)iapManager didCancelPurchaseWithProductIdentifier:(NSString *)productId {
    NSString *json = [OVIapManagerDelegateForwarder jsonStringFromDictionary:@{
        @"productId": productId,
    }];

    if (json != nil) {
        UnitySendMessage("OVIapManagerCallbacks", "ForwardOnPurchaseCancelEvent", json.UTF8String);
    }
}

+ (NSString *)jsonStringFromDictionary:(NSDictionary *)dictionary
{
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject: dictionary
                                                       options: kNilOptions
                                                         error: &error];

    if (jsonData == nil) {
        return nil;
    }

    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

+ (OVIapManagerDelegateForwarder *)sharedInstance
{
    static dispatch_once_t token;
    static OVIapManagerDelegateForwarder *shared;
    dispatch_once(&token, ^{
        shared = [OVIapManagerDelegateForwarder new];
    });
    return shared;
}


@end
