//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity cityEntity { get { return GetGroup(GameMatcher.City).GetSingleEntity(); } }
    public CityComponent city { get { return cityEntity.city; } }
    public bool hasCity { get { return cityEntity != null; } }

    public GameEntity SetCity(City newValue) {
        if (hasCity) {
            throw new Entitas.EntitasException("Could not set City!\n" + this + " already has an entity with CityComponent!",
                "You should check if the context already has a cityEntity before setting it or use context.ReplaceCity().");
        }
        var entity = CreateEntity();
        entity.AddCity(newValue);
        return entity;
    }

    public void ReplaceCity(City newValue) {
        var entity = cityEntity;
        if (entity == null) {
            entity = SetCity(newValue);
        } else {
            entity.ReplaceCity(newValue);
        }
    }

    public void RemoveCity() {
        cityEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CityComponent city { get { return (CityComponent)GetComponent(GameComponentsLookup.City); } }
    public bool hasCity { get { return HasComponent(GameComponentsLookup.City); } }

    public void AddCity(City newValue) {
        var index = GameComponentsLookup.City;
        var component = (CityComponent)CreateComponent(index, typeof(CityComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCity(City newValue) {
        var index = GameComponentsLookup.City;
        var component = (CityComponent)CreateComponent(index, typeof(CityComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCity() {
        RemoveComponent(GameComponentsLookup.City);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCity;

    public static Entitas.IMatcher<GameEntity> City {
        get {
            if (_matcherCity == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.City);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCity = matcher;
            }

            return _matcherCity;
        }
    }
}