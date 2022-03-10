namespace OVSdk
{
    /// <summary>
    /// Reasons on why connection to the wallet may have failed
    /// </summary>
    public enum AppConnectStatus
    {
        /// <summary>
        /// SDK is not initialized
        /// </summary>
        NotInitialized,
        
        /// <summary>
        /// The wallet is disconnected
        /// </summary>
        Disconnected,
        
        /// <summary>
        /// The wallet is connected
        /// </summary>
        Connected,
        
        /// <summary>
        /// User canceled app connect operation
        /// </summary>
        ErrorCanceled,
        
        /// <summary>
        /// A user declined an app connect operation or the operation is expired
        /// </summary>
        ErrorDeclined,
        
        /// <summary>
        /// Unable to connect wallet application: it was not installed. OVL OVSdk has opened app store
        /// </summary>
        ErrorWalletNotInstalled,
        
        /// <summary>
        /// An non-recoverable error has occured
        /// </summary>
        Error,
    }
}