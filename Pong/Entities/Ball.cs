using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pong.Shared;

namespace Pong.Entities
{
    public class Ball : Entity
    {
        public Vector2 ballPos { get; private set; }
        private Vector2 prevPos;
        public Vector2 ballVelocity { get; set; } // change back to private later
        public int ballSize { get; private set; }
        private int ballSpeed = 400; // was 250, pixels per second
                                     // private Texture2D ballTexture;
        public Rectangle ballRect =>
            new Rectangle(
                (int)MathF.Round(ballPos.X), 
                (int)MathF.Round(ballPos.Y),
                ballSize,
                ballSize
             );

        public Ball(Rectangle rect)
        {
           ballPos = new Vector2(rect.X, rect.Y);
           prevPos = ballPos;
           ballSize = rect.Width; 
 
           ballVelocity = Vector2.Normalize(new Vector2(1f, -1f)); // the direction
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            prevPos = ballPos;
            ballPos += ballVelocity * ballSpeed * dt;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.dummyTexture, ballPos, ballRect, Color.White);
        }

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

        public void ResolvePaddleCollision(Rectangle paddle)
        {
            Rectangle curr = ballRect;
            if (!curr.Intersects(paddle)) return;

            // the ball's previous position
            Rectangle prev = new Rectangle((int)prevPos.X, (int)prevPos.Y, ballSize, ballSize); 

            // the last frame, the ball was just above the paddle's top, and currently it's inside the paddle's top.
            if (prev.Bottom <= paddle.Top && curr.Bottom > paddle.Top)
            {
                // so snap it to the top
                ballPos = new Vector2(ballPos.X, paddle.Top - ballSize);
                // reflect vertically
                ballVelocity = new Vector2(ballVelocity.X, -ballVelocity.Y);
                return;
            }

            // in the previous frame, the ball was just below the paddle's bottom, currently it's inside the paddle's bottom.
            if (prev.Top >= paddle.Bottom && curr.Top < paddle.Bottom)
            {
                // so snap it to the bottom
                ballPos = new Vector2(ballPos.X, paddle.Bottom);
                // reflect vertically
                ballVelocity = new Vector2(ballVelocity.X, -ballVelocity.Y);
                return;
            }

            // Came from left
            if (prev.Right <= paddle.Left && curr.Right > paddle.Left)
            {
                // snap to left side
                ballPos = new Vector2(paddle.Left - ballSize, ballPos.Y);

                // figure out which zone it hit
                float ballCenterY = ballPos.Y + ballSize * 0.5f;
                float t = MathHelper.Clamp((ballCenterY - paddle.Top) / (float)paddle.Height, 0f, 0.9999f);
                int zone = (int)(t * 7); // 0..6

                // preserve speed
                float speed = ballVelocity.Length();
                if (speed <= 0.0001f) speed = 1f;

                Vector2 dir = ZoneDirsRight[zone];
                dir.X = -dir.X; // flip it so the ball goes left
                ballVelocity = dir * speed;

                // push ball out a little so it doesn't stick
                ballPos = new Vector2(paddle.Left - ballSize - 1, ballPos.Y);

                return;
            }

            // Came from right
            if (prev.Left >= paddle.Right && curr.Left < paddle.Right)
            {
                // snap to right side
                ballPos = new Vector2(paddle.Right, ballPos.Y);

                // figure out which zone it hit
                float ballCenterY = ballPos.Y + ballSize * 0.5f;
                float t = MathHelper.Clamp((ballCenterY - paddle.Top) / (float)paddle.Height, 0f, 0.9999f);
                int zone = (int)(t * 7); // 0..6

                // preserve speed
                float speed = ballVelocity.Length();
                if (speed <= 0.0001f) speed = 1f;

                Vector2 dir = ZoneDirsRight[zone]; // rightward table
                ballVelocity = dir * speed;

                // push ball out a little so it doesn't stick
                ballPos = new Vector2(paddle.Right + 1, ballPos.Y);

                return;
            }
        }
    }
}
