//
//  OVIapManagerDelegateForwarder.h
//  Unity-iPhone
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface OVIapManagerDelegateForwarder : NSObject

- (void)attachDelegate;

+ (instancetype)sharedInstance;

- (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
