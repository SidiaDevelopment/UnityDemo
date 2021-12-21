using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarView : EntityView
{
    public Button MoveButton;
    public Button ScaleButton;
    public Button RotateButton;    
    public Image MoveButtonImage;
    public Image ScaleButtonImage;
    public Image RotateButtonImage;
    public Button DeleteButton;
    public Image CurrentMode;

    private IGroup<GameEntity> _group;
    private readonly List<GameEntity> _buffer = new List<GameEntity>();

    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        
        MoveButton.onClick.AddListener(OnMoveClick);
        ScaleButton.onClick.AddListener(OnScaleClick);
        RotateButton.onClick.AddListener(OnRotateClick);
        DeleteButton.onClick.AddListener(OnDeleteClick);

        _group = _contexts.game.GetGroup(GameMatcher.PlaceableSelected);
    }

    private void OnMoveClick()
    {
        _contexts.game.ReplacePlaceableTransformMode(TransformMode.Position);
        CurrentMode.sprite = MoveButtonImage.sprite;
    }

    private void OnScaleClick()
    {
        _contexts.game.ReplacePlaceableTransformMode(TransformMode.Scale);
        CurrentMode.sprite = ScaleButtonImage.sprite;
    }

    private void OnRotateClick()
    {
        _contexts.game.ReplacePlaceableTransformMode(TransformMode.Rotation);
        CurrentMode.sprite = RotateButtonImage.sprite;
    }

    private void OnDeleteClick()
    {
        if (_group.count <= 0) return;

        foreach (var gameEntity in _group.GetEntities(_buffer))
        {
            gameEntity.isDestroyed = true;
        }
    }
}