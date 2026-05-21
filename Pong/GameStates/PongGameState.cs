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
using System.Net.NetworkInformation;

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
        private int defaultBallSpeed = 400;

        ScoreBoard theScoreBoard;

        // static because this belongs to the class, no matter if theres one Ball or 10 million. 
        private static readonly Vector2 startPosition = new Vector2(Globals.PreferredBackBufferWidth / 2 - 20, Globals.PreferredBackBufferHeight / 2 - 20);

        public bool gameStart = false;

        public int timesHit = 0; // times the ball is hit in a single round, to track speed increases over a rally

        public PongGameState(GameServices services)
        {
            gameServices = services; 

            Rectangle ballRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, 20, 20);
            theBall = new Ball(ballRect, Vector2.Normalize(new Vector2(1f, -1f)), defaultBallSpeed);
            playerPaddleTest = new Paddle(new Rectangle(60, 100, 20, 100), true);
            villain = new Paddle(new Rectangle(880, 300, 20, 100), true); // 880 = PreferredWidth - 60 (player is x = 60, so offset) - 20 (size)

            theScoreBoard = new ScoreBoard();
        }

        public void Update(GameTime gameTime)
        {
            if (gameStart)
            {    
                playerPaddleTest.Update(gameTime);
                villain.Update(gameTime);
                theBall.Update(gameTime);
                RegisterPongHit();

                HandleScoreUpdate();
                ResolveTopBottomCollisionWindow();
                ResolvePaddleBallCollision();

                // Check if someone won the game
                if (theScoreBoard.playerScore > 2 || theScoreBoard.villainScore > 2)
                {
                    // Game over
                    Debug.WriteLine("Game over!");
                    theScoreBoard.ResetScore();
                    gameStart = false; 
                }
            } 
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    gameStart = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            theBall.Draw(spriteBatch);
            playerPaddleTest.Draw(spriteBatch);
            villain.Draw(spriteBatch);
            theScoreBoard.Draw(spriteBatch);

            if (!gameStart)
            {
                Vector2 promptPosition = new Vector2(40, Globals.PreferredBackBufferHeight - 100);
                spriteBatch.DrawString(Globals.DefaultFont, "Press 'spacebar' to start!", promptPosition, Color.White);
            }
        }

        private int speedTier = 0; // TODO: test this
        private void RegisterPongHit()
        {
            int newTier = timesHit / 5;
            if (newTier > speedTier && theBall.CurrSpeed < 1000)
            {
                theBall.SetSpeed(theBall.CurrSpeed + 100);
                speedTier = newTier;
            }
        }

        private void ResolveTopBottomCollisionWindow()
        {
            // check ball collision top or bottom of window
            if (theBall.ballRect.Y < 0 || theBall.ballRect.Y + theBall.Size > Globals.PreferredBackBufferHeight)
            {
                theBall.ReverseY();
            }
        }

        private void ResolvePaddleBallCollision()
        {
            Rectangle ballRect = new Rectangle( // TODO: pretty sure i can remove this, just do theBall.ballRect
                (int)MathF.Round(theBall.Position.X),
                (int)MathF.Round(theBall.Position.Y),
                theBall.Size,
                theBall.Size);

            if (!ballRect.Intersects(playerPaddleTest.paddleRect) && !ballRect.Intersects(villain.paddleRect))
                return;

            float ballCenterY = theBall.Position.Y + theBall.Size * 0.5f;
            float t = MathHelper.Clamp((ballCenterY - playerPaddleTest.paddleRect.Top) / playerPaddleTest.paddleRect.Height, 0f, 1f);

            // 3 zones instead of freaking 7 
            float newVy = Helpers.GetBounceOffsetFromZones(t, 3, 0.75f); // 3 zones, 0.75 = max offest
            // if (t < 0.33f) newVy = -0.75f;
            // else if (t < 0.66f) newVy = 0f;
            // else newVy = 0.75f;


            // float speed = ball.ballVelocity.Length(); not sure if we'll modify speed or not, maybe after a certain rally of hits
            // if (speed < 0.001f) speed = 1f;

            // Figure out which side we hit based on velocity direction
            if (theBall.Direction.X > 0) // moving right
            {
                gameServices.soundManager.Play(SoundKeys.Paddle1);
                theBall.SetPosition(new Vector2(villain.paddleRect.Left - theBall.Size - 1, theBall.Position.Y));
                theBall.SetDirection(new Vector2(-1f, newVy));
            }
            else
            {
                gameServices.soundManager.Play(SoundKeys.Paddle2);
                theBall.SetPosition(new Vector2(playerPaddleTest.paddleRect.Right + 1, theBall.Position.Y));
                theBall.SetDirection(new Vector2(1f, newVy));
            }

          //  theBall.Direction.Normalize();
            timesHit += 1;
            // ball.ballVelocity *= speed;
        }

        private void HandleScoreUpdate()
        {
            bool playerScored = theBall.ballRect.X + theBall.Size > Globals.PreferredBackBufferWidth; // pretty sure you can do theBall.ballRect.Right instead of calculating the X plus ballSize
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

                ResetBall();
            }
        }

        public void ResetBall() // -1 = left, +1 = right
        {
            timesHit = 0;

            int xDirection = Globals.Random.Next(0, 2) == 0 ? -1 : 1;
            Vector2 serveDirection = new(xDirection, -1);

            theBall.Reset(startPosition, serveDirection, defaultBallSpeed);


            //todo: pick a random y direction
            //todo: have a slight pause.

        }
    }
}
