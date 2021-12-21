using SidiaKit;
using UnityEngine;

public partial interface IGameConfig
{
    [ConfigHeader("Skybox")]
    Color DayColor { get; }
    float DayExposure { get; }
    float DayLight { get; }
    Color EveningColor { get; }
    float EveningExposure { get; }
    float EveningLight { get; }
    Color NightColor { get; }
    float NightExposure { get; }
    float NightLight { get; }
    Color MorningColor { get; }
    float MorningExposure { get; }
    float MorningLight { get; }
}