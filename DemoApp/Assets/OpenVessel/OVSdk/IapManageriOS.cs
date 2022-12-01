using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

namespace OVSdk
{
#if UNITY_IOS
    public class IapManageriOs : IapManagerBase
    {
        
        [DllImport("__Internal")]
        private static extern void _OVPurchaseProduct(string productId);

        /// <summary>
        /// Purchase an In-App product.
        /// Use <c>OnPurchaseSuccess</c>, <c>OnPurchaseCancel</c> and <c>OnPurchaseFailure</c> to listen for a purchase result.
        /// </summary>
        /// <param name="productId">
        /// A product id from a store.
        /// </param>
        public void PurchaseProduct(string productId)
        {
            _OVPurchaseProduct(productId);
        }

    }
#endif
}