using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace Paper3D;

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

    public void ApplyCamera(PerspectiveCamera camera)
    {
        Effect.View = camera.ViewMatrix;
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