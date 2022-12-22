//
//  OVCustomPresenterForwarder.m
//  Unity-iPhone
//

#import "OVCustomPresenterForwarder.h"


#import "OVSdkPluginUtils.h"
#import <OpenVessel/OpenVessel.h>

static BOOL __IsLoadBalancePresenterProvided = NO;

#ifdef __cplusplus
extern "C" {
#endif
    // UnityAppController.mm
    UIViewController* UnityGetGLViewController(void);
    UIWindow* UnityGetMainWindow(void);
    
    // life cycle management
    void UnityPause(int pause);
    void UnitySendMessage(const char* obj, const char* method, const char* msg);

    void _OVSetIsLoadBalancePresenterProvided(bool isLoadBalancePresenterProvided)
    {
        __IsLoadBalancePresenterProvided = isLoadBalancePresenterProvided;
    }
#ifdef __cplusplus
}
#endif

@interface OVCustomPresenterForwarder() <OVLCustomPresenter>

@end

@implementation OVCustomPresenterForwarder

- (instancetype)init
{
    if ( self = [super init] )
    {
        // do nothing
    }
    
    return self;
}

- (BOOL)respondsToSelector:(SEL)aSelector
{
    if (@selector(presentLoadBalance) == aSelector)
    {
        return __IsLoadBalancePresenterProvided;
    }

    return [super respondsToSelector:aSelector];
}

- (void)attachPresenter
{
    OVLSdk.sharedInstance.presentationController.customPresenter = self;
}

- (void)presentLoadBalance
{
    UnitySendMessage("OVCustomPresenter", "ShowLoadBalance", "");
}

+ (OVCustomPresenterForwarder *)sharedInstance
{
    static dispatch_once_t token;
    static OVCustomPresenterForwarder *shared;
    dispatch_once(&token, ^{
        shared = [OVCustomPresenterForwarder new];
    });
    return shared;
}


@end
