public sealed class CityFeature : Feature
{
    public CityFeature(Contexts contexts)
    {
        Add(new CitySystem(contexts));
    }
}