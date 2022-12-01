//
//  OVSdkUtils.h
//  Unity-iPhone
//
//  Created by Basil Shikin on 11/10/21.
//

#ifndef OVSdkUtils_h
#define OVSdkUtils_h

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

#define NSSTRING(_X) ( (_X != NULL) ? [NSString stringWithCString: _X encoding: NSStringEncodingConversionAllowLossy] : nil)

#define KEY_WINDOW [UIApplication sharedApplication].keyWindow

#define UNITY_VIEW_CONTROLLER UnityGetGLViewController() ?: UnityGetMainWindow().rootViewController ?: [KEY_WINDOW rootViewController]

#ifdef __cplusplus
extern "C" {
#endif
    
//    // UnityAppController.mm
//    UIViewController* UnityGetGLViewController(void);
//    UIWindow* UnityGetMainWindow(void);
//    
//    // life cycle management
//    void UnityPause(int pause);
//    void UnitySendMessage(const char* obj, const char* method, const char* msg);
    
    void ov_unity_dispatch_on_main_thread(dispatch_block_t block)
    {
        if ( block )
        {
            if ( [NSThread isMainThread] )
            {
                block();
            }
            else
            {
                dispatch_async(dispatch_get_main_queue(), block);
            }
        }
    }

#ifdef __cplusplus
}
#endif

#endif /* OVSdkUtils_h */
