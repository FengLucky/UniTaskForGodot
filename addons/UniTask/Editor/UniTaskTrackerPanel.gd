@tool extends Panel
@export var tree:Tree;
@export var stack_label:RichTextLabel;
@export var track_btn:Button;
@export var stack_trace_btn:Button;
@export var reload_btn:Button;
@export var auto_reload_btn:Button;
@export var clear_btn:Button;

const EnableAutoReloadKey:String = "EnableAutoReload";
const EnableTrackingKey:String = "EnableTracking";
const EnableStackTraceKey:String = "EnableStackTrace";

class TrackData:
	static var regex:RegEx = RegEx.new();
	var id:int;
	var type:String;
	var start_time:int;
	var stack:String;
	var first_line:String;
	var has_first_line_cache:bool;
	
	static func init_regex():
		regex.compile(r'\[color=.+\]\[url=.+\](.+)\[/url\]\[/color\]'); 

	func get_elapsed(cur_time:float) -> String:
		return String.num((cur_time - self.start_time) / 1000,2);
	func get_stack_first_line() -> String:
		if self.has_first_line_cache:
			return self.first_line;
		self.has_first_line_cache = true;
		if self.stack == null or self.stack == "":
			self.first_line = "";
		else:
			self.first_line = regex.sub(self.stack.split("\n",true,1)[0],"$1");
		return self.first_line;
		
var tracking:Array[TrackData] = [];
var real_tracking:Array[TrackData] = [];
var items:Array[TreeItem] = [];
var session_id:int;
var session:EditorDebuggerSession;
var config:ConfigFile = ConfigFile.new();
var select_id:int = -1;
var auto_reload:bool = false;
var refresh_delta:float = 0;
var running:bool = false;
var regex:RegEx = RegEx.new();

func init(id:int,session:EditorDebuggerSession)->void:
	self.session_id = id
	self.session = session;
	self.load_config();
	
func start():
	self.running = true;
	self.clear();
	
func stop():
	self.running = false;		
	
func add_track(id:int,type_name:String,start_time:int,stack_trace:String):
	var data = TrackData.new();
	data.id = id;
	data.type = type_name;
	data.start_time = start_time;
	data.stack = stack_trace;
	real_tracking.push_back(data);
	if self.auto_reload:
		tracking.push_back(data);
		self.refresh_items();

func remove_track(id:int):
	var index = self.real_tracking.find_custom(func(item:TrackData)->bool:
		return item.id == id;)
	if index > -1:
		real_tracking.remove_at(index);
		if self.auto_reload:
			tracking.remove_at(index);
			self.refresh_items();
			
func send_setting():
	self.session.send_message("uniTaskSetting:tracking",[self.config.get_value(String.num_int64(self.session_id),EnableTrackingKey,false) as bool]);
	self.session.send_message("uniTaskSetting:stackTrace",[self.config.get_value(String.num_int64(self.session_id),EnableStackTraceKey,false) as bool]);
				
func clear()->void:
	self.tracking.clear();
	self.real_tracking.clear();
	self.refresh_items();
	
func refresh_items():
	for i in range(self.items.size(),self.tracking.size(),1):
		var item = self.tree.create_item();
		item.set_text_alignment(1,HorizontalAlignment.HORIZONTAL_ALIGNMENT_CENTER);	
		self.items.push_back(item);
	
	for	i in range(self.tracking.size(),self.items.size(),1):
		self.items[i].visible = false;
		
	var cur_time: int = Time.get_unix_time_from_system() * 1000;	
	for i in range(self.tracking.size()):
		var item: TreeItem = self.items[i];
		item.visible = true;
		item.set_text(0,tracking[i].type);
		item.set_text(1,tracking[i].get_elapsed(cur_time));
		item.set_text(2,tracking[i].get_stack_first_line());
		
	self.refresh_selected_item();	
	self.refresh_delta = 0;

func refresh_selected_item():
	var index = self.tracking.find_custom(func(item:TrackData)->bool: 
		return item.id == self.select_id;)
	if(index > -1):
		stack_label.text = tracking[index].stack;
	else:
		tree.deselect_all();		
		stack_label.text = "";
		
func load_config():
	self.config.load("user://unitask_tracker.cfg");
	self.auto_reload = self.config.get_value(String.num_int64(self.session_id),EnableAutoReloadKey,false);
	self.auto_reload_btn.button_pressed = self.auto_reload;
	self.stack_trace_btn.button_pressed = self.config.get_value(String.num_int64(self.session_id),EnableStackTraceKey,false);
	self.track_btn.button_pressed = self.config.get_value(String.num_int64(self.session_id),EnableTrackingKey,false);
	
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	TrackData.init_regex();
	self.tree.item_selected.connect(self.on_select_item);
	self.reload_btn.pressed.connect(self.refresh_items);
	self.auto_reload_btn.toggled.connect(self._on_toggled_auto_reload);
	self.stack_trace_btn.toggled.connect(self._on_toggled_stack_trace);
	self.track_btn.toggled.connect(self._on_toggled_track);
	self.clear_btn.pressed.connect(self.clear);
	self.stack_label.connect("meta_clicked",self._on_click_code_link);
	self.tree.set_column_title(0,"TaskType");
	self.tree.set_column_title(1,"Elapsed");
	self.tree.set_column_title(2,"Position");
	self.tree.set_column_expand_ratio(0,20);
	self.tree.set_column_expand_ratio(1,5);
	self.tree.set_column_expand_ratio(2,75);
	
	for i in self.tree.columns:
		self.tree.set_column_expand(i,true);
		
func _process(delta: float) -> void:
	if !self.running:
		return;
	self.refresh_delta += delta;
	if self.refresh_delta > 0.1:
		self.refresh_items();
	
func on_select_item():
	var item: TreeItem = self.tree.get_selected();
	if item == null:
		return
	var index: int = self.items.find(item);
	if index > -1:
		self.select_id = self.tracking[index].id;
		
	self.refresh_selected_item();	

func _on_toggled_auto_reload(open:bool):
	self.auto_reload = open;
	self.config.set_value(String.num_int64(self.session_id),EnableAutoReloadKey,open);
	self.config.save("user://unitask_tracker.cfg");
	self.reload_btn.visible = !open;
	self.tracking.clear();
	self.tracking.append_array(real_tracking);
	self.refresh_items();
	
func _on_toggled_stack_trace(open:bool):
	self.config.set_value(String.num_int64(self.session_id),EnableStackTraceKey,open);
	self.config.save("user://unitask_tracker.cfg");
	self.session.send_message("uniTaskSetting:stackTrace",[open]);

func _on_toggled_track(open:bool):
	self.config.set_value(String.num_int64(self.session_id),EnableTrackingKey,open);
	self.config.save("user://unitask_tracker.cfg");
	self.session.send_message("uniTaskSetting:tracking",[open]);
	if !open :
		self.clear()

func _on_click_code_link(json):
	var settings = EditorInterface.get_editor_settings();
	var external_editor = settings.get_setting("dotnet/editor/external_editor");
	var editor_path = settings.get_setting("dotnet/editor/editor_path_optional");
	var param = JSON.parse_string(json);
	var path:String = ProjectSettings.globalize_path("res://") + param.path;
	var line:int = param.line;
	var args:String;
	
	match external_editor:
		2:
			pass
		3:
			pass
		4:
			pass
		5:
			args = "\"" + path + "\" --line " + str(line);
	print(editor_path + " " + args)		
	OS.execute(editor_path,[args])				
