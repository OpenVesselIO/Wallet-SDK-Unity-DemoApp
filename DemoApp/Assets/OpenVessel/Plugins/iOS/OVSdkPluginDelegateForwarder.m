//
//  OVSdkPluginCallbackForwarder.m
//  Unity-iPhone
//
//  Created by Basil Shikin on 11/10/21.
//

#import "OVSdkPluginDelegateForwarder.h"

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


@interface OVSdkPluginDelegateForwarder()<OVLSdkDelegate>
@end

@implementation OVSdkPluginDelegateForwarder

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
    [[OVLSdk sharedInstance] setDelegate: self
                           delegateQueue: dispatch_get_main_queue()];
}

- (void)openVesselSdkDidStart:(OVLSdk *)sdk
{
    UnitySendMessage("OVSdkCallbacks", "ForwardOnSdkInitializedEvent", "");
}

+ (OVSdkPluginDelegateForwarder *)sharedInstance
{
    static dispatch_once_t token;
    static OVSdkPluginDelegateForwarder *shared;
    dispatch_once(&token, ^{
        shared = [[OVSdkPluginDelegateForwarder alloc] init];
    });
    return shared;
}


@end
