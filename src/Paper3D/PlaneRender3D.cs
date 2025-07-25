using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Paper3D;

public class PlaneRender3D : TriangleListRender3D<VertexPositionNormalTexture, AlphaTestEffect>
{
    // I chose VertexPositionNormalTexture because it's the most open type and
    // we can provide reasonable defaults for it.
    const int UVLeft = 0;
    const int UVRight = 1;
    const int UVTop = 0;
    const int UVBottom = 1;

    private static readonly Vector2 TopLeftUV = new(UVLeft, UVTop);
    private static readonly Vector2 TopRightUV = new(UVRight, UVTop);
    private static readonly Vector2 BottomLeftUV = new(UVLeft, UVBottom);
    private static readonly Vector2 BottomRightUV = new(UVRight, UVBottom);

    public PlaneRender3D(GraphicsDevice device, SizeF dimensions, Texture2D texture, bool useAlpha, BillboardAnchor anchorPoint = BillboardAnchor.MiddleCenter) :
        base(
            device,
            MakeVertices(dimensions, anchorPoint),
            new AlphaTestEffect(device)
            {
                VertexColorEnabled = false,
                Texture = texture,
            })
    {}

    private static VertexPositionNormalTexture[] MakeVertices(SizeF billboardSize, BillboardAnchor anchorPoint)
    {
        Vector3 topLeftPos, topRightPos, bottomLeftPos, bottomRightPos;

        float left, right, top, bottom;

        if ((anchorPoint & BillboardAnchor.Right) == BillboardAnchor.Right)
        {
            left = 0;
            right = billboardSize.Width;
        }
        else if ((anchorPoint & BillboardAnchor.Left) == BillboardAnchor.Left)
        {
            left = -billboardSize.Width;
            right = 0;
        }
        else // Center
        {
            left = billboardSize.Width / 2;
            right = -left;
        }

        if ((anchorPoint & BillboardAnchor.Top) == BillboardAnchor.Top)
        {
            top = 0;
            bottom = -billboardSize.Height;
        }
        else if ((anchorPoint & BillboardAnchor.Bottom) == BillboardAnchor.Bottom)
        {
            top = billboardSize.Height;
            bottom = 0;
        }
        else // Middle
        {
            top = billboardSize.Height / 2;
            bottom = -top;
        }

        // Forward is along the z-axis
        topLeftPos = new Vector3(left, top, 0); // 0
        topRightPos = new Vector3(right, top, 0); // 1
        bottomLeftPos = new Vector3(left, bottom, 0); // 2
        bottomRightPos = new Vector3(right, bottom, 0); // 3

        VertexPositionNormalTexture topLeft, topRight, bottomLeft, bottomRight;

        topLeft = new(topLeftPos, Vector3.Forward, TopLeftUV); // 0
        topRight = new(topRightPos, Vector3.Forward, TopRightUV); // 1
        bottomLeft = new(bottomLeftPos, Vector3.Forward, BottomLeftUV); // 2
        bottomRight = new(bottomRightPos, Vector3.Forward, BottomRightUV); // 3
        return [
            topRight, bottomLeft, topLeft,
            bottomRight, bottomLeft, topRight,
        ];
    }
}