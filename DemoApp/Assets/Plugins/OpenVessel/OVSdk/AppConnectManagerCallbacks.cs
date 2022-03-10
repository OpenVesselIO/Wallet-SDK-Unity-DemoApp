using System;
using OVSdk.Utils;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    [Serializable]
    public class AppConnectStateJson
    {
        [SerializeField] public string status;

        [SerializeField] public string walletAddress;
    }

    public class AppConnectManagerCallbacks : MonoBehaviour
    {
        public static AppConnectManagerCallbacks Instance { get; private set; }

        private static Action<AppConnectState> _onStateUpdatedEvent;

        public AppConnectState State { get; private set; } = new AppConnectState(AppConnectStatus.NotInitialized, null);

        public static event Action<AppConnectState> OnStateUpdated
        {
            add
            {
                Logger.LogSubscribedToEvent("OnStateUpdated");
                _onStateUpdatedEvent += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnStateUpdated");
                _onStateUpdatedEvent -= value;
            }
        }

        public void ForwardOnStateUpdatedEvent(string connectResultJson)
        {
            var eventJson = JsonUtility.FromJson<AppConnectStateJson>(connectResultJson);

            var statusParsed = Enum.TryParse(eventJson.status, out AppConnectStatus parsedStatus);
            if (!statusParsed)
            {
                Logger.E("Failed to parse app connect status '" + eventJson.status + "'");
                parsedStatus = AppConnectStatus.Error;
            }

            State = new AppConnectState(parsedStatus, eventJson.walletAddress);
            EventInvoker.InvokeEvent(_onStateUpdatedEvent, State);
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