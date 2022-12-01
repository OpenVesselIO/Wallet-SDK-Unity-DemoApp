// Copyright © 2021 OpenVessel. All rights reserved.

#import <OpenVessel/OpenVessel.h>

#import "OVSdkPluginUtils.h"

extern "C" {

    void _OVPurchaseProduct(const char *productId)
    {
        [OVLSdk.sharedInstance.iapManager purchaseProductWithIdentifier:NSSTRING(productId)];
    }
    
}
