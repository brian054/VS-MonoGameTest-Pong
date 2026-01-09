using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Pong.Shared;
using Pong.UI;

namespace Pong.GameStates
{
    internal class OptionsMenuState : IGameState
    {
        private SpriteFont OptionsMenuFont; 

        // MouseState mouse;
        // bool hoverPlay;
        // bool hoverOptions;
        // bool hoverRect;

        public Button LanguageButton { get; private set; }

        public OptionsMenuState()
        {
            int buttonWidth = 240;
            int centerX = Globals.PreferredBackBufferWidth / 2 - (buttonWidth / 2);


            LanguageButton = new Button("LANGUAGE", new Vector2(centerX, 200), buttonWidth, Color.RoyalBlue);
        }

        public void Update(GameTime gameTime)
        {
            LanguageButton.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LanguageButton.Draw(spriteBatch);
        }
    }
}
