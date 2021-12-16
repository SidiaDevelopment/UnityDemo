public sealed class ItemPickerFeature : Feature
{
    public ItemPickerFeature(Contexts contexts)
    {
        Add(new LoadItemPickerSystem(contexts));
    }
}