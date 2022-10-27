package io.openvessel.sdk.unity;


import android.os.AsyncTask;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import io.openvessel.wallet.sdk.AppConnectListener;
import io.openvessel.wallet.sdk.AppConnectManager;
import io.openvessel.wallet.sdk.AppConnectState;
import io.openvessel.wallet.sdk.AppConnectStatus;
import io.openvessel.wallet.sdk.VesselSdk;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class AppConnectManagerPlugin
{
    private static final String TAG = "AppConnectManagerPlugin";

    private static final AppConnectListener forwardingListener = new ForwardingAppConnectListener();

    public static void connectWallet(final String userId)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getAppConnectManager().connectWallet( userId );
    }

    public static void disconnectCurrentSession()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getAppConnectManager().disconnectCurrentSession();
    }

    public static void disconnectAllSessions()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getAppConnectManager().disconnectAllSessions();
    }

    public static void loadConnectWalletView(final String userId)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getAppConnectManager().startConnectActivity( userId, currentActivity );
    }

    public static void cancelConnect()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getAppConnectManager().cancelConnect();
    }

    static void initialize()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getAppConnectManager().setAppConnectListener( forwardingListener );
    }

    private static class ForwardingAppConnectListener
            implements AppConnectListener
    {

        @Override
        public void onStateUpdated(final AppConnectManager manager)
        {
            final AppConnectState newState = manager.getState();
            Log.w( "Plugin", "Forwarding ForwardOnStatusUpdated " + toJsonStr( newState ) );

            AsyncTask.THREAD_POOL_EXECUTOR.execute( () -> {
                UnityPlayer.UnitySendMessage( "OVAppConnectManagerCallbacks", "ForwardOnStateUpdatedEvent", toJsonStr( newState ) );
            } );
        }

        private static String toJsonStr(final AppConnectState newState)
        {
            try
            {
                final JSONObject result = new JSONObject();
                result.put( "status", toUnityString( newState.getStatus() ) );
                result.put( "walletAddress", newState.getWalletAddress() );
                result.put( "userId", newState.getUserId() );
                result.put( "accessToken", newState.getAccessToken() );
                return result.toString();
            }
            catch ( JSONException e )
            {
                Log.e( TAG, "Failed to convert app connect state to JSON: " + newState, e );

                return "{\"status\": \"Error\"}";
            }
        }

        private static String toUnityString(final AppConnectStatus status)
        {
            if ( status == AppConnectStatus.NOT_INITIALIZED )
            {
                return "NotInitialized";
            }
            else if ( status == AppConnectStatus.DISCONNECTED )
            {
                return "Disconnected";
            }
            else if ( status == AppConnectStatus.CONNECTED )
            {
                return "Connected";
            }
            else if ( status == AppConnectStatus.ERROR_CANCELED )
            {
                return "ErrorCanceled";
            }
            else if ( status == AppConnectStatus.ERROR_DECLINED )
            {
                return "ErrorDeclined";
            }
            else if ( status == AppConnectStatus.ERROR_WALLET_NOT_INSTALLED )
            {
                return "ErrorWalletNotInstalled";
            }
            else
            {
                return "Error";
            }
        }
    }
}
