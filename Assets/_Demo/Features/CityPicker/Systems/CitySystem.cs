using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CitySystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    readonly Contexts _contexts;

    public CitySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
        context.CreateCollector(GameMatcher.City);

    protected override bool Filter(GameEntity entity) => entity.hasCity;

    public string[] cities = new[]
    {
        "London",
        "Berlin",
        "Moscow",
        "New York",
        "Sydney",
        "Hong kong",
    };

    private string Key = "City";
    protected override void Execute(List<GameEntity> entities)
    {
        var city = _contexts.game.city.Value;
        
        PlayerPrefs.SetInt(Key, (int)city);
        
        var g = new GraphQl();
        g.Request(cities[(int)city]);
    }


    public void Initialize()
    {
        var city = PlayerPrefs.GetInt(Key, 0);
        _contexts.game.SetCity((City)city);

        _contexts.game.Instantiate(Res.CityPicker, _contexts.game.uiParent.Value);
    }
}
