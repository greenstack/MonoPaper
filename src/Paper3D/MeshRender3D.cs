using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Paper3D;

public class ModelRender3D : IRender3D
{
    readonly Model _model;

    private Transform3 _transform = new();

    public ModelRender3D(Model model)
    {
        _model = model;
    }

    public BasicEffect Effect => null;

    public void ApplyCamera(PerspectiveCamera camera)
    {
        foreach (var mesh in _model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects.Cast<BasicEffect>())
            {
                effect.AmbientLightColor = new Vector3(1f, 0, 0);
                effect.View = camera.ViewMatrix;
                effect.Projection = camera.ProjectionMatrix;
                effect.World = camera.WorldMatrix;
                ApplyTransformMatrix();
            }
        }
    }

    public void ApplyTransformMatrix()
    {
        foreach (var mesh in _model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects.Cast<BasicEffect>())
            {
                effect.World *= _transform.WorldMatrix;
            }
        }
    }

    public void Draw(GraphicsDevice device)
    {
        foreach (var mesh in _model.Meshes)
        {
            mesh.Draw();
        }
    }

    public void Rotate(Quaternion rotationDelta)
    {
        throw new System.NotImplementedException();
    }

    public void SetPosition(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public void SetRotation(Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

    public void Translate(Vector3 translationDelta)
    {
        throw new System.NotImplementedException();
    }
}