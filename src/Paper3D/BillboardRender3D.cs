using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Paper3D;

/// <summary>
/// A billboard render.
/// </summary>
/// <param name="device">The graphics device this mesh will belong to.</param>
/// <param name="dimensions">The dimensions of this billboard.</param>
/// <param name="texture">The texture this billboard will have.</param>
/// <param name="anchorPoint">The anchor point of the billboard.</param>
public class BillboardRender3D(
    PerspectiveCamera camera,
    GraphicsDevice device,
    SizeF dimensions,
    Texture2D texture,
    bool useAlpha,
    BillboardAnchor anchorPoint = BillboardAnchor.MiddleCenter
    ) : PlaneRender3D(device, dimensions, texture, useAlpha, anchorPoint)
{
    public override void ApplyCamera(PerspectiveCamera camera)
    {
        Effect.View = camera.ViewMatrix;
        Effect.Projection = camera.ProjectionMatrix;
        Effect.World = camera.WorldMatrix;
        ApplyTransformMatrix();
    }

    public override void ApplyTransformMatrix()
    {
        Vector3 forward = -camera.Forward;
        forward.Y = 0;
        if (forward.LengthSquared() < 0.0001f) forward = Vector3.Forward;
        forward.Normalize();
        // Thanks to u/sethvl on the MonoGame subreddit
        Effect.World = Matrix.CreateWorld(Transform.WorldPosition, forward, Vector3.Up);
        // This will always face the camera, but not perfectly
        //Effect.World = Matrix.CreateBillboard(Transform.WorldPosition, camera.Position, Vector3.Up, Vector3.Up);
        //Effect.World = Matrix.CreateConstrainedBillboard(Transform.WorldPosition, camera.Position, Vector3.UnitX, camera.Forward, null);
    }
}