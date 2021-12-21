using Entitas.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceableCharacterParentUiView : EntityView
    , IDragHandler
    , IBeginDragHandler
{
    public RectTransform RectTransform;
    
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_contexts.game.characterEntity != null)
        {
            _contexts.game.characterEntity.isDestroyed = true;
        }

        var e = _contexts.game.CreateEntity();
        e.AddPlaceableIndex("Character");
        e.isCharacter = true;
        _contexts.game.Instantiate(Res.Character, null, e);
    }

    private void OnDestroy()
    {
        gameObject.Unlink();
    }
}