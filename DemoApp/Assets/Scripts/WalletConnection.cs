﻿using OVSdk;
using UnityEngine;
using UnityEngine.UI;

public class WalletConnection : MonoBehaviour
{
    private const string USER_ID = "<Unique User ID of your application>";

    OVSdk.AppConnectState _appConnectState;

    public Button _presentConnectButton;
    public Button _connectButton;
    public Text _statusText;

    public InputField _fqtnInputField;
    public InputField _fqcnInputField;
    public InputField _fqgnInputField;

    public InputField _loadBalanceAmountInputField;

    void Start()
    {
        Debug.Log("Starting...");

        UpdateButtons(false);

        _connectButton.interactable = false;
        _presentConnectButton.interactable = false;

        _statusText.text = "Starting...";

        var environment = VesselEnvironment.Staging;
#if OV_ENVIRONMENT_DEV
        environment = VesselEnvironment.Development;
#elif OV_ENVIRONMENT_STAGING
        environment = VesselEnvironment.Staging;
#elif OV_ENVIRONMENT_PROD
        environment = VesselEnvironment.Production;
#endif

        OVSdk.Sdk.Environment = environment;
        OVSdk.AppConnectManagerCallbacks.OnStateUpdated += HandleAppConnectState;

        OVSdk.Sdk.Configuration = new SdkConfiguration
        {
            MinLogLevel = SdkLogLevel.Debug
        };

        Debug.Log("Initializing the SDK...");
        OVSdk.Sdk.Initialize(USER_ID);
    }

    public void PresentConnect()
    {
        Debug.Log("Connecting wallet...");
        OVSdk.Sdk.AppConnectManager.LoadConnectWalletView(USER_ID);
    }

    public void ConnectWallet()
    {
        foreach (var button in GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }

        Debug.Log("Connecting wallet...");
        OVSdk.Sdk.AppConnectManager.ConnectWallet(USER_ID);
    }

    public void DisconnectCurrent()
    {
        Debug.Log("Disconnecting wallet...");
        OVSdk.Sdk.AppConnectManager.DisconnectCurrentSession();
    }

    public void DisconnectAll()
    {
        Debug.Log("Disconnecting wallet...");
        OVSdk.Sdk.AppConnectManager.DisconnectAllSessions();
    }

    public void ShowWallet()
    {
        Debug.Log("Showing wallet inside of the current application...");
        OVSdk.Sdk.WalletPresenter.ShowWallet();
    }

    public void OpenWalletApplication()
    {
        Debug.Log("Opening wallet application...");
        OVSdk.Sdk.WalletPresenter.OpenWalletApplication();
    }

    public void ShowToken()
    {
        Debug.Log("Showing a token inside of the current application...");
        OVSdk.Sdk.WalletPresenter.ShowToken(_fqtnInputField.text);
    }

    public void OpenTokenInWalletApplication()
    {
        Debug.Log("Opening a token in wallet application...");
        OVSdk.Sdk.WalletPresenter.OpenTokenInWalletApplication(_fqtnInputField.text);
    }

    public void ShowCollection()
    {
        Debug.Log("Showing a collection inside of the current application...");
        OVSdk.Sdk.WalletPresenter.ShowCollection(_fqcnInputField.text);
    }

    public void OpenCollectionInWalletApplication()
    {
        Debug.Log("Opening a collection in wallet application...");
        OVSdk.Sdk.WalletPresenter.OpenCollectionInWalletApplication(_fqcnInputField.text);
    }

    public void ShowGame()
    {
        Debug.Log("Showing a game inside of the current application...");
        OVSdk.Sdk.WalletPresenter.ShowGame(_fqgnInputField.text);
    }

    public void OpenGameInWalletApplication()
    {
        Debug.Log("Opening a game in wallet application...");
        OVSdk.Sdk.WalletPresenter.OpenGameInWalletApplication(_fqgnInputField.text);
    }

    public void VerifyWalletAddress()
    {
        var walletAddress = _appConnectState?.WalletAddress;

        if (walletAddress != null)
        {
            Debug.Log("Verifying wallet address '" + walletAddress + "' in wallet application...");
            OVSdk.Sdk.WalletPresenter.VerifyWalletAddressInWalletApplication(_appConnectState.WalletAddress);
        }
    }

    public void LoadBalance()
    {
        var walletAddress = _appConnectState?.WalletAddress;

        if (walletAddress == null)
        {
            return;
        }

        var amountStr = _loadBalanceAmountInputField.text;

        if (amountStr.Length == 0)
        {
            Debug.Log("Loading balance for '" + walletAddress + "'");
            OVSdk.Sdk.WalletPresenter.LoadBalanceInWalletApplication(walletAddress);
        }
        else
        {
            var amount = int.Parse(amountStr);

            Debug.Log("Loading balance for '" + walletAddress + "' by " + amount);
            OVSdk.Sdk.WalletPresenter.LoadBalanceInWalletApplication(walletAddress, amount);
        }
    }

    private void HandleAppConnectState(OVSdk.AppConnectState state)
    {
        Debug.Log("Got new wallet state: " + state);

        _appConnectState = state;

        var isConnected = state.Status == OVSdk.AppConnectStatus.Connected;
        if (isConnected)
        {
            _statusText.text = "Wallet: " + state.WalletAddress + "\n(" + OVSdk.Sdk.Environment + ")";
        }
        else
        {
            _statusText.text = "Error: " + state.Status + "\n(" + OVSdk.Sdk.Environment + ")";
        }

        UpdateButtons(isConnected);
    }

    private void UpdateButtons(bool isConnected)
    {
        foreach (var button in GetComponentsInChildren<Button>())
        {
            if (button.name.StartsWith("BtnOpen"))
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = isConnected;
            }
        }

        _presentConnectButton.interactable = !isConnected;
        _connectButton.interactable = !isConnected;
    }

}