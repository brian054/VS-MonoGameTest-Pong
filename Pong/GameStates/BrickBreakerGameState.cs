using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Audio;
using Pong.Entities;
using Pong.Services;
using Pong.Shared;
using Pong.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pong.Shared;

namespace Pong.GameStates
{
    internal class BrickBreakerGameState : IGameState
    {
        private readonly GameServices gameServices;

        Paddle playerPaddleTest;

        ScoreBoard theScoreBoard;

        public bool gameStart = false;

        public BrickBreakerGameState(GameServices services)
        {
            gameServices = services;

            playerPaddleTest = new Paddle(new Rectangle(Globals.PreferredBackBufferWidth / 2, Globals.PreferredBackBufferHeight - 80, 100, 20), false   );

            theScoreBoard = new ScoreBoard();
        }

        public void Update(GameTime gameTime)
        {
            playerPaddleTest.Update(gameTime);

            //// Check if someone won the game
            //if (theScoreBoard.playerScore > 2)
            //{
            //    // Game over
            //    Debug.WriteLine("Game over!");
            //    theScoreBoard.ResetScore();
            //    gameStart = false;
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            playerPaddleTest.Draw(spriteBatch);
            //theScoreBoard.Draw(spriteBatch);

            if (gameStart)
            {
                Vector2 promptPosition = new Vector2(40, Globals.PreferredBackBufferHeight - 100);
                spriteBatch.DrawString(Globals.DefaultFont, "Press 'spacebar' to start!", promptPosition, Color.White);
            }
        }
    }
}
