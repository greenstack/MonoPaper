namespace Paper3D;

/// <summary>
/// Provides the necessary parameters to create a <see cref="PerspectiveCamera"/>.
/// </summary>
public readonly struct PerspectiveParameters
{
    public required float NearClipPlane { get; init; }
    public required float FarClipPlane { get; init; }

    /// <summary>
    /// The target field of view in degrees.
    /// </summary>
    public required float VerticalFieldOfView { get; init; }
    /// <summary>
    /// The current aspect ratio. This should be obtained from the graphics device.
    /// </summary>
    public required float AspectRatio { get; init; }
}