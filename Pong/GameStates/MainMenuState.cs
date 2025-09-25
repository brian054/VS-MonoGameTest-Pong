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

namespace Pong.GameStates
{
    internal class MainMenuState
    {
        private SpriteFont MainMenuFont; 

        private Vector2 TitlePos;
        //private Vector2 PlayButtonPos;
        //private Vector2 OptionsButtonPos;
        //private Vector2 ExitButtonPos;

        private Rectangle PlayRect;
        private Rectangle OptionsRect;
        private Rectangle ExitRect;

        private int ButtonWidth = 180;
        private int ButtonHeight = 60;

        MouseState mouse;
        bool hoverPlay;
        bool hoverOptions;
        bool hoverRect;

        public MainMenuState(SpriteFont font)
        {
            MainMenuFont = font;

            int centerX = Globals.PreferredBackBufferWidth / 2 - (ButtonWidth / 2);

            TitlePos = new Vector2(centerX, 100);

            PlayRect = new Rectangle(centerX, 250, ButtonWidth, ButtonHeight);
            OptionsRect = new Rectangle(centerX, 320, ButtonWidth, ButtonHeight);
            ExitRect = new Rectangle(centerX, 390, ButtonWidth, ButtonHeight);
        }

        public void Update()
        {
            mouse = Mouse.GetState();

            Point mousePos = new Point(mouse.X, mouse.Y);

            if (PlayRect.Contains(mousePos))
            {
                hoverPlay = true;
            } 
            else
            {
                hoverPlay = false;
            }

            if (OptionsRect.Contains(mousePos))
            {
                hoverOptions = true;
            }
            else
            {
                hoverOptions = false;
            }

            if (ExitRect.Contains(mousePos))
            {
                hoverRect = true;
            }
            else
            {
                hoverRect = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MainMenuFont, "PONG", TitlePos, Color.White);

            spriteBatch.Draw(Globals.dummyTexture, new Vector2(PlayRect.X, PlayRect.Y), PlayRect, Color.RoyalBlue);
            if (hoverPlay) { 
                spriteBatch.Draw(Globals.dummyTexture, new Vector2(PlayRect.X, PlayRect.Y), PlayRect, Color.Black * 0.7f); 
            }


            spriteBatch.Draw(Globals.dummyTexture, new Vector2(OptionsRect.X, OptionsRect.Y), OptionsRect, Color.PowderBlue); // powder blue
            if (hoverOptions)
            {
                spriteBatch.Draw(Globals.dummyTexture, new Vector2(OptionsRect.X, OptionsRect.Y), OptionsRect, Color.Black * 0.7f);
            }

            spriteBatch.Draw(Globals.dummyTexture, new Vector2(ExitRect.X, ExitRect.Y), ExitRect, Color.BlueViolet);
            if (hoverRect)
            {
                spriteBatch.Draw(Globals.dummyTexture, new Vector2(ExitRect.X, ExitRect.Y), ExitRect, Color.Black * 0.7f);
            }
        }

    }
}
