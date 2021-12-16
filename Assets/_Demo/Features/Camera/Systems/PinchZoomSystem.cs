using Entitas;
using UnityEngine;

public sealed class PinchZoomSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    private bool _pinching = false;
    private float _initialDistance;
    private float _initialZoom;

    public PinchZoomSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if (Input.touchCount != 2)
        {
            _pinching = false;
            _contexts.game.isPinch = false;
            return;
        }

        Vector2 touch0, touch1;
        var cameraEntity = _contexts.game.cameraEntity;

        if (!_pinching)
        {
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            _initialDistance = Vector2.Distance(touch0, touch1);
            _initialZoom = cameraEntity.cameraOffset.Zoom;
            _pinching = true;
            _contexts.game.isPinch = true;
        }
        
        touch0 = Input.GetTouch(0).position;
        touch1 = Input.GetTouch(1).position;
        var distance = Mathf.Max(Vector2.Distance(touch0, touch1), 0.01f);
        var offset = _initialDistance / distance;
        
        var zoom = _initialZoom * (offset * offset);

        cameraEntity.ReplaceCameraOffset(cameraEntity.cameraOffset.Position, zoom);
    }
}
