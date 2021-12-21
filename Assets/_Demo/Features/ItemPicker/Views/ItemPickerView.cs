using Entitas;
using UnityEngine;

public class ItemPickerView : EntityView
    , IAnyEditmodeListener
    , IAnyEditmodeRemovedListener
{
    public GameObject ParentPrefab;
    
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        
        _entity.AddAnyEditmodeListener(this);
        _entity.AddAnyEditmodeRemovedListener(this);

        var chargo = Instantiate(ParentPrefab, transform);
        _contexts.game.Instantiate(Res.PlaceableCharacterPreview, chargo.transform);
        
        foreach (var gameEntity in _contexts.game.GetGroup(GameMatcher.Placeable))
        {
            var go = Instantiate(ParentPrefab, transform);
            _contexts.game.Instantiate(Res.PlaceableUiParent, go.transform, gameEntity);
        }
    }

    public void OnAnyEditmode(GameEntity entity)
    {
        gameObject.SetActive(true);
    }

    public void OnAnyEditmodeRemoved(GameEntity entity)
    {
        gameObject.SetActive(false);
    }
}