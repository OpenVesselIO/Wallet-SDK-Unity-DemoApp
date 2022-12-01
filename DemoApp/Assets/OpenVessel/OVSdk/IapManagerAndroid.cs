using System;
using UnityEngine;

namespace OVSdk
{
#if UNITY_ANDROID
    public class IapManagerAndroid : IapManagerBase
    {

        private static readonly AndroidJavaClass OvSdkPluginClass =
            new AndroidJavaClass("io.openvessel.sdk.unity.IapManagerPlugin");

        /// <summary>
        /// Purchase an In-App product.
        /// Use <c>OnPurchaseSuccess</c>, <c>OnPurchaseCancel</c> and <c>OnPurchaseFailure</c> to listen for a purchase result.
        /// </summary>
        /// <param name="productId">
        /// A product id from a store.
        /// </param>
        public void PurchaseProduct(string productId)
        {
            OvSdkPluginClass.CallStatic("purchaseProduct", productId);
        }
        
    }
#endif
}