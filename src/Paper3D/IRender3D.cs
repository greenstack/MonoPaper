using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paper3D;

/// <summary>
/// Provides an interface for 3D rendering.
/// </summary>
public interface IRender3D
{
    /// <summary>
    /// The effect used to render the model.
    /// </summary>
    BasicEffect Effect { get; }

    // TODO: Introduce a camera interface
    void ApplyCamera(PerspectiveCamera camera);

    /// <summary>
    /// Draws the mesh to the targeted graphics device.
    /// </summary>
    /// <param name="device">The graphics device to draw to</param>
    void Draw(GraphicsDevice device);

    /// <summary>
    /// Moves the render by the desired amount in local space.
    /// </summary>
    /// <param name="translationDelta">The amount to move the render by.</param>
    public void Translate(Vector3 translationDelta);

    /// <summary>
    /// Sets the rotation in local space.
    /// </summary>
    /// <param name="position">The desired position.</param>
    public void SetPosition(Vector3 position);

    /// <summary>
    /// Applies a rotation delta in local space.
    /// </summary>
    /// <param name="rotationDelta">The rotation delta.</param>
    public void Rotate(Quaternion rotationDelta);

    /// <summary>
    /// Sets the rotation in local space.
    /// </summary>
    /// <param name="rotation">The desired rotation.</param>
    public void SetRotation(Quaternion rotation);
}