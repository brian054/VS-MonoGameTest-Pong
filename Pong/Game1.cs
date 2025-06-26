using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

            ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0); // 60 updates per second
            base.Initialize();
        }

        // Called once per game, within the Initialize method, before the main game loop starts
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            _dummyTexture.SetData(new[] { Color.White });

            ballTexture = Content.Load<Texture2D>("ball");
            playerPaddleTest = new Paddle(new Rectangle(100, 100, 60, 100), _dummyTexture);
            villain = new Paddle(new Rectangle(700, 500, 60, 100), _dummyTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyState = Keyboard.GetState();

            playerPaddleTest.Update(gameTime, keyState);
            if (keyState.IsKeyDown(Keys.W))
            {
                ballPos.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                ballPos.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                ballPos.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                ballPos.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (ballPos.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2) {
                ballPos.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
            }
            else if (ballPos.X < ballTexture.Width / 2) {
                ballPos.X = ballTexture.Width / 2;
            }

            if (ballPos.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2) {
                ballPos.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
            }
            else if (ballPos.Y < ballTexture.Height / 2) {
                ballPos.Y = ballTexture.Height / 2;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, ballPos,
                null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                Vector2.One, SpriteEffects.None, 0f);
            playerPaddleTest.Draw(_spriteBatch);
            villain.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
