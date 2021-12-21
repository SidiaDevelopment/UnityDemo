//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PlaceableSelectedRemovedListenerComponent placeableSelectedRemovedListener { get { return (PlaceableSelectedRemovedListenerComponent)GetComponent(GameComponentsLookup.PlaceableSelectedRemovedListener); } }
    public bool hasPlaceableSelectedRemovedListener { get { return HasComponent(GameComponentsLookup.PlaceableSelectedRemovedListener); } }

    public void AddPlaceableSelectedRemovedListener(System.Collections.Generic.List<IPlaceableSelectedRemovedListener> newValue) {
        var index = GameComponentsLookup.PlaceableSelectedRemovedListener;
        var component = (PlaceableSelectedRemovedListenerComponent)CreateComponent(index, typeof(PlaceableSelectedRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePlaceableSelectedRemovedListener(System.Collections.Generic.List<IPlaceableSelectedRemovedListener> newValue) {
        var index = GameComponentsLookup.PlaceableSelectedRemovedListener;
        var component = (PlaceableSelectedRemovedListenerComponent)CreateComponent(index, typeof(PlaceableSelectedRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePlaceableSelectedRemovedListener() {
        RemoveComponent(GameComponentsLookup.PlaceableSelectedRemovedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherPlaceableSelectedRemovedListener;

    public static Entitas.IMatcher<GameEntity> PlaceableSelectedRemovedListener {
        get {
            if (_matcherPlaceableSelectedRemovedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlaceableSelectedRemovedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlaceableSelectedRemovedListener = matcher;
            }

            return _matcherPlaceableSelectedRemovedListener;
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

    public void AddPlaceableSelectedRemovedListener(IPlaceableSelectedRemovedListener value) {
        var listeners = hasPlaceableSelectedRemovedListener
            ? placeableSelectedRemovedListener.value
            : new System.Collections.Generic.List<IPlaceableSelectedRemovedListener>();
        listeners.Add(value);
        ReplacePlaceableSelectedRemovedListener(listeners);
    }

    public void RemovePlaceableSelectedRemovedListener(IPlaceableSelectedRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = placeableSelectedRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemovePlaceableSelectedRemovedListener();
        } else {
            ReplacePlaceableSelectedRemovedListener(listeners);
        }
    }
}
