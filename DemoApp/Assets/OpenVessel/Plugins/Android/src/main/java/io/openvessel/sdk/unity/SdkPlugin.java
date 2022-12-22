package io.openvessel.sdk.unity;


import android.net.Uri;
import android.os.AsyncTask;

import com.unity3d.player.UnityPlayer;

import org.json.JSONObject;

import io.openvessel.wallet.sdk.SdkConfiguration;
import io.openvessel.wallet.sdk.VesselEnvironment;
import io.openvessel.wallet.sdk.VesselSdk;
import io.openvessel.wallet.sdk.VesselSdkListener;
import io.openvessel.wallet.sdk.utils.Logger;
import io.openvessel.wallet.sdk.utils.StringUtils;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class SdkPlugin
{
    private static final VesselSdkListener forwardingListener = new ForwardingSdkListener();

    public static void initialize(final String cuid)
    {
        AppConnectManagerPlugin.initialize();
        WalletPresenterPlugin.initialize();
        CustomPresenterPlugin.initialize();

        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.setListener( forwardingListener );

        sdk.initialize( cuid );
    }

    public static void setEnvironment(final String environment)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.setEnvironment( VesselEnvironment.valueOf( environment ) );
    }

    public static void setConfiguration(final String settingsJson)
    {
        if ( StringUtils.isValidString( settingsJson ) )
        {
            try
            {
                final JSONObject settings = new JSONObject( settingsJson );
                final int logLevelInt = settings.getInt( "MinLogLevel" );
                final SdkConfiguration.SdkLogLevel logLevel = SdkConfiguration.SdkLogLevel.valueOfOrdinal( logLevelInt );

                final String callbackUrl = settings.getString( "CallbackUrl" );

                final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
                sdk.setConfiguration( SdkConfiguration.builder()
                                              .minLogLevel( logLevel )
                                              .callbackUrl( Uri.parse( callbackUrl ) )
                                              .build() );
            }
            catch ( Exception ex )
            {
                Logger.userError( "SdkPlugin", "Unable to parse settings", ex );
            }
        }
    }

    public static String getEnvironment()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        return sdk.getEnvironment().toString();
    }

    public static boolean handleDeeplink(final String deeplink)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        return sdk.handleDeeplink( deeplink, currentActivity );
    }

    private static class ForwardingSdkListener
            implements VesselSdkListener
    {
        @Override public void onInitialized()
        {
            AsyncTask.THREAD_POOL_EXECUTOR.execute( () -> UnityPlayer.UnitySendMessage( "OVSdkCallbacks", "ForwardOnSdkInitializedEvent", "" ) );
        }
    }
}
