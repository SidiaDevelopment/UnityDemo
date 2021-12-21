using Entitas;

public sealed class InitializeEditmodeSystem : IInitializeSystem
{
    readonly Contexts _contexts;

    public InitializeEditmodeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        _contexts.game.isEditmode = true;
    }
}
