package io.openvessel.sdk.unity;

import android.os.AsyncTask;

import com.unity3d.player.UnityPlayer;

import org.json.JSONObject;

import io.openvessel.wallet.sdk.EarningsActivitySettings;
import io.openvessel.wallet.sdk.EarningsManager;
import io.openvessel.wallet.sdk.VesselSdk;
import io.openvessel.wallet.sdk.utils.ExceptionToUserMessage;
import io.openvessel.wallet.sdk.utils.Logger;
import io.openvessel.wallet.sdk.utils.StringUtils;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class EarningsManagerPlugin
{

    private static final String TAG = "EarningsManagerPlugin";

    private static final String CALLBACKS_OBJECT_NAME = "OVEarningsManagerCallbacks";

    public static void trackRevenuedAd(final String adTypeString)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );

        EarningsManager.AdType.maybeValueOf( adTypeString ).ifPresent( adType -> {
            sdk.getEarningsManager().trackRevenuedAd( adType );
        } );
    }

    public static void trackImpression(final String triggerName)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );

        sdk.getEarningsManager().trackImpression( triggerName );
    }

    public static void showEarnings(final String settingsJson)
    {
        if ( StringUtils.isValidString( settingsJson ) )
        {
            try
            {
                final JSONObject settings = new JSONObject( settingsJson );

                final VesselSdk sdk = VesselSdk.getInstance( currentActivity );

                EarningsActivitySettings.PromoType
                        .maybeValueOf( settings.getString( "promoType" ) )
                        .ifPresent( promoType -> {
                            try
                            {
                                sdk.getEarningsManager().startEarningsActivity(
                                        EarningsActivitySettings.builder( settings.getString( "userId" ) )
                                                .promoType( promoType )
                                                .triggerName( settings.getString( "triggerName" ) )
                                                .build(),
                                        currentActivity
                                );
                            }
                            catch ( Exception ex )
                            {
                                Logger.userError( TAG, "Unable to parse settings", ex );
                            }
                        } );
            }
            catch ( Exception ex )
            {
                Logger.userError( TAG, "Unable to parse settings", ex );
            }
        }
    }

    public static void generatePhoneAuthCode(final String phoneNumber)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );

        sdk.getEarningsManager().generatePhoneAuthCode( phoneNumber )
                .whenCompleteAsync( (authCodeMetadata, th) -> {
                    if ( authCodeMetadata != null )
                    {
                        try
                        {
                            UnitySendMessageAsync(
                                    "ForwardOnAuthCodeMetadataEvent",
                                    new JSONObject()
                                            .put( "phoneNumber", phoneNumber )
                                            .put( "createdAt", authCodeMetadata.getCreatedAt() )
                                            .put( "expiresAt", authCodeMetadata.getExpiresAt() )
                                            .put( "ttl", authCodeMetadata.getCooldown() )
                            );
                        }
                        catch ( Exception ex )
                        {
                            UnitySendAuthErrorAsync( ExceptionToUserMessage.convert( ex, currentActivity ) );
                        }
                    }
                    else
                    {
                        UnitySendAuthErrorAsync( ExceptionToUserMessage.convert( th, currentActivity ) );
                    }
                } );
    }

    public static void loginByPhoneAuthCode(final String json)
    {
        if ( StringUtils.isValidString( json ) )
        {
            try
            {
                final JSONObject jsonObject = new JSONObject( json );

                final VesselSdk sdk = VesselSdk.getInstance( currentActivity );

                sdk.getEarningsManager()
                        .loginByPhoneAuthCode(
                                jsonObject.getString( "phoneNumber" ),
                                jsonObject.getString( "code" ),
                                jsonObject.getLong( "codeCreatedAt" ),
                                jsonObject.getString( "userId" )
                        )
                        .whenComplete( (unused, th) -> {
                            if ( th != null )
                            {
                                UnitySendAuthErrorAsync( ExceptionToUserMessage.convert( th, currentActivity ) );
                            }
                        } );
            }
            catch ( Exception ex )
            {
                Logger.userError( TAG, "Unable to parse parameters", ex );
            }
        }
    }

    public static void generateEmailVerificationCode(final String email)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );

        sdk.getEarningsManager().generateEmailVerificationCode( email )
                .whenCompleteAsync( (authCodeMetadata, th) -> {
                    if ( authCodeMetadata != null )
                    {
                        try
                        {
                            UnitySendMessageAsync(
                                    "ForwardOnVerificationCodeMetadataEvent",
                                    new JSONObject()
                                            .put( "email", email )
                                            .put( "createdAt", authCodeMetadata.getCreatedAt() )
                                            .put( "expiresAt", authCodeMetadata.getExpiresAt() )
                                            .put( "ttl", authCodeMetadata.getCooldown() )
                            );
                        }
                        catch ( Exception ex )
                        {
                            UnitySendVerificationErrorAsync( ExceptionToUserMessage.convert( ex, currentActivity ) );
                        }
                    }
                    else
                    {
                        UnitySendVerificationErrorAsync( ExceptionToUserMessage.convert( th, currentActivity ) );
                    }
                } );
        ;
    }

    public static void verifyEmailByCode(final String json)
    {
        if ( StringUtils.isValidString( json ) )
        {
            try
            {
                final JSONObject jsonObject = new JSONObject( json );

                final VesselSdk sdk = VesselSdk.getInstance( currentActivity );

                sdk.getEarningsManager()
                        .verifyEmailByCode(
                                jsonObject.getString( "email" ),
                                jsonObject.getString( "code" ),
                                jsonObject.getLong( "codeCreatedAt" )
                        )
                        .whenComplete( (unused, th) -> {
                            if ( th != null )
                            {
                                UnitySendVerificationErrorAsync( ExceptionToUserMessage.convert( th, currentActivity ) );
                            }
                            else
                            {
                                UnitySendVerificationSuccess();
                            }
                        } );
            }
            catch ( Exception ex )
            {
                Logger.userError( TAG, "Unable to parse parameters", ex );
            }
        }
    }

    private static void UnitySendMessageAsync(final String method, final JSONObject jsonObj)
    {
        UnitySendMessageAsync( method, jsonObj.toString() );
    }

    private static void UnitySendMessageAsync(final String method, final String msg)
    {
        AsyncTask.THREAD_POOL_EXECUTOR.execute( () -> {
            UnityPlayer.UnitySendMessage( CALLBACKS_OBJECT_NAME, method, msg );
        } );
    }

    private static void UnitySendAuthErrorAsync(final String msg)
    {
        UnitySendMessageAsync( "ForwardOnAuthFailureEvent", msg );
    }

    private static void UnitySendVerificationErrorAsync(final String msg)
    {
        UnitySendMessageAsync( "ForwardOnVerificationFailureEvent", msg );
    }

    private static void UnitySendVerificationSuccess()
    {
        UnitySendMessageAsync( "ForwardOnVerificationSuccessEvent", "" );
    }

}
