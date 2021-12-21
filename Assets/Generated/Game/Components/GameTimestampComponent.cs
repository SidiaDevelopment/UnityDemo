//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity timestampEntity { get { return GetGroup(GameMatcher.Timestamp).GetSingleEntity(); } }
    public TimestampComponent timestamp { get { return timestampEntity.timestamp; } }
    public bool hasTimestamp { get { return timestampEntity != null; } }

    public GameEntity SetTimestamp(long newValue) {
        if (hasTimestamp) {
            throw new Entitas.EntitasException("Could not set Timestamp!\n" + this + " already has an entity with TimestampComponent!",
                "You should check if the context already has a timestampEntity before setting it or use context.ReplaceTimestamp().");
        }
        var entity = CreateEntity();
        entity.AddTimestamp(newValue);
        return entity;
    }

    public void ReplaceTimestamp(long newValue) {
        var entity = timestampEntity;
        if (entity == null) {
            entity = SetTimestamp(newValue);
        } else {
            entity.ReplaceTimestamp(newValue);
        }
    }

    public void RemoveTimestamp() {
        timestampEntity.Destroy();
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

    public TimestampComponent timestamp { get { return (TimestampComponent)GetComponent(GameComponentsLookup.Timestamp); } }
    public bool hasTimestamp { get { return HasComponent(GameComponentsLookup.Timestamp); } }

    public void AddTimestamp(long newValue) {
        var index = GameComponentsLookup.Timestamp;
        var component = (TimestampComponent)CreateComponent(index, typeof(TimestampComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTimestamp(long newValue) {
        var index = GameComponentsLookup.Timestamp;
        var component = (TimestampComponent)CreateComponent(index, typeof(TimestampComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTimestamp() {
        RemoveComponent(GameComponentsLookup.Timestamp);
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

    static Entitas.IMatcher<GameEntity> _matcherTimestamp;

    public static Entitas.IMatcher<GameEntity> Timestamp {
        get {
            if (_matcherTimestamp == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Timestamp);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTimestamp = matcher;
            }

            return _matcherTimestamp;
        }
    }
}
