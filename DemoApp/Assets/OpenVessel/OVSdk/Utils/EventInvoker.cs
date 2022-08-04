using System;

namespace OVSdk.Utils
{
    public static class EventInvoker
    {
        public static void InvokeEvent(Action evt)
        {
            if (!CanInvokeEvent(evt)) return;

            Logger.UserDebug("Invoking event: " + evt);
            evt();
        }

        public static void InvokeEvent<T>(Action<T> evt, T param)
        {
            if (!CanInvokeEvent(evt)) return;

            Logger.UserDebug("Invoking event: " + evt + ". Param: " + param);
            evt(param);
        }

        public static void InvokeEvent<T1, T2>(Action<T1, T2> evt, T1 param1, T2 param2)
        {
            if (!CanInvokeEvent(evt)) return;

            Logger.UserDebug("Invoking event: " + evt + ". Params: " + param1 + ", " + param2);
            evt(param1, param2);
        }

        public static void InvokeEvent<T1, T2, T3>(Action<T1, T2, T3> evt, T1 param1, T2 param2, T3 param3)
        {
            if (!CanInvokeEvent(evt)) return;

            Logger.UserDebug("Invoking event: " + evt + ". Params: " + param1 + ", " + param2 + ", " + param3);
            evt(param1, param2, param3);
        }

        private static bool CanInvokeEvent(Delegate evt)
        {
            if (evt == null) return false;

            // Check that publisher is not over-subscribing
            if (evt.GetInvocationList().Length > 5)
            {
                Logger.UserWarning("Ads Event (" + evt +
                                   ") has over 5 subscribers. Please make sure you are properly un-subscribing to actions!!!");
            }

            return true;
        }
    }
}