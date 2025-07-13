using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Paper3D;

/// <summary>
/// Base class to provide common functionality for 3D renders
/// </summary>
/// <param name="effect"></param>
public abstract class Render3DBase<TEffect>(TEffect effect) : IRender3D<TEffect>
    where TEffect : Effect, IEffectMatrices
{
    /// <summary>
    /// The effect used to render this 3D model
    /// </summary>
    public TEffect Effect { get; } = effect;

    /// <summary>
    /// The transform of this render.
    /// </summary>
    public Transform3 Transform { get; } = new();

    public virtual void ApplyCamera(PerspectiveCamera camera)
    {
        Effect.View = camera.ViewMatrix;
        Effect.Projection = camera.ProjectionMatrix;
        Effect.World = camera.WorldMatrix;
        ApplyTransformMatrix();
    }

    public abstract void Draw(GraphicsDevice device);

    public void Rotate(Quaternion rotationDelta)
    {
        Transform.Rotation += rotationDelta;
        ApplyTransformMatrix();
    }

    public void SetPosition(Vector3 position)
    {
        Transform.Position = position;
        ApplyTransformMatrix();
    }

    public void SetRotation(Quaternion rotation)
    {
        Transform.Rotation = rotation;
        ApplyTransformMatrix();
    }

    public void Translate(Vector3 translationDelta)
    {
        Transform.Position += translationDelta;
        ApplyTransformMatrix();
    }

    public virtual void ApplyTransformMatrix()
    {
        // TODO: We need to be more clever about this!
        Effect.World *= Transform.WorldMatrix;
    }
}