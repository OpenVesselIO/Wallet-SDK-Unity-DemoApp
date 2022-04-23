// Copyright © 2021 OpenVessel. All rights reserved.

#import <OpenVessel/OpenVessel.h>

#import "OVSdkPluginUtils.h"

extern "C" {

    void _OVWalletPresenterShowWallet()
    {
        [[[OVLSdk sharedInstance] presentationController] presentWalletFromViewController:UNITY_VIEW_CONTROLLER animated:YES];
    }
    
    void _OVWalletPresenterShowToken(const char * fqtn)
    {
        [[[OVLSdk sharedInstance] presentationController] presentToken:NSSTRING(fqtn) fromViewController:UNITY_VIEW_CONTROLLER animated:YES];
    }

    void _OVWalletPresenterOpenWalletApplication()
    {
        [[[OVLSdk sharedInstance] presentationController] openWalletApplication];
    }

    void _OVWalletPresenterOpenTokenInWalletApplication(const char * fqtn)
    {
        [[[OVLSdk sharedInstance] presentationController] openTokenInWalletApplication:NSSTRING(fqtn)];
    }
}
