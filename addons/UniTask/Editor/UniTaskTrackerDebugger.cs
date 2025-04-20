using System;
using System.Diagnostics;
using Godot;
using Array = Godot.Collections.Array;

namespace Cysharp.Threading.Tasks.Editor;

#if TOOLS
[Tool]
public partial class UniTaskTrackerDebugger:EditorDebuggerPlugin
{
    private readonly System.Collections.Generic.Dictionary<int, UniTaskTrackerPanel> _panels = new();
    
    public override bool _HasCapture(string capture)
    {
        return capture == "uniTask";
    }

    public override bool _Capture(string message, Array data, int sessionId)
    {
        if (message.EndsWith(":active"))
        {
            if (_panels.TryGetValue(sessionId, out var panel))
            {
                panel.AddTrack(data[0].AsInt32(),data[1].AsString(),data[2].AsInt64(),data[3].AsString());
            }
            return true;
        }
        
        if (message.EndsWith(":remove"))
        {
            if (_panels.TryGetValue(sessionId, out var panel))
            {
                panel.RemoveTrack(data[0].AsInt32());
            }
            return true;
        }

        if (message.EndsWith(":requestSetting"))
        {
            if (_panels.TryGetValue(sessionId, out var tree))
            {
                tree.SendSetting();
            }
            return true;
        }

        return false;
    }

    public override void _SetupSession(int sessionId)
    {
        base._SetupSession(sessionId);
        var session = GetSession(sessionId);
        var resource = ResourceLoader.Load<PackedScene>("res://addons/UniTask/Editor/UniTaskTrackerPanel.tscn");
        var panel = resource.Instantiate<UniTaskTrackerPanel>();
        session.AddSessionTab(panel);
        _panels.Add(sessionId,panel);
        panel.Init(sessionId, session);
        
        session.Started += () =>
        {
            GD.Print("session started");
            if (_panels.TryGetValue(sessionId, out var p))
            {
                p.Clear();
            }
        };
    }
}
#endif