using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

[Serializable]
public struct SavedMap
{
    public Placeable[] Placeables;
}

[Serializable]
public struct Placeable
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public string Index;
    public bool IsCharacter;
}

public sealed class LoadSaveSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _group;

    public LoadSaveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _group = _contexts.game.GetGroup(GameMatcher.PlaceableIndex);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
        context.CreateCollector(GameMatcher.AnyOf(GameMatcher.PlaceableIndex, GameMatcher.Editmode, GameMatcher.Save));

    protected override bool Filter(GameEntity entity) => true;

    private const string Key = "SaveState";
    protected override void Execute(List<GameEntity> entities)
    {
        var list = new List<Placeable>();
        foreach (var gameEntity in _group)
        {

            var index = gameEntity.placeableIndex.Value;
            var position = gameEntity.placeablePosition.Value;
            var rotation = gameEntity.placeableRotation.Value;
            var scale = gameEntity.placeableScale.Value;
            var isCharacter = gameEntity.isCharacter;
            
            list.Add(new Placeable()
            {
                Position = position,
                Rotation = rotation,
                Scale = scale,
                Index = index,
                IsCharacter = isCharacter
            });
        }

        var savestate = new SavedMap()
        {
            Placeables = list.ToArray()
        };

        var text = JsonUtility.ToJson(savestate);
        PlayerPrefs.SetString(Key, text);
        _contexts.game.isSave = false;
    }

    public void Initialize()
    {
        if (!PlayerPrefs.HasKey(Key)) return;
        
        _contexts.game.isLoading = true;
        var data = JsonUtility.FromJson<SavedMap>(PlayerPrefs.GetString(Key));

        foreach (var placeable in data.Placeables)
        {
            if (placeable.IsCharacter)
            {
                var c = _contexts.game.CreateEntity();
                c.AddPlaceableIndex("Character");
                c.isCharacter = true;
                c.AddPlaceablePosition(placeable.Position);
                c.AddPlaceableRotation(placeable.Rotation);
                c.AddPlaceableScale(placeable.Scale);
                _contexts.game.Instantiate(Res.Character, null, c);
                continue;
            }
            var e = _contexts.game.CreateEntity();
            e.AddPlaceablePosition(placeable.Position);
            e.AddPlaceableRotation(placeable.Rotation);
            e.AddPlaceableScale(placeable.Scale);
            e.AddPlaceableIndex(placeable.Index);
            _contexts.game.Instantiate(Res.Placeable, null, e);
        }
        _contexts.game.isLoading = false;
    }
}
