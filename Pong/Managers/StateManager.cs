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
        public GameState currState { get; private set; } = GameState.MainMenu;

        private IGameState currStateObject; // so i can avoid multiple switch statements

        private MainMenuState mainMenu;
        private OptionsMenuState optionsMenu;
        private PongGameState pongGameState;

        public StateManager(GraphicsDeviceManager _graphics, Texture2D _dummyTexture) { // TODO: review the params
            mainMenu = new MainMenuState();
            optionsMenu = new OptionsMenuState();
            pongGameState = new PongGameState(_graphics, _dummyTexture);
        }

        public void ChangeState(GameState newState)
        {
            currState = newState;

            //switch(currState)
            //{
            //    case GameState.MainMenu:
            //        currStateObject = mainMenu;
            //        break;
            //    case GameState.OptionsMenu:
            //        currStateObject = mainMenu;
            //    case GameState.PlayingState:
            //        currStateObject = mainMenu;
            //    default:
            //        currStateObject = mainMenu;
            //}

            // new type of switch, easier to type lol
            currStateObject = currState switch
            {
                GameState.MainMenu => mainMenu,
                GameState.OptionsMenu => optionsMenu,
                GameState.PlayingState => pongGameState
            };

        }
        public void Update(GameTime gameTime)
        {
            //switch(currState)
            //{

            //}
            //currState.Update();
            currStateObject?.Update(gameTime); // TODO: do we need the '?'
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currStateObject?.Draw(spriteBatch);
        }

        /*
         * each state is its own class
         * an enum can represent the id prolly (like enum States: {"mainMenu", "gameState", "optionsState", etc} 
         * Then a switch can be used to pick which state we're in, then we know which states Update method to call
         * 
         * 
         * 
         */
    }
}
