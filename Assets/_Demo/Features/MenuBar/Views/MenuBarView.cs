using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarView : EntityView
    , IAnyEditmodeListener
    , IAnyEditmodeRemovedListener
{
    public GameObject EditBar;
    
    public Button SaveButton;
    public Button MoveButton;
    public Button ScaleButton;
    public Button RotateButton;    
    public Image MoveButtonImage;
    public Image ScaleButtonImage;
    public Image RotateButtonImage;
    public Button DeleteButton;
    public Button ClearButton;
    public Image CurrentMode;

    public CanvasGroup Error;

    public Button StartButton;
    public Button EditButton;

    private IGroup<GameEntity> _group;
    private IGroup<GameEntity> _characterGroup;
    private IGroup<GameEntity> _placeableGroup;
    
    private readonly List<GameEntity> _buffer = new List<GameEntity>();
    private Sequence _sequence;

    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        
        _entity.AddAnyEditmodeListener(this);
        _entity.AddAnyEditmodeRemovedListener(this);
        
        SaveButton.onClick.AddListener(OnSaveClick);
        ClearButton.onClick.AddListener(OnClearClick);
        MoveButton.onClick.AddListener(OnMoveClick);
        ScaleButton.onClick.AddListener(OnScaleClick);
        RotateButton.onClick.AddListener(OnRotateClick);
        DeleteButton.onClick.AddListener(OnDeleteClick);
        StartButton.onClick.AddListener(OnPlayClick);
        EditButton.onClick.AddListener(() => _contexts.game.isEditmode = true);

        _group = _contexts.game.GetGroup(GameMatcher.PlaceableSelected);
        _characterGroup = _contexts.game.GetGroup(GameMatcher.Character);
        _placeableGroup = _contexts.game.GetGroup(GameMatcher.PlaceableIndex);
    }

    private void OnClearClick()
    {
        foreach (var gameEntity in _placeableGroup.GetEntities(_buffer))
        {
            gameEntity.isDestroyed = true;
        }

        _contexts.game.isSave = true;
        PlayerPrefs.DeleteAll();
    }

    private void OnSaveClick()
    {
        _contexts.game.isSave = true;
    }

    private void OnPlayClick()
    {
        if (_characterGroup.count <= 0)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            Error.gameObject.SetActive(true);
            _sequence.Append(Error.DOFade(1, 0.5f));
            _sequence.AppendInterval(2);
            _sequence.Append(Error.DOFade(0, 0.5f));
            _sequence.OnComplete(() => Error.gameObject.SetActive(false));
            return;
        }
        _contexts.game.isEditmode = false;
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

    public void OnAnyEditmode(GameEntity entity)
    {
        _contexts.game.isSave = true;
        EditBar.SetActive(true);
        StartButton.gameObject.SetActive(true);
        EditButton.gameObject.SetActive(false);
        CurrentMode.gameObject.SetActive(true);
    }

    public void OnAnyEditmodeRemoved(GameEntity entity)
    {
        _contexts.game.isSave = true;
        EditBar.SetActive(false);
        StartButton.gameObject.SetActive(false);
        EditButton.gameObject.SetActive(true);
        CurrentMode.gameObject.SetActive(false);
    }
}