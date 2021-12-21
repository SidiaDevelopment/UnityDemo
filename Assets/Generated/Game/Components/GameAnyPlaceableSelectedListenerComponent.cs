//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnyPlaceableSelectedListenerComponent anyPlaceableSelectedListener { get { return (AnyPlaceableSelectedListenerComponent)GetComponent(GameComponentsLookup.AnyPlaceableSelectedListener); } }
    public bool hasAnyPlaceableSelectedListener { get { return HasComponent(GameComponentsLookup.AnyPlaceableSelectedListener); } }

    public void AddAnyPlaceableSelectedListener(System.Collections.Generic.List<IAnyPlaceableSelectedListener> newValue) {
        var index = GameComponentsLookup.AnyPlaceableSelectedListener;
        var component = (AnyPlaceableSelectedListenerComponent)CreateComponent(index, typeof(AnyPlaceableSelectedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyPlaceableSelectedListener(System.Collections.Generic.List<IAnyPlaceableSelectedListener> newValue) {
        var index = GameComponentsLookup.AnyPlaceableSelectedListener;
        var component = (AnyPlaceableSelectedListenerComponent)CreateComponent(index, typeof(AnyPlaceableSelectedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyPlaceableSelectedListener() {
        RemoveComponent(GameComponentsLookup.AnyPlaceableSelectedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherAnyPlaceableSelectedListener;

    public static Entitas.IMatcher<GameEntity> AnyPlaceableSelectedListener {
        get {
            if (_matcherAnyPlaceableSelectedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnyPlaceableSelectedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnyPlaceableSelectedListener = matcher;
            }

            return _matcherAnyPlaceableSelectedListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddAnyPlaceableSelectedListener(IAnyPlaceableSelectedListener value) {
        var listeners = hasAnyPlaceableSelectedListener
            ? anyPlaceableSelectedListener.value
            : new System.Collections.Generic.List<IAnyPlaceableSelectedListener>();
        listeners.Add(value);
        ReplaceAnyPlaceableSelectedListener(listeners);
    }

    public void RemoveAnyPlaceableSelectedListener(IAnyPlaceableSelectedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyPlaceableSelectedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyPlaceableSelectedListener();
        } else {
            ReplaceAnyPlaceableSelectedListener(listeners);
        }
    }
}
