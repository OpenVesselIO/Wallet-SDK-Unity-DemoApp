package io.openvessel.sdk.unity;


import io.openvessel.wallet.sdk.VesselSdk;
import io.openvessel.wallet.sdk.WalletPresenterListener;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class WalletPresenterPlugin
{

    private static final WalletPresenterListener forwardingListener = new ForwardingWalletPresenterListener();

    static void initialize()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().setListener( forwardingListener );
    }

    public static void openWalletApplication()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().openWalletApplication( currentActivity );
    }

    public static void showWallet()
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().showWallet( currentActivity );
    }

    public static void openTokenInWalletApplication(final String fqtn)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().openTokenInWalletApplication( fqtn, currentActivity );
    }

    public static void showToken(final String fqtn)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().showToken( fqtn, currentActivity );
    }

    public static void openCollectionInWalletApplication(final String fqcn)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().openCollectionInWalletApplication( fqcn, currentActivity );
    }

    public static void showCollection(final String fqcn)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().showCollection( fqcn, currentActivity );
    }

    public static void openGameInWalletApplication(final String fqgn)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().openGameInWalletApplication( fqgn, currentActivity );
    }

    public static void showGame(final String fqgn)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().showGame( fqgn, currentActivity );
    }

    public static void verifyWalletAddressInWalletApplication(final String walletAddress)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().verifyWalletAddressInWalletApplication( walletAddress, currentActivity );
    }

    public static void loadBalanceInWalletApplication(final String walletAddress)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().loadBalanceInWalletApplication( walletAddress, currentActivity );
    }

    public static void loadBalanceByAmountInWalletApplication(final String walletAddress, final int amount)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().loadBalanceInWalletApplication( walletAddress, amount, currentActivity );
    }

    public static void confirmTransactionInWalletApplication(final String transactionId)
    {
        final VesselSdk sdk = VesselSdk.getInstance( currentActivity );
        sdk.getWalletPresenter().confirmTransactionInWalletApplication( transactionId, currentActivity );
    }

    private static class ForwardingWalletPresenterListener
            implements WalletPresenterListener
    {

        private static final String CallbacksClassName = "OVWalletPresenterCallbacks";

        @Override
        public void onWalletShow()
        {
            AsyncTask.THREAD_POOL_EXECUTOR.execute( () -> UnityPlayer.UnitySendMessage( CallbacksClassName, "ForwardOnWalletShowEvent", "" ) );
        }

        @Override
        public void onWalletDismiss()
        {
            AsyncTask.THREAD_POOL_EXECUTOR.execute( () -> UnityPlayer.UnitySendMessage( CallbacksClassName, "ForwardOnWalletDismissEvent", "" ) );
        }

    }

}
