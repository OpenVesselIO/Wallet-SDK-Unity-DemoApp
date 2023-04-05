using System;
using OVSdk.Utils;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{

    public class EarningsManagerCallbacks : MonoBehaviour
    {

        [Serializable]
        private class AuthCodeMetadataJson
        {

            [SerializeField] internal string phoneNumber;
            [SerializeField] internal Int64 createdAt;
            [SerializeField] internal Int64 expiresAt;
            [SerializeField] internal Int64 ttl;

        }

        public class AuthCodeMetadata
        {

            internal static void Init()
            {
                _newAuthCodeMetadata = value => new AuthCodeMetadata(value);
            }

            public string PhoneNumber { get; }
            public Int64 CreatedAt { get; }
            public Int64 ExpiresAt { get; }
            public Int64 Ttl { get; }

            private AuthCodeMetadata(AuthCodeMetadataJson json)
            {
                PhoneNumber = json.phoneNumber;
                CreatedAt = json.createdAt;
                ExpiresAt = json.expiresAt;
                Ttl = json.ttl;
            }

        }

        private static Func<AuthCodeMetadataJson, AuthCodeMetadata> _newAuthCodeMetadata;

        public static EarningsManagerCallbacks Instance { get; private set; }

        private static Action<AuthCodeMetadata> _onAuthCodeMetadataEvent;
        private static Action<string> _onAuthFailure;

        public static event Action<AuthCodeMetadata> OnAuthCodeMetadata
        {
            add
            {
                Logger.LogSubscribedToEvent("OnAuthCodeMetadata");
                _onAuthCodeMetadataEvent += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnAuthCodeMetadata");
                _onAuthCodeMetadataEvent -= value;
            }
        }

        public static event Action<string> OnAuthFailure
        {
            add
            {
                Logger.LogSubscribedToEvent("OnAuthFailure");
                _onAuthFailure += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnAuthFailure");
                _onAuthFailure -= value;
            }
        }

        public void ForwardOnAuthCodeMetadataEvent(string json)
        {
            var eventJson = JsonUtility.FromJson<AuthCodeMetadataJson>(json);
            var meta = _newAuthCodeMetadata(eventJson);

            EventInvoker.InvokeEvent(_onAuthCodeMetadataEvent, meta);
        }

        public void ForwardOnAuthFailure(string description)
        {
            EventInvoker.InvokeEvent(_onAuthFailure, description);
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                AuthCodeMetadata.Init();
            }
        }
    }

}