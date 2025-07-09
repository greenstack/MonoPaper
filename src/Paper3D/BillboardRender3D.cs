using System;
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
public class BillboardRender3D(PerspectiveCamera camera, GraphicsDevice device, SizeF dimensions, Texture2D texture, BillboardAnchor anchorPoint = BillboardAnchor.MiddleCenter) : PlaneRender3D(device, dimensions, texture, anchorPoint)
{
    public override void ApplyCamera(PerspectiveCamera camera)
    {
        Effect.View = camera.ViewMatrix;
        Effect.Projection = camera.ProjectionMatrix;
        ApplyTransformMatrix();
    }

    public override void ApplyTransformMatrix()
    {
        Effect.World = Matrix.Identity;
        Effect.World *= Matrix.CreateBillboard(Transform.WorldPosition, camera.Position, Vector3.Up, Vector3.Forward);
    }
}