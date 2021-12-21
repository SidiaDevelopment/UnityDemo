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
    public BoxCollider DragCollider;
    
    protected CameraView _cameraView;
    protected bool _dragging;
    protected Plane _plane;
    protected MeshRenderer _renderer;
    protected Sequence _sequence;
    protected MaterialPropertyBlock _mpb;
    protected static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    protected Color _originalColor;
    protected bool _scaling;
    protected bool _rotating;
    protected Vector2 _startPosition;
    protected Vector3 _originalScale;
    protected Quaternion _originalRotation;

    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);

        _entity.AddPlaceableSelectedListener(this);
        _entity.AddPlaceableSelectedRemovedListener(this);
        _entity.AddAnyPlaceableSelectedListener(this);
        _entity.AddAnyEditmodeListener(this);
        _entity.AddAnyEditmodeRemovedListener(this);
        
        InitializeModel();
        _cameraView = (CameraView)_contexts.game.cameraEntity.view.Value;
        _plane = new Plane(Vector3.up, Vector3.zero);

        if (_contexts.game.isLoading)
        {
            if (_entity.hasPlaceablePosition)
            {
                transform.position = _entity.placeablePosition.Value;
            }

            if (_entity.hasPlaceableRotation)
            {
                transform.rotation = Quaternion.Euler(_entity.placeableRotation.Value);
            }

            if (_entity.hasPlaceableScale)
            {
                transform.localScale = _entity.placeableScale.Value;
            }
        }
        else
        {
            SetInitialPosition();
        }

        _entity.isPlaceableSelected = !_contexts.game.isLoading;
    }

    private void Awake()
    {
        _mpb = new MaterialPropertyBlock();
    }

    protected virtual void InitializeModel()
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

    protected void SetInitialPosition()
    {
        _dragging = true;
        _entity.ReplacePlaceableScale(!_entity.isCharacter ? Vector3.one : Vector3.one * 3);
        _entity.ReplacePlaceableRotation(Vector3.zero);
        UpdatePosition();
    }

    private void FitBoxCollider(GameObject go)
    {
        var mesh = go.GetComponent<MeshFilter>().mesh;
        DragCollider.center = mesh.bounds.center;
        DragCollider.size = mesh.bounds.size;
        
        DragCollider.enabled = _contexts.game.isEditmode;
    }

    protected virtual void Update()
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

    protected void UpdatePosition()
    {
        if (!_dragging && !_scaling && !_rotating) return;
        if (!Input.GetMouseButton(0))
        {
            _dragging = false;
            _scaling = false;
            _rotating = false;
            return;
        }

        var ray = _cameraView.MainCamera.ScreenPointToRay(Input.mousePosition);
        if (!_plane.Raycast(ray, out var distance)) return;
        var point = ray.GetPoint(distance);

        if (_dragging)
        {
            transform.position = point;
            _entity.ReplacePlaceablePosition(point);
        } 
        else if (_scaling)
        {
            var yDelta = Input.mousePosition.y - _startPosition.y;
            var scaleModifier = yDelta / 500f;
            var value = _originalScale;
            value += Vector3.one * scaleModifier;
            value = Vector3.Max(Vector3.one * 0.1f, value);
            value = Vector3.Min(Vector3.one * 5, value);
            transform.localScale = value;
            _entity.ReplacePlaceableScale(value);
        }
        else if (_rotating)
        {
            var xDelta = Input.mousePosition.x - _startPosition.x;
            var rotationModifier = xDelta / 5f;
            var value = _originalRotation;
            value *= Quaternion.Euler(Vector3.down * rotationModifier);
            transform.rotation = value;
            _entity.ReplacePlaceableRotation(value.eulerAngles);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _entity.isPlaceableSelected = _contexts.game.isEditmode;
        _startPosition = eventData.position;
        if (_contexts.game.placeableTransformMode.Value == TransformMode.Position)
        {
            _dragging = _contexts.game.isEditmode;
        }
        else if (_contexts.game.placeableTransformMode.Value == TransformMode.Scale)
        {
            _scaling = _contexts.game.isEditmode;
            _originalScale = transform.localScale;
        }
        else if (_contexts.game.placeableTransformMode.Value == TransformMode.Rotation)
        {
            _rotating = _contexts.game.isEditmode;
            _originalRotation = transform.rotation;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPlaceableSelected(GameEntity entity)
    {
        if (_entity.isCharacter) return;
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

    public virtual void OnAnyEditmode(GameEntity entity)
    {
        DragCollider.enabled = true;
        _entity.isPlaceableSelected = false;
    }

    public void OnAnyEditmodeRemoved(GameEntity entity)
    {
        DragCollider.enabled = false;
        _entity.isPlaceableSelected = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _entity.isPlaceableSelected = _contexts.game.isEditmode;
    }

    private void OnDestroy()
    {
        _sequence?.Kill();
    }
}