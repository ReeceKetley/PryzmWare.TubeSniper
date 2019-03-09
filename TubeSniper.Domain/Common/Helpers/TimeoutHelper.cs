using System;
using System.Threading;

namespace TubeSniper.Domain.Common.Helpers
{
    public static class TimeoutHelper
    {
        public static WaitCode Wait(Func<bool> callback, TimeSpan timeout)
        {
            var endTime = DateTime.Now + timeout;
            for (;; Thread.Sleep(250))
            {
                if (callback())
                {
                    return WaitCode.Success;
                }

                if (DateTime.Now > endTime)
                {
                    return WaitCode.Timeout;
                }
            }
        }
    }
}
