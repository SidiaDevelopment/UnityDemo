public sealed class DemoFeatures : Feature
{
    public DemoFeatures(Contexts contexts)
    {
        Add(new GameEventSystems(contexts));

        Add(new CameraFeature(contexts));
        Add(new UiParentFeature(contexts));
        Add(new ItemPickerFeature(contexts));
        
        Add(new GameCleanupSystems(contexts));
    }
}