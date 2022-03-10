using UnityEngine;

namespace OVSdk.Utils
{
    public static class Logger
    {
        private const string SdkTag = "OVSdk";
        public const string KeyVerboseLoggingEnabled = "io.openvessel.verbose_logging_enabled";

        /// <summary>
        /// Log debug messages.
        /// </summary>
        public static void UserDebug(string message)
        {
            Debug.Log("Debug [" + SdkTag + "] " + message);
        }

        /// <summary>
        /// Log debug messages when verbose logging is enabled.
        ///
        /// Verbose logging can be enabled by calling <see cref="SdkUnityEditor.SetVerboseLogging"/> or via the Integration Manager for build time logs.
        /// </summary>
        public static void D(string message)
        {
            Debug.Log("Debug [" + SdkTag + "] " + message);
        }

        /// <summary>
        /// Log warning messages.
        /// </summary>
        public static void UserWarning(string message)
        {
            Debug.LogWarning("Warning [" + SdkTag + "] " + message);
        }

        /// <summary>
        /// Log warning messages when verbose logging is enabled.
        ///
        /// Verbose logging can be enabled by calling <see cref="Sdk.SetVerboseLogging"/> or via the Integration Manager for build time logs.
        /// </summary>
        public static void W(string message)
        {
            Debug.LogWarning("Warning [" + SdkTag + "] " + message);
        }

        /// <summary>
        /// Log error messages.
        /// </summary>
        public static void UserError(string message)
        {
            Debug.LogError("Error [" + SdkTag + "] " + message);
        }
        
        public static void LogSubscribedToEvent(string eventName)
        {
            D("Listener has been added to callback: " + eventName);
        }

        public static void LogUnsubscribedToEvent(string eventName)
        {
            D("Listener has been removed from callback: " + eventName);
        }

        /// <summary>
        /// Log error messages when verbose logging is enabled.
        ///
        /// Verbose logging can be enabled by calling <see cref="Sdk.SetVerboseLogging"/> or via the Integration Manager for build time logs.
        /// </summary>
        public static void E(string message)
        {
            Debug.LogError("Error [" + SdkTag + "] " + message);
        }
    }
}