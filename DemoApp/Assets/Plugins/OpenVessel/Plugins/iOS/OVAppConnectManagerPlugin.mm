// Copyright © 2021 OpenVessel. All rights reserved.

#import <OpenVessel/OpenVessel.h>

#import "OVSdkPluginUtils.h"

extern "C" {

    void _OVLoadConnectWalletView(const char * userId)
    {
        [[[OVLSdk sharedInstance] appConnectManager]
                 presentConnectFromViewController:UNITY_VIEW_CONTROLLER
                 withUserId:NSSTRING(userId)
                 delegate:nil
                 animated:YES];
    }
    void _OVConnectWallet(const char * userId)
    {
        [[[OVLSdk sharedInstance] appConnectManager] connectWalletWithUserId: NSSTRING(userId)];
    }

    void _OVCancelConnect()
    {
        [[[OVLSdk sharedInstance] appConnectManager] cancelConnect];
    }

    void _OVDisconnectCurrentSession()
    {
        [[[OVLSdk sharedInstance] appConnectManager] disconnectCurrentSessionWithResultHandler: ^(BOOL success) {}];
    }
    
    void _OVDisconnectAllSessions()
    {
        [[[OVLSdk sharedInstance] appConnectManager] disconnectAllSessionsWithResultHandler: ^(BOOL success) {}];
    }
    
    
}
