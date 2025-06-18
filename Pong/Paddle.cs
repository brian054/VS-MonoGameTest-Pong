using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Pong {
    internal class Paddle {

        private Vector2 paddlePos;
        private Rectangle paddleRect;
        private int paddleSpeed = 100; // pixels per second
        private Texture2D dummyTexture;

        private int windowHeight = Globals.PreferredBackBufferHeight;

        public Paddle(Rectangle rect, Texture2D dummyTexture) {
            this.paddleRect = rect;
            this.paddlePos = new Vector2(rect.X, rect.Y);
            this.dummyTexture = dummyTexture;
        }

        public void Update(GameTime gameTime, KeyboardState keyState) {
            if (keyState.IsKeyDown(Keys.W))
            {
                paddlePos.Y -= paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Debug.WriteLine(paddlePos.Y);
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                paddlePos.Y += paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (paddlePos.Y > windowHeight - paddleRect.Height / 2)
            {
                paddlePos.Y = windowHeight - paddleRect.Height / 2;
            }
            else if (paddlePos.Y < paddleRect.Height / 2)
            {
                paddlePos.Y = paddleRect.Height / 2;
            }
        }
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(dummyTexture, paddlePos, paddleRect, Color.Red); 
        }
    }
}
