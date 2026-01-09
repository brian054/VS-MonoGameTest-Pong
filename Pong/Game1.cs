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

        //Paddle playerPaddleTest;
        //Paddle villain;

        //Ball theBall;

        //ScoreBoard theScoreBoard;

        //private CollisionManager collisionManager;

        //MainMenuState mainMenu;
        //OptionsMenuState optionsMenu;
        //PongGameState pongGameState;

        MouseState currMouse;
        MouseState prevMouse; // move out to state manager?
        bool playButtonClicked = false;
        //bool gameStart = false;

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

            //mainMenu = new MainMenuState();
            //optionsMenu = new OptionsMenuState();
            //pongGameState = new PongGameState(_graphics, _dummyTexture);
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
            // if (optionsMenu.LanguageButton.hoverPlay && currMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released)
            // {
                
            // }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                pongGameState.gameStart = false;
                playButtonClicked = false;
            }

            if (pongGameState.gameStart)
            {
                pongGameState.Update(gameTime);
            } 
            else
            {
                // TODO: Fix this immediately, just make a state manager class, cuz press space on the menu and it breaks 
                // immediately, of course lol. this is such a horrible way to do it LOL WRITE THE STATE MANAGER well when
                // you wake up.
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    pongGameState.gameStart = true;
                } 
                else
                {
                    mainMenu.Update();
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
                pongGameState.Draw(_spriteBatch);

                //if (!pongGameState.gameStart)
                //{
                //    Vector2 promptPosition = new Vector2(40, Globals.PreferredBackBufferHeight - 100);
                //    _spriteBatch.DrawString(Globals.DefaultFont, "Press 'spacebar' to start!", promptPosition, Color.White);
                //}
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
