public sealed class CameraFeature : Feature
{
    public CameraFeature(Contexts contexts)
    {
        Add(new SpawnCameraSystem(contexts));
        Add(new SpawnCameraPlaneSystem(contexts));
        Add(new ScrollZoomSystem(contexts));
        Add(new PinchZoomSystem(contexts));
    }
}