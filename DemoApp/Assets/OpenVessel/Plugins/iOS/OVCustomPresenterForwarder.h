//
//  OVCustomPresenterForwarder.h
//  Unity-iPhone
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface OVCustomPresenterForwarder : NSObject

- (void)attachPresenter;

+ (instancetype)sharedInstance;

- (instancetype)init NS_UNAVAILABLE;

@end

NS_ASSUME_NONNULL_END
