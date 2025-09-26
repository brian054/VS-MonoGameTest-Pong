using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Pong.UI
{
    // Why? Well the Main Menu already has 3 buttons, I wanna make it more simple and readable, Options menu 
    // and game over menu will have buttons, so let's just go ahead and do it.
    internal class Button
    {
        private SpriteFont ButtonFont;

        private string ButtonText;

        private Vector2 ButtonPos;

        private Rectangle ButtonRect;

        private int ButtonWidth;
        private int ButtonHeight;

        MouseState mouse;
        bool hoverPlay;

        private const float AspectRatio = 3f; // width : height

        private Color ButtonColor;

        private Vector2 textPosition;

        public Button(string buttonText, Vector2 buttonPos, int buttonWidth, Color buttonColor) { 
            ButtonText = buttonText;
            ButtonColor = buttonColor;
            ButtonPos = buttonPos;
            ButtonWidth = buttonWidth;
            ButtonHeight = (int)(ButtonWidth / AspectRatio);

            ButtonRect = new Rectangle((int)ButtonPos.X, (int)ButtonPos.Y, ButtonWidth, ButtonHeight);

            Vector2 textSize = Globals.DefaultFont.MeasureString(ButtonText);
            float centerX = ButtonPos.X + ButtonWidth / 2f;
            float centerY = ButtonPos.Y + ButtonHeight / 2f;

            textPosition = new Vector2(centerX - textSize.X / 2f, centerY - textSize.Y / 2f);

        }

        public void Update()
        {
            mouse = Mouse.GetState();

            Point mousePos = new Point(mouse.X, mouse.Y);

            if (ButtonRect.Contains(mousePos))
            {
                hoverPlay = true;
            }
            else
            {
                hoverPlay = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.dummyTexture, new Vector2(ButtonRect.X, ButtonRect.Y), ButtonRect, ButtonColor); 
            if (hoverPlay)
            {
                spriteBatch.Draw(Globals.dummyTexture, new Vector2(ButtonRect.X, ButtonRect.Y), ButtonRect, Color.Black * 0.7f);
            }

            spriteBatch.DrawString(Globals.DefaultFont, ButtonText, textPosition, Color.White);
        }

    }
}
