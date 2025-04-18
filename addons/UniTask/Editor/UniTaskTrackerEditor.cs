using Godot;

namespace Cysharp.Threading.Tasks.Editor;

#if TOOLS
public partial class UniTaskTrackerEditor:EditorPlugin
{
    private UniTaskTrackerDebugger _debugger;
    public override void _EnterTree()
    {
        base._EnterTree();
        if (!Engine.IsEditorHint())
        {
            return;
        }

        _debugger = new();
        AddDebuggerPlugin(_debugger);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        if (!Engine.IsEditorHint())
        {
            return;
        }
        RemoveDebuggerPlugin(_debugger);
        _debugger = null;
    }
}
#endif