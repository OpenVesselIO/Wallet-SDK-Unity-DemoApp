package io.openvessel.sdk.unity;

import com.unity3d.player.UnityPlayer;

import java.util.Optional;

import org.json.JSONObject;

import io.openvessel.wallet.sdk.EarningsActivitySettings;
import io.openvessel.wallet.sdk.EarningsManager;
import io.openvessel.wallet.sdk.VesselSdk;
import io.openvessel.wallet.sdk.utils.Logger;
import io.openvessel.wallet.sdk.utils.StringUtils;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class EarningsManagerPlugin
{

    public static void trackRevenuedAd(final String adTypeString)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );

        EarningsManager.AdType.maybeValueOf( adTypeString ).ifPresent( adType -> {
            sdk.getEarningsManager().trackRevenuedAd( adType );
        } );
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
                        sdk.getEarningsManager().startEarningsActivity(
                            EarningsActivitySettings.builder( settings.getString( "userId" ) )
                                    .promoType( promoType )
                                    .triggerName( settings.getString( "triggerName" ) )
                                    .build(),
                            currentActivity
                        );
                    } );
            }
            catch ( Exception ex )
            {
                Logger.userError( "EarningsManagerPlugin", "Unable to parse settings", ex );
            }
        }
    }

}
