#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using Godot;
using Array = Godot.Collections.Array;

namespace Cysharp.Threading.Tasks
{
    // public for add user custom.
    
    public static class TaskTracker
    {
#if TOOLS
        static int trackingId = 0;
        static readonly WeakDictionary<IUniTaskSource, int> tracking = new ();
        static bool enableTracking = true;
        static bool enableStackTrace = true;
        static bool inited;

        static TaskTracker()
        {
            if (!Engine.IsEditorHint())
            {
                Init();
            }
        }

        public static void Init()
        {
            if (inited)
            {
                return;
            }
            var action = OnCapture;
            EngineDebugger.Singleton.SendMessage("uniTask:requestSetting",[]);
            EngineDebugger.RegisterMessageCapture("uniTaskSetting",Callable.From(action));
            inited = true;
        }
        
        static bool OnCapture(string message,Array data)
        {
            if (message == "tracking")
            {
                enableTracking = data[0].AsBool();
                return true;
            }
            if (message == "stackTrace")
            {
                enableStackTrace = data[0].AsBool();
                return true;
            }

            return false;
        }
#endif

        [Conditional("TOOLS")]
        public static void TrackActiveTask(IUniTaskSource task, int skipFrame)
        {
#if TOOLS
            if (!enableTracking) return;
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
            EngineDebugger.SendMessage("uniTask:active",[id,typeName,((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds(),stackTrace]);
            tracking.TryAdd(task, id);
#endif
        }

        [Conditional("TOOLS")]
        public static void RemoveTracking(IUniTaskSource task)
        {
#if TOOLS
            if (!enableTracking) return;
            if (tracking.TryGetValue(task, out var id))
            {
                tracking.TryRemove(task);
                EngineDebugger.SendMessage("uniTask:remove", [id]);
            }
#endif
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
    }
}

