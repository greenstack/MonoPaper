using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paper3D;

/// <summary>
/// A camera that exists in 3D space to render 3D objects.
/// </summary>
/// <param name="perspective">The parameters used to create the perspective matrix.</param>
/// <param name="cameraPosition">The position of the camera.</param>
public class Camera3D(PerspectiveParameters perspective, Vector3 cameraPosition)
{
    // TODO: Integrate field of view changes, among other things
    Matrix _viewMatrix = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
    /// <summary>
    /// The camera's current view matrix.
    /// </summary>
    public Matrix ViewMatrix => _viewMatrix;

    /// <summary>
    /// The camera's current projection matrix.
    /// </summary>
    Matrix _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
            perspective.VerticalFieldOfView,
            perspective.AspectRatio,
            perspective.NearClipPlane,
            perspective.FarClipPlane);
    public Matrix ProjectionMatrix => _projectionMatrix;

    Matrix _worldMatrix = Matrix.CreateWorld(cameraPosition, Vector3.Forward, Vector3.Up);
    /// <summary>
    /// The camera's current world matrix.
    /// </summary>
    public Matrix WorldMatrix => _worldMatrix;

    private Vector3 _position;
    /// <summary>
    /// The current position of the camera.
    /// </summary>
    public Vector3 Position => _position;

    /// <summary>
    /// Jumps the camera to the target position.
    /// </summary>
    /// <param name="position">The camera's new position.</param>
    public void SetPosition(Vector3 position)
    {
        _position = position;
        RegenerateViewMatrix();
    }

    /// <summary>
    /// Moves the camera by the desired offset.
    /// </summary>
    /// <param name="offset">The amount to move the camera by.</param>
    public void Translate(Vector3 offset)
    {
        _position += offset;
        RegenerateViewMatrix();
    }

    /// <summary>
    /// Recreates the view matrix. Should be called whenever the camera's position is changed.
    /// </summary>
    private void RegenerateViewMatrix()
    {
        _viewMatrix = Matrix.CreateLookAt(_position, Vector3.Zero, Vector3.Up);
    }
}
