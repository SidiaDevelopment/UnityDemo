using Entitas;
using UnityEngine;

public sealed class SpawnCameraSystem : IInitializeSystem
{
    readonly Contexts _contexts;

    public SpawnCameraSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var cameraEntity = _contexts.game.Instantiate(Res.Camera);
        cameraEntity.isCamera = true;
        cameraEntity.AddCameraOffset(Vector2.zero, 3);
    }
}
