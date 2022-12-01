package io.openvessel.sdk.unity;

import android.os.AsyncTask;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Optional;

import io.openvessel.wallet.sdk.VesselSdk;
import io.openvessel.wallet.sdk.iap.IapException;
import io.openvessel.wallet.sdk.iap.models.IapVerificationObject;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class IapManagerPlugin
{
    private static final String TAG = "IapManagerPlugin";

    public static void purchaseProduct(final String productId)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getIapManager().purchaseProduct( productId, currentActivity ).whenComplete( (verificationObject, th) -> {
            if ( th != null )
            {
                final boolean isCancelled = Optional
                        .ofNullable( th.getCause() )
                        .filter( IapException.class::isInstance )
                        .map( IapException.class::cast )
                        .map( IapException::isCanceled )
                        .orElse( false );

                if ( isCancelled )
                {
                    final String json = String.format( "{ \"productId\": \"%s\" }", productId );

                    Log.w( "Plugin", "Forwarding ForwardOnPurchaseCancel " + json );

                    UnitySendMessageAsync( "ForwardOnPurchaseCancelEvent", json );
                }
                else
                {
                    final String json = toJsonStr( productId, th );

                    Log.w( "Plugin", "Forwarding ForwardOnPurchaseFailure " + json );

                    UnitySendMessageAsync( "ForwardOnPurchaseFailureEvent", json );
                }
            }
            else
            {
                final String json = toJsonStr( productId, verificationObject );

                Log.w( "Plugin", "Forwarding ForwardOnPurchaseSuccess " + json );

                UnitySendMessageAsync( "ForwardOnPurchaseSuccessEvent", json );
            }
        } );
    }

    private static String toJsonStr(final String productId, final IapVerificationObject verificationObject)
    {
        try
        {
            final JSONObject result = new JSONObject();
            result.put( "productId", productId );
            result.put( "receipt", verificationObject.getIapReceipt() );
            result.put( "receiptSignature", verificationObject.getIapReceiptSignature() );
            return result.toString();
        }
        catch ( JSONException e )
        {
            Log.e( TAG, "Failed to convert IAP verification object to JSON: " + verificationObject, e );

            return toJsonStr( productId, e );
        }
    }

    private static String toJsonStr(final String productId, final Throwable th)
    {
        return String.format( "{ \"productId\": \"%s\", \"error\": { \"message\": \"%s\", \"detailedMessage\": \"%s\" } }", productId, th.getMessage(), th.toString() );
    }

    private static void UnitySendMessageAsync(final String method, final String json)
    {
        AsyncTask.THREAD_POOL_EXECUTOR.execute( () -> {
            UnityPlayer.UnitySendMessage( "OVIapManagerCallbacks", method, json );
        } );
    }

}
