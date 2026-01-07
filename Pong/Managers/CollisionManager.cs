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

        public void ResolvePaddleCollision(Rectangle paddle, Ball ball)
        {
            Rectangle ballRect = new Rectangle(
                (int)MathF.Round(ball.ballPos.X),
                (int)MathF.Round(ball.ballPos.Y),
                ball.ballSize,
                ball.ballSize);

            if (!ballRect.Intersects(paddle))
                return;

            float ballCenterY = ball.ballPos.Y + ball.ballSize * 0.5f;
            float t = MathHelper.Clamp((ballCenterY - paddle.Top) / paddle.Height, 0f, 1f);

            // 3 zones instead of freaking 7 
            float newVy;
            if (t < 0.33f)      newVy = -0.75f;
            else if (t < 0.66f) newVy = 0f;
            else                newVy = 0.75f;

            // float speed = ball.ballVelocity.Length(); not sure if we'll modify speed or not, maybe after a certain rally of hits
            // if (speed < 0.001f) speed = 1f;

            // Figure out which side we hit based on velocity direction
            if (ball.ballVelocity.X > 0) // moving right
            {
                ball.ballPos.X = paddle.Left - ball.ballSize - 1;
                ball.ballVelocity = new Vector2(-1f, newVy);
            }
            else
            {
                ball.ballPos.X = paddle.Right + 1;
                ball.ballVelocity = new Vector2(1f, newVy);
            }

            ball.ballVelocity.Normalize();
           // ball.ballVelocity *= speed;
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
