using System;
using OVSdk.Utils;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{

    public class IapManagerCallbacks : MonoBehaviour
    {

        [Serializable]
        private class SuccessfulPurchaseJson
        {

            [SerializeField] public string productId;
            [SerializeField] public string receipt;
            [SerializeField] public string receiptSignature;

        }

        [Serializable]
        private class PurchaseErrorJson
        {

            [SerializeField] public string message;
            [SerializeField] public string detailedMessage;

        }

        [Serializable]
        private class FailedPurchaseJson
        {

            [SerializeField] public string productId;
            [SerializeField] public PurchaseErrorJson error;

        }

        [Serializable]
        private class CancelledPurchaseJson
        {

            [SerializeField] public string productId;

        }

        public static IapManagerCallbacks Instance { get; private set; }

        private static Action<SuccessfulPurchase> _onPurchaseSuccessEvent;
        private static Action<CancelledPurchase> _onPurchaseCancelEvent;
        private static Action<FailedPurchase> _onPurchaseFailureEvent;

        public static event Action<SuccessfulPurchase> OnPurchaseSuccess
        {
            add
            {
                Logger.LogSubscribedToEvent("OnPurchaseSuccess");
                _onPurchaseSuccessEvent += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnPurchaseSuccess");
                _onPurchaseSuccessEvent -= value;
            }
        }

        public static event Action<CancelledPurchase> OnPurchaseCancel
        {
            add
            {
                Logger.LogSubscribedToEvent("OnPurchaseCancel");
                _onPurchaseCancelEvent += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnPurchaseCancel");
                _onPurchaseCancelEvent -= value;
            }
        }

        public static event Action<FailedPurchase> OnPurchaseFailure
        {
            add
            {
                Logger.LogSubscribedToEvent("OnPurchaseFailure");
                _onPurchaseFailureEvent += value;
            }
            remove
            {
                Logger.LogUnsubscribedToEvent("OnPurchaseFailure");
                _onPurchaseFailureEvent -= value;
            }
        }

        public void ForwardOnPurchaseSuccessEvent(string json)
        {
            var eventJson = JsonUtility.FromJson<SuccessfulPurchaseJson>(json);

            var info = new SuccessfulPurchase(eventJson.productId, eventJson.receipt, eventJson.receiptSignature);

            EventInvoker.InvokeEvent(_onPurchaseSuccessEvent, info);
        }

        public void ForwardOnPurchaseCancelEvent(string json)
        {
            var eventJson = JsonUtility.FromJson<CancelledPurchaseJson>(json);

            var info = new CancelledPurchase(eventJson.productId);

            EventInvoker.InvokeEvent(_onPurchaseCancelEvent, info);
        }

        public void ForwardOnPurchaseFailureEvent(string json)
        {
            var eventJson = JsonUtility.FromJson<FailedPurchaseJson>(json);

            var info = new FailedPurchase(
                eventJson.productId,
                new PurchaseError(eventJson.error.message, eventJson.error.detailedMessage)
            );

            EventInvoker.InvokeEvent(_onPurchaseFailureEvent, info);
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