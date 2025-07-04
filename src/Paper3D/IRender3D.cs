using Microsoft.Xna.Framework.Graphics;

namespace Paper3D;

public interface IRender3D
{
    BasicEffect Effect { get; }

    // TODO: Introduce a camera interface
    void ApplyCamera(PerspectiveCamera camera);

    void Draw(GraphicsDevice device);
}