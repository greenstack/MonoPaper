using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paper3D;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Camera3D _camera;

    VertexPositionColor[] _triangleVerts = new VertexPositionColor[3];

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _camera = new Camera3D(new PerspectiveParameters()
        {
            NearClipPlane = 1f,
            FarClipPlane = 1000f,
            VerticalFieldOfView = MathHelper.ToRadians(45f),
            AspectRatio = GraphicsDevice.DisplayMode.AspectRatio,
        }, new Vector3(0, 0, -100));

        _triangleVerts[0] = new(new Vector3(0, 20, 0), Color.Green);
        _triangleVerts[1] = new(new Vector3(20, -20, 0), Color.Green);
        _triangleVerts[2] = new(new Vector3(20, -20, 0), Color.Green);
        // TODO: use this.Content to load your game content here

        _buffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
        _buffer.SetData(_triangleVerts);

        renderEffect = new(GraphicsDevice);
        renderEffect.Alpha = 1.0f;
        renderEffect.VertexColorEnabled = true;
        renderEffect.LightingEnabled = false;

        base.Initialize();
    }

    VertexBuffer _buffer;

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    BasicEffect renderEffect;

    protected override void Draw(GameTime gameTime)
    {
        renderEffect.Projection = _camera.ProjectionMatrix;
        renderEffect.View = _camera.ViewMatrix;
        renderEffect.World = _camera.WorldMatrix;

        GraphicsDevice.Clear(Color.CornflowerBlue);
        GraphicsDevice.SetVertexBuffer(_buffer);
        RasterizerState rasterizerState = new()
        {
            CullMode = CullMode.None
        };
        GraphicsDevice.RasterizerState = rasterizerState;
        // TODO: Add your drawing code here

        foreach (EffectPass effectPass in renderEffect.CurrentTechnique.Passes)
        {
            effectPass.Apply();
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
        }

        base.Draw(gameTime);
    }
}
