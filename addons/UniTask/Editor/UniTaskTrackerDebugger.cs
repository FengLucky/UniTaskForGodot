using Godot;
using Godot.Collections;

namespace Cysharp.Threading.Tasks.Editor;

#if TOOLS
public partial class UniTaskTrackerDebugger:EditorDebuggerPlugin
{
    private readonly System.Collections.Generic.Dictionary<int, UniTaskTrackerPanel> _trees = new();

    public override bool _HasCapture(string capture)
    {
        return capture == "uniTask";
    }

    public override bool _Capture(string message, Array data, int sessionId)
    {
        if (message.EndsWith(":active"))
        {
            if (_trees.TryGetValue(sessionId, out var tree))
            {
                tree.AddTrack(data[0].AsInt32(),data[1].AsString(),data[2].AsInt64(),data[3].AsString());
            }
        }
        else if (message.EndsWith(":remove"))
        {
            if (_trees.TryGetValue(sessionId, out var tree))
            {
                tree.RemoveTrack(data[0].AsInt32());
            }
        }

        return false;
    }

    public override void _SetupSession(int sessionId)
    {
        base._SetupSession(sessionId);
        var session = GetSession(sessionId);
        UniTaskTrackerPanel panel = null;
        session.AddSessionTab(panel);
        _trees.Add(sessionId,panel);
        session.Connect(EditorDebuggerSession.SignalName.Stopped, Callable.From(() =>
        {
            _trees.Remove(sessionId);
            session.RemoveSessionTab(panel);
        }), (uint)ConnectFlags.OneShot);
    }
}
#endif