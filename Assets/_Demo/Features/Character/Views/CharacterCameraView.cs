using System;
using Entitas;
using UnityEngine;

public class CharacterCameraView : EntityView
    , ICameraOffsetListener
    , IAnyPinchRemovedListener
    , IAnyEditmodeListener
    , IAnyEditmodeRemovedListener
{
    public Camera MainCamera;
    public Camera UiCamera;

    private float _defaultDistance;
    private float _zoomTarget = 5;
    private float _zoom = 5;
    private bool _rotating;
    private Vector2 _initialPosition;
    private Quaternion _initialRotation;
    
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        _entity.AddCameraOffsetListener(this);
        _entity.AddAnyPinchRemovedListener(this);
        _entity.AddAnyEditmodeListener(this);
        _entity.AddAnyEditmodeRemovedListener(this);

        _defaultDistance = _contexts.config.game.DefaultZoom;
    }

    public void OnCameraOffset(GameEntity entity, Vector3 position, float zoom)
    {
        _zoomTarget = zoom;
    }

    private void Update()
    {
        SetZoom();

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !_rotating)
#else
        if ((Input.touchCount == 1) && !_rotating)
#endif
        {
            _rotating = true;
            _initialPosition = Input.mousePosition;
            _initialRotation = transform.rotation;
        }
        else if (Input.touchCount != 1)
        {
            _rotating = false;
            return;
        }

        var offset = (Vector2)Input.mousePosition - _initialPosition;
        var euler = _initialRotation.eulerAngles;
        euler += new Vector3(offset.y / 5f, offset.x / 5f, 0);
        euler = new Vector3(Mathf.Clamp(euler.x, 0, 90), euler.y, euler.z);
        transform.rotation = Quaternion.Euler(euler);
    }

    private void SetZoom()
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

    public void OnAnyEditmode(GameEntity entity)
    {
        gameObject.SetActive(false);
    }

    public void OnAnyEditmodeRemoved(GameEntity entity)
    {
        gameObject.SetActive(true);
    }
}