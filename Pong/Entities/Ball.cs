using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Entities
{
    public class Ball : Entity
    {

        private Vector2 ballPos;
        private Rectangle ballRect; // a mf square
        private int ballSpeed = 100; // pixels per second
        private Texture2D dummyTexture;

        public Ball(Rectangle rect, Texture2D dummyTexture)
        {
            ballRect = rect;
            ballPos = new Vector2(rect.X, rect.Y);
            this.dummyTexture = dummyTexture;
        }

        public override void Update(GameTime gameTime)
        {
        
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dummyTexture, ballRect, Color.Red);
        }
    }
}
