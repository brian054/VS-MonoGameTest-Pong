using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pong.Shared;

/*
    Oof, so in the original pong game, the ball was a rectangle

    Honestly.....switch this to be a rectangle instead of a circle

    Collision becomes 10000x easier, just AABB, plus you'll get this done
    way way faster with just using the rectangle.

    This would be a great time to rename your var's in paddle and ball too

    Brick breaker will literally just have a circle texture on top of a rectangle
    and the rectangle can be used for collision.

    Lesson Learned: Don't overengineer, focus on MVP.

    Thoughts: I'm just glad I tried with the ball thing but this is just annoying af esp 
    when I know I can just do AABB super quickly. 

*/

namespace Pong.Entities
{
    public class Ball : Entity
    {
        public Vector2 ballPos { get; private set; }
        public Vector2 ballVelocity { get; set; } // change back to private later
        public int radius { get; private set; }
        private Rectangle ballRect; // a mf square
        private int ballSpeed = 400; // was 250, pixels per second
       // private Texture2D ballTexture;

        public Ball(Vector2 initialBallPos, GraphicsDevice gd, int radius)
        {
            this.ballPos = initialBallPos;
            this.radius = radius;
            //this.ballTexture = Helpers.CreateCircleTexture(gd, 10);
            

            float directionAngle = ballSpeed / MathF.Sqrt(2f);
            ballVelocity = new Vector2(directionAngle, -directionAngle);
        }

        public override void Update(GameTime gameTime)
        {
            ballPos = new Vector2(ballPos.X + ballVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, ballPos.Y + ballVelocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.dummyTexture, ballPos, Color.White);
        }
    }
}
