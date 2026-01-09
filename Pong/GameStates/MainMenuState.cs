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

namespace Pong.GameStates
{
    internal class MainMenuState : IGameState
    {
        private SpriteFont MainMenuFont; 

        private Vector2 TitlePos;

        // MouseState mouse;
        // bool hoverPlay;
        // bool hoverOptions;
        // bool hoverRect;

        public Button PlayButton { get; private set; }
        Button OptionsButton;
        Button ExitButton;

        public MainMenuState()
        {
            int buttonWidth = 240;
            int centerX = Globals.PreferredBackBufferWidth / 2 - (buttonWidth / 2);

            TitlePos = new Vector2(centerX + 50, 100);

            int buttonGroupCenterY = 200;

            PlayButton = new Button("PLAY", new Vector2(centerX, buttonGroupCenterY), buttonWidth, Color.RoyalBlue);
            OptionsButton = new Button("OPTIONS", new Vector2(centerX, buttonGroupCenterY + 100), buttonWidth, Color.MonoGameOrange);
            ExitButton = new Button("EXIT", new Vector2(centerX, buttonGroupCenterY + 200), buttonWidth, Color.BlueViolet);
        }

        public void Update(GameTime gameTime) // gametime variable orrr??? I mean nothing here is time dependent soooo...TODO...look it up
        {
            PlayButton.Update();
            OptionsButton.Update();
            ExitButton.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Globals.DefaultFont, "PONG", TitlePos, Color.White);

            PlayButton.Draw(spriteBatch);
            OptionsButton.Draw(spriteBatch);
            ExitButton.Draw(spriteBatch);
        }

    }
}
