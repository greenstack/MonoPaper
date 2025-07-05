using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Paper3D;

public class BillboardRender3D : TriangleListRender3D
{
    public BillboardRender3D(GraphicsDevice device, IEnumerable<VertexPositionColor> vertices) : base(device, vertices)
    {
    }
}