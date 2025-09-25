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
        public Vector2 ballPos { get; set; }
        public Vector2 prevPos { get; set; }
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

        public void Reset() // -1 = left, +1 = right
        {
            ballPos = new Vector2(Globals.PreferredBackBufferWidth / 2 - 20, Globals.PreferredBackBufferHeight / 2 - 20);

            int xDirection = Globals.Random.Next(0, 2) == 0 ? -1 : 1;
            ballVelocity = new Vector2(ballVelocity.X * xDirection, -ballVelocity.Y);

            //todo: pick a random y direction
            //todo: have a slight pause.

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
    }
}
