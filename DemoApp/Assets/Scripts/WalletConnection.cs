using OVSdk;
using UnityEngine;
using UnityEngine.UI;

public class WalletConnection : MonoBehaviour
{
    private const string USER_ID = "<Unique User ID of your application>";

    private Button _connectButton;
    private Button _disconnectButton;
    private Button _showWalletButton;
    private Button _openWalletAppButton;
    private Text _statusText;

    void Start()
    {
        Debug.Log("Starting...");

        _connectButton = GameObject.Find("BtnConnect").GetComponent<Button>();
        _connectButton.onClick.AddListener(ConnectWallet);
        _connectButton.enabled = false;

        _disconnectButton = GameObject.Find("BtnDisconnect").GetComponent<Button>();
        _disconnectButton.onClick.AddListener(DisconnectWallet);
        _disconnectButton.enabled = false;

        _showWalletButton = GameObject.Find("BtnShowWallet").GetComponent<Button>();
        _showWalletButton.onClick.AddListener(ShowWallet);
        _showWalletButton.enabled = false;

        _openWalletAppButton = GameObject.Find("BtnOpenWalletApp").GetComponent<Button>();
        _openWalletAppButton.onClick.AddListener(OpenWalletApplication);


        _statusText = GameObject.Find("TxtStatus").GetComponent<Text>();
        _statusText.text = "Starting...";


        OVSdk.Sdk.Environment = VesselEnvironment.Staging;
        OVSdk.AppConnectManagerCallbacks.OnStateUpdated += RenderAppConnectState;

        OVSdk.Sdk.Configuration = new SdkConfiguration
        {
            MinLogLevel = SdkLogLevel.Debug,
            CallbackUrl = "vesseldemo://connect"
        };

        Debug.Log("Initializing the SDK...");
        OVSdk.Sdk.Initialize(USER_ID);
    }

    private void ConnectWallet()
    {
        Debug.Log("Connecting wallet...");
        OVSdk.Sdk.AppConnectManager.LoadConnectWalletView(USER_ID);
    }

    private void DisconnectWallet()
    {
        Debug.Log("Disconnecting wallet...");
        OVSdk.Sdk.AppConnectManager.DisconnectCurrentSession();
    }

    private void ShowWallet()
    {
        Debug.Log("Showing wallet inside of the current application...");
        OVSdk.Sdk.WalletPresenter.ShowWallet();
    }

    private void OpenWalletApplication()
    {
        Debug.Log("Opening wallet application...");
        OVSdk.Sdk.WalletPresenter.OpenWalletApplication();
    }

    private void RenderAppConnectState(OVSdk.AppConnectState state)
    {
        Debug.Log("Got new wallet state: " + state);

        var isConnected = state.Status == OVSdk.AppConnectStatus.Connected;
        if (isConnected)
        {
            _statusText.text = "Wallet: " + state.WalletAddress + "\n(" + OVSdk.Sdk.Environment + ")";
        }
        else
        {
            _statusText.text = "Error: " + state.Status + "\n(" + OVSdk.Sdk.Environment + ")";
        }

        _connectButton.enabled = !isConnected;
        _disconnectButton.enabled = isConnected;
        _showWalletButton.enabled = isConnected;
    }
}