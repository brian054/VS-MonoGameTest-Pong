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

            // ballTexture = Content.Load<Texture2D>("ball");
            //ballTexture = Helpers.CreateCircleTexture(GraphicsDevice, 10);

            // _graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2
            theBall = new Ball(new Vector2(150, 100), GraphicsDevice, 40); // new Rectangle(150, 100, 40, 40)
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
            if (Helpers.IsCircleRectColliding(theBall.ballPos + new Vector2(theBall.radius, theBall.radius), theBall.radius, playerPaddleTest.paddleRect))
            {
                Console.WriteLine("They colliding!");
                // if (theBall.currDirectionX == Ball.Direction.Right)
                // {
                //     theBall.currDirectionX = Ball.Direction.Left;
                // }
                // else
                // {
                //     theBall.currDirectionX = Ball.Direction.Right;
                // }
                theBall.ballVelocity = new Vector2(-theBall.ballVelocity.X, theBall.ballVelocity.Y);
            }

            // remember in the game it'll never bounce off the wall, so i dont think i should give a f about doing it from the center, paddle collision is the
            // only thing that matters, so don't go rewriting all the stuff tomorrow.
            if (theBall.ballPos.X > Globals.PreferredBackBufferWidth - (theBall.radius / 2)) // why tf does this work? I figured it'd be radius * 2. I feel dumb lol
            {
                //theBall.currDirectionX = Ball.Direction.Left;
               theBall.ballVelocity = new Vector2(-theBall.ballVelocity.X, theBall.ballVelocity.Y);
            }
            if (theBall.ballPos.X < 0)
            {
                //theBall.currDirectionX = Ball.Direction.Right;
                theBall.ballVelocity = new Vector2(-theBall.ballVelocity.X, theBall.ballVelocity.Y);
            }

            if (theBall.ballPos.Y > Globals.PreferredBackBufferHeight - (theBall.radius / 2))  
            {
                //theBall.currDirectionX = Ball.Direction.Left;
               theBall.ballVelocity = new Vector2(theBall.ballVelocity.X, -theBall.ballVelocity.Y);
            }
            if (theBall.ballPos.Y < 0)
            {
                theBall.ballVelocity = new Vector2(theBall.ballVelocity.X, -theBall.ballVelocity.Y);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            theBall.Draw(_spriteBatch);
            playerPaddleTest.Draw(_spriteBatch);
            villain.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
