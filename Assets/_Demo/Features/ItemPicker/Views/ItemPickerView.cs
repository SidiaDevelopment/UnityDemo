using Entitas;
using UnityEngine;

public class ItemPickerView : UiElementView
{
    public GameObject ParentPrefab;
    
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);

        foreach (var gameEntity in _contexts.game.GetGroup(GameMatcher.Placeable))
        {
            var go = Instantiate(ParentPrefab, transform);
            _contexts.game.Instantiate(Res.PlaceableUiParent, go.transform, gameEntity);
        }
    }
}