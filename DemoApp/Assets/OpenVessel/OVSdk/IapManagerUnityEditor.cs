using System;
using System.Collections;
using UnityEngine;
using Logger = OVSdk.Utils.Logger;

namespace OVSdk
{
    public class IapManagerUnityEditor : IapManagerBase
    {

        /// <summary>
        /// Purchase an In-App product.
        /// Use <c>OnPurchaseSuccess</c>, <c>OnPurchaseCancel</c> and <c>OnPurchaseFailure</c> to listen for a purchase result.
        /// </summary>
        /// <param name="productId">
        /// A product id from a store.
        /// </param>
        public void PurchaseProduct(string productId)
        {
            ExecuteWithDelay(3f,
                () =>
                {
                    IapManagerCallbacks.Instance.ForwardOnPurchaseSuccessEvent(
                        $"{{\"productId\": \"{productId}\", \"receipt\": \"dGVzdCByZWNlaXB0\"}}"
                    );
                });
        }

        private static void ExecuteWithDelay(float seconds, Action action)
        {
            IapManagerCallbacks.Instance.StartCoroutine(ExecuteAction(seconds, action));
        }

        private static IEnumerator ExecuteAction(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);

            action();
        }
    }
}