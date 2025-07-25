﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Paper3D;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

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
            new Vector3(0, 0, 10f),
            Vector3.Zero,
            GraphicsDevice.DisplayMode.AspectRatio,
            45f,
            AngleUnit.Degree,
            1f,
            1000f
        );

        Texture2D tex = Content.Load<Texture2D>("MonoCubeTexture");
        Texture2D cassie = Content.Load<Texture2D>("Cassie_2");
        Texture2D eden = Content.Load<Texture2D>("Eden_4");
        //_billboard = new PlaneRender3D(GraphicsDevice, new SizeF(5, 5), tex);
        _billboard = new BillboardRender3D(_camera, GraphicsDevice, new SizeF(5, 5), cassie, true);

        // I do not like this. But I suppose it's all I have for now.
        GraphicsDevice.SamplerStates[0] = new()
        {
            Filter = TextureFilter.Point
        };
    }

    ModelRender3D _model;
    PlaneRender3D _billboard;

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

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            _billboard.Translate(new Vector3(-1f, 0, 0));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            _billboard.Translate(new Vector3(1, 0, 0));
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
        _billboard.ApplyCamera(_camera);
        _billboard.Draw(GraphicsDevice);

        _model.ApplyCamera(_camera);
        _model.Draw(null);

        base.Draw(gameTime);
    }
}
