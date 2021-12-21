using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

public sealed class PlaceableComponent : IComponent
{
    [PrimaryEntityIndex]
    public string Name;
    public GameObject Value;
}