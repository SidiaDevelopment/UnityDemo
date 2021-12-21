using System;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class MobileControlView : EntityView
    , IAnyEditmodeListener
    , IAnyEditmodeRemovedListener
{
    public FixedJoystick Joystick;
    public Button JumpButton;
    
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        _entity.AddAnyEditmodeListener(this);
        _entity.AddAnyEditmodeRemovedListener(this);
        
        gameObject.SetActive(_contexts.game.isEditmode);
        JumpButton.onClick.AddListener(() => _contexts.game.isJump = true);
    }

    public void OnAnyEditmode(GameEntity entity)
    {
        gameObject.SetActive(false);
    }

    public void OnAnyEditmodeRemoved(GameEntity entity)
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        _contexts.game.ReplaceMovement(Joystick.Direction);
    }
}