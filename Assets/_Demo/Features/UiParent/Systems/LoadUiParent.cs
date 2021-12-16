using Entitas;

public sealed class LoadUiParent : IInitializeSystem
{
    readonly Contexts _contexts;

    public LoadUiParent(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var parent = _contexts.game.Instantiate(Res.UiParent);
        var transform = parent.view.Value.gameObject.transform;
        _contexts.game.SetUiParent(transform);
    }
}
