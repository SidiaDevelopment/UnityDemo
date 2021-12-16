using Entitas;
using Entitas.CodeGeneration.Attributes;

[Event(EventTarget.Self), Event(EventTarget.Self, EventType.Removed)]
public sealed class UiElementVisibleComponent : IComponent
{
}