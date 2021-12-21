using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;

public sealed class WeatherSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts _contexts;
    private static readonly int SkyTint = Shader.PropertyToID("_SkyTint");
    private static readonly int Exposure = Shader.PropertyToID("_Exposure");

    public WeatherSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
        context.CreateCollector(GameMatcher.Timestamp);

    protected override bool Filter(GameEntity entity) => entity.hasTimestamp;

    protected int[] cityTimestamps = new[]
    {
        0,
        1,
        3,
        -5,
        11,
        8
    };
    protected override void Execute(List<GameEntity> entities)
    {
        var timestamp = _contexts.game.timestamp.Value;
        var date = UnixTimeStampToDateTime(timestamp);

        var city = _contexts.game.city.Value;
        var offset = cityTimestamps[(int)city];
        
        Color c = Color.white;
        var exposure = 1.3f;
        var intensity = 1f;
        var dateHour = (date.Hour + offset) % 24;
        Debug.Log(dateHour);
        if (dateHour <= 4 || dateHour > 22)
        {
            c = _contexts.config.game.NightColor;
            exposure = _contexts.config.game.NightExposure;
            intensity = _contexts.config.game.NightLight;
        } 
        else if (dateHour > 4 || dateHour <= 10)
        {
            c = _contexts.config.game.MorningColor;
            exposure = _contexts.config.game.MorningExposure;
            intensity = _contexts.config.game.MorningLight;
        }
        else if (dateHour > 10 || dateHour <= 16)
        {
            c = _contexts.config.game.DayColor;
            exposure = _contexts.config.game.DayExposure;
            intensity = _contexts.config.game.DayLight;
        }
        else if (dateHour > 16 || dateHour <= 22)
        {
            c = _contexts.config.game.EveningColor;
            exposure = _contexts.config.game.EveningExposure;
            intensity = _contexts.config.game.EveningLight;
        }
        
        RenderSettings.skybox.SetColor(SkyTint, c);
        RenderSettings.skybox.SetFloat(Exposure, exposure);
        _contexts.game.loadingScreen.Value.SetActive(false);
        var l = GameObject.FindObjectOfType<Light>();
        l.intensity = intensity;
    }
    
    public static DateTime UnixTimeStampToDateTime( double unixTimeStamp )
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }
}
