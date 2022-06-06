using System;
using UnityEngine;

namespace OVSdk
{
    public class AppConnectState
    {
        public AppConnectStatus Status { get; }

        public string WalletAddress { get; }
        
        public string AccessToken{ get; }

        internal AppConnectState(AppConnectStatus status, string walletAddress, string accessToken)
        {
            Status = status;
            WalletAddress = walletAddress;
            AccessToken= accessToken;
        }
    }
}