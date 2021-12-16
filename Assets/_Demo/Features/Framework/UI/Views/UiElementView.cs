using Entitas;
using UnityEngine;

public class UiElementView : EntityView
    , IUiElementVisibleListener
    , IUiElementVisibleRemovedListener
{
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        _entity.AddUiElementVisibleListener(this);
        _entity.AddUiElementVisibleRemovedListener(this);

        if (_entity.isUiElementVisible)
        {
            OnUiElementVisible(null);
        }
        else
        {
            OnUiElementVisibleRemoved(null);
        }
    }

    public void OnUiElementVisible(GameEntity entity)
    {
        gameObject.SetActive(true);
    }

    public void OnUiElementVisibleRemoved(GameEntity entity)
    {
        gameObject.SetActive(false);
    }
}