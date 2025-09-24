using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Entities;
using Pong.Shared;
using System;
using System.Diagnostics;

namespace Pong
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Vector2 ballPos; 
        float ballSpeed;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Texture2D _dummyTexture;

        Paddle playerPaddleTest;
        Paddle villain;

        Ball theBall; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Globals.PreferredBackBufferWidth;
            _graphics.PreferredBackBufferHeight = Globals.PreferredBackBufferHeight;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // Called after the constructor but before the main game loop (Update/Draw)
        // Query any required services and load any non-graphic related content
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.AllowUserResizing = false;

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0); // 60 updates per second

            base.Initialize();
        }

        // Called once per game, within the Initialize method, before the main game loop starts
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            Globals.dummyTexture.SetData(new[] { Color.White });

            theBall = new Ball(new Rectangle(_graphics.PreferredBackBufferWidth / 2 - 20, _graphics.PreferredBackBufferHeight / 2 - 20, 20, 20));
            playerPaddleTest = new Paddle(new Rectangle(60, 100, 20, 100), _dummyTexture);
            villain = new Paddle(new Rectangle(920, 300, 20, 100), _dummyTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            playerPaddleTest.Update(gameTime);
            theBall.Update(gameTime);

            // check paddle and ball collision
            if (Helpers.AABB_Collision(theBall.ballRect, playerPaddleTest.paddleRect))
            {
                theBall.ballVelocity = new Vector2(-theBall.ballVelocity.X, theBall.ballVelocity.Y);
            }

            // check ball and window collision
            if (theBall.ballRect.Y < 0 || theBall.ballRect.Y + theBall.ballSize > _graphics.PreferredBackBufferHeight)
            {
                theBall.ballVelocity = new Vector2(theBall.ballVelocity.X, -theBall.ballVelocity.Y);
            }

            if (theBall.ballRect.X < 0 || theBall.ballRect.X + theBall.ballSize > _graphics.PreferredBackBufferWidth)
            {
                theBall.ballVelocity = new Vector2(-theBall.ballVelocity.X, theBall.ballVelocity.Y);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            theBall.Draw(_spriteBatch);
            playerPaddleTest.Draw(_spriteBatch);
            villain.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
