public sealed class LoadSaveFeature : Feature
{
    public LoadSaveFeature(Contexts contexts)
    {
        Add(new LoadSaveSystem(contexts));
    }
}