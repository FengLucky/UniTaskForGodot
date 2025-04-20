@tool
extends Panel

var sessionId:int;
var session:EditorDebuggerSession;

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