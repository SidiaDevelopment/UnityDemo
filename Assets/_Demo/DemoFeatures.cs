public sealed class DemoFeatures : Feature
{
    public DemoFeatures(Contexts contexts)
    {
        Add(new GameEventSystems(contexts));

        Add(new EditmodeFeature(contexts));
        Add(new CameraFeature(contexts));
        Add(new UiParentFeature(contexts));
        Add(new MenuBarFeature(contexts));
        Add(new CharacterMovementFeature(contexts));
        Add(new PlaceableFeature(contexts));
        Add(new ItemPickerFeature(contexts));
        Add(new LoadSaveFeature(contexts));
        Add(new WeatherFeature(contexts));
        Add(new CityFeature(contexts));

        Add(new GameCleanupSystems(contexts));
    }
}