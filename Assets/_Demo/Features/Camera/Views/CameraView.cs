using System;
using Entitas;
using UnityEngine;

public class CameraView : EntityView
    , ICameraOffsetListener
    , IAnyPinchRemovedListener
{
    public Camera MainCamera;
    public Camera UiCamera;

    private float _defaultDistance;
    private float _zoomTarget = 5;
    private float _zoom = 5;
    
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        _entity.AddCameraOffsetListener(this);
        _entity.AddAnyPinchRemovedListener(this);

        _defaultDistance = _contexts.config.game.DefaultZoom;
    }

    public void OnCameraOffset(GameEntity entity, Vector3 position, float zoom)
    {
        transform.position = position;
        _zoomTarget = zoom;
    }

    private void Update()
    {
        if (Math.Abs(_zoom - _zoomTarget) < 0.01f)
        {
            return;
        }
        
        _zoomTarget = Mathf.Clamp(_zoomTarget, 0.4f, 5.8f);
        _zoom = Mathf.Lerp(_zoom, _zoomTarget, _contexts.config.game.ZoomLerpSpeed);
        MainCamera.transform.localPosition = new Vector3(0, 0, _zoom * -_defaultDistance);
    }

    public void OnAnyPinchRemoved(GameEntity entity)
    {
        var zoom = Mathf.Clamp(_zoomTarget, 0.5f, 5f);
        _entity.ReplaceCameraOffset(_entity.cameraOffset.Position, zoom);
    }
}