using System;
using System.Runtime.Loader;
using Godot;

namespace Cysharp.Threading.Tasks.Editor;

#if TOOLS
[Tool]
public partial class UniTaskTrackerPlugin:EditorPlugin
{
    private UniTaskTrackerDebugger _debugger;

    public UniTaskTrackerPlugin()
    {
        if (!Engine.IsEditorHint())
        {
            return;
        }
        // _debugger = new();
        //
        // AddDebuggerPlugin(_debugger);
        
       // AssemblyLoadContext.GetLoadContext(System.Reflection.Assembly.GetExecutingAssembly())!.Unloading += OnAssemblyUnloading;
    }
    
    public override void _EnterTree()
    {
        base._EnterTree();
        if (!Engine.IsEditorHint())
        {
            return;
            
        }
        
        // _debugger = new();
        // AddDebuggerPlugin(_debugger);
        
        var dirPath = "res://addons/UniTask/Translations/";
        var dir = DirAccess.Open(dirPath);
        if (dir != null)
        {
            dir.ListDirBegin();
            var path = dir.GetNext();
            while (!string.IsNullOrWhiteSpace(path))
            {
                if (path.EndsWith(".translation"))
                {
                    var translation = GD.Load<Translation>($"{dirPath}/{path}");
                    
                    TranslationServer.AddTranslation(translation);
                }
                path = dir.GetNext();
            }
            dir.ListDirEnd();
            dir.Dispose();
        }
    }
    
    public override void _Notification(int what)
    {
        base._Notification(what);
        switch ((long)what)
        {
            case NotificationExtensionReloaded:
                GD.Print("NotificationExtensionReloaded");
                break;
            case NotificationExitTree:
                GD.Print("NotificationExitTree");
                break;
            case NotificationPostinitialize:
                GD.Print("NotificationPostinitialize");
                
                break;
        }
    }

    private void OnAssemblyUnloading(AssemblyLoadContext context)
    {
        GD.Print("remove start");
        
        RemoveDebugger();
        GD.Print("remove end");
        AssemblyLoadContext.GetLoadContext(System.Reflection.Assembly.GetExecutingAssembly())!.Unloading -= OnAssemblyUnloading;
    }

    private void RemoveDebugger()
    {
        if (_debugger != null)
        {
            RemoveDebuggerPlugin(_debugger);
            _debugger = null;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        if (!Engine.IsEditorHint())
        {
            return;
        }

        RemoveDebugger();
    }
}
#endif