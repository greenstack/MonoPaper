namespace Paper3D;

public enum BillboardAnchor
{
    // left right
    // top bottom
    Left = 0b0010,
    Right = 0b0001,
    Top = 0b1000,
    Bottom = 0b0100,
    TopLeft = Top | Left,
    TopCenter = Top,
    TopRight = Top | Right,
    MiddleLeft = Left,
    MiddleCenter = 0,
    MiddleRight = Right,
    BottomLeft = Bottom | Left,
    BottomCenter = Bottom,
    BottomRight = Bottom | Right,
}