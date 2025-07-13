using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Paper3D;

/// <summary>
/// Renders a triangle list to the chosen graphics device.
/// </summary>
public class TriangleListRender3D<TVertex, TEffect> : Render3DBase<TEffect>
    where TVertex : struct, IVertexType
    where TEffect : Effect, IEffectMatrices
{
    private readonly TVertex[] _triangleVertices;

    private readonly VertexBuffer _vbo;
    protected VertexBuffer Vbo => _vbo;

    public TriangleListRender3D(GraphicsDevice device, IEnumerable<TVertex> vertices, TEffect effect = null) :
        base(effect)
    {
        _triangleVertices = [.. vertices];
        _vbo = new VertexBuffer(device, typeof(TVertex), _triangleVertices.Length, BufferUsage.WriteOnly);
        _vbo.SetData(_triangleVertices);
    }

    /// <inheritdoc/>
    public override void Draw(GraphicsDevice device)
    {
        device.SetVertexBuffer(_vbo);
        foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
        {
            pass.Apply();
        }
        device.DrawPrimitives(PrimitiveType.TriangleList, 0, _triangleVertices.Length / 3);
    }
}