package io.openvessel.sdk.unity;


import io.openvessel.wallet.sdk.VesselSdk;

import static com.unity3d.player.UnityPlayer.currentActivity;

public class WalletPresenterPlugin
{
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
}
