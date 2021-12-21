using SidiaKit;
using UnityEngine;

public partial interface IGameConfig
{
    [ConfigHeader("Placeables")]
    Color HighlightColor { get; }
    
    [ConfigHeader("Placeables Debug")]
    bool DrawWireframe { get; }
}