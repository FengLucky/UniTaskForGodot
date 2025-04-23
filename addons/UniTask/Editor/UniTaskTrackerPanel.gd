@tool
extends Panel
@export
const tree:Tree;
@export
const stackLabel:RichTextLabel;
@export 
const trackBtn:Button;
@export
const stackTraceBtn:Button;
@export
const reloadBtn:Button;
@export
const autoReloadBtn;

const EnableAutoReloadKey:String = "EnableAutoReload";
const EnableTrackingKey:String = "EnableTracking";
const EnableStackTraceKey:String = "EnableStackTrace";

class TrackData:
	var id:int;
	var type:String;
	var startTime:int;
	var stack:String;

	func get_duration_seconds() -> String:
		return ""
	func get_stack_first_line() -> String:
		return ""
		
var tracking:Array[TrackData] = [];
var realTracking:Array[TrackData] = [];
var items:Array[TreeItem] = [];
var sessionId:int;
var session:EditorDebuggerSession;
var config:ConfigFile = ConfigFile.new();

func init(id:int,session:EditorDebuggerSession)->void:
	self.sessionId = id
	self.session = session;

func clear()->void:
	pass
	
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func _on_toggled_stack_trace(open:bool):
	self.config.set_value(String.num(self.sessionId),EnableStackTraceKey,open);
	

func _on_toggled_track(open:bool):
	self.config.set_value(String.num(self.sessionId),EnableTrackingKey,open);
	self.session.send_message("uniTaskSetting:tracking",[open]);
	if open :
		clear()