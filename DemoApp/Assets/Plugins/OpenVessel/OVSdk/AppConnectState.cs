using System;
using UnityEngine;

namespace OVSdk
{
    public class AppConnectState
    {
        public AppConnectStatus Status { get; }

        public string WalletAddress { get; }

        internal AppConnectState(AppConnectStatus status, string walletAddress)
        {
            Status = status;
            WalletAddress = walletAddress;
        }
    }
}