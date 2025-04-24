extends EditorDebuggerPlugin
const UniTaskTrackerPanel = preload("res://addons/UniTask/Editor/UniTaskTrackerPanel.gd");
var panelRes:PackedScene = preload("res://addons/UniTask/Editor/UniTaskTrackerPanel.tscn");
var panels:Dictionary[int,UniTaskTrackerPanel] = {};
func _has_capture(capture: String) -> bool:
	return capture == "uniTask";
	
func _capture(message: String, data: Array, session_id: int) -> bool:
	if message.ends_with(":active"):
		if self.panels.has(session_id):
			self.panels[session_id].add_track(data[0],data[1],data[2],data[3]);
		return true;
	if message.ends_with(":remove"):
		if self.panels.has(session_id):
			self.panels[session_id].remove_track(data[0]);
		return true;
	if message.ends_with(":requestSetting"):
		if self.panels.has(session_id):
			self.panels[session_id].send_setting();
		return true;
	return false;
	
func _setup_session(session_id: int) -> void:
	var session: EditorDebuggerSession = get_session(session_id);	
	var panel: UniTaskTrackerPanel = panelRes.instantiate();
	session.add_session_tab(panel);
	self.panels[session_id] = panel;
	panel.init(session_id,session);
	session.started.connect(panel.start);
	session.stopped.connect(panel.stop);
