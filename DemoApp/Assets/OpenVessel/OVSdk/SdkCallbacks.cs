using System;
using OVSdk.Utils;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class SdkCallbacks : MonoBehaviour
    {
        public static SdkCallbacks Instance { get; private set; }

        private static Action _onSdkInitialized;


        public static event Action OnSdkInitialized
        {
            add
            {
                Logger.LogSubscribedToEvent("OnSdkInitialized");
                _onSdkInitialized += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnSdkInitialized");
                _onSdkInitialized -= value;
            }
        }

        public void ForwardOnSdkInitializedEvent(string msg)
        {
            EventInvoker.InvokeEvent(_onSdkInitialized);
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