#import <UIKit/UIKit.h>

#import "OVSdkPluginUtils.h"

extern "C" {

    void _OVUShowAlert(const char * text)
    {
        UIAlertController *alertController;
        alertController = [UIAlertController alertControllerWithTitle:nil message:NSSTRING(text)
                                                       preferredStyle:UIAlertControllerStyleAlert];

        UIAlertAction *okAction;
        okAction = [UIAlertAction actionWithTitle:@"OK" style:UIAlertActionStyleDefault handler:NULL];

        [alertController addAction:okAction];

        [UNITY_VIEW_CONTROLLER presentViewController:alertController animated:YES completion:NULL];
    }

}