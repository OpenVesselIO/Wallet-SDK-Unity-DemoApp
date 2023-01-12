using System;
using UnityEngine;

namespace OVSdk
{
    public class AppConnectState
    {
        public AppConnectStatus Status { get; }

        public string UserId { get; }

        public string WalletAddress { get; }
        
        public string AccessToken { get; }

        internal AppConnectState(AppConnectStatus status, string userId, string walletAddress, string accessToken)
        {
            Status = status;
            UserId = userId;
            WalletAddress = walletAddress;
            AccessToken = accessToken;
        }
    }
}