using SidiaKit;

public partial interface IGameConfig
{
    [ConfigHeader("Camera")]
    float DefaultZoom { get; }
    float ScrollZoomSpeed { get; }
    float ZoomLerpSpeed { get; }
}