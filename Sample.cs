using System;
using Godot;
using Cysharp.Threading.Tasks;

public partial class Sample : Node
{
    public override void _EnterTree()
    {
        base._EnterTree();
        Loop().Forget();
    }

    private async UniTaskVoid Loop()
    {
        while (true)
        {
            Delay(3).Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(5));
        }
    }

    private async UniTaskVoid Delay(float time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
    }
}
