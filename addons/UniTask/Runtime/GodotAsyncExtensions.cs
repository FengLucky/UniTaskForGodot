#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Threading;
using Godot;

namespace Cysharp.Threading.Tasks
{
    public static partial class GodotAsyncExtensions
    {
        private static readonly Dictionary<Node, CancellationTokenSource> CancellationTokenDict = new();
        public static CancellationToken GetCancellationTokenOnExitTree(this Node node)
        {
            if (!GodotObject.IsInstanceValid(node))
            {
                GD.PushError(new InvalidOperationException("node is invalid"));
                return CancellationToken.None;
            }

            if (CancellationTokenDict.TryGetValue(node, out var source))
            {
                return source.Token;
            }

            source = new CancellationTokenSource();
            CancellationTokenDict.Add(node,source);
            node.Connect(Node.SignalName.TreeExited, Callable.From(() =>
            {
                CancellationTokenDict.Remove(node);
                source.Cancel();
            }),(uint)GodotObject.ConnectFlags.OneShot);
            return source.Token;
        }
        
        public static UniTask NextFrame(this Node node)
        {
            return NextFrame(node, PlayerLoopTiming.Process, CancellationToken.None);
        }

        public static UniTask NextFrame(this Node node, PlayerLoopTiming timing)
        {
            return NextFrame(node, timing, CancellationToken.None);
        }

        public static UniTask NextFrame(this Node node, CancellationToken token)
        {
            return NextFrame(node, PlayerLoopTiming.Process, token);
        }
        
        public static UniTask NextFrame(this Node node, PlayerLoopTiming timing, CancellationToken cancellationToken)
        {
            switch (timing)
            {
                case PlayerLoopTiming.Process:
                    return node.OnInvokeAsync(SceneTree.SignalName.ProcessFrame, cancellationToken);
                case PlayerLoopTiming.PhysicsProcess:
                    return node.OnInvokeAsync(SceneTree.SignalName.PhysicsFrame, cancellationToken);
            }
            return UniTask.CompletedTask;
        }

        public static UniTask OnReadyAsync(this Node node)
        {
            return OnReadyAsync(node, CancellationToken.None);
        }

        public static UniTask OnReadyAsync(this Node node, CancellationToken cancellationToken)
        {
            if (node.IsNodeReady())
            {
                return UniTask.CompletedTask;
            }

            return node.OnInvokeAsync(Node.SignalName.Ready, cancellationToken);
        }

        public static UniTask OnTreeEnterAsync(this Node node)
        {
            return OnTreeEnterAsync(node, CancellationToken.None);
        }

        public static UniTask OnTreeEnterAsync(this Node node,CancellationToken cancellationToken)
        {
            return node.OnInvokeAsync(Node.SignalName.TreeEntered, cancellationToken);
        }
        
        public static UniTask OnTreeExitAsync(this Node node)
        {
            return OnTreeExitAsync(node, CancellationToken.None);
        }
        
        public static UniTask OnTreeExitAsync(this Node node,CancellationToken cancellationToken)
        {
            return node.OnInvokeAsync(Node.SignalName.TreeExited, cancellationToken);
        }
    }
}