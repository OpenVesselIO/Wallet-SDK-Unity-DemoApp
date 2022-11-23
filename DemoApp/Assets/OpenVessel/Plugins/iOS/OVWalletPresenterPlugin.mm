// Copyright © 2021 OpenVessel. All rights reserved.

#import <OpenVessel/OpenVessel.h>

#import "OVSdkPluginUtils.h"

extern "C" {
    
    void _OVWalletPresenterShowToken(const char * fqtn)
    {
        [OVLSdk.sharedInstance.presentationController presentToken:NSSTRING(fqtn) fromViewController:UNITY_VIEW_CONTROLLER animated:YES];
    }

    void _OVWalletPresenterShowCollection(const char * fqcn)
    {
        [OVLSdk.sharedInstance.presentationController presentCollection: NSSTRING(fqcn) fromViewController: UNITY_VIEW_CONTROLLER animated: YES];
    }

    void _OVWalletPresenterShowGame(const char * fqgn)
    {
        [OVLSdk.sharedInstance.presentationController presentGame: NSSTRING(fqgn) fromViewController: UNITY_VIEW_CONTROLLER animated: YES];
    }
    
    void _OVWalletPresenterShowWallet()
    {
        [OVLSdk.sharedInstance.presentationController presentWalletFromViewController:UNITY_VIEW_CONTROLLER animated:YES];
    }

    void _OVWalletPresenterShowProfile()
    {
        [OVLSdk.sharedInstance.presentationController presentProfileFromViewController:UNITY_VIEW_CONTROLLER animated:YES];
    }

    void _OVWalletPresenterOpenTokenInWalletApplication(const char * fqtn)
    {
        [OVLSdk.sharedInstance.presentationController openTokenInWalletApplication:NSSTRING(fqtn)];
    }
    
    void _OVWalletPresenterOpenCollectionInWalletApplication(const char * fqtn)
    {
        [OVLSdk.sharedInstance.presentationController openCollectionInWalletApplication: NSSTRING(fqtn)];
    }
    
    void _OVWalletPresenterOpenGameInWalletApplication(const char * fqgn)
    {
        [OVLSdk.sharedInstance.presentationController openGameInWalletApplication: NSSTRING(fqgn)];
    }
    
    void _OVWalletPresenterOpenWalletApplication()
    {
        [OVLSdk.sharedInstance.presentationController openWalletApplication];
    }
    
    void _OVWalletPresenterVerifyWalletAddressInWalletApplication(const char * walletAddress)
    {
        [OVLSdk.sharedInstance.presentationController verifyWalletAddressInWalletApplication: NSSTRING(walletAddress)];
    }

    void _OVWalletPresenterLoadBalanceInWalletApplication(const char * walletAddress)
    {
        [OVLSdk.sharedInstance.presentationController loadBalanceInWalletApplication: NSSTRING(walletAddress)];
    }

    void _OVWalletPresenterLoadBalanceByAmountInWalletApplication(const char * walletAddress, int32_t amount)
    {
        [OVLSdk.sharedInstance.presentationController loadBalanceInWalletApplication: NSSTRING(walletAddress) byAmount: amount];
    }

    void _OVWalletPresenterConfirmTransactionInWalletApplication(const char * transactionId)
    {
        [OVLSdk.sharedInstance.presentationController confirmTransactionInWalletApplication: NSSTRING(transactionId)];
    }

}
