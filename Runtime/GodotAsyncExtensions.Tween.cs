using System;
using System.Threading;
using Godot;

namespace Cysharp.Threading.Tasks;

public static partial class GodotAsyncExtensions
{
    /// <param name="loopCount">valid only in loop mode</param>
    public static AsyncGodotTweenHandler GetAsyncTweenHandler(this Tween tween, CancellationToken cancellationToken = default,long loopCount = 1)
    {
        return new AsyncGodotTweenHandler(tween, cancellationToken, false,loopCount);
    }
    
    /// <param name="loopCount">valid only in loop mode</param>
    public static UniTask WaitFinished(this Tween tween, CancellationToken cancellationToken = default,long loopCount = 1)
    {
        return new AsyncGodotTweenHandler(tween, cancellationToken, true,loopCount).OnInvokeAsync();
    }
}

public class AsyncGodotTweenHandler : IUniTaskSource, IDisposable
{
    static Action<object> cancellationCallback = CancellationCallback;

    readonly Tween tween;

    CancellationToken cancellationToken;
    CancellationTokenRegistration registration;
    bool isDisposed;
    bool callOnce;
    long loopCount;

    UniTaskCompletionSourceCore<AsyncUnit> core;

    public AsyncGodotTweenHandler(Tween tween, CancellationToken cancellationToken, bool callOnce,long loopCount = 1)
    {
        this.cancellationToken = cancellationToken;
        if (cancellationToken.IsCancellationRequested)
        {
            isDisposed = true;
            return;
        }

        this.tween = tween;
        this.callOnce = callOnce;
        this.loopCount = loopCount;

        tween.Finished += Invoke;
        tween.LoopFinished += LoopInvoke;
        if (cancellationToken.CanBeCanceled)
        {
            registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
        }

        TaskTracker.TrackActiveTask(this, 3);
    }

    public UniTask OnInvokeAsync()
    {
        core.Reset();
        if (isDisposed)
        {
            core.TrySetCanceled(this.cancellationToken);
        }

        return new UniTask(this, core.Version);
    }

    void Invoke()
    {
        core.TrySetResult(AsyncUnit.Default);
    }

    void LoopInvoke(long loopCount)
    {
        if (loopCount >= this.loopCount)
        {
            core.TrySetResult(AsyncUnit.Default);
        }
    }

    static void CancellationCallback(object state)
    {
        var self = (AsyncGodotTweenHandler)state;
        self.Dispose();
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
            TaskTracker.RemoveTracking(this);
            registration.Dispose();
            if (tween != null && GodotObject.IsInstanceValid(tween))
            {
                tween.Finished -= Invoke;
                tween.LoopFinished -= LoopInvoke;
            }
            core.TrySetCanceled(cancellationToken);
        }
    }

    void IUniTaskSource.GetResult(short token)
    {
        try
        {
            core.GetResult(token);
        }
        finally
        {
            if (callOnce)
            {
                Dispose();
            }
        }
    }

    UniTaskStatus IUniTaskSource.GetStatus(short token)
    {
        return core.GetStatus(token);
    }

    UniTaskStatus IUniTaskSource.UnsafeGetStatus()
    {
        return core.UnsafeGetStatus();
    }

    void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
    {
        core.OnCompleted(continuation, state, token);
    }
}