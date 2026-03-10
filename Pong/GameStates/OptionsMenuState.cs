using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Managers;
using Pong.Shared;
using Pong.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.GameStates
{
    internal class OptionsMenuState : IGameState
    {
        private readonly StateManager stateManager;
        private SpriteFont OptionsMenuFont; 

        // MouseState mouse;
        // bool IsMouseHovering;
        // bool hoverOptions;
        // bool hoverRect;

        public Button LanguageButton { get; private set; }
        public Button BackButton { get; private set; }

        public OptionsMenuState(StateManager sm)
        {
            stateManager = sm;

            int buttonWidth = 240;
            int centerX = Globals.PreferredBackBufferWidth / 2 - (buttonWidth / 2);

            // add a back button
            BackButton = new Button("BACK", new Vector2(centerX, 200), buttonWidth, Color.Gray);
        }

        public void Update(GameTime gameTime)
        {
            BackButton.Update();

            if (BackButton.IsMouseHovering && MouseManager.LeftClicked())
            {
                stateManager.ChangeState(new MainMenuState(stateManager));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Globals.DefaultFont, "There are currently no \noptions...go play!", new Vector2(10, 80), Color.White);

            BackButton.Draw(spriteBatch);
        }
    }
}
