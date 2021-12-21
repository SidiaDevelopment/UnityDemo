public sealed class PlaceableFeature : Feature
{
    public PlaceableFeature(Contexts contexts)
    {
        Add(new LoadPlaceablesSystem(contexts));
    }
}