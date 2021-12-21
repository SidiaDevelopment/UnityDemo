using Entitas;
using UnityEngine;

public sealed class InitializeControlsSystem : IInitializeSystem
{
    readonly Contexts _contexts;

    public InitializeControlsSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        _contexts.game.ReplaceMovement(Vector2.zero);
        var parent = _contexts.game.uiParent.Value;
        _contexts.game.Instantiate(Res.MobileControls, parent);
    }
}
