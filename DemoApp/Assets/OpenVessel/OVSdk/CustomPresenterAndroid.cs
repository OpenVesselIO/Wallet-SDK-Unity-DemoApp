using System;
using System.Runtime.InteropServices;
using OVSdk.Utils;
using UnityEngine;

namespace OVSdk
{
#if UNITY_ANDROID

    public class CustomPresenterAndroid : CustomPresenterBase
    {

        private static readonly AndroidJavaClass OvCustomPresenterPluginClass =
            new AndroidJavaClass("io.openvessel.sdk.unity.CustomPresenterPlugin");

        private static Action mLoadBalancePresenter;

        /// <summary>
        /// Show UI for load balance.
        /// </summary>
        public static Action LoadBalancePresenter
        {
            get
            {
                return mLoadBalancePresenter;
            }

            set
            {
                mLoadBalancePresenter = value;

                OvCustomPresenterPluginClass.CallStatic("setIsLoadBalancePresenterProvided", value != null);
            }
        }

        private void ShowLoadBalance(string msg)
        {
            EventInvoker.InvokeEvent(mLoadBalancePresenter);
        }

    }

#endif
}

