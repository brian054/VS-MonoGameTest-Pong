using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Vector2 ballPos;
        float ballSpeed;

        Vector2 PaddleXPos;
        float paddleSpeed;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // Called after the constructor but before the main game loop (Update/Draw)
        // Query any required services and load any non-graphic related content
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            PaddleXPos = new Vector2(50,
                200);

            base.Initialize();
        }

        // Called once per game, within the Initialize method, before the main game loop starts
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
        }

        // check collisions, gather input, play audio, etc.
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
            {
                ballPos.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                ballPos.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                ballPos.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                ballPos.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (ballPos.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
            {
                ballPos.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
            }
            else if (ballPos.X < ballTexture.Width / 2)
            {
                ballPos.X = ballTexture.Width / 2;
            }

            if (ballPos.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
            {
                ballPos.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
            }
            else if (ballPos.Y < ballTexture.Height / 2)
            {
                ballPos.Y = ballTexture.Height / 2;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, ballPos, 
                null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                Vector2.One * 3, SpriteEffects.None, 0f);
            _spriteBatch.Draw(ballTexture, new Vector2(10, 200),
                null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                Vector2.One * 3, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
