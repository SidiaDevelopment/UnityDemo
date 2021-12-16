using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPlaneView : EntityView
    , IDragHandler
    , IBeginDragHandler
    , IEndDragHandler
{
    private Vector3 _dragStart;
    private bool _isDragging;
    private GameEntity _cameraEntity;
    private Vector3 _cameraStart;
    private CameraView _cameraView;
    private Plane _plane;

    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        _cameraEntity = _contexts.game.cameraEntity;
        _cameraView = (CameraView)_cameraEntity.view.Value;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;
        if (Input.touchCount > 1) return;

        var ray = _cameraView.Camera.ScreenPointToRay(eventData.position);
        if (_plane.Raycast(ray, out var hit))
        {
            var point = ray.GetPoint(hit) - _cameraView.transform.position;
            _cameraEntity.ReplaceCameraOffset(_cameraStart + _dragStart - point, _cameraEntity.cameraOffset.Zoom);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDragging) return;

        _isDragging = true;
        _cameraStart = _cameraEntity.cameraOffset.Position;
        
        var ray = _cameraView.Camera.ScreenPointToRay(eventData.position);
        if (_plane.Raycast(ray, out var hit))
        {
            _dragStart = ray.GetPoint(hit) - _cameraView.transform.position;
        }
        else
        {
            _isDragging = false;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }
}