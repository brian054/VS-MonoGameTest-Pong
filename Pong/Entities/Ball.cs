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

        public enum Direction
        {
            Left, Right
        }
        public Direction currDirection { get; set; }

        public Vector2 ballPos { get; private set; }

        public int radius { get; private set; }
        private Rectangle ballRect; // a mf square
        private int ballSpeed = 85; // was 250, pixels per second
        private Texture2D ballTexture;

        public Ball(Vector2 initialBallPos, GraphicsDevice gd, int radius)
        {
            this.ballPos = initialBallPos;
            this.radius = radius;
            this.ballTexture = Helpers.CreateCircleTexture(gd, 10);
        }

        public override void Update(GameTime gameTime)
        {
            if (currDirection == Direction.Left)
            {
                ballPos = new Vector2(ballPos.X - ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, ballPos.Y);
            }
            else if (currDirection == Direction.Right)
            {
                ballPos = new Vector2(ballPos.X + ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, ballPos.Y);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballTexture, ballPos, Color.White);
        }
    }
}
