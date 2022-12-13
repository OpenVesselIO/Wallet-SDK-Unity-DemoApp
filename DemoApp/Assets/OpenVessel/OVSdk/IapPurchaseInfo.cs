using System;
using UnityEngine;

namespace OVSdk
{

    public class SuccessfulPurchase
    {

        public string ProductId { get; }
        public string Receipt { get; }
        public string ReceiptSignature { get; }

        internal SuccessfulPurchase(string productId, string receipt, string receiptSignature)
        {
            ProductId = productId;
            Receipt = receipt;
            ReceiptSignature = receiptSignature;
        }
    }

    public class PurchaseError
    {

        public string Message { get; }
        public string DetailedMessage { get; }

        internal PurchaseError(string message, string detailedMessage)
        {
            Message = message;
            DetailedMessage = detailedMessage;
        }
    }

    public class FailedPurchase
    {

        public string ProductId { get; }
        public PurchaseError Error { get; }

        internal FailedPurchase(string productId, PurchaseError error)
        {
            ProductId = productId;
            Error = error;
        }
    }

    public class CancelledPurchase
    {

        public string ProductId { get; }

        internal CancelledPurchase(string productId)
        {
            ProductId = productId;
        }

    }

}

