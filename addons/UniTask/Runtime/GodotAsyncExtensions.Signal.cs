using System;
using System.Threading;
using Godot;

namespace Cysharp.Threading.Tasks;

public static partial class GodotAsyncExtensions
{
    public static AsyncGodotSignalHandler GetAsyncSignalHandler(this GodotObject obj, StringName signalName,
        CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler(obj, signalName, cancellationToken, false);
    }
    
    public static UniTask OnInvokeAsync(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }

    public static IUniTaskAsyncEnumerable<AsyncUnit> OnInvokeAsAsyncEnumerable(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable(obj,signalName, cancellationToken);
    }

    public static AsyncGodotSignalHandler<T> GetAsyncEventHandler<[MustBeVariant]T>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T>(obj,signalName, cancellationToken, false);
    }
    public static AsyncGodotSignalHandler<T1,T2> GetAsyncEventHandler<[MustBeVariant]T1,[MustBeVariant]T2>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2>(obj,signalName, cancellationToken, false);
    }
    public static AsyncGodotSignalHandler<T1,T2,T3> GetAsyncEventHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3>(obj,signalName, cancellationToken, false);
    }
    public static AsyncGodotSignalHandler<T1,T2,T3,T4> GetAsyncEventHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4>(obj,signalName, cancellationToken, false);
    }
    public static AsyncGodotSignalHandler<T1,T2,T3,T4,T5> GetAsyncEventHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4,T5>(obj,signalName, cancellationToken, false);
    }
    public static AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6> GetAsyncEventHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6>(obj,signalName, cancellationToken, false);
    }
    public static AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6,T7> GetAsyncEventHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6,T7>(obj,signalName, cancellationToken, false);
    }
    public static AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6,T7,T8> GetAsyncEventHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7,[MustBeVariant]T8>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6,T7,T8>(obj,signalName, cancellationToken, false);
    }

    public static UniTask<T> OnInvokeAsync<[MustBeVariant]T>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T>(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }
    public static UniTask<(T1,T2)> OnInvokeAsync<[MustBeVariant]T1,[MustBeVariant]T2>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2>(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }
    public static UniTask<(T1,T2,T3)> OnInvokeAsync<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3>(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }
    public static UniTask<(T1,T2,T3,T4)> OnInvokeAsync<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4>(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }
    public static UniTask<(T1,T2,T3,T4,T5)> OnInvokeAsync<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4,T5>(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }
    public static UniTask<(T1,T2,T3,T4,T5,T6)> OnInvokeAsync<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6>(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }
    public static UniTask<(T1,T2,T3,T4,T5,T6,T7)> OnInvokeAsync<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6,T7>(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }
    public static UniTask<(T1,T2,T3,T4,T5,T6,T7,T8)> OnInvokeAsync<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7,[MustBeVariant]T8>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new AsyncGodotSignalHandler<T1,T2,T3,T4,T5,T6,T7,T8>(obj,signalName, cancellationToken, true).OnInvokeAsync();
    }

    public static IUniTaskAsyncEnumerable<T> OnInvokeAsAsyncEnumerable<[MustBeVariant]T>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable<T>(obj,signalName, cancellationToken);
    }
    public static IUniTaskAsyncEnumerable<(T1,T2)> OnInvokeAsAsyncEnumerable<[MustBeVariant]T1,[MustBeVariant]T2>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable<T1,T2>(obj,signalName, cancellationToken);
    }
    public static IUniTaskAsyncEnumerable<(T1,T2,T3)> OnInvokeAsAsyncEnumerable<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable<T1,T2,T3>(obj,signalName, cancellationToken);
    }
    public static IUniTaskAsyncEnumerable<(T1,T2,T3,T4)> OnInvokeAsAsyncEnumerable<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable<T1,T2,T3,T4>(obj,signalName, cancellationToken);
    }
    public static IUniTaskAsyncEnumerable<(T1,T2,T3,T4,T5)> OnInvokeAsAsyncEnumerable<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable<T1,T2,T3,T4,T5>(obj,signalName, cancellationToken);
    }
    public static IUniTaskAsyncEnumerable<(T1,T2,T3,T4,T5,T6)> OnInvokeAsAsyncEnumerable<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable<T1,T2,T3,T4,T5,T6>(obj,signalName, cancellationToken);
    }
    public static IUniTaskAsyncEnumerable<(T1,T2,T3,T4,T5,T6,T7)> OnInvokeAsAsyncEnumerable<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable<T1,T2,T3,T4,T5,T6,T7>(obj,signalName, cancellationToken);
    }
    public static IUniTaskAsyncEnumerable<(T1,T2,T3,T4,T5,T6,T7,T8)> OnInvokeAsAsyncEnumerable<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7,[MustBeVariant]T8>(this GodotObject obj, StringName signalName, CancellationToken cancellationToken)
    {
        return new GodotSignalHandlerAsyncEnumerable<T1,T2,T3,T4,T5,T6,T7,T8>(obj,signalName, cancellationToken);
    }
}

public class AsyncGodotSignalHandler : IUniTaskSource, IDisposable
{
    static Action<object> cancellationCallback = CancellationCallback;

    readonly GodotObject obj;
    readonly Callable callable;
    readonly StringName signalName;

    CancellationToken cancellationToken;
    CancellationTokenRegistration registration;
    bool isDisposed;
    bool callOnce;

    UniTaskCompletionSourceCore<AsyncUnit> core;

    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
    {
        this.cancellationToken = cancellationToken;
        if (cancellationToken.IsCancellationRequested)
        {
            isDisposed = true;
            return;
        }

        this.obj = obj;
        this.callable = Callable.From(Invoke);
        this.signalName = signalName;
        this.callOnce = callOnce;

        obj.Connect(signalName, callable);
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

    static void CancellationCallback(object state)
    {
        var self = (AsyncGodotSignalHandler)state;
        self.Dispose();
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
            TaskTracker.RemoveTracking(this);
            registration.Dispose();
            if (signalName != null && obj != null && GodotObject.IsInstanceValid(obj))
            {
                obj.Disconnect(signalName, callable);
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
public class AsyncGodotSignalGenericHandler<T> : IUniTaskSource<T>, IDisposable
{
    static Action<object> cancellationCallback = CancellationCallback;

    readonly GodotObject obj;
    readonly StringName signalName;
    
    Callable callable;
    CancellationToken cancellationToken;
    CancellationTokenRegistration registration;
    bool isDisposed;
    bool callOnce;
    protected UniTaskCompletionSourceCore<T> core;

    protected AsyncGodotSignalGenericHandler(GodotObject obj, StringName signalName,CancellationToken cancellationToken, bool callOnce)
    {
        this.cancellationToken = cancellationToken;
        if (cancellationToken.IsCancellationRequested)
        {
            isDisposed = true;
            return;
        }

        this.obj = obj;
        this.signalName = signalName;
        this.callOnce = callOnce;

        if (cancellationToken.CanBeCanceled)
        {
            registration = cancellationToken.RegisterWithoutCaptureExecutionContext(cancellationCallback, this);
        }

        TaskTracker.TrackActiveTask(this, 3);
    }

    protected void Connect(Callable callable)
    {
        this.callable = callable;
        obj.Connect(signalName, callable);
    }

    public UniTask<T> OnInvokeAsync()
    {
        core.Reset();
        if (isDisposed)
        {
            core.TrySetCanceled(this.cancellationToken);
        }

        return new UniTask<T>(this, core.Version);
    }

    static void CancellationCallback(object state)
    {
        var self = (AsyncGodotSignalGenericHandler<T>)state;
        self.Dispose();
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
            TaskTracker.RemoveTracking(this);
            registration.Dispose();
            if (signalName != null && obj != null && GodotObject.IsInstanceValid(obj))
            {
                obj.Disconnect(signalName,callable);
            }
    
            core.TrySetCanceled();
        }
    }
    
    T IUniTaskSource<T>.GetResult(short token)
    {
        try
        {
            return core.GetResult(token);
        }
        finally
        {
            if (callOnce)
            {
                Dispose();
            }
        }
    }

    void IUniTaskSource.GetResult(short token)
    {
        ((IUniTaskSource<T>)this).GetResult(token);
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
public class AsyncGodotSignalHandler<[MustBeVariant]T> : AsyncGodotSignalGenericHandler<T>
{
    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
    :base(obj,signalName,cancellationToken, callOnce)
    {
        Connect(Callable.From<T>(Invoke));
    }

    void Invoke(T result)
    {
        core.TrySetResult(result);
    }
}
public class AsyncGodotSignalHandler<[MustBeVariant]T1,[MustBeVariant]T2> : AsyncGodotSignalGenericHandler<(T1,T2)>
{
    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
        :base(obj,signalName,cancellationToken, callOnce)
    {
        Connect(Callable.From<T1,T2>(Invoke));
    }

    void Invoke(T1 result1,T2 result2)
    {
        core.TrySetResult((result1,result2));
    }
}
public class AsyncGodotSignalHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3> : AsyncGodotSignalGenericHandler<(T1,T2,T3)>
{
    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
        :base(obj,signalName,cancellationToken, callOnce)
    {
        Connect(Callable.From<T1,T2,T3>(Invoke));
    }

    void Invoke(T1 result1,T2 result2,T3 result3)
    {
        core.TrySetResult((result1,result2,result3));
    }
}
public class AsyncGodotSignalHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4> : AsyncGodotSignalGenericHandler<(T1,T2,T3,T4)>
{
    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
        :base(obj,signalName,cancellationToken, callOnce)
    {
        Connect(Callable.From<T1,T2,T3,T4>(Invoke));
    }

    void Invoke(T1 result1,T2 result2,T3 result3,T4 result4)
    {
        core.TrySetResult((result1,result2,result3,result4));
    }
}
public class AsyncGodotSignalHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5> : AsyncGodotSignalGenericHandler<(T1,T2,T3,T4,T5)>
{
    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
        :base(obj,signalName,cancellationToken, callOnce)
    {
        Connect(Callable.From<T1,T2,T3,T4,T5>(Invoke));
    }

    void Invoke(T1 result1,T2 result2,T3 result3,T4 result4,T5 result5)
    {
        core.TrySetResult((result1,result2,result3,result4,result5));
    }
}
public class AsyncGodotSignalHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6> : AsyncGodotSignalGenericHandler<(T1,T2,T3,T4,T5,T6)>
{
    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
        :base(obj,signalName,cancellationToken, callOnce)
    {
        Connect(Callable.From<T1,T2,T3,T4,T5,T6>(Invoke));
    }

    void Invoke(T1 result1,T2 result2,T3 result3,T4 result4,T5 result5,T6 result6)
    {
        core.TrySetResult((result1,result2,result3,result4,result5,result6));
    }
}
public class AsyncGodotSignalHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7> : AsyncGodotSignalGenericHandler<(T1,T2,T3,T4,T5,T6,T7)>
{
    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
        :base(obj,signalName,cancellationToken, callOnce)
    {
        Connect(Callable.From<T1,T2,T3,T4,T5,T6,T7>(Invoke));
    }

    void Invoke(T1 result1,T2 result2,T3 result3,T4 result4,T5 result5,T6 result6,T7 result7)
    {
        core.TrySetResult((result1,result2,result3,result4,result5,result6,result7));
    }
}
public class AsyncGodotSignalHandler<[MustBeVariant]T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7,[MustBeVariant]T8> : AsyncGodotSignalGenericHandler<(T1,T2,T3,T4,T5,T6,T7,T8)>
{
    public AsyncGodotSignalHandler(GodotObject obj, StringName signalName, CancellationToken cancellationToken, bool callOnce)
        :base(obj,signalName,cancellationToken, callOnce)
    {
        Connect(Callable.From<T1,T2,T3,T4,T5,T6,T7,T8>(Invoke));
    }

    void Invoke(T1 result1,T2 result2,T3 result3,T4 result4,T5 result5,T6 result6,T7 result7,T8 result8)
    {
        core.TrySetResult((result1,result2,result3,result4,result5,result6,result7,result8));
    }
}
public class GodotSignalHandlerAsyncEnumerable : IUniTaskAsyncEnumerable<AsyncUnit>
{
        readonly GodotObject obj;
        readonly StringName signalName;
        readonly CancellationToken cancellationToken1;

        public GodotSignalHandlerAsyncEnumerable(GodotObject obj,StringName signalName,CancellationToken cancellationToken)
        {
            this.obj = obj;
            this.signalName = signalName;
            this.cancellationToken1 = cancellationToken;
        }

        public IUniTaskAsyncEnumerator<AsyncUnit> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            if (this.cancellationToken1 == cancellationToken)
            {
                return new GodotSignalHandlerAsyncEnumerator(obj,signalName, this.cancellationToken1, CancellationToken.None);
            }
            else
            {
                return new GodotSignalHandlerAsyncEnumerator(obj,signalName, this.cancellationToken1, cancellationToken);
            }
        }

        class GodotSignalHandlerAsyncEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<AsyncUnit>
        {
            static readonly Action<object> cancel1 = OnCanceled1;
            static readonly Action<object> cancel2 = OnCanceled2;

            readonly GodotObject obj;
            readonly StringName signalName;
            CancellationToken cancellationToken1;
            CancellationToken cancellationToken2;
            Callable? callable;
            
            CancellationTokenRegistration registration1;
            CancellationTokenRegistration registration2;
            bool isDisposed;

            public GodotSignalHandlerAsyncEnumerator(GodotObject obj,StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
            {
                this.obj = obj;
                this.signalName = signalName;
                this.cancellationToken1 = cancellationToken1;
                this.cancellationToken2 = cancellationToken2;
            }

            public AsyncUnit Current => default;

            public UniTask<bool> MoveNextAsync()
            {
                cancellationToken1.ThrowIfCancellationRequested();
                cancellationToken2.ThrowIfCancellationRequested();
                completionSource.Reset();

                if (callable == null)
                {
                    callable = Callable.From(Invoke);
                    TaskTracker.TrackActiveTask(this, 3);
                    obj.Connect(signalName, callable.Value);
                    if (cancellationToken1.CanBeCanceled)
                    {
                        registration1 = cancellationToken1.RegisterWithoutCaptureExecutionContext(cancel1, this);
                    }
                    if (cancellationToken2.CanBeCanceled)
                    {
                        registration2 = cancellationToken2.RegisterWithoutCaptureExecutionContext(cancel2, this);
                    }
                }

                return new UniTask<bool>(this, completionSource.Version);
            }

            void Invoke()
            {
                completionSource.TrySetResult(true);
            }

            static void OnCanceled1(object state)
            {
                var self = (GodotSignalHandlerAsyncEnumerator)state;
                try
                {
                    self.completionSource.TrySetCanceled(self.cancellationToken1);
                }
                finally
                {
                    self.DisposeAsync().Forget();
                }
            }

            static void OnCanceled2(object state)
            {
                var self = (GodotSignalHandlerAsyncEnumerator)state;
                try
                {
                    self.completionSource.TrySetCanceled(self.cancellationToken2);
                }
                finally
                {
                    self.DisposeAsync().Forget();
                }
            }

            public UniTask DisposeAsync()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    TaskTracker.RemoveTracking(this);
                    registration1.Dispose();
                    registration2.Dispose();
                    if (callable != null)
                    {
                        obj.Disconnect(signalName,callable.Value);
                    }
                    completionSource.TrySetCanceled();
                }

                return default;
            }
        }
    }
public abstract class GodotSignalHandlerAsyncGenericEnumerable<T> : IUniTaskAsyncEnumerable<T>
{
    readonly GodotObject obj;
    readonly StringName signalName;
    readonly CancellationToken cancellationToken1;

    protected GodotSignalHandlerAsyncGenericEnumerable(GodotObject obj,StringName signalName, CancellationToken cancellationToken)
    {
        this.obj = obj;
        this.signalName = signalName;
        this.cancellationToken1 = cancellationToken;
    }

    public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        if (this.cancellationToken1 == cancellationToken)
        {
            return GetAsyncEnumerator(obj, signalName, cancellationToken1, CancellationToken.None);
        }
        return GetAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken);
    }

    protected abstract IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(GodotObject obj, StringName signalName,
        CancellationToken cancellationToken1, CancellationToken cancellationToken2);
    
    protected abstract class GodotSignalHandlerAsyncEnumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>
    {
        static readonly Action<object> cancel1 = OnCanceled1;
        static readonly Action<object> cancel2 = OnCanceled2;

        readonly GodotObject obj;
        readonly StringName signalName;
        CancellationToken cancellationToken1;
        CancellationToken cancellationToken2;
        Callable? callable;
        
        CancellationTokenRegistration registration1;
        CancellationTokenRegistration registration2;
        bool isDisposed;

        public GodotSignalHandlerAsyncEnumerator(GodotObject obj,StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2)
        {
            this.obj = obj;
            this.signalName = signalName;
            this.cancellationToken1 = cancellationToken1;
            this.cancellationToken2 = cancellationToken2;
        }

        public T Current { get; protected set; }
        
        protected abstract Callable GetCallable();

        public UniTask<bool> MoveNextAsync()
        {
            cancellationToken1.ThrowIfCancellationRequested();
            cancellationToken2.ThrowIfCancellationRequested();
            completionSource.Reset();

            if (callable == null)
            {
                callable = GetCallable();
                TaskTracker.TrackActiveTask(this, 3);
                obj.Connect(signalName, callable.Value);
                if (cancellationToken1.CanBeCanceled)
                {
                    registration1 = cancellationToken1.RegisterWithoutCaptureExecutionContext(cancel1, this);
                }
                if (cancellationToken2.CanBeCanceled)
                {
                    registration2 = cancellationToken2.RegisterWithoutCaptureExecutionContext(cancel2, this);
                }
            }

            return new UniTask<bool>(this, completionSource.Version);
        }

        static void OnCanceled1(object state)
        {
            var self = (GodotSignalHandlerAsyncEnumerator)state;
            try
            {
                self.completionSource.TrySetCanceled(self.cancellationToken1);
            }
            finally
            {
                self.DisposeAsync().Forget();
            }
        }

        static void OnCanceled2(object state)
        {
            var self = (GodotSignalHandlerAsyncEnumerator)state;
            try
            {
                self.completionSource.TrySetCanceled(self.cancellationToken2);
            }
            finally
            {
                self.DisposeAsync().Forget();
            }
        }

        public UniTask DisposeAsync()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                TaskTracker.RemoveTracking(this);
                registration1.Dispose();
                registration2.Dispose();
                if (callable != null)
                {
                    obj.Disconnect(signalName,callable.Value);
                }
                completionSource.TrySetCanceled();
            }

            return default;
        }
    }
}
public class GodotSignalHandlerAsyncEnumerable<[MustBeVariant] T> : GodotSignalHandlerAsyncGenericEnumerable<T>
{
    public GodotSignalHandlerAsyncEnumerable(GodotObject obj, StringName signalName, CancellationToken cancellationToken) : base(obj, signalName, cancellationToken)
    {
    }

    protected override IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1,
        CancellationToken cancellationToken2)
    {
        return new GodotSignalHandlerAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken2);
    }

    class GodotSignalHandlerAsyncEnumerator : GodotSignalHandlerAsyncGenericEnumerable<T>.GodotSignalHandlerAsyncEnumerator
    {
        public GodotSignalHandlerAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2) : base(obj, signalName, cancellationToken1, cancellationToken2)
        {
        }

        protected override Callable GetCallable()
        {
            return Callable.From<T>(Invoke);
        }
        
        void Invoke(T value)
        {
            Current = value;
            completionSource.TrySetResult(true);
        }
    }
}
public class GodotSignalHandlerAsyncEnumerable<[MustBeVariant] T1,[MustBeVariant]T2> : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2)>
{
    public GodotSignalHandlerAsyncEnumerable(GodotObject obj, StringName signalName, CancellationToken cancellationToken) : base(obj, signalName, cancellationToken)
    {
    }

    protected override IUniTaskAsyncEnumerator<(T1,T2)> GetAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1,
        CancellationToken cancellationToken2)
    {
        return new GodotSignalHandlerAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken2);
    }

    class GodotSignalHandlerAsyncEnumerator : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2)>.GodotSignalHandlerAsyncEnumerator
    {
        public GodotSignalHandlerAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2) : base(obj, signalName, cancellationToken1, cancellationToken2)
        {
        }

        protected override Callable GetCallable()
        {
            return Callable.From<T1,T2>(Invoke);
        }
        
        void Invoke(T1 value1,T2 value2)
        {
            Current = (value1,value2);
            completionSource.TrySetResult(true);
        }
    }
}
public class GodotSignalHandlerAsyncEnumerable<[MustBeVariant] T1,[MustBeVariant]T2,[MustBeVariant]T3> : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3)>
{
    public GodotSignalHandlerAsyncEnumerable(GodotObject obj, StringName signalName, CancellationToken cancellationToken) : base(obj, signalName, cancellationToken)
    {
    }

    protected override IUniTaskAsyncEnumerator<(T1,T2,T3)> GetAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1,
        CancellationToken cancellationToken2)
    {
        return new GodotSignalHandlerAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken2);
    }

    class GodotSignalHandlerAsyncEnumerator : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3)>.GodotSignalHandlerAsyncEnumerator
    {
        public GodotSignalHandlerAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2) : base(obj, signalName, cancellationToken1, cancellationToken2)
        {
        }

        protected override Callable GetCallable()
        {
            return Callable.From<T1,T2,T3>(Invoke);
        }
        
        void Invoke(T1 value1,T2 value2,T3 value3)
        {
            Current = (value1,value2,value3);
            completionSource.TrySetResult(true);
        }
    }
}
public class GodotSignalHandlerAsyncEnumerable<[MustBeVariant] T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4> : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4)>
{
    public GodotSignalHandlerAsyncEnumerable(GodotObject obj, StringName signalName, CancellationToken cancellationToken) : base(obj, signalName, cancellationToken)
    {
    }

    protected override IUniTaskAsyncEnumerator<(T1,T2,T3,T4)> GetAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1,
        CancellationToken cancellationToken2)
    {
        return new GodotSignalHandlerAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken2);
    }

    class GodotSignalHandlerAsyncEnumerator : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4)>.GodotSignalHandlerAsyncEnumerator
    {
        public GodotSignalHandlerAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2) : base(obj, signalName, cancellationToken1, cancellationToken2)
        {
        }

        protected override Callable GetCallable()
        {
            return Callable.From<T1,T2,T3,T4>(Invoke);
        }
        
        void Invoke(T1 value1,T2 value2,T3 value3,T4 value4)
        {
            Current = (value1,value2,value3,value4);
            completionSource.TrySetResult(true);
        }
    }
}
public class GodotSignalHandlerAsyncEnumerable<[MustBeVariant] T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5> : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4,T5)>
{
    public GodotSignalHandlerAsyncEnumerable(GodotObject obj, StringName signalName, CancellationToken cancellationToken) : base(obj, signalName, cancellationToken)
    {
    }

    protected override IUniTaskAsyncEnumerator<(T1,T2,T3,T4,T5)> GetAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1,
        CancellationToken cancellationToken2)
    {
        return new GodotSignalHandlerAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken2);
    }

    class GodotSignalHandlerAsyncEnumerator : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4,T5)>.GodotSignalHandlerAsyncEnumerator
    {
        public GodotSignalHandlerAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2) : base(obj, signalName, cancellationToken1, cancellationToken2)
        {
        }

        protected override Callable GetCallable()
        {
            return Callable.From<T1,T2,T3,T4,T5>(Invoke);
        }
        
        void Invoke(T1 value1,T2 value2,T3 value3,T4 value4,T5 value5)
        {
            Current = (value1,value2,value3,value4,value5);
            completionSource.TrySetResult(true);
        }
    }
}
public class GodotSignalHandlerAsyncEnumerable<[MustBeVariant] T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6> : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4,T5,T6)>
{
    public GodotSignalHandlerAsyncEnumerable(GodotObject obj, StringName signalName, CancellationToken cancellationToken) : base(obj, signalName, cancellationToken)
    {
    }

    protected override IUniTaskAsyncEnumerator<(T1,T2,T3,T4,T5,T6)> GetAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1,
        CancellationToken cancellationToken2)
    {
        return new GodotSignalHandlerAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken2);
    }

    class GodotSignalHandlerAsyncEnumerator : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4,T5,T6)>.GodotSignalHandlerAsyncEnumerator
    {
        public GodotSignalHandlerAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2) : base(obj, signalName, cancellationToken1, cancellationToken2)
        {
        }

        protected override Callable GetCallable()
        {
            return Callable.From<T1,T2,T3,T4,T5,T6>(Invoke);
        }
        
        void Invoke(T1 value1,T2 value2,T3 value3,T4 value4,T5 value5,T6 value6)
        {
            Current = (value1,value2,value3,value4,value5,value6);
            completionSource.TrySetResult(true);
        }
    }
}
public class GodotSignalHandlerAsyncEnumerable<[MustBeVariant] T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7> : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4,T5,T6,T7)>
{
    public GodotSignalHandlerAsyncEnumerable(GodotObject obj, StringName signalName, CancellationToken cancellationToken) : base(obj, signalName, cancellationToken)
    {
    }

    protected override IUniTaskAsyncEnumerator<(T1,T2,T3,T4,T5,T6,T7)> GetAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1,
        CancellationToken cancellationToken2)
    {
        return new GodotSignalHandlerAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken2);
    }

    class GodotSignalHandlerAsyncEnumerator : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4,T5,T6,T7)>.GodotSignalHandlerAsyncEnumerator
    {
        public GodotSignalHandlerAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2) : base(obj, signalName, cancellationToken1, cancellationToken2)
        {
        }

        protected override Callable GetCallable()
        {
            return Callable.From<T1,T2,T3,T4,T5,T6,T7>(Invoke);
        }
        
        void Invoke(T1 value1,T2 value2,T3 value3,T4 value4,T5 value5,T6 value6,T7 value7)
        {
            Current = (value1,value2,value3,value4,value5,value6,value7);
            completionSource.TrySetResult(true);
        }
    }
}
public class GodotSignalHandlerAsyncEnumerable<[MustBeVariant] T1,[MustBeVariant]T2,[MustBeVariant]T3,[MustBeVariant]T4,[MustBeVariant]T5,[MustBeVariant]T6,[MustBeVariant]T7,[MustBeVariant]T8> : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4,T5,T6,T7,T8)>
{
    public GodotSignalHandlerAsyncEnumerable(GodotObject obj, StringName signalName, CancellationToken cancellationToken) : base(obj, signalName, cancellationToken)
    {
    }

    protected override IUniTaskAsyncEnumerator<(T1,T2,T3,T4,T5,T6,T7,T8)> GetAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1,
        CancellationToken cancellationToken2)
    {
        return new GodotSignalHandlerAsyncEnumerator(obj, signalName, cancellationToken1, cancellationToken2);
    }

    class GodotSignalHandlerAsyncEnumerator : GodotSignalHandlerAsyncGenericEnumerable<(T1,T2,T3,T4,T5,T6,T7,T8)>.GodotSignalHandlerAsyncEnumerator
    {
        public GodotSignalHandlerAsyncEnumerator(GodotObject obj, StringName signalName, CancellationToken cancellationToken1, CancellationToken cancellationToken2) : base(obj, signalName, cancellationToken1, cancellationToken2)
        {
        }

        protected override Callable GetCallable()
        {
            return Callable.From<T1,T2,T3,T4,T5,T6,T7,T8>(Invoke);
        }
        
        void Invoke(T1 value1,T2 value2,T3 value3,T4 value4,T5 value5,T6 value6,T7 value7,T8 value8)
        {
            Current = (value1,value2,value3,value4,value5,value6,value7,value8);
            completionSource.TrySetResult(true);
        }
    }
}