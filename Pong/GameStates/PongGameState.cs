using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Audio;
using Pong.Entities;
using Pong.Managers;
using Pong.Services;
using Pong.Shared;
using Pong.UI;
using System;
using System.Diagnostics;

namespace Pong.GameStates
{
    internal class PongGameState : IGameState
    {
        private readonly GameServices gameServices;
        //private SpriteFont MainMenuFont;
        // MouseState mouse;
        // bool IsMouseHovering;
        // bool hoverOptions;
        // bool hoverRect;

        Paddle playerPaddleTest;
        Paddle villain;

        Ball theBall;

        ScoreBoard theScoreBoard;

        public bool gameStart = false;

        public PongGameState(GameServices services)
        {
            gameServices = services; 

            theBall = new Ball(new Rectangle(Globals.PreferredBackBufferWidth / 2 - 20, Globals.PreferredBackBufferHeight / 2 - 20, 20, 20));
            playerPaddleTest = new Paddle(new Rectangle(60, 100, 20, 100));
            villain = new Paddle(new Rectangle(880, 300, 20, 100)); // 880 = PreferredWidth - 60 (player is x = 60, so offset) - 20 (size)

            theScoreBoard = new ScoreBoard();
        }

        public void Update(GameTime gameTime)
        {
            playerPaddleTest.Update(gameTime);
            villain.Update(gameTime);
            theBall.Update(gameTime);

            HandleScoreUpdate();
            HandleCollisions();
            ResolvePaddleCollision();
            ResolvePaddleCollision();

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

        private void HandleCollisions()
        {
            // check ball collision top or bottom of window
            if (theBall.ballRect.Y < 0 || theBall.ballRect.Y + theBall.ballSize > Globals.PreferredBackBufferHeight)
            {
                theBall.ballVelocity = new Vector2(theBall.ballVelocity.X, -theBall.ballVelocity.Y);
            }
        }

        private void ResolvePaddleCollision()
        {
            Rectangle ballRect = new Rectangle(
                (int)MathF.Round(theBall.ballPos.X),
                (int)MathF.Round(theBall.ballPos.Y),
                theBall.ballSize,
                theBall.ballSize);

            if (!ballRect.Intersects(playerPaddleTest.paddleRect) && !ballRect.Intersects(villain.paddleRect))
                return;

            float ballCenterY = theBall.ballPos.Y + theBall.ballSize * 0.5f;
            float t = MathHelper.Clamp((ballCenterY - playerPaddleTest.paddleRect.Top) / playerPaddleTest.paddleRect.Height, 0f, 1f);

            // 3 zones instead of freaking 7 
            float newVy;
            if (t < 0.33f) newVy = -0.75f;
            else if (t < 0.66f) newVy = 0f;
            else newVy = 0.75f;

            // float speed = ball.ballVelocity.Length(); not sure if we'll modify speed or not, maybe after a certain rally of hits
            // if (speed < 0.001f) speed = 1f;

            // Figure out which side we hit based on velocity direction
            if (theBall.ballVelocity.X > 0) // moving right
            {
                gameServices.soundManager.Play(SoundKeys.Paddle1);
                theBall.ballPos.X = villain.paddleRect.Left - theBall.ballSize - 1;
                theBall.ballVelocity = new Vector2(-1f, newVy);
            }
            else
            {
                gameServices.soundManager.Play(SoundKeys.Paddle2);
                theBall.ballPos.X = playerPaddleTest.paddleRect.Right + 1;
                theBall.ballVelocity = new Vector2(1f, newVy);
            }

            theBall.ballVelocity.Normalize();
            theBall.timesHit += 1;
            // ball.ballVelocity *= speed;
        }

        private void HandleScoreUpdate()
        {
            bool playerScored = theBall.ballRect.X + theBall.ballSize > Globals.PreferredBackBufferWidth; // pretty sure you can do theBall.ballRect.Right instead of calculating the X plus ballSize
            bool villainScored = theBall.ballRect.X < 0;

            // Update score if ball goes outside the boundary 
            if (playerScored || villainScored)
            {
                if (playerScored)
                {
                    theScoreBoard.playerScore += 1;
                }
                else
                {
                    theScoreBoard.villainScore += 1;
                }

                theBall.Reset();
            }
        }
    }
}
