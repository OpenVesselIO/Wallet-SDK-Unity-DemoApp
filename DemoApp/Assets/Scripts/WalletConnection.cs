using UnityEngine;
using UnityEngine.UI;

public class WalletConnection : MonoBehaviour
{
    private const string USER_ID = "<Unique User ID of your application>";

    private Button _connectButton;
    private Button _disconnectButton;
    private Text _statusText;

    void Start()
    {
        _connectButton = GameObject.Find("BtnConnect").GetComponent<Button>();
        _connectButton.onClick.AddListener(ConnectWallet);
        _connectButton.enabled = false;

        _disconnectButton = GameObject.Find("BtnDisconnect").GetComponent<Button>();
        _disconnectButton.onClick.AddListener(DisconnectWallet);
        _disconnectButton.enabled = false;


        _statusText = GameObject.Find("TxtStatus").GetComponent<Text>();
        _statusText.text = "Starting...";


        OVSdk.AppConnectManagerCallbacks.OnStateUpdated += RenderAppConnectState;

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

    private void RenderAppConnectState(OVSdk.AppConnectState state)
    {
        Debug.Log("Got new wallet state: " + state);

        var isConnected = state.Status == OVSdk.AppConnectStatus.Connected;
        if (isConnected)
        {
            _statusText.text = "Wallet: " + state.WalletAddress;
        }
        else
        {
            _statusText.text = "Error: " + state.Status;
        }

        _connectButton.enabled = !isConnected;
        _disconnectButton.enabled = isConnected;
    }
}