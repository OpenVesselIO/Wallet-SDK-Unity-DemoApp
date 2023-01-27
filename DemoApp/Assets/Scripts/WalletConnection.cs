using System;
using OVSdk;
using UnityEngine;
using UnityEngine.UI;

public class WalletConnection : MonoBehaviour
{
    private const string USER_ID = "<Unique User ID of your application>";

    private OVSdk.AppConnectState _appConnectState;

    public Button _presentConnectButton;
    public Button _connectButton;
    public Text _statusText;

    public InputField _fqtnInputField;
    public InputField _fqcnInputField;
    public InputField _fqgnInputField;

    public InputField _loadBalanceAmountInputField;

    public Button _checkWalletAppInstallButton;

    public Text _setOrUnsetCustomPresenterButtonText;

    private int _walletShowCallCount;
    private int _walletDismissCallCount;

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
#elif OV_ENVIRONMENT_TESTING
        environment = VesselEnvironment.Testing;
#elif OV_ENVIRONMENT_STAGING
        environment = VesselEnvironment.Staging;
#elif OV_ENVIRONMENT_PROD
        environment = VesselEnvironment.Production;
#endif

        OVSdk.Sdk.Environment = environment;
        OVSdk.AppConnectManagerCallbacks.OnStateUpdated += HandleAppConnectState;
        OVSdk.WalletPresenterCallbacks.OnWalletShow += HandleWalletShow;
        OVSdk.WalletPresenterCallbacks.OnWalletDismiss += HandleWalletDismiss;

        UpdateSetOrUnsetCustomPresenterButtonText();

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

    public void ShowProfile()
    {
        Debug.Log("Showing profile inside of the current application...");
        OVSdk.Sdk.WalletPresenter.ShowProfile();
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

    public void CheckWalletAppInstall()
    {
        string text;
        if (OVSdk.Sdk.WalletPresenter.IsWalletApplicationInstalled())
        {
            text = "Wallet App installed";
        }
        else
        {
            text = "Wallet App is not installed";
        }

        PopupUtils.ShowPopup(text);
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

    public void SetOrUnsetCustomPresenter()
    {
        if (OVSdk.CustomPresenter.LoadBalancePresenter == null)
        {
            OVSdk.CustomPresenter.LoadBalancePresenter = delegate
            {
                PopupUtils.ShowPopup("Custom Load Balance Presenter invoked");
            };
        } else
        {
            OVSdk.CustomPresenter.LoadBalancePresenter = null;
        }

        UpdateSetOrUnsetCustomPresenterButtonText();
    }

    public void ShowKyc()
    {
        Debug.Log("Showing KYC inside of the current application...");
        OVSdk.Sdk.WalletPresenter.ShowKyc();
    }

    private void HandleAppConnectState(OVSdk.AppConnectState state)
    {
        Debug.Log("Got new wallet state: " + state);

        _appConnectState = state;

        UpdateStatusText();
        UpdateButtons(state.Status == OVSdk.AppConnectStatus.Connected);
    }

    private void HandleWalletShow()
    {
        _walletShowCallCount++;

        UpdateStatusText();
    }

    private void HandleWalletDismiss()
    {
        _walletDismissCallCount++;

        UpdateStatusText();
    }

    private void UpdateStatusText()
    {
        var isConnected = _appConnectState.Status == OVSdk.AppConnectStatus.Connected;
        var statusText = "";

        if (isConnected)
        {
            statusText = "Wallet: " + _appConnectState.WalletAddress;
        }
        else
        {
            statusText = "Error: " + _appConnectState.Status;
        }

        statusText += "\n";
        statusText += "(" + OVSdk.Sdk.Environment + ")";
        statusText += "\n";
        statusText += $"Wallet Show/Dismiss: {_walletShowCallCount}/{_walletDismissCallCount}";

        _statusText.text = statusText;
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

        _checkWalletAppInstallButton.interactable = true;
    }

    private void UpdateSetOrUnsetCustomPresenterButtonText()
    {
        string textPrefix;

        if (OVSdk.CustomPresenter.LoadBalancePresenter == null)
        {
            textPrefix = "Set";
        }
        else
        {
            textPrefix = "Unset";
        }

        _setOrUnsetCustomPresenterButtonText.text = textPrefix + " Custom Presenter";
    }

}