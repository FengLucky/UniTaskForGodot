#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Threading;
using Godot;

namespace Cysharp.Threading.Tasks
{
    // UniTask has no scheduler like TaskScheduler.
    // Only handle unobserved exception.

    public static class UniTaskScheduler
    {
        public enum LogType
        {
            Log,
            Warning,
            Error,
            Assert,
            Exception
        }
        
        public static event Action<Exception> UnobservedTaskException;

        /// <summary>
        /// Propagate OperationCanceledException to UnobservedTaskException when true. Default is false.
        /// </summary>
        public static bool PropagateOperationCanceledException = false;
        
        /// <summary>
        /// Write log type when catch unobserved exception and not registered UnobservedTaskException. Default is Exception.
        /// </summary>
        public static LogType UnobservedExceptionWriteLogType = LogType.Exception;

        /// <summary>
        /// Dispatch exception event to Godot MainThread. Default is true.
        /// </summary>
        public static bool DispatchGodotMainThread = true;
        
        // cache delegate.
        static readonly SendOrPostCallback handleExceptionInvoke = InvokeUnobservedTaskException;

        static void InvokeUnobservedTaskException(object state)
        {
            UnobservedTaskException((Exception)state);
        }

        internal static void PublishUnobservedTaskException(Exception ex)
        {
            if (ex != null)
            {
                if (!PropagateOperationCanceledException && ex is OperationCanceledException)
                {
                    return;
                }

                if (UnobservedTaskException != null)
                {
                    if (!DispatchGodotMainThread || Thread.CurrentThread.ManagedThreadId == PlayerLoopHelper.MainThreadId)
                    {
                        // allows inlining call.
                        UnobservedTaskException.Invoke(ex);
                    }
                    else
                    {
                        // Post to MainThread.
                        PlayerLoopHelper.GodotSynchronizationContext.Post(handleExceptionInvoke,ex);
                    }
                }
                else
                {
                    string msg = null;
                    if (UnobservedExceptionWriteLogType != LogType.Exception)
                    {
                        msg = "UnobservedTaskException: " + ex.ToString();
                    }
                    switch (UnobservedExceptionWriteLogType)
                    {
                        case LogType.Error:
                            GD.PushError(msg);
                            break;
                        case LogType.Assert:
                            GD.PushError(msg);
                            break;
                        case LogType.Warning:
                            GD.PushWarning(msg);
                            break;
                        case LogType.Log:
                            GD.Print(msg);
                            break;
                        case LogType.Exception:
                            GD.PushError(ex);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}