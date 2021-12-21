using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public sealed class CityComponent : IComponent
{
    public City Value;
}

public enum City
{
    London,
    Berlin,
    Moscow,
    NewYork,
    Sydney,
    HongKong
}