//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly UiElementVisibleComponent uiElementVisibleComponent = new UiElementVisibleComponent();

    public bool isUiElementVisible {
        get { return HasComponent(GameComponentsLookup.UiElementVisible); }
        set {
            if (value != isUiElementVisible) {
                var index = GameComponentsLookup.UiElementVisible;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : uiElementVisibleComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherUiElementVisible;

    public static Entitas.IMatcher<GameEntity> UiElementVisible {
        get {
            if (_matcherUiElementVisible == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.UiElementVisible);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUiElementVisible = matcher;
            }

            return _matcherUiElementVisible;
        }
    }
}