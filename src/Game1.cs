using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paper3D;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // // Geometric info
    VertexPositionColor[] triangleVertices;

    bool orbit = false;

    Model mesh;

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
            new Vector3(0, 0, -10f),
            Vector3.Zero,
            GraphicsDevice.DisplayMode.AspectRatio,
            45f,
            AngleUnit.Degree,
            1f,
            1000f
        );
        triangleVertices = new VertexPositionColor[3];
        triangleVertices[0] = new VertexPositionColor(new Vector3(0, 2, 0), Color.Red);

        triangleVertices[1] = new VertexPositionColor(new Vector3(-2, -2, 0), Color.Green);

        triangleVertices[2] = new VertexPositionColor(new Vector3(2, -2, 0), Color.Blue);


        _triangle = new TriangleListRender3D(GraphicsDevice, triangleVertices);
    }


    TriangleListRender3D _triangle;
    ModelRender3D _model;

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

         mesh = Content.Load<Model>("MonoCube");
        _model = new ModelRender3D(mesh);
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
        GraphicsDevice.Clear(Color.CornflowerBlue);
        RasterizerState defaultState = GraphicsDevice.RasterizerState;
        RasterizerState rasterizerState = new RasterizerState();
        rasterizerState.CullMode = CullMode.None;
        GraphicsDevice.RasterizerState = rasterizerState;

        _triangle.ApplyCamera(_camera);
        _triangle.Draw(GraphicsDevice);

        GraphicsDevice.RasterizerState = defaultState;
        _model.ApplyCamera(_camera);
        _model.Draw(null);

        base.Draw(gameTime);
    }
}
