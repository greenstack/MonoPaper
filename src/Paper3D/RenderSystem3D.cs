using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;

namespace Paper3D;

/// <summary>
/// A simple system that renders 3D objects to a world.
/// </summary>
/// <param name="device">The device being drawn to.</param>
/// <param name="camera">The camera that defines this 3D space.</param>
/// <param name="rasterizerState">The default rasterizer state for this system.</param>
public class RenderSystem3D(GraphicsDevice device, PerspectiveCamera camera, RasterizerState rasterizerState) : EntityDrawSystem(Aspect.All(typeof(IRender3D)))
{
    ComponentMapper<IRender3D> _3dRenders;

    public override void Initialize(IComponentMapperService mapperService)
    {
        _3dRenders = mapperService.GetMapper<IRender3D>();
    }

    public override void Draw(GameTime gameTime)
    {
        // TODO: Does this get cleared each frame at the GraphicsDevice.Clear stage?
        // If not, we can absolutely remove this call from each draw
        device.RasterizerState = rasterizerState;

        foreach (var entity in ActiveEntities)
        {
            IRender3D render = _3dRenders.Get(entity);
            render.ApplyCamera(camera);
            // foreach (EffectPass pass in render.Effect.CurrentTechnique.Passes)
            // {
            //     pass.Apply();
            // }
            render.Draw(device);
        }

        throw new System.NotImplementedException();
    }
}