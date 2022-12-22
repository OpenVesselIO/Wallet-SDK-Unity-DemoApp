using System;
using System.Runtime.InteropServices;
using OVSdk.Utils;

namespace OVSdk
{
#if UNITY_IOS

    public class CustomPresenteriOs : CustomPresenterBase
    {

        [DllImport("__Internal")]
        private static extern void _OVSetIsLoadBalancePresenterProvided(bool flag);

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

                _OVSetIsLoadBalancePresenterProvided(value != null);
            }
        }

        private void ShowLoadBalance(string msg)
        {
            EventInvoker.InvokeEvent(mLoadBalancePresenter);
        }

    }

#endif
}

