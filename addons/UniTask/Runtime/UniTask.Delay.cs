#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using Godot;

namespace Cysharp.Threading.Tasks
{
    public enum DelayType
    {
        /// <summary>use Time.deltaTime.</summary>
        DeltaTime,
        /// <summary>Ignore timescale, use Time.unscaledDeltaTime.</summary>
        UnscaledDeltaTime,
        /// <summary>use Stopwatch.GetTimestamp().</summary>
        Realtime
    }

    public partial struct UniTask
    {
        public static YieldAwaitable Yield()
        {
            // optimized for single continuation
            return new YieldAwaitable(PlayerLoopTiming.Process);
        }

        public static YieldAwaitable Yield(PlayerLoopTiming timing)
        {
            // optimized for single continuation
            return new YieldAwaitable(timing);
        }

        public static UniTask Yield(CancellationToken cancellationToken, bool cancelImmediately = false)
        {
            return new UniTask(YieldPromise.Create(PlayerLoopTiming.Process, cancellationToken, cancelImmediately, out var token), token);
        }

        public static UniTask Yield(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately = false)
        {
            return new UniTask(YieldPromise.Create(timing, cancellationToken, cancelImmediately, out var token), token);
        }

        /// <summary>
        /// Similar as UniTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static UniTask NextFrame()
        {
            return new UniTask(NextFramePromise.Create(PlayerLoopTiming.Process, CancellationToken.None, false, out var token), token);
        }

        /// <summary>
        /// Similar as UniTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static UniTask NextFrame(PlayerLoopTiming timing)
        {
            return new UniTask(NextFramePromise.Create(timing, CancellationToken.None, false, out var token), token);
        }

        /// <summary>
        /// Similar as UniTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static UniTask NextFrame(CancellationToken cancellationToken, bool cancelImmediately = false)
        {
            return new UniTask(NextFramePromise.Create(PlayerLoopTiming.Process, cancellationToken, cancelImmediately, out var token), token);
        }

        /// <summary>
        /// Similar as UniTask.Yield but guaranteed run on next frame.
        /// </summary>
        public static UniTask NextFrame(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately = false)
        {
            return new UniTask(NextFramePromise.Create(timing, cancellationToken, cancelImmediately, out var token), token);
        }

        /// <summary>
        /// Same as UniTask.Yield(PlayerLoopTiming.Process).
        /// </summary>
        public static YieldAwaitable WaitForProcessFrame()
        {
            // use LastFixedUpdate instead of FixedUpdate
            // https://github.com/Cysharp/UniTask/issues/377
            return UniTask.Yield(PlayerLoopTiming.Process);
        }

        /// <summary>
        /// Same as UniTask.Yield(PlayerLoopTiming.PhysicsProcess, cancellationToken).
        /// </summary>
        public static UniTask WaitForPhysicsFrame(CancellationToken cancellationToken, bool cancelImmediately = false)
        {
            return UniTask.Yield(PlayerLoopTiming.PhysicsProcess, cancellationToken, cancelImmediately);
        }

		public static UniTask WaitForSeconds(float duration, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return Delay(Mathf.RoundToInt(1000 * duration), ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
		}

		public static UniTask WaitForSeconds(int duration, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			return Delay(1000 * duration, ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
		}

		public static UniTask DelayFrame(int delayFrameCount, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
        {
            if (delayFrameCount < 0)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayFrameCount. delayFrameCount:" + delayFrameCount);
            }

            return new UniTask(DelayFramePromise.Create(delayFrameCount, delayTiming, cancellationToken, cancelImmediately, out var token), token);
        }

        public static UniTask Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
        {
            var delayTimeSpan = TimeSpan.FromMilliseconds(millisecondsDelay);
            return Delay(delayTimeSpan, ignoreTimeScale, delayTiming, cancellationToken, cancelImmediately);
        }

        public static UniTask Delay(TimeSpan delayTimeSpan, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
        {
            var delayType = ignoreTimeScale ? DelayType.UnscaledDeltaTime : DelayType.DeltaTime;
            return Delay(delayTimeSpan, delayType, delayTiming, cancellationToken, cancelImmediately);
        }

        public static UniTask Delay(int millisecondsDelay, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
        {
            var delayTimeSpan = TimeSpan.FromMilliseconds(millisecondsDelay);
            return Delay(delayTimeSpan, delayType, delayTiming, cancellationToken, cancelImmediately);
        }

        public static UniTask Delay(TimeSpan delayTimeSpan, DelayType delayType, PlayerLoopTiming delayTiming = PlayerLoopTiming.Process, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
        {
            if (delayTimeSpan < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("Delay does not allow minus delayTimeSpan. delayTimeSpan:" + delayTimeSpan);
            }

#if UNITY_EDITOR
            // force use Realtime.
            if (PlayerLoopHelper.IsMainThread && !UnityEditor.EditorApplication.isPlaying)
            {
                delayType = DelayType.Realtime;
            }
#endif

            switch (delayType)
            {
                case DelayType.UnscaledDeltaTime:
                    {
                        return new UniTask(DelayIgnoreTimeScalePromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out var token), token);
                    }
                case DelayType.Realtime:
                    {
                        return new UniTask(DelayRealtimePromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out var token), token);
                    }
                case DelayType.DeltaTime:
                default:
                    {
                        return new UniTask(DelayPromise.Create(delayTimeSpan, delayTiming, cancellationToken, cancelImmediately, out var token), token);
                    }
            }
        }

        sealed class YieldPromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<YieldPromise>
        {
            static TaskPool<YieldPromise> pool;
            YieldPromise nextNode;
            public ref YieldPromise NextNode => ref nextNode;

            static YieldPromise()
            {
                TaskPool.RegisterSizeGetter(typeof(YieldPromise), () => pool.Size);
            }

            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool cancelImmediately;
            UniTaskCompletionSourceCore<object> core;

            YieldPromise()
            {
            }

            public static IUniTaskSource Create(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new YieldPromise();
                }

                result.cancellationToken = cancellationToken;
                result.cancelImmediately = cancelImmediately;
                
                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (YieldPromise)state;
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                    else
                    {
                        TaskTracker.RemoveTracking(this);
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                core.TrySetResult(null);
                return false;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                cancelImmediately = default;
                return pool.TryPush(this);
            }
        }

        sealed class NextFramePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<NextFramePromise>
        {
            static TaskPool<NextFramePromise> pool;
            NextFramePromise nextNode;
            public ref NextFramePromise NextNode => ref nextNode;

            static NextFramePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(NextFramePromise), () => pool.Size);
            }

            ulong frameCount;
            UniTaskCompletionSourceCore<AsyncUnit> core;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool cancelImmediately;

            NextFramePromise()
            {
            }

            public static IUniTaskSource Create(PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new NextFramePromise();
                }

                result.frameCount = PlayerLoopHelper.IsMainThread ? Engine.GetProcessFrames() : ulong.MaxValue;
                result.cancellationToken = cancellationToken;
                result.cancelImmediately = cancelImmediately;

                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (NextFramePromise)state;
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                    else
                    {
                        TaskTracker.RemoveTracking(this);
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (frameCount == Engine.GetProcessFrames())
                {
                    return true;
                }

                core.TrySetResult(AsyncUnit.Default);
                return false;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                return pool.TryPush(this);
            }
        }

        sealed class DelayFramePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayFramePromise>
        {
            static TaskPool<DelayFramePromise> pool;
            DelayFramePromise nextNode;
            public ref DelayFramePromise NextNode => ref nextNode;

            static DelayFramePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayFramePromise), () => pool.Size);
            }

            ulong initialFrame;
            int delayFrameCount;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool cancelImmediately;

            int currentFrameCount;
            UniTaskCompletionSourceCore<AsyncUnit> core;

            DelayFramePromise()
            {
            }

            public static IUniTaskSource Create(int delayFrameCount, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayFramePromise();
                }

                result.delayFrameCount = delayFrameCount;
                result.cancellationToken = cancellationToken;
                result.initialFrame = PlayerLoopHelper.IsMainThread ? Engine.GetProcessFrames() : ulong.MaxValue;
                result.cancelImmediately = cancelImmediately;

                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (DelayFramePromise)state;
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                    else
                    {
                        TaskTracker.RemoveTracking(this);
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (currentFrameCount == 0)
                {
                    if (delayFrameCount == 0) // same as Yield
                    {
                        core.TrySetResult(AsyncUnit.Default);
                        return false;
                    }

                    // skip in initial frame.
                    if (initialFrame == Engine.GetProcessFrames())
                    {
                        return true;
                    }
                }

                if (++currentFrameCount >= delayFrameCount)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                currentFrameCount = default;
                delayFrameCount = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                cancelImmediately = default;
                return pool.TryPush(this);
            }
        }
        
        sealed class DelayPromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayPromise>
        {
            static TaskPool<DelayPromise> pool;
            DelayPromise nextNode;
            public ref DelayPromise NextNode => ref nextNode;

            static DelayPromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayPromise), () => pool.Size);
            }

            ulong initialFrame;
            float delayTimeSpan;
            float elapsed;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool cancelImmediately;
            DateTime lasTime;

            UniTaskCompletionSourceCore<object> core;

            DelayPromise()
            {
            }

            public static IUniTaskSource Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayPromise();
                }

                result.elapsed = 0.0f;
                result.delayTimeSpan = (float)delayTimeSpan.TotalSeconds;
                result.cancellationToken = cancellationToken;
                result.initialFrame = PlayerLoopHelper.IsMainThread ? Engine.GetProcessFrames() : ulong.MaxValue;
                result.lasTime = DateTime.Now;
                result.cancelImmediately = cancelImmediately;

                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (DelayPromise)state;
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                    else
                    {
                        TaskTracker.RemoveTracking(this);
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (elapsed == 0.0f)
                {
                    if (initialFrame == Engine.GetProcessFrames())
                    {
                        return true;
                    }
                }

                elapsed += (float)((DateTime.Now - lasTime).TotalSeconds * Engine.TimeScale);
                lasTime = DateTime.Now;
                if (elapsed >= delayTimeSpan)
                {
                    core.TrySetResult(null);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                delayTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                cancelImmediately = default;
                lasTime = default;
                return pool.TryPush(this);
            }
        }

        sealed class DelayIgnoreTimeScalePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayIgnoreTimeScalePromise>
        {
            static TaskPool<DelayIgnoreTimeScalePromise> pool;
            DelayIgnoreTimeScalePromise nextNode;
            public ref DelayIgnoreTimeScalePromise NextNode => ref nextNode;

            static DelayIgnoreTimeScalePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayIgnoreTimeScalePromise), () => pool.Size);
            }

            float delayFrameTimeSpan;
            float elapsed;
            ulong initialFrame;
            DateTime lastTime;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool cancelImmediately;

            UniTaskCompletionSourceCore<object> core;

            DelayIgnoreTimeScalePromise()
            {
            }

            public static IUniTaskSource Create(TimeSpan delayFrameTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayIgnoreTimeScalePromise();
                }

                result.elapsed = 0.0f;
                result.delayFrameTimeSpan = (float)delayFrameTimeSpan.TotalSeconds;
                result.initialFrame = PlayerLoopHelper.IsMainThread ? Engine.GetProcessFrames() : ulong.MaxValue;
                result.lastTime = DateTime.Now;
                result.cancellationToken = cancellationToken;
                result.cancelImmediately = cancelImmediately;

                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (DelayIgnoreTimeScalePromise)state;
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                    else
                    {
                        TaskTracker.RemoveTracking(this);
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (elapsed == 0.0f)
                {
                    if (initialFrame == Engine.GetProcessFrames())
                    {
                        return true;
                    }
                }

#if TOOLS
                if (Engine.TimeScale != 0) // maybe editor paused
#endif
                {
                    elapsed += (float)(DateTime.Now - lastTime).TotalSeconds;
                }
                lastTime = DateTime.Now;
                if (elapsed >= delayFrameTimeSpan)
                {
                    core.TrySetResult(null);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                delayFrameTimeSpan = default;
                elapsed = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                cancelImmediately = default;
                lastTime = DateTime.Now;
                return pool.TryPush(this);
            }
        }

        sealed class DelayRealtimePromise : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<DelayRealtimePromise>
        {
            static TaskPool<DelayRealtimePromise> pool;
            DelayRealtimePromise nextNode;
            public ref DelayRealtimePromise NextNode => ref nextNode;

            static DelayRealtimePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(DelayRealtimePromise), () => pool.Size);
            }

            long delayTimeSpanTicks;
            ValueStopwatch stopwatch;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool cancelImmediately;

            UniTaskCompletionSourceCore<AsyncUnit> core;

            DelayRealtimePromise()
            {
            }

            public static IUniTaskSource Create(TimeSpan delayTimeSpan, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayRealtimePromise();
                }

                result.stopwatch = ValueStopwatch.StartNew();
                result.delayTimeSpanTicks = delayTimeSpan.Ticks;
                result.cancellationToken = cancellationToken;
                result.cancelImmediately = cancelImmediately;

                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (DelayRealtimePromise)state;
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                    else
                    {
                        TaskTracker.RemoveTracking(this);
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                if (stopwatch.IsInvalid)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                if (stopwatch.ElapsedTicks >= delayTimeSpanTicks)
                {
                    core.TrySetResult(AsyncUnit.Default);
                    return false;
                }

                return true;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                stopwatch = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                cancelImmediately = default;
                return pool.TryPush(this);
            }
        }
    }

    public readonly struct YieldAwaitable
    {
        readonly PlayerLoopTiming timing;

        public YieldAwaitable(PlayerLoopTiming timing)
        {
            this.timing = timing;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(timing);
        }

        public UniTask ToUniTask()
        {
            return UniTask.Yield(timing, CancellationToken.None);
        }

        public readonly struct Awaiter : ICriticalNotifyCompletion
        {
            readonly PlayerLoopTiming timing;

            public Awaiter(PlayerLoopTiming timing)
            {
                this.timing = timing;
            }

            public bool IsCompleted => false;

            public void GetResult() { }

            public void OnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(timing, continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                PlayerLoopHelper.AddContinuation(timing, continuation);
            }
        }
    }
}
