using System;
using OVSdk.Utils;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class WalletPresenterCallbacks : MonoBehaviour
    {
        public static WalletPresenterCallbacks Instance { get; private set; }

        private static Action _onWalletShow;
        private static Action _onWalletDismiss;

        public static event Action OnWalletShow
        {
            add
            {
                Logger.LogSubscribedToEvent("OnWalletShow");
                _onWalletShow += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnWalletShow");
                _onWalletShow -= value;
            }
        }

        public static event Action OnWalletDismiss
        {
            add
            {
                Logger.LogSubscribedToEvent("OnWalletDismiss");
                _onWalletDismiss += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnWalletDismiss");
                _onWalletDismiss -= value;
            }
        }

        public void ForwardOnWalletShowEvent(string msg)
        {
            EventInvoker.InvokeEvent(_onWalletShow);
        }

        public void ForwardOnWalletDismissEvent(string msg)
        {
            EventInvoker.InvokeEvent(_onWalletDismiss);
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}