#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using Godot;

namespace Cysharp.Threading.Tasks
{
    // public for add user custom.

    public static class TaskTracker
    {
#if TOOLS

        static int trackingId = 0;

        public const string EnableAutoReloadKey = "UniTaskTrackerWindow_EnableAutoReloadKey";
        public const string EnableTrackingKey = "UniTaskTrackerWindow_EnableTrackingKey";
        public const string EnableStackTraceKey = "UniTaskTrackerWindow_EnableStackTraceKey";

        public static class EditorEnableState
        {
            static ConfigFile config;
            static bool enableAutoReload;

            static EditorEnableState()
            {
                config = new ConfigFile();
                config.Load("user://unitask.cfg");
                enableTracking = config.GetValue("switch", EnableTrackingKey, false).AsBool();
                enableAutoReload = config.GetValue("switch", EnableAutoReloadKey, false).AsBool();
                enableStackTrace = config.GetValue("switch", EnableStackTraceKey, false).AsBool();
            }
            
            public static bool EnableAutoReload
            {
                get { return enableAutoReload; }
                set
                {
                    if (enableAutoReload == value)
                    {
                        return;
                    }
                    enableAutoReload = value;
                    config.SetValue("switch",EnableAutoReloadKey,value);
                    config.Save("user://unitask.cfg");
                }
            }

            static bool enableTracking;
            public static bool EnableTracking
            {
                get { return enableTracking; }
                set
                {
                    if (enableTracking == value)
                    {
                        return;
                    }
                    enableTracking = value;
                    config.SetValue("switch",EnableTrackingKey,value);
                    config.Save("user://unitask.cfg");
                }
            }

            static bool enableStackTrace;
            public static bool EnableStackTrace
            {
                get { return enableStackTrace; }
                set
                {
                    if (enableStackTrace == value)
                    {
                        return;
                    }
                    enableStackTrace = value;
                    config.SetValue("switch",EnableStackTraceKey,value);
                    config.Save("user://unitask.cfg");
                }
            }
        }

#endif
        
        static readonly WeakDictionary<IUniTaskSource, int> tracking = new ();

        [Conditional("TOOLS")]
        public static void TrackActiveTask(IUniTaskSource task, int skipFrame)
        {
#if TOOLS
            if (!EditorEnableState.EnableTracking) return;
            var stackTrace = EditorEnableState.EnableStackTrace ? new StackTrace(skipFrame, true).CleanupAsyncStackTrace() : "";

            string typeName;
            if (EditorEnableState.EnableStackTrace)
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
            EngineDebugger.SendMessage("uniTask:active",[typeName,id,((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds(),stackTrace]);
            tracking.TryAdd(task, id);
#endif
        }

        [Conditional("TOOLS")]
        public static void RemoveTracking(IUniTaskSource task)
        {
#if TOOLS
            if (!EditorEnableState.EnableTracking) return;
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

