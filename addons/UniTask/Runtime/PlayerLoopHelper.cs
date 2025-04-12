#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using Cysharp.Threading.Tasks.Internal;
using System.Threading;
using Godot;

namespace Cysharp.Threading.Tasks
{
    public enum PlayerLoopTiming
    {
        Process = 0,
        PhysicsProcess = 1,
    }

    public interface IPlayerLoopItem
    {
        bool MoveNext();
    }

    public static class PlayerLoopHelper
    {
        static readonly ContinuationQueue ThrowMarkerContinuationQueue = new (PlayerLoopTiming.Process);
        static readonly PlayerLoopRunner ThrowMarkerPlayerLoopRunner = new (PlayerLoopTiming.Process);

        public static SynchronizationContext GodotSynchronizationContext => godotSynchronizationContext;
        public static int MainThreadId => mainThreadId;
        internal static string ApplicationDataPath => applicationDataPath;

        public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == mainThreadId;

        static int mainThreadId;
        static string applicationDataPath;
        static SynchronizationContext godotSynchronizationContext;
        static ContinuationQueue[] yielders;
        static PlayerLoopRunner[] runners;
        
        public static void Init()
        {
            // capture default(godot) sync-context.
            
            godotSynchronizationContext = new GodotSynchronizationContext();
            mainThreadId = (int)OS.GetMainThreadId();
            try
            {
                applicationDataPath = ProjectSettings.GlobalizePath("res://");
            }
            catch
            {
                // ignored
            }

            if (runners != null) // already initialized
            {
                return; 
            }
            Initialize();
        }
        
        static void InsertLoop(PlayerLoopTiming playerLoopTiming)
        {
            yielders[(int)playerLoopTiming] = new ContinuationQueue(playerLoopTiming);
            runners[(int)playerLoopTiming] = new PlayerLoopRunner(playerLoopTiming);
            switch (playerLoopTiming)
            {
                case PlayerLoopTiming.Process:
                    Engine.Singleton.GetMainLoop().Connect("process_frame", Callable.From(() =>
                    {
                        yielders[(int)playerLoopTiming].Run();
                        runners[(int)playerLoopTiming].Run();
                    }));
                    break;
                case PlayerLoopTiming.PhysicsProcess:
                    Engine.Singleton.GetMainLoop().Connect("physics_frame", Callable.From(() =>
                    {
                        yielders[(int)playerLoopTiming].Run();
                        runners[(int)playerLoopTiming].Run();
                    }));
                    break;
            }
        }

        static void Initialize()
        {
            yielders = new ContinuationQueue[2];
            runners = new PlayerLoopRunner[2];
            
            // Update
            InsertLoop(PlayerLoopTiming.Process);
            // PostLateUpdate
            InsertLoop(PlayerLoopTiming.PhysicsProcess);
        }

        public static void AddAction(PlayerLoopTiming timing, IPlayerLoopItem action)
        {
            var runner = runners[(int)timing];
            if (runner == null)
            {
                ThrowInvalidLoopTiming(timing);
                return;
            }
            runner.AddAction(action);
        }

        static void ThrowInvalidLoopTiming(PlayerLoopTiming playerLoopTiming)
        {
            throw new InvalidOperationException("Target playerLoopTiming is not injected. Please check PlayerLoopHelper.Initialize. PlayerLoopTiming:" + playerLoopTiming);
        }

        public static void AddContinuation(PlayerLoopTiming timing, Action continuation)
        {
            var q = yielders[(int)timing];
            if (q == null)
            {
                ThrowInvalidLoopTiming(timing);
                return;
            }
            q.Enqueue(continuation);
        }
    }
}

