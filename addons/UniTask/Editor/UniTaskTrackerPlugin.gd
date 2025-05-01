@tool extends EditorPlugin
const UniTaskTrackerDebugger = preload("res://addons/UniTask/Editor/UniTaskTrackerDebugger.gd")

var debugger: UniTaskTrackerDebugger = null;

func _enter_tree() -> void:		
	var dir_path: String = "res://addons/UniTask/Translations/";
	var dir: DirAccess = DirAccess.open(dir_path);
	if dir != null:
		dir.list_dir_begin();
		var domain = TranslationServer.get_or_add_domain("godot.editor");
		var path: String = dir.get_next();
		while path != null && path != "":
			if(path.ends_with(".translation")):
				var res = load(dir_path +"/"+ path);
				domain.add_translation(res);
			path = dir.get_next();
		dir.list_dir_end();
	
	debugger = UniTaskTrackerDebugger.new();
	add_debugger_plugin(debugger);	
	
func _exit_tree() -> void:
	if self.debugger != null:
		remove_debugger_plugin(debugger);
		self.debugger = null;	
