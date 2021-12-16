//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnyPinchRemovedListenerComponent anyPinchRemovedListener { get { return (AnyPinchRemovedListenerComponent)GetComponent(GameComponentsLookup.AnyPinchRemovedListener); } }
    public bool hasAnyPinchRemovedListener { get { return HasComponent(GameComponentsLookup.AnyPinchRemovedListener); } }

    public void AddAnyPinchRemovedListener(System.Collections.Generic.List<IAnyPinchRemovedListener> newValue) {
        var index = GameComponentsLookup.AnyPinchRemovedListener;
        var component = (AnyPinchRemovedListenerComponent)CreateComponent(index, typeof(AnyPinchRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyPinchRemovedListener(System.Collections.Generic.List<IAnyPinchRemovedListener> newValue) {
        var index = GameComponentsLookup.AnyPinchRemovedListener;
        var component = (AnyPinchRemovedListenerComponent)CreateComponent(index, typeof(AnyPinchRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyPinchRemovedListener() {
        RemoveComponent(GameComponentsLookup.AnyPinchRemovedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherAnyPinchRemovedListener;

    public static Entitas.IMatcher<GameEntity> AnyPinchRemovedListener {
        get {
            if (_matcherAnyPinchRemovedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnyPinchRemovedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnyPinchRemovedListener = matcher;
            }

            return _matcherAnyPinchRemovedListener;
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

    public void AddAnyPinchRemovedListener(IAnyPinchRemovedListener value) {
        var listeners = hasAnyPinchRemovedListener
            ? anyPinchRemovedListener.value
            : new System.Collections.Generic.List<IAnyPinchRemovedListener>();
        listeners.Add(value);
        ReplaceAnyPinchRemovedListener(listeners);
    }

    public void RemoveAnyPinchRemovedListener(IAnyPinchRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyPinchRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyPinchRemovedListener();
        } else {
            ReplaceAnyPinchRemovedListener(listeners);
        }
    }
}
