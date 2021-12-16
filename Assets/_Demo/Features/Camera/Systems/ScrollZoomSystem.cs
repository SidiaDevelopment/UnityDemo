using Entitas;
using UnityEngine;

public sealed class ScrollZoomSystem : IExecuteSystem
{
    readonly Contexts _contexts;

    public ScrollZoomSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if (Input.mouseScrollDelta.Equals(Vector2.zero)) return;

        var cameraEntity = _contexts.game.cameraEntity;
        var zoom = cameraEntity.cameraOffset.Zoom - Input.mouseScrollDelta.y * _contexts.config.game.ScrollZoomSpeed;
        cameraEntity.ReplaceCameraOffset(cameraEntity.cameraOffset.Position, zoom);
    }
}
