using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Paper3D;

/// <summary>
/// How do I move these
/// </summary>
public class PrimitiveRender3D : IRender3D
{
    public BasicEffect Effect { get; }

    private VertexPositionColor[] _triangleVertices;

    private VertexBuffer _vbo;

    public PrimitiveRender3D(GraphicsDevice device, IEnumerable<VertexPositionColor> vertices)
    {
        // TODO: We probably want to pass this in manually
        Effect = new BasicEffect(device);
        Effect.Alpha = 1f;
        Effect.VertexColorEnabled = true;
        Effect.LightingEnabled = false;

        _triangleVertices = [.. vertices];
        _vbo = new VertexBuffer(device, typeof(VertexPositionColor), _triangleVertices.Length, BufferUsage.WriteOnly);
        _vbo.SetData(_triangleVertices);
    }

    // TODO: This probably needs to be virtual - it makes sense for the eventual
    // billboard render to inherit from this class.
    public void ApplyCamera(PerspectiveCamera camera)
    {
        Effect.View = camera.ViewMatrix;
        // TODO: Apply a transform matrix to this spot right here
        Effect.World = camera.WorldMatrix;
        Effect.Projection = camera.ProjectionMatrix;
    }

    public void Draw(GraphicsDevice device)
    {
        device.SetVertexBuffer(_vbo);
        foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
        {
            pass.Apply();
        }
        device.DrawPrimitives(PrimitiveType.TriangleList, 0, _triangleVertices.Length);
    }
}