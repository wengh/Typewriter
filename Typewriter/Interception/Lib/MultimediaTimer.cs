using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HighPrecisionTimer
{
    /// <summary>
    /// A timer based on the multimedia timer API with 1ms precision.
    /// <a href="https://github.com/mzboray/HighPrecisionTimer">HighPrecisionTimer</a>
    /// </summary>
    public static class MultimediaTimer
    {
        private const int EventTypeSingle = 0;
        private static readonly Task TaskDone = Task.FromResult<object>(null);

        public static Task Delay(int millisecondsDelay, CancellationToken token = default(CancellationToken))
        {
            if (millisecondsDelay < 0)
            {
                throw new ArgumentOutOfRangeException("millisecondsDelay", millisecondsDelay, "The value cannot be less than 0.");
            }

            if (millisecondsDelay == 0)
            {
                return TaskDone;
            }

            token.ThrowIfCancellationRequested();

            // allocate an object to hold the callback in the async state.
            object[] state = new object[1];
            var completionSource = new TaskCompletionSource<object>(state);
            MultimediaTimerCallback callback = (uint id, uint msg, ref uint uCtx, uint rsv1, uint rsv2) =>
            {
                // Note we don't need to kill the timer for one-off events.
                completionSource.TrySetResult(null);
            };

            state[0] = callback;
            UInt32 userCtx = 0;
            var timerId = NativeMethods.TimeSetEvent((uint)millisecondsDelay, (uint)0, callback, ref userCtx, EventTypeSingle);
            if (timerId == 0)
            {
                int error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error);
            }

            return completionSource.Task;
        }
    }

    internal delegate void MultimediaTimerCallback(UInt32 id, UInt32 msg, ref UInt32 userCtx, UInt32 rsv1, UInt32 rsv2);

    internal static class NativeMethods
    {
        [DllImport("winmm.dll", SetLastError = true, EntryPoint = "timeSetEvent")]
        internal static extern UInt32 TimeSetEvent(UInt32 msDelay, UInt32 msResolution, MultimediaTimerCallback callback, ref UInt32 userCtx, UInt32 eventType);
    }
}
