//
//  OVSdkPluginCallbackForwarder.h
//  Unity-iPhone
//
//  Created by Basil Shikin on 11/10/21.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface OVWalletPresenterPluginDelegateForwarder : NSObject

- (void)attachDelegate;

+ (instancetype)sharedInstance;

- (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
