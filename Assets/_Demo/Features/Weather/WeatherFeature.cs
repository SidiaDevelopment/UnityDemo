public sealed class WeatherFeature : Feature
{
    public WeatherFeature(Contexts contexts)
    {
        Add(new WeatherSystem(contexts));
    }
}