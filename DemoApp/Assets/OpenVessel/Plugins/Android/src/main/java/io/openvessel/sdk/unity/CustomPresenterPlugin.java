package io.openvessel.sdk.unity;

import android.os.AsyncTask;

import com.unity3d.player.UnityPlayer;

import io.openvessel.wallet.sdk.CustomPresenter;
import io.openvessel.wallet.sdk.VesselSdk;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class CustomPresenterPlugin
{
    private static final String TAG = "CustomPresenterPlugin";

    private static final CustomPresenter forwardingCustomPresenter = new ForwardingCustomPresenter();

    private static boolean isLoadBalancePresenterProvided = false;

    public static void setIsLoadBalancePresenterProvided(final boolean isLoadBalancePresenterProvided)
    {
        CustomPresenterPlugin.isLoadBalancePresenterProvided = isLoadBalancePresenterProvided;
    }

    private static void UnitySendMessageAsync(final String method, final String json)
    {
        AsyncTask.THREAD_POOL_EXECUTOR.execute( () -> {
            UnityPlayer.UnitySendMessage( "OVCustomPresenter", method, json );
        } );
    }

    static void initialize()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().setCustomPresenter( forwardingCustomPresenter );
    }

    private static class ForwardingCustomPresenter
            implements CustomPresenter
    {

        @Override
        public boolean showLoadBalance()
        {
            if ( isLoadBalancePresenterProvided )
            {
                UnitySendMessageAsync( "ShowLoadBalance", "" );
                return true;
            }

            return CustomPresenter.super.showLoadBalance();
        }
    }

}
