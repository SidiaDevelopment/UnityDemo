public sealed class MenuBarFeature : Feature
{
    public MenuBarFeature(Contexts contexts)
    {
        Add(new LoadMenuBarSystem(contexts));
    }
}