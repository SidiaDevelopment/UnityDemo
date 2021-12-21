using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Unique]
public sealed class LoadingScreenComponent : IComponent
{
    public GameObject Value;
}