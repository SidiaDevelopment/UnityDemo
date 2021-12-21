using System;
using Entitas;
using UnityEngine;

public class CameraView : EntityView
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
    private Vector3 _initialPosition;
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
        if (_contexts.game.isEditmode)
            transform.position = position;
        _zoomTarget = zoom;
    }

    private void Update()
    {
        SetZoom();

        if (_contexts.game.isEditmode)
        {
            _rotating = false;
            return;
        }

        var pos = Input.mousePosition;
        if (_contexts.game.movement.Value != Vector2.zero)
        {
            if (Input.touchCount > 1)
            {
                pos = Input.touches[1].position;
                if (!_rotating)
                {
                    _rotating = true;
                    _initialPosition = pos;
                    _initialRotation = transform.rotation;
                }
            }
            else
            {
                _rotating = false;
                return;
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && !_rotating)
            {
                _rotating = true;
                _initialPosition = pos;
                _initialRotation = transform.rotation;
            }
            else if (!Input.GetMouseButton(0))
            {
                _rotating = false;
                return;
            }
        }
        
        var offset = pos - _initialPosition;
        var euler = _initialRotation.eulerAngles;
        euler += new Vector3(-offset.y / 5f, offset.x / 5f, 0);
        euler = new Vector3(Mathf.Clamp(euler.x, 0, 90), euler.y, euler.z);
        transform.rotation = Quaternion.Euler(euler);
    }

    private void SetZoom()
    {
        if (Math.Abs(_zoom - _zoomTarget) < 0.01f)
        {
            return;
        }

        _zoomTarget = Mathf.Clamp(_zoomTarget, 0.4f, 10f);
        _zoom = Mathf.Lerp(_zoom, _zoomTarget, _contexts.config.game.ZoomLerpSpeed);
        MainCamera.transform.localPosition = new Vector3(0, 0, _zoom * -_defaultDistance);
    }

    public void OnAnyPinchRemoved(GameEntity entity)
    {
        var zoom = Mathf.Clamp(_zoomTarget, 0.5f, 11.6f);
        _entity.ReplaceCameraOffset(_entity.cameraOffset.Position, zoom);
    }
    
    public void OnAnyEditmode(GameEntity entity)
    {
        transform.SetParent(null);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(60, 0, 0);
        _entity.ReplaceCameraOffset(Vector3.zero, _zoomTarget);
    }

    public void OnAnyEditmodeRemoved(GameEntity entity)
    {
        var character = _contexts.game.characterEntity.view.Value.transform;
        transform.SetParent(character);
        transform.localPosition = new Vector3(0, 1, 0);
        transform.rotation = Quaternion.identity;
    }
}