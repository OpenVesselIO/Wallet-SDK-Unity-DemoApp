// Copyright © 2021 OpenVessel. All rights reserved.

#import <OpenVessel/OpenVessel.h>

#import "OVSdkPluginUtils.h"
#import "OVSdkPluginDelegateForwarder.h"
#import "OVAppConnectManagerDelegateForwarder.h"

extern "C" {
        
    void _OVInitialize(const char * userId)
    {
        [[OVSdkPluginDelegateForwarder sharedInstance] attachDelegate];
        [[OVAppConnectManagerDelegateForwarder sharedInstance] attachDelegate];
        
        [[OVLSdk sharedInstance] startWithUserId: NSSTRING(userId)];
    }

    void _OVLoadWalletView()
    {
        [[OVLSdk sharedInstance] presentWalletFromViewController:UNITY_VIEW_CONTROLLER];
    }
}
