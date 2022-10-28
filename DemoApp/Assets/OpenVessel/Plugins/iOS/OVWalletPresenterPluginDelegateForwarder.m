//
//  OVSdkPluginCallbackForwarder.m
//  Unity-iPhone
//
//  Created by Basil Shikin on 11/10/21.
//

#import "OVWalletPresenterPluginDelegateForwarder.h"

#import "OVSdkPluginUtils.h"
#import <OpenVessel/OpenVessel.h>

#ifdef __cplusplus
extern "C" {
#endif
    static const char * const kCallbacksClassName = "OVWalletPresenterCallbacks";

    // UnityAppController.mm
    UIViewController* UnityGetGLViewController(void);
    UIWindow* UnityGetMainWindow(void);
    
    // life cycle management
    void UnityPause(int pause);
    void UnitySendMessage(const char* obj, const char* method, const char* msg);
#ifdef __cplusplus
}
#endif


@interface OVWalletPresenterPluginDelegateForwarder() <OVLPresentationControllerDelegate>
@end

@implementation OVWalletPresenterPluginDelegateForwarder

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
    OVLSdk.sharedInstance.presentationController.delegate = self;
}

- (void)presentationControllerWillPresentWallet:(OVLPresentationController *)presentationController
{
    UnitySendMessage(kCallbacksClassName, "ForwardOnWalletShowEvent", "");
}

- (void)presentationControllerWillDismissWallet:(OVLPresentationController *)presentationController
{
    UnitySendMessage(kCallbacksClassName, "ForwardOnWalletDismissEvent", "");
}

+ (OVWalletPresenterPluginDelegateForwarder *)sharedInstance
{
    static dispatch_once_t token;
    static OVWalletPresenterPluginDelegateForwarder *shared;
    dispatch_once(&token, ^{
        shared = [[OVWalletPresenterPluginDelegateForwarder alloc] init];
    });
    return shared;
}


@end
