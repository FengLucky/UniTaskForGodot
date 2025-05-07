#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using Godot;
using Array = Godot.Collections.Array;

namespace Cysharp.Threading.Tasks
{
    public sealed partial class TaskTracker : EngineProfiler
    {
#if DEBUG
        static int trackingId = 0;
        static readonly WeakDictionary<IUniTaskSource, int> tracking = new ();
        static bool enableTracking = true;
        static bool enableStackTrace = true;
        static bool inited;
        static Array trackData = new();
        
        private TaskTracker(){ }

#pragma warning disable CA2255
        [ModuleInitializer]
#pragma warning restore CA2255
        internal static void Init()
        {
            if (Engine.IsEditorHint())
            {
                return;
            }
            
            if (inited)
            {
                return;
            }
            
            EngineDebugger.RegisterProfiler("uniTask",new TaskTracker());
            inited = true;
        }

        [Conditional("DEBUG")]
        public static void TrackActiveTask(IUniTaskSource task, int skipFrame)
        {
            if (!enableTracking)
            {
                return;
            }
            
            var stackTrace = enableStackTrace ? new StackTrace(0, true).CleanupAsyncStackTrace() : "";
            string typeName;
            if (enableStackTrace)
            {
                var sb = new StringBuilder();
                TypeBeautify(task.GetType(), sb);
                typeName = sb.ToString();
            }
            else
            {
                typeName = task.GetType().Name;
            }

            var id = Interlocked.Increment(ref trackingId);
            tracking.TryAdd(task, id);
            
            trackData.Add(1);
            trackData.Add(id);
            trackData.Add(typeName);
            trackData.Add(((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds());
            trackData.Add(stackTrace);
        }

        [Conditional("DEBUG")]
        public static void RemoveTracking(IUniTaskSource task)
        {
            if (!enableTracking) return;
            if (tracking.TryGetValue(task, out var id))
            {
                tracking.TryRemove(task);
                trackData.Add(2);
                trackData.Add(id);
            }
        }

        static void TypeBeautify(Type type, StringBuilder sb)
        {
            if (type.IsNested)
            {
                // TypeBeautify(type.DeclaringType, sb);
                sb.Append(type.DeclaringType.Name.ToString());
                sb.Append(".");
            }

            if (type.IsGenericType)
            {
                var genericsStart = type.Name.IndexOf("`");
                if (genericsStart != -1)
                {
                    sb.Append(type.Name.Substring(0, genericsStart));
                }
                else
                {
                    sb.Append(type.Name);
                }
                sb.Append("<");
                var first = true;
                foreach (var item in type.GetGenericArguments())
                {
                    if (!first)
                    {
                        sb.Append(", ");
                    }
                    first = false;
                    TypeBeautify(item, sb);
                }
                sb.Append(">");
            }
            else
            {
                sb.Append(type.Name);
            }
        }

        public override void _Toggle(bool enable, Array options)
        {
            base._Toggle(enable, options);
            enableTracking = enable;
            if (options.Count > 0)
            {
                enableStackTrace = options[0].AsBool();
            }

            if (!enableTracking)
            {
                trackData.Clear();
            }

            GD.Print("toggle enable:"+enable+" options:"+options);
        }

        public override void _Tick(double frameTime, double processTime, double physicsTime, double physicsFrameTime)
        {
            base._Tick(frameTime, processTime, physicsTime, physicsFrameTime);
            if (trackData.Count > 0)
            {
                EngineDebugger.SendMessage("uniTask:track",trackData);
                GD.Print("send data:"+trackData.Count);
                trackData = new();
            }
        }
#endif
    }
}

