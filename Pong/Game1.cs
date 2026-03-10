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

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Texture2D dummyTexture;

        private StateManager stateManager;
        //IGameState currState;

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
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Globals.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = Globals.PreferredBackBufferHeight;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            Globals.dummyTexture.SetData(new[] { Color.White });
            Globals.DefaultFont = Content.Load<SpriteFont>("ScoreBoardFont");

            //mainMenu = new MainMenuState();
            //optionsMenu = new OptionsMenuState();
            //pongGameState = new PongGameState(graphics, dummyTexture);
            stateManager = new();
            stateManager.ChangeState(new MainMenuState(stateManager));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardManager.Update();
            MouseManager.Update();

            stateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            stateManager.Draw(spriteBatch);

            //if (playButtonClicked)
            //{
            //    pongGameState.Draw(spriteBatch);

            //    //if (!pongGameState.gameStart)
            //    //{
            //    //    Vector2 promptPosition = new Vector2(40, Globals.PreferredBackBufferHeight - 100);
            //    //    spriteBatch.DrawString(Globals.DefaultFont, "Press 'spacebar' to start!", promptPosition, Color.White);
            //    //}
            //} 
            //else
            //{
            //    mainMenu.Draw(spriteBatch);
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
