using Entitas;
using Entitas.CodeGeneration.Attributes;

[Event(EventTarget.Self), Event(EventTarget.Self, EventType.Removed), Event(EventTarget.Any)]
public sealed class PlaceableSelectedComponent : IComponent
{
}