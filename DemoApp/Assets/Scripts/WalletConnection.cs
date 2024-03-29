﻿using System;
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
    public Button _showEarningsWithStaticPromoButton;
    public Button _showEarningsWithVideoPromoButton;

    public Text _setOrUnsetCustomPresenterButtonText;

    public Button _trackRandomRevenuedAdButton;

    public InputField _earningsImpressionTriggerNameInputField;
    public Button _trackEarningsImpressionButton;

    public InputField _earningsAuthPhoneNumberInputField;
    public Button _earningsAuthGeneratePhoneCodeButton;

    public InputField _earningsAuthPhoneCodeInputField;
    public Button _earningsAuthLoginByPhoneCodeButton;

    public InputField _earningsVerificationEmailInputField;
    public Button _earningsVerificationGenerateEmailCodeButton;

    public InputField _earningsVerificationEmailCodeInputField;
    public Button _earningsVerifyEmailButton;

    private int _walletShowCallCount;
    private int _walletDismissCallCount;

    private EarningsManagerCallbacks.AuthCodeMetadata _earningsAuthCodeMetadata;
    private EarningsManagerCallbacks.AuthCodeMetadata _earningsVerificationCodeMetadata;

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
        OVSdk.EarningsManagerCallbacks.OnAuthCodeMetadata += HandleEarningsAuthCodeMetadata;
        OVSdk.EarningsManagerCallbacks.OnVerificationCodeMetadata += HandleEarningsVerificationCodeMetadata;
        OVSdk.EarningsManagerCallbacks.OnAuthFailure += HandleEarningsAuthFailure;
        OVSdk.EarningsManagerCallbacks.OnVerificationFailure += HandleEarningsVerificationFailure;
        OVSdk.EarningsManagerCallbacks.OnVerificationSuccess += HandleEarningsVerificationSuccess;

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

    public void ShowEarningsWithStaticPromo()
    {
        Debug.Log("Showing earnings with static promo inside of the current application...");

        OVSdk.Sdk.EarningsManager.ShowEarnings(USER_ID);
    }

    public void ShowEarningsWithVideoPromo()
    {
        Debug.Log("Showing earnings with video promo inside of the current application...");

        var settings = new EarningsPresentationSettings(USER_ID);
        settings.PromoType = EarningsPromoType.Video;
        settings.TriggerName = "show_earnings_with_video_promo_button";

        OVSdk.Sdk.EarningsManager.ShowEarnings(settings);
    }

    public void TrackRandomRevenuedAd()
    {
        Debug.Log("Tracking random revenued ad...");

        var values = (AdType[]) Enum.GetValues(typeof(AdType));
        var adType = values[new System.Random().Next(values.Length)];

        OVSdk.Sdk.EarningsManager.TrackRevenuedAd(adType);

        PopupUtils.ShowPopup(adType.ToString());
    }

    public void TrackEarningsImpression()
    {
#if UNITY_IOS
        OVSdk.Sdk.EarningsManager.TrackImpression(_earningsImpressionTriggerNameInputField.text);
#endif
    }

    public void GenerateEarningsPhoneAuthCode()
    {
#if UNITY_IOS
        OVSdk.Sdk.EarningsManager.GenerateAuthCodeForPhoneNumber(_earningsAuthPhoneNumberInputField.text);
#endif
    }

    public void LoginEarningsByPhoneAuthCode()
    {
#if UNITY_IOS
        OVSdk.Sdk.EarningsManager.LoginByPhoneAuthCode(
            _earningsAuthCodeMetadata.PhoneNumber,
            _earningsAuthPhoneCodeInputField.text,
            _earningsAuthCodeMetadata.CreatedAt,
            USER_ID
        );
#endif
    }

    public void GenerateEarningsEmailVerificationCode()
    {
#if UNITY_IOS
        OVSdk.Sdk.EarningsManager.GenerateVerificationCodeForEmail(_earningsVerificationEmailInputField.text);
#endif
    }

    public void VerifyEarningsEmail()
    {
#if UNITY_IOS
        OVSdk.Sdk.EarningsManager.VerifyEmail(
            _earningsVerificationCodeMetadata.Email,
            _earningsVerificationEmailCodeInputField.text,
            _earningsVerificationCodeMetadata.CreatedAt
        );
#endif
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

    private void HandleEarningsAuthCodeMetadata(EarningsManagerCallbacks.AuthCodeMetadata codeMetadata)
    {
        _earningsAuthCodeMetadata = codeMetadata;

        _earningsAuthPhoneNumberInputField.text = null;
    }

    private void HandleEarningsVerificationCodeMetadata(EarningsManagerCallbacks.AuthCodeMetadata codeMetadata)
    {
        _earningsVerificationCodeMetadata = codeMetadata;

        _earningsVerificationEmailInputField.text = null;
    }

    private void HandleEarningsAuthFailure(string failure)
    {
        PopupUtils.ShowPopup(failure);
    }

    private void HandleEarningsVerificationFailure(string failure)
    {
        PopupUtils.ShowPopup(failure);
    }

    private void HandleEarningsVerificationSuccess()
    {
        PopupUtils.ShowPopup("Email is successfully verified!");

        _earningsVerificationEmailCodeInputField.text = null;
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
        _showEarningsWithStaticPromoButton.interactable = true;
        _showEarningsWithVideoPromoButton.interactable = true;
        _trackRandomRevenuedAdButton.interactable = true;
        _trackEarningsImpressionButton.interactable = true;
        _earningsAuthGeneratePhoneCodeButton.interactable = true;
        _earningsAuthLoginByPhoneCodeButton.interactable = true;

        if (isConnected)
        {
            _earningsAuthPhoneCodeInputField.text = null;
        }
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