using System;
using System.Collections.Generic;
using Godot;

namespace Cysharp.Threading.Tasks.Editor;

public partial class UniTaskTrackerPanel:Control
{
    [Export]
    private Tree _tree;
    [Export]
    private RichTextLabel _stackLabel;
    
    private readonly Dictionary<int, (string, DateTime, string)> _tracking = new();

    public override void _Ready()
    {
        base._Ready();
        _tree.ItemSelected += OnSelectedItem;
    }

    public void AddTrack(int id,string typeName,long startTime,string stackTrace)
    {
        _tracking.TryAdd(id, (typeName, DateTimeOffset.FromUnixTimeMilliseconds(startTime).DateTime, stackTrace));
        Refresh();
    }

    public void RemoveTrack(int id)
    {
        _tracking.Remove(id);
        Refresh();
    }

    private void Refresh()
    {
        
    }

    private void OnSelectedItem()
    {
        var item = _tree.GetSelected();
        if (item == null)
        {
            
        }
    }
}