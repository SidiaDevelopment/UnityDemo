using System;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceableParentUiView : EntityView
    , IDragHandler
    , IBeginDragHandler
{
    public RectTransform RectTransform;
    
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);

        var placeableComponent = _entity.placeable;
        var go = Instantiate(placeableComponent.Value, transform);
        go.layer = gameObject.layer;
        var mesh = go.GetComponent<MeshFilter>().mesh;

        var padding = 20;
        var xScale = (RectTransform.rect.width - padding) / mesh.bounds.size.x;
        var yScale = (RectTransform.rect.height - padding) / mesh.bounds.size.y;
        var scale = Mathf.Min(xScale, yScale);
        go.transform.localScale = Vector3.one * scale;
        go.transform.localPosition = new Vector3(-(mesh.bounds.center.x) * scale, -(mesh.bounds.center.y) * scale, -30);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var entity = _contexts.game.CreateEntity();
        entity.AddPlaceableIndex(_entity.placeable.Name);
        _contexts.game.Instantiate(Res.Placeable, null, entity);
    }

    private void OnDestroy()
    {
        gameObject.Unlink();
    }
}