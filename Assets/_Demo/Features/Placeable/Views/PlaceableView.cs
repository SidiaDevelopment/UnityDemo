using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceableView : EntityView
    , IBeginDragHandler
    , IDragHandler
    , IPlaceableSelectedListener
    , IPlaceableSelectedRemovedListener
    , IAnyPlaceableSelectedListener
    , IAnyEditmodeListener
    , IAnyEditmodeRemovedListener
    , IPointerClickHandler
{
    // TODO: Turn off when edit mode exited
    public BoxCollider DragCollider;
    
    private CameraView _cameraView;
    private bool _dragging;
    private Plane _plane;
    private MeshRenderer _renderer;
    private Sequence _sequence;
    private MaterialPropertyBlock _mpb;
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private Color _originalColor;

    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);

        _entity.AddPlaceableSelectedListener(this);
        _entity.AddPlaceableSelectedRemovedListener(this);
        _entity.AddAnyPlaceableSelectedListener(this);
        _entity.AddAnyEditmodeListener(this);
        _entity.AddAnyEditmodeRemovedListener(this);
        
        InitializeModel();
        SetInitialPosition();

        // TODO: Flag loading phase
        var isSelected = Input.GetMouseButton(0) && _contexts.game.isEditmode;
        _entity.isPlaceableSelected = isSelected;
    }

    private void Awake()
    {
        _mpb = new MaterialPropertyBlock();
    }

    private void InitializeModel()
    {
        var index = _entity.placeableIndex.Value;
        var placeable = _contexts.game.GetEntityWithPlaceable(index).placeable;
        
        var go = Instantiate(placeable.Value, transform);
        go.layer = gameObject.layer;
        _renderer = go.GetComponent<MeshRenderer>();
        _renderer.GetPropertyBlock(_mpb);
        _originalColor = _renderer.material.GetColor(BaseColor);
        FitBoxCollider(go);
    }

    private void SetInitialPosition()
    {
        _cameraView = (CameraView)_contexts.game.cameraEntity.view.Value;
        _plane = new Plane(Vector3.up, Vector3.zero);
        _dragging = true;

        UpdatePosition();
    }

    private void FitBoxCollider(GameObject go)
    {
        var mesh = go.GetComponent<MeshFilter>().mesh;
        DragCollider.center = mesh.bounds.center;
        DragCollider.size = mesh.bounds.size;
        
        DragCollider.enabled = _contexts.game.isEditmode;
    }

    private void Update()
    {
        UpdatePosition();
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (Application.isPlaying &&  _contexts.config.game.DrawWireframe && Camera.current == Camera.main)
            Gizmos.DrawWireCube(transform.position + DragCollider.center, DragCollider.size);
    }
#endif

    private void UpdatePosition()
    {
        if (!_dragging) return;
        if (!Input.GetMouseButton(0))
        {
            _dragging = false;
            return;
        }

        var ray = _cameraView.MainCamera.ScreenPointToRay(Input.mousePosition);
        if (!_plane.Raycast(ray, out var distance)) return;

        var point = ray.GetPoint(distance);
        transform.position = point;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = _contexts.game.isEditmode;
        _entity.isPlaceableSelected = _contexts.game.isEditmode;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPlaceableSelected(GameEntity entity)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        var subtractColor = Color.white - _contexts.config.game.HighlightColor;
        
        _sequence.Append(DOVirtual.Float(0, 1, 0.8f, f =>
        {
            _mpb.SetColor(BaseColor, Color.Lerp(_originalColor, _originalColor - subtractColor, f));
            _renderer.SetPropertyBlock(_mpb);
        }));
        _sequence.Append(DOVirtual.Float(1, 0, 0.8f, f =>
        {
            _mpb.SetColor(BaseColor, Color.Lerp(_originalColor, _originalColor - subtractColor, f));
            _renderer.SetPropertyBlock(_mpb);
        }));
        _sequence.OnKill(() =>
        {
            _mpb.SetColor(BaseColor, _originalColor);
            _renderer.SetPropertyBlock(_mpb);
        });

        _sequence.SetLoops(-1);
    }

    public void OnPlaceableSelectedRemoved(GameEntity entity)
    {
        _sequence?.Kill();
    }

    public void OnAnyPlaceableSelected(GameEntity entity)
    {
        if (!entity.Equals(_entity) && _entity.isPlaceableSelected)
        {
            _entity.isPlaceableSelected = false;
        }
    }

    public void OnAnyEditmode(GameEntity entity)
    {
        DragCollider.enabled = true;
    }

    public void OnAnyEditmodeRemoved(GameEntity entity)
    {
        DragCollider.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _entity.isPlaceableSelected = _contexts.game.isEditmode;
    }
}