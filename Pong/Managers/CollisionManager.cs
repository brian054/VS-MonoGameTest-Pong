using Pong.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Pong.Shared;
using System.Runtime.CompilerServices;
using Pong.UI;
using System.Threading;

// To manage collisions so I can clean up the Game1 and Ball files.

namespace Pong.Managers
{
    public class CollisionManager
    {
        // The Paddle is separated into 7 zones, tbh this shouldn't be in ball, should be in Collision Manager.
        static readonly Vector2[] ZoneDirsRight = new Vector2[]
        {
            new Vector2(0.5f, -0.87f), // Zone 0: steep up-right
            new Vector2(0.71f, -0.71f),// Zone 1: medium up-right
            new Vector2(0.87f, -0.5f), // Zone 2: shallow up-right
            new Vector2(1.0f, 0.0f),  // Zone 3: flat right
            new Vector2(0.87f, 0.5f), // Zone 4: shallow down-right
            new Vector2(0.71f, 0.71f),// Zone 5: medium down-right
            new Vector2(0.5f, 0.87f) // Zone 6: steep down-right
        };

        public CollisionManager() { }

        public void HandleCollisions(Rectangle playerRect, Rectangle villainRect, Ball theBall, ScoreBoard scoreBoard)
        {
            HandleScoreUpdate(theBall, scoreBoard);

            // check ball collision top or bottom of window
            if (theBall.ballRect.Y < 0 || theBall.ballRect.Y + theBall.ballSize > Globals.PreferredBackBufferHeight)
            {
                theBall.ballVelocity = new Vector2(theBall.ballVelocity.X, -theBall.ballVelocity.Y);
            }

            // check player paddle collision ball
            ResolvePaddleCollision(playerRect, theBall);
            // check enemy paddle collision ball
            ResolvePaddleCollision(villainRect, theBall);
        }

        // Gotta be a way to clean this up a little
        public void ResolvePaddleCollision(Rectangle paddle, Ball theBall)
        {
            Rectangle curr = theBall.ballRect;
            if (!curr.Intersects(paddle)) return;

            // the ball's previous position
            Rectangle prev = new Rectangle((int)theBall.prevPos.X, (int)theBall.prevPos.Y, theBall.ballSize, theBall.ballSize);

            // the last frame, the ball was just above the paddle's top, and currently it's inside the paddle's top.
            if (prev.Bottom <= paddle.Top && curr.Bottom > paddle.Top)
            {
                // so snap it to the top
                theBall.ballPos = new Vector2(theBall.ballPos.X, paddle.Top - theBall.ballSize);
                // reflect vertically
                theBall.ballVelocity = new Vector2(theBall.ballVelocity.X, -theBall.ballVelocity.Y);
                return;
            }

            // in the previous frame, the ball was just below the paddle's bottom, currently it's inside the paddle's bottom.
            if (prev.Top >= paddle.Bottom && curr.Top < paddle.Bottom)
            {
                // so snap it to the bottom
                theBall.ballPos = new Vector2(theBall.ballPos.X, paddle.Bottom);
                // reflect vertically
                theBall.ballVelocity = new Vector2(theBall.ballVelocity.X, -theBall.ballVelocity.Y);
                return;
            }

            // Came from left
            if (prev.Right <= paddle.Left && curr.Right > paddle.Left)
            {
                // snap to left side
                theBall.ballPos = new Vector2(paddle.Left - theBall.ballSize - 1, theBall.ballPos.Y);

                // figure out which zone it hit
                float ballCenterY = theBall.ballPos.Y + theBall.ballSize * 0.5f;
                float t = MathHelper.Clamp((ballCenterY - paddle.Top) / (float)paddle.Height, 0f, 0.9999f);
                int zone = (int)(t * 7); // 0..6

                // preserve speed
                float speed = theBall.ballVelocity.Length();
                if (speed <= 0.0001f) speed = 1f;

                Vector2 dir = ZoneDirsRight[zone];
                dir.X = -dir.X; // flip it so the ball goes left
                theBall.ballVelocity = dir * speed;

                return;
            }

            // Came from right
            if (prev.Left >= paddle.Right && curr.Left < paddle.Right)
            {
                // snap to right side
                theBall.ballPos = new Vector2(paddle.Right + 1, theBall.ballPos.Y);

                // figure out which zone it hit
                float ballCenterY = theBall.ballPos.Y + theBall.ballSize * 0.5f;
                float t = MathHelper.Clamp((ballCenterY - paddle.Top) / (float)paddle.Height, 0f, 0.9999f);
                int zone = (int)(t * 7); // 0..6

                // preserve speed
                float speed = theBall.ballVelocity.Length();
                if (speed <= 0.0001f) speed = 1f;

                Vector2 dir = ZoneDirsRight[zone]; // rightward table
                theBall.ballVelocity = dir * speed;

                return;
            }
        }

        // I'm starting to think about maybe moving this out of this class, having the bool's calculated in Game1.cs, or renaming this EventManager, 
        // where collisions are just included as an Event. 
        public void HandleScoreUpdate(Ball theBall, ScoreBoard scoreBoard)
        {
            bool playerScored = theBall.ballRect.X + theBall.ballSize > Globals.PreferredBackBufferWidth; // pretty sure you can do theBall.ballRect.Right instead of calculating the X plus ballSize
            bool villainScored = theBall.ballRect.X < 0;

            // Update score if ball goes outside the boundary 
            if (playerScored || villainScored)
            {
                if (playerScored)
                {
                    scoreBoard.playerScore += 1;
                } else
                {
                    scoreBoard.villainScore += 1;
                }

                theBall.Reset();
            }
        }
    }
}
