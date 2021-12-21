using DG.Tweening;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class CityPickerView : EntityView
    , IAnyEditmodeListener
    , IAnyEditmodeRemovedListener
{
    public Button[] CityButtons;

    public Button CloseButton;
    public Button OpenButton;
    
    public override void Link(Contexts contexts, GameEntity entity)
    {
        base.Link(contexts, entity);
        _entity.AddAnyEditmodeListener(this);
        _entity.AddAnyEditmodeRemovedListener(this);

        for (var index = 0; index < CityButtons.Length; index++)
        {
            var cityButton = CityButtons[index];
            var i = index;
            cityButton.onClick.AddListener(() => _contexts.game.ReplaceCity((City)i));
        }
        
        OpenButton.onClick.AddListener(() =>
        {
            transform.GetComponent<RectTransform>().DOAnchorPosX(0, 1);
            OpenButton.gameObject.SetActive(false);
            CloseButton.gameObject.SetActive(true);
        });
        CloseButton.onClick.AddListener(() =>
        {
            transform.GetComponent<RectTransform>().DOAnchorPosX(400, 1);
            OpenButton.gameObject.SetActive(true);
            CloseButton.gameObject.SetActive(false);
        });
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