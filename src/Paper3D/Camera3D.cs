using Microsoft.Xna.Framework;

namespace Paper3D;

/// <summary>
/// A camera that exists in 3D space to render 3D objects.
/// </summary>
/// <param name="perspective">The parameters used to create the perspective matrix.</param>
/// <param name="cameraPosition">The position of the camera.</param>
public class PerspectiveCamera
{
    private Vector3 _target;

    public Vector3 Target => _target;

    private Vector3 _position;

    public Vector3 Position => _position;

    private Matrix _projectionMatrix;
    public Matrix ProjectionMatrix => _projectionMatrix;
    private Matrix _viewMatrix;
    public Matrix ViewMatrix => _viewMatrix;
    private Matrix _worldMatrix;
    public Matrix WorldMatrix => _worldMatrix;

    public PerspectiveCamera(Vector3 position, Vector3 viewTarget, float aspectRatio, float fov, AngleUnit unit, float nearClipPlane = 1f, float farClipPlane = 1000f)
    {
        _target = viewTarget;
        _position = position;
        _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
            unit == AngleUnit.Radian ? fov : MathHelper.ToRadians(fov),
            aspectRatio,
            nearClipPlane,
            farClipPlane
        );

        RegenerateViewMatrix();
        RegenerateWorldMatrix();
    }

    /// <summary>
    /// Sets the camera's position. You'll need to call <see cref="RegenerateViewMatrix"/> once you're done with your work. 
    /// </summary>
    /// <param name="position">The new camera position;</param>
    public void SetPositionDeferred(Vector3 position)
    {
        _position = position;
    }

    /// <summary>
    /// Sets the camera's target. You'll need to call <see cref="RegenerateViewMatrix"/> once you're done with your work.
    /// </summary>
    /// <param name="target">The new target location.</param>
    public void SetTargetDeferred(Vector3 target)
    {
        _target = target;
    }

    /// <summary>
    /// Regenerates the view matrix. You should only call this method as needed
    /// </summary>
    public void RegenerateViewMatrix() => _viewMatrix = Matrix.CreateLookAt(_position, _target, Vector3.Up);
    private void RegenerateWorldMatrix() => _worldMatrix = Matrix.CreateWorld(_target, Vector3.Forward, Vector3.Up);
}
