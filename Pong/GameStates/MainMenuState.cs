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
    internal class MainMenuState
    {
        private SpriteFont MainMenuFont; 

        private Vector2 TitlePos;

        MouseState mouse;
        bool hoverPlay;
        bool hoverOptions;
        bool hoverRect;

        Button PlayButton;
        Button OptionsButton;
        Button ExitButton;

        public MainMenuState()
        {
            int buttonWidth = 180;
            int centerX = Globals.PreferredBackBufferWidth / 2 - (buttonWidth / 2);

            TitlePos = new Vector2(centerX, 100);

            PlayButton = new Button("PLAY", new Vector2(centerX, 250), buttonWidth, Color.RoyalBlue);
            OptionsButton = new Button("OPTIONS", new Vector2(centerX, 320), buttonWidth, Color.MonoGameOrange);
            ExitButton = new Button("EXIT", new Vector2(centerX, 390), buttonWidth, Color.BlueViolet);
        }

        public void Update()
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
