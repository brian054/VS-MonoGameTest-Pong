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
            this.ballPos = new Vector2(rect.X, rect.Y);
            this.ballSize = rect.Width; 
 
            ballVelocity = Vector2.Normalize(new Vector2(1f, -1f)); // the direction
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballPos += ballVelocity * ballSpeed * dt;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.dummyTexture, ballPos, ballRect, Color.White);
        }
    }
}
