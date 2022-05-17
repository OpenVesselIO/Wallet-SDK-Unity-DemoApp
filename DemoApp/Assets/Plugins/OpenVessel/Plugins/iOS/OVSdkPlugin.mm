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

    void _OVSetConfiguration(const char * configurationJson)
    {
        NSError *jsonError;
        NSData *configurationJsonData = [NSSTRING(configurationJson) dataUsingEncoding: NSUTF8StringEncoding];
        NSDictionary * configurationDict = [NSJSONSerialization JSONObjectWithData:configurationJsonData
                                              options:NSJSONReadingMutableContainers
                                                error:&jsonError];
        if ( jsonError == nullptr && configurationDict ) {
            
            OVLLogLevel minLogLevel = OVLLogLevelError;
            NSURL * connectCallbackUrl = NULL;
            
            if ([[configurationDict allKeys] containsObject: @"MinLogLevel"]) {
                int minLogLevelInt = [configurationDict[@"MinLogLevel"] integerValue];
                if ( minLogLevelInt == 0) {
                    minLogLevel = OVLLogLevelDebug;
                }
                else if ( minLogLevelInt == 1) {
                    minLogLevel = OVLLogLevelInfo;
                }
                else if ( minLogLevelInt == 2) {
                    minLogLevel = OVLLogLevelWarning;
                }
                else if ( minLogLevelInt == 3) {
                    minLogLevel = OVLLogLevelError;
                }
            }
            
            if ([configurationDict objectForKey: @"ConnectCallbackUrl"]) {
                connectCallbackUrl = [NSURL URLWithString:[configurationDict objectForKey: @"ConnectCallbackUrl"]];
            }
            
            OVLSdkConfiguration * configuration = [[OVLSdkConfiguration alloc] init];
            configuration.minLogLevel = minLogLevel;
            configuration.connectCallbackUrl = connectCallbackUrl;
            
            [[OVLSdk sharedInstance] setConfiguration: configuration];
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
    
    extern bool _OVHandleConnectDeeplink(const char * deeplink) 
    {
        NSURL * deeplinkUrl = [NSURL URLWithString: NSSTRING(deeplink)];
        
        return [[OVLSdk sharedInstance] handleDeepLink: deeplinkUrl];
    }
}
