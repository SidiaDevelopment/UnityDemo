using Entitas;
using UnityEngine;

public sealed class LoadPlaceablesSystem : IInitializeSystem
{
    readonly Contexts _contexts;

    public LoadPlaceablesSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var placeables = Resources.LoadAll<GameObject>("PlaceablePrefabs");

        foreach (var placeable in placeables)
        {
            var e = _contexts.game.CreateEntity();
            e.AddPlaceable(placeable.name, placeable);
        }
    }
}
