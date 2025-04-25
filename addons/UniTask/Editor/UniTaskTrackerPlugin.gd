@tool extends EditorPlugin
const UniTaskTrackerDebugger = preload("res://addons/UniTask/Editor/UniTaskTrackerDebugger.gd")

var debugger: UniTaskTrackerDebugger = null;

func _enter_tree() -> void:
	if !Engine.is_editor_hint():
		return;
	
	debugger = UniTaskTrackerDebugger.new();
	add_debugger_plugin(debugger);
	
	var dir_path: String = "res://addons/UniTask/Translations/";
	var dir: DirAccess = DirAccess.open(dir_path);
	if dir != null:
		dir.list_dir_begin();
		var path: String = dir.get_next();
		while path != null && path != "":
			if(path.ends_with(".translation")):
				var res = load(dir_path +"/"+ path);
				TranslationServer.add_translation(res);
			path = dir.get_next();
		dir.list_dir_end();
	TranslationServer.set_locale("zh_CN")	
	TranslationServer.set_translation_domain("zh_CN")
	print(TranslationServer.get_language_name("zh"))
	print(TranslationServer.translate("UniTask_Clear"))
	print(TranslationServer.get_translation_domain())
	print(EditorInterface.can_translate_messages())
	
func _exit_tree() -> void:
	if !Engine.is_editor_hint():
		return;
	if self.debugger != null:
		remove_debugger_plugin(debugger);
		self.debugger = null;	
