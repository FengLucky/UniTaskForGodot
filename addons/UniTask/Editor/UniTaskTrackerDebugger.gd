extends EditorDebuggerPlugin
const UniTaskTrackerPanel = preload("res://addons/UniTask/Editor/UniTaskTrackerPanel.gd");
var panelRes:PackedScene = preload("res://addons/UniTask/Editor/UniTaskTrackerPanel.tscn");
var panels:Dictionary[int,UniTaskTrackerPanel] = {};
var sessions:Array[EditorDebuggerSession] = [];
func _has_capture(capture: String) -> bool:
	return capture == "uniTask";
	
func _capture(message: String, data: Array, session_id: int) -> bool:
	if message.ends_with(":track"):
		if self.panels.has(session_id):
			var i = 0;
			var panel = self.panels[session_id];
			while i < data.size():
				match data[i]:
					1:
						panel.add_track(data[i+1],data[i+2],data[i+3],data[i+4]);
						i+=5;
					2:
						panel.remove_track(data[i+1]);
						i+=2;
					_:
						printerr("unknow track type:"+data[i])		
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
	self.sessions.push_back(session)

func stop_all_profiler():
	for session in self.sessions:
		session.toggle_profiler("uniTask",false)
