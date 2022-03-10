//
//  OVAppConnectManagerDelegateForwarder.m
//  Unity-iPhone
//
//  Created by Basil Shikin on 11/10/21.
//

#import "OVAppConnectManagerDelegateForwarder.h"


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

@interface OVAppConnectManagerDelegateForwarder()<OVLAppConnectManagerDelegate>

+(NSString *)jsonStringFromState:(OVLAppConnectState *)state;
+(NSString *)unityStringFromStatus:(OVLAppConnectStatus)status;

@end

@implementation OVAppConnectManagerDelegateForwarder

- (instancetype)init
{
    self = [super init];
    if ( self )
    {
    }
    
    return self;
}

-(void)attachDelegate
{
    [[OVLSdk sharedInstance].appConnectManager setDelegate: self
                           delegateQueue: dispatch_get_main_queue()];
}

- (void)appConnectManagerDidUpdateState:(OVLAppConnectManager *)appConnectManager
{
    NSString * stateJson = [OVAppConnectManagerDelegateForwarder jsonStringFromState:appConnectManager.state];
    UnitySendMessage("OVAppConnectManagerCallbacks", "ForwardOnStateUpdatedEvent", stateJson.UTF8String);
}

+(NSString *)jsonStringFromState:(OVLAppConnectState *)state
{
    NSDictionary * dictionary = [NSDictionary dictionaryWithObjectsAndKeys:
                                 [self unityStringFromStatus: state.status], @"status",
                                 (state.walletAddress != nil ? state.walletAddress : @""), @"walletAddress",
                                 nil];
    
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dictionary
                                                       options:0
                                                         error:&error];
    NSString *jsonString;
    if (! jsonData) {
        jsonString = @"{\"status\": \"Error\"}";
    } else {
        jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    }

    return jsonString;
}

+(NSString *)unityStringFromStatus:(OVLAppConnectStatus)status
{
    if ( status == OVLAppConnectStatusNotInitialized )
    {
        return @"NotInitialized";
    }
    else if ( status == OVLAppConnectStatusDisconnected )
    {
        return @"Disconnected";
    }
    else if ( status == OVLAppConnectStatusConnected )
    {
        return @"Connected";
    }
    else if ( status == OVLAppConnectStatusWalletNotInstalled )
    {
        return @"WalletNotInstalled";
    }
    else if ( status == OVLAppConnectStatusCancelled )
    {
        return @"Cancelled";
    }
    else if ( status == OVLAppConnectStatusDeclined )
    {
        return @"Declined";
    }
    else
    {
        return @"Error";
    }
}

+ (OVAppConnectManagerDelegateForwarder *)sharedInstance
{
    static dispatch_once_t token;
    static OVAppConnectManagerDelegateForwarder *shared;
    dispatch_once(&token, ^{
        shared = [[OVAppConnectManagerDelegateForwarder alloc] init];
    });
    return shared;
}


@end
