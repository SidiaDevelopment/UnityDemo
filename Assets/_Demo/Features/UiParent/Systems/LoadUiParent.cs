using Entitas;
using UnityEngine;

public sealed class LoadUiParent : IInitializeSystem
{
    readonly Contexts _contexts;

    public LoadUiParent(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var parent = _contexts.game.Instantiate(Res.UiParent);
        var transform = parent.view.Value.gameObject.transform;
        
        var canvas = parent.view.Value.GetComponent<Canvas>();
        var cameraView = (CameraView)_contexts.game.cameraEntity.view.Value;
        canvas.worldCamera = cameraView.UiCamera;
        
        _contexts.game.SetUiParent(transform);
    }
}
