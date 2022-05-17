// Copyright © 2021 OpenVessel. All rights reserved.

#import <OpenVessel/OpenVessel.h>

#import "OVSdkPluginUtils.h"

extern "C" {
    
    void _OVWalletPresenterShowToken(const char * fqtn)
    {
        [[[OVLSdk sharedInstance] presentationController] presentToken:NSSTRING(fqtn) fromViewController:UNITY_VIEW_CONTROLLER animated:YES];
    }

    void _OVWalletPresenterShowCollection(const char * fqcn)
    {
        [[[OVLSdk sharedInstance] presentationController] presentCollection: NSSTRING(fqcn) fromViewController: UNITY_VIEW_CONTROLLER animated: YES];
    }
    
    void _OVWalletPresenterShowGame(const char * fqgn)
    {
        [[[OVLSdk sharedInstance] presentationController] presentGame: NSSTRING(fqgn) fromViewController: UNITY_VIEW_CONTROLLER animated: YES];
    }
    
    void _OVWalletPresenterShowWallet()
    {
        [[[OVLSdk sharedInstance] presentationController] presentWalletFromViewController:UNITY_VIEW_CONTROLLER animated:YES];
    }

    void _OVWalletPresenterOpenTokenInWalletApplication(const char * fqtn)
    {
        [[[OVLSdk sharedInstance] presentationController] openTokenInWalletApplication:NSSTRING(fqtn)];
    }
    
    
    void _OVWalletPresenterOpenCollectionInWalletApplication(const char * fqtn)
    {
        [[[OVLSdk sharedInstance] presentationController] openCollectionInWalletApplication: NSSTRING(fqtn)];
    }
    
    void _OVWalletPresenterOpenGameInWalletApplication(const char * fqgn)
    {
        [[[OVLSdk sharedInstance] presentationController] openGameInWalletApplication: NSSTRING(fqgn)];
    }
    
    void _OVWalletPresenterOpenWalletApplication()
    {
        [[[OVLSdk sharedInstance] presentationController] openWalletApplication];
    }
}
