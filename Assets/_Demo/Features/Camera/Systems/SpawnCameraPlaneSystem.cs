using Entitas;

public sealed class SpawnCameraPlaneSystem : IInitializeSystem
{
    readonly Contexts _contexts;

    public SpawnCameraPlaneSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        _contexts.game.Instantiate(Res.DragPane);
    }
}
