using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong {
    internal class Paddle {

        private Vector2 paddlePos;
        private Rectangle paddleRect;
        private int paddleSpeed = 100; // pixels per second
        private Texture2D dummyTexture;

        public Paddle(Rectangle rect, Texture2D dummyTexture) {
            this.paddleRect = rect;
            this.paddlePos = new Vector2(rect.X, rect.Y);
            this.dummyTexture = dummyTexture;
        }

        public void Update() {

        }
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(dummyTexture, paddleRect, Color.Red); 
        }
    }
}
