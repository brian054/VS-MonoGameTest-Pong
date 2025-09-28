using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Entities;
using Pong.GameStates;
using Pong.Managers;
using Pong.Shared;
using Pong.UI;
using System;
using System.Diagnostics;
using System.Transactions;

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

        ScoreBoard theScoreBoard;

        private CollisionManager collisionManager;

        MainMenuState mainMenu;

        MouseState currMouse;
        MouseState prevMouse; // move out to state manager?
        bool playButtonClicked = false;
        bool gameStart = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Globals.PreferredBackBufferWidth;
            _graphics.PreferredBackBufferHeight = Globals.PreferredBackBufferHeight;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Window.Title = "Pong";
            Window.AllowUserResizing = false;

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0); // 60 updates per second
        }

        // Called after the constructor but before the main game loop (Update/Draw)
        // Query any required services and load any non-graphic related content
        protected override void Initialize()
        {
            base.Initialize();

            // game specific initializations
        }

        // Called once per game, within the Initialize method, before the main game loop starts
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            Globals.dummyTexture.SetData(new[] { Color.White });
            Globals.DefaultFont = Content.Load<SpriteFont>("ScoreBoardFont");

            mainMenu = new MainMenuState();

            theBall = new Ball(new Rectangle(_graphics.PreferredBackBufferWidth / 2 - 20, _graphics.PreferredBackBufferHeight / 2 - 20, 20, 20));
            playerPaddleTest = new Paddle(new Rectangle(60, 100, 20, 100), _dummyTexture);
            villain = new Paddle(new Rectangle(880, 300, 20, 100), _dummyTexture); // 880 = PreferredWidth - 60 (player is x = 60, so offset) - 20 (size)

            theScoreBoard = new ScoreBoard();

            collisionManager = new CollisionManager(); // what's the point if we do nothing in the constructor? just wondering
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currMouse = Mouse.GetState();

            // test click
            if (mainMenu.PlayButton.hoverPlay && currMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
            {
                playButtonClicked = true;
            }

            if (gameStart)
            {
                playerPaddleTest.Update(gameTime);
                villain.Update(gameTime);
                theBall.Update(gameTime);

                collisionManager.HandleCollisions(playerPaddleTest.paddleRect, villain.paddleRect, theBall, theScoreBoard);

                // Check if someone won the game
                if (theScoreBoard.playerScore > 2 || theScoreBoard.villainScore > 2)
                {
                    // Game over
                    Debug.WriteLine("Game over!");
                    theScoreBoard.ResetScore();
                    gameStart = false;
                }
            } 
            else
            {
                mainMenu.Update();

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    gameStart = true;
                }
            }

            prevMouse = currMouse;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (playButtonClicked)
            {
                theBall.Draw(_spriteBatch);
                playerPaddleTest.Draw(_spriteBatch);
                villain.Draw(_spriteBatch);
                theScoreBoard.Draw(_spriteBatch);

                if (!gameStart)
                {
                    Vector2 promptPosition = new Vector2(Globals.PreferredBackBufferWidth / 2 - 300, 20);
                    _spriteBatch.DrawString(Globals.DefaultFont, "Press 'spacebar' to start the game", promptPosition, Color.White);
                }
            } 
            else
            {
                mainMenu.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
