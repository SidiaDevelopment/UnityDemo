public sealed class UiParentFeature : Feature
{
    public UiParentFeature(Contexts contexts)
    {
        Add(new LoadUiParent(contexts));
    }
}