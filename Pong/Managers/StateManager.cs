using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.GameStates;

namespace Pong.Managers
{
    // polymorphism: treats many diff objects as one type as long as they share the same interface
    // aka: MainMenuState, PongGameState, etc. should be treated as one type, as long as they are IGameState
    public class StateManager
    {
        public IGameState currState { get; private set; }

       // private IGameState currStateObject; // so i can avoid multiple switch statements

        private MainMenuState mainMenu;
        private OptionsMenuState optionsMenu;
        private PongGameState pongGameState;

        //public StateManager(GraphicsDeviceManager _graphics, Texture2D _dummyTexture) { // TODO: review the params
        //    mainMenu = new MainMenuState();
        //    optionsMenu = new OptionsMenuState();
        //    pongGameState = new PongGameState(_graphics, _dummyTexture);
        //}

        public void ChangeState(IGameState newState)
        {
            currState = newState;
        }
        public void Update(GameTime gameTime)
        {
            currState?.Update(gameTime); // TODO: do we need the '?'
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currState?.Draw(spriteBatch);
        }
    }
}
