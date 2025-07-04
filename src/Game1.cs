using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paper3D;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Camera
    // Vector3 camTarget;
    // Vector3 camPosition;
    // Matrix projectionMatrix;
    // Matrix viewMatrix;
    // Matrix worldMatrix;

    // Basic effect
    BasicEffect basicEffect;

    // Geometric info
    VertexPositionColor[] triangleVertices;
    VertexBuffer vertexBuffer;

    bool orbit = false;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    PerspectiveCamera _camera;

    protected override void Initialize()
    {
        base.Initialize();

        _camera = new(
            new Vector3(0, 0, -100f),
            Vector3.Zero,
            GraphicsDevice.DisplayMode.AspectRatio,
            45f,
            AngleUnit.Degree,
            1f,
            1000f
        );

        //camTarget = new Vector3();
        //camPosition = new Vector3(0, 0, -100f);
        //projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
        //    MathHelper.ToRadians(45f),
        //    GraphicsDevice.DisplayMode.AspectRatio,
        //    1f,
        //    1000f
        //);

        //viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
        //worldMatrix = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);

        // BasicEffect
        basicEffect = new BasicEffect(GraphicsDevice);
        basicEffect.Alpha = 1f;
        basicEffect.VertexColorEnabled = true;

        basicEffect.LightingEnabled = false;

        triangleVertices = new VertexPositionColor[3];
        triangleVertices[0] = new VertexPositionColor(new Vector3(0, 20, 0), Color.Red);

        triangleVertices[1] = new VertexPositionColor(new Vector3(-20, -20, 0), Color.Green);

        triangleVertices[2] = new VertexPositionColor(new Vector3(20, -20, 0), Color.Blue);

        vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
        vertexBuffer.SetData(triangleVertices);
    }


    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        Vector3 camPosition = _camera.Position;
        Vector3 camTarget = _camera.Target;
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            camPosition.X -= 1f;
            camTarget.X -= 1f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            camPosition.X += 1f;
            camTarget.X += 1f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            camPosition.Y -= 1f;
            camTarget.Y -= 1f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            camPosition.Y += 1f;
            camTarget.Y += 1f;
        }
        if(Keyboard.GetState().IsKeyDown(Keys.OemPlus))
        {
            camPosition.Z += 1f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
        {
            camPosition.Z -= 1f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            orbit = !orbit;
        }

        if (orbit)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(
                                    MathHelper.ToRadians(1f));
            camPosition = Vector3.Transform(camPosition, 
                            rotationMatrix);
        }
        _camera.SetPositionDeferred(camPosition);
        _camera.SetTargetDeferred(camTarget);
        // Typically we wouldn't want to call this each frame,
        // but maybe MonoGame has really fast matrix multiplication.
        _camera.RegenerateViewMatrix();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        basicEffect.Projection = _camera.ProjectionMatrix;
        basicEffect.View = _camera.ViewMatrix;
        basicEffect.World = _camera.WorldMatrix;

        GraphicsDevice.Clear(Color.CornflowerBlue);
        GraphicsDevice.SetVertexBuffer(vertexBuffer);

        RasterizerState rasterizerState = new RasterizerState();
        rasterizerState.CullMode = CullMode.None;
        GraphicsDevice.RasterizerState = rasterizerState;

        foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
        {
            pass.Apply();
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
        }

        base.Draw(gameTime);
    }
}
