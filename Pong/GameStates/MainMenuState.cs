using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pong.Shared;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Pong.UI;
using Pong.Managers;
using Pong.Services;

namespace Pong.GameStates
{
    internal class MainMenuState : IGameState
    {
        private SpriteFont MainMenuFont;

        private GameServices gameServices;

        private Vector2 TitlePos;

        public Button PlayButton { get; set; }
        public Button BrickBreakerButton { get; set; }
        public Button OptionsButton { get; set; }  
        public Button ExitButton { get; set; }

        public MainMenuState(GameServices services)
        {
            gameServices = services;

            int buttonWidth = 240;
            int centerX = Globals.PreferredBackBufferWidth / 2 - (buttonWidth / 2);

            TitlePos = new Vector2(centerX - 200, 80);

            int buttonGroupCenterY = 140;

            // For a tutorial: I'd rather add the state where you pick which game you wanna play rather than change the whole layout.
            // For testing I'll have this brickbreaker button
            // NEW TODO: turn the play button into 'pick which game' button. 
            PlayButton = new Button("PONG", new Vector2(centerX, buttonGroupCenterY), buttonWidth, Color.RoyalBlue);
            BrickBreakerButton = new Button("BRICK-BREAK", new Vector2(centerX, buttonGroupCenterY + 100), buttonWidth, Color.LightSkyBlue);
            OptionsButton = new Button("OPTIONS", new Vector2(centerX, buttonGroupCenterY + 200), buttonWidth, Color.MonoGameOrange);
            ExitButton = new Button("EXIT", new Vector2(centerX, buttonGroupCenterY + 300), buttonWidth, Color.BlueViolet);
        }

        public void Update(GameTime gameTime) // gametime variable orrr??? I mean nothing here is time dependent soooo...TODO...look it up
        {
            PlayButton.Update();
            BrickBreakerButton.Update();
            OptionsButton.Update();
            ExitButton.Update();

            if (PlayButton.IsMouseHovering && MouseManager.LeftClicked())
            {
                gameServices.stateManager.ChangeState(new PongGameState(gameServices));
            }
            else if (BrickBreakerButton.IsMouseHovering && MouseManager.LeftClicked())
            {
                gameServices.stateManager.ChangeState(new BrickBreakerGameState(gameServices));
            }
            else if (OptionsButton.IsMouseHovering && MouseManager.LeftClicked())
            {
                gameServices.stateManager.ChangeState(new OptionsMenuState(gameServices));
            }
            else if (ExitButton.IsMouseHovering && MouseManager.LeftClicked())
            {
                Environment.Exit(0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Globals.DefaultFont, "PONG & BRICK-BREAKER!!!", TitlePos, Color.White);

            PlayButton.Draw(spriteBatch);
            BrickBreakerButton.Draw(spriteBatch);
            OptionsButton.Draw(spriteBatch);
            ExitButton.Draw(spriteBatch);
        }

    }
}
