extends EditorDebuggerPlugin
const UniTaskTrackerPanel = preload("res://addons/UniTask/Editor/UniTaskTrackerPanel.gd");
var panelRes:PackedScene = preload("res://addons/UniTask/Editor/UniTaskTrackerPanel.tscn");
var panels:Dictionary[int,UniTaskTrackerPanel] = {};
func _has_capture(capture: String) -> bool:
	return capture == "uniTask";
	
func _capture(message: String, data: Array, session_id: int) -> bool:
	if message.ends_with(":active"):
		return true;
	if message.ends_with(":remove"):
		return true;
	if message.ends_with(":requestSetting"):
		return true;
	return false;
	
func _setup_session(session_id: int) -> void:
	var session: EditorDebuggerSession = get_session(session_id);	
	var panel: UniTaskTrackerPanel = panelRes.instantiate();
	session.add_session_tab(panel);
	panels[session_id] = panel;
	panel.init(session_id,session);
	session.started.connect(func():
		panel.clear();
	)
