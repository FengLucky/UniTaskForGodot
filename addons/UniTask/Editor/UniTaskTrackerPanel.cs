using System;
using System.Collections.Generic;
using Godot;

namespace Cysharp.Threading.Tasks.Editor;

[Tool]
public partial class UniTaskTrackerPanel:Panel
{
    private const string EnableAutoReloadKey = "EnableAutoReloadKey";
    private const string EnableTrackingKey = "EnableTrackingKey";
    private const string EnableStackTraceKey = "EnableStackTraceKey";
    
    [Export]
    private Tree _tree;
    [Export]
    private RichTextLabel _stackLabel;
    [Export]
    private Button _trackBtn;
    [Export]
    private Button _stackTrackBtn;
    [Export]
    private Button _reloadBtn;
    [Export]
    private Button _autoReloadBtn;
    
    private readonly List<(int,string, DateTime, string)> _tracking = new();
    private readonly List<(int, string, DateTime, string)> _realTracking = new();
    private int _sessionId;
    private EditorDebuggerSession _session;
    private bool _autoReload;
    private bool _enableStackTrack;
    private bool _enableTracking;
    private ConfigFile _config = new();
    private readonly List<TreeItem> _items = new();
    private int _selectId = -1;
    
    public override void _Ready()
    {
        base._Ready();
        _tree.ItemSelected += OnSelectedItem;
        _reloadBtn.Pressed += OnPressedReload;
        _autoReloadBtn.Toggled += OnToggledAutoReload;
        _stackTrackBtn.Toggled += OnToggledStackTrack;
        _trackBtn.Toggled += OnToggledTrack;
        _tree.SetColumnTitle(0,"TaskType");
        _tree.SetColumnTitle(1,"Elapsed");
        _tree.SetColumnTitle(2,"Position");
    }

    public void Init(int sessionId,EditorDebuggerSession session)
    {
        _sessionId = sessionId;
        _session = session;
        LoadConfig();
    }

    public void AddTrack(int id,string typeName,long startTime,string stackTrace)
    {
        var utcTime = DateTimeOffset.FromUnixTimeMilliseconds(startTime).UtcDateTime;
        var time = utcTime.AddMilliseconds((DateTime.Now - DateTime.UtcNow).TotalMilliseconds);
        _realTracking.Add((id,typeName,time , stackTrace));
        if (_autoReload)
        {
            _tracking.Add((id,typeName, time, stackTrace));
            RefreshItems();
        }
    }

    public void RemoveTrack(int id)
    {
        var index = _realTracking.FindIndex(item => item.Item1 == id);
        if (index >= 0)
        {
            _realTracking.RemoveAt(index);
            if (_autoReload)
            {
                _tracking.RemoveAt(index);
                RefreshItems();
            }
        }
    }

    public void SendSetting()
    {
        _session.SendMessage("uniTaskSetting:tracking",[_enableTracking]);
        _session.SendMessage("uniTaskSetting:stackTrace",[_enableStackTrack]);
    }
    
    public void Clear()
    {
        _tracking.Clear();
        _realTracking.Clear();
        RefreshItems();
    }

    private void RefreshItems()
    {
        for (int i = _items.Count; i < _tracking.Count; i++)
        {
            _items.Add(_tree.CreateItem());
        }

        for (int i = _tracking.Count; i < _items.Count; i++)
        {
            _items[i].Visible = false;
        }

        for (int i = 0; i < _tracking.Count; i++)
        {
            _items[i].Visible = true;
            _items[i].SetText(0,_tracking[i].Item2);
            _items[i].SetText(1,(DateTime.Now - _tracking[i].Item3).TotalSeconds.ToString("0.00"));
            var index = _tracking[i].Item4.IndexOf('\n');
            var firstLine = index > -1 ? _tracking[i].Item4.Substring(0, index) : _tracking[i].Item4;
            _items[i].SetText(2,firstLine);
        }

        RefreshSelectedItem();
    }

    private void RefreshSelectedItem()
    {
        var index = _tracking.FindIndex(item => item.Item1 == _selectId);
        if (index > -1)
        {
            _stackLabel.Text = _tracking[index].Item4;
        }
        else
        {
            _tree.DeselectAll();
        }
    }

    private void OnSelectedItem()
    {
        var item = _tree.GetSelected();
        if (item == null)
        {
            return;
        }
        var index = _items.IndexOf(item);
        if (index > -1)
        {
            _selectId = _tracking[index].Item1;
        }
        
        RefreshSelectedItem();
    }

    private void LoadConfig()
    {
        _config.Load("user://unitask_tracker.cfg");
        _autoReload = _config.GetValue(_sessionId.ToString(), EnableAutoReloadKey, true).AsBool();
        _enableStackTrack = _config.GetValue(_sessionId.ToString(), EnableStackTraceKey, true).AsBool();
        _enableTracking = _config.GetValue(_sessionId.ToString(), EnableTrackingKey, true).AsBool();
        
        _autoReloadBtn._Toggled(_autoReload);
        _stackTrackBtn._Toggled(_enableStackTrack);
        _trackBtn._Toggled(_enableTracking);
    }

    private void OnPressedReload()
    {
        RefreshItems();
    }
    
    private void OnToggledAutoReload(bool open)
    {
        _reloadBtn.Visible = !open;
       _tracking.Clear();
       _tracking.AddRange(_realTracking);
       RefreshItems();
    }

    private void OnToggledStackTrack(bool open)
    {
        _enableStackTrack = open;
        _stackTrackBtn._Toggled(open);
        _config.SetValue(_sessionId.ToString(),EnableStackTraceKey,open);
        _session.SendMessage("uniTaskSetting:stackTrace",[_enableStackTrack]);
    }

    private void OnToggledTrack(bool open)
    {
        _enableTracking = open;
        _trackBtn._Toggled(open);
        _config.SetValue(_sessionId.ToString(),EnableTrackingKey,open);
        _session.SendMessage("uniTaskSetting:tracking",[_enableTracking]);
        Clear();
    }
}