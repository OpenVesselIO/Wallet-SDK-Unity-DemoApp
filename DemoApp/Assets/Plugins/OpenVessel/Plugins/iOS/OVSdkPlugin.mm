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

    void _OVSetEnvironment(const char * environment)
    {
        if (strcmp("PRODUCTION", environment) == 0) {
            [[OVLSdk sharedInstance] setEnvironment: OVLEnvironmentProduction];
        } else if (strcmp("STAGING", environment) == 0) {
            [[OVLSdk sharedInstance] setEnvironment: OVLEnvironmentStaging];
        } else if (strcmp("DEVELOPMENT", environment) == 0) {
            [[OVLSdk sharedInstance] setEnvironment: OVLEnvironmentDevelopment];
        }
    }

    char * _OVGetEnvironment()
    {
        const char * environmentStr = "PRODUCTION";
        OVLEnvironment environment = [[OVLSdk sharedInstance] environment];
        if (environment == OVLEnvironmentStaging) {
            environmentStr = "STAGING";
        } else if (environment == OVLEnvironmentDevelopment) {
            environmentStr = "DEVELOPMENT";
        }
        
        char* environmentStrCopy = (char*)malloc(strlen(environmentStr) + 1);
        strcpy(environmentStrCopy, environmentStr);

        return environmentStrCopy;
    }
}
