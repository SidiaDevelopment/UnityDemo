using Entitas;

public sealed class LoadMenuBarSystem : IInitializeSystem
{
    readonly Contexts _contexts;

    public LoadMenuBarSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var parent = _contexts.game.uiParent.Value;

        _contexts.game.ReplacePlaceableTransformMode(TransformMode.Position);
        _contexts.game.Instantiate(Res.MenuBar, parent);
    }
}
