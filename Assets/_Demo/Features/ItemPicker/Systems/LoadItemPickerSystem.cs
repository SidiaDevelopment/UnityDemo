using Entitas;

public sealed class LoadItemPickerSystem : IInitializeSystem
{
    readonly Contexts _contexts;

    public LoadItemPickerSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var parent = _contexts.game.uiParent.Value;
        _contexts.game.Instantiate(Res.ItemPicker, parent);
    }
}
