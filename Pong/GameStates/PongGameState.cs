using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Entities;
using Pong.Managers;
using Pong.Shared;
using Pong.UI;
using System.Diagnostics;
using System;

namespace Pong.GameStates
{
    internal class PongGameState : IGameState
    {
        //private SpriteFont MainMenuFont;

        // MouseState mouse;
        // bool hoverPlay;
        // bool hoverOptions;
        // bool hoverRect;

        Paddle playerPaddleTest;
        Paddle villain;

        Ball theBall;

        ScoreBoard theScoreBoard;

        private CollisionManager collisionManager;

        public bool gameStart = false; // should i do get, set? Idc to know right now lol so TODO

        public PongGameState(GraphicsDeviceManager _graphics, Texture2D _dummyTexture)
        {
            theBall = new Ball(new Rectangle(_graphics.PreferredBackBufferWidth / 2 - 20, _graphics.PreferredBackBufferHeight / 2 - 20, 20, 20));
            playerPaddleTest = new Paddle(new Rectangle(60, 100, 20, 100), _dummyTexture);
            villain = new Paddle(new Rectangle(880, 300, 20, 100), _dummyTexture); // 880 = PreferredWidth - 60 (player is x = 60, so offset) - 20 (size)

            theScoreBoard = new ScoreBoard();

            collisionManager = new CollisionManager(); 
        }

        public void Update(GameTime gameTime)
        {
            playerPaddleTest.Update(gameTime);
            villain.Update(gameTime);
            theBall.Update(gameTime);

            collisionManager.HandleCollisions(playerPaddleTest.paddleRect, villain.paddleRect, theBall, theScoreBoard);

            // Check if someone won the game
            if (theScoreBoard.playerScore > 2 || theScoreBoard.villainScore > 2)
            {
                // Game over
                Debug.WriteLine("Game over!");
                theScoreBoard.ResetScore();
                gameStart = false; 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            theBall.Draw(spriteBatch);
            playerPaddleTest.Draw(spriteBatch);
            villain.Draw(spriteBatch);
            theScoreBoard.Draw(spriteBatch);

            if (gameStart)
            {
                Vector2 promptPosition = new Vector2(40, Globals.PreferredBackBufferHeight - 100);
                spriteBatch.DrawString(Globals.DefaultFont, "Press 'spacebar' to start!", promptPosition, Color.White);
            }
        }
    }
}
