#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Threading;
using System;
using Godot;

namespace Cysharp.Threading.Tasks
{

    public static partial class CancellationTokenSourceExtensions
    {
        static readonly Action<object> CancelCancellationTokenSourceStateDelegate = CancelCancellationTokenSourceState;

        static void CancelCancellationTokenSourceState(object state)
        {
            var cts = (CancellationTokenSource)state;
            cts.Cancel();
        }

        public static IDisposable CancelAfterSlim(this CancellationTokenSource cts, int millisecondsDelay, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process)
        {
            return CancelAfterSlim(cts, TimeSpan.FromMilliseconds(millisecondsDelay), delayType, delayTiming);
        }

        public static IDisposable CancelAfterSlim(this CancellationTokenSource cts, TimeSpan delayTimeSpan, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process)
        {
            return PlayerLoopTimer.StartNew(delayTimeSpan, false, delayType, delayTiming, cts.Token, CancelCancellationTokenSourceStateDelegate, cts);
        }

        public static void RegisterRaiseCancelOnExitTree(this CancellationTokenSource cts, Node node)
        {
            node.GetCancellationTokenOnExitTree().RegisterWithoutCaptureExecutionContext(CancelCancellationTokenSourceStateDelegate, cts);
        }
    }
}