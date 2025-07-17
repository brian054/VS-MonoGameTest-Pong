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
using Pong.Helpers;

namespace Pong.Entities
{
    public class Paddle: Entity
    {

        private Vector2 paddlePos;
        private Rectangle paddleRect;
        private int paddleSpeed = 100; // pixels per second
        private Texture2D dummyTexture;

        private int windowHeight = Globals.PreferredBackBufferHeight;

        public Paddle(Rectangle rect, Texture2D dummyTexture)
        {
            paddleRect = rect;
            paddlePos = new Vector2(rect.X, rect.Y);
            this.dummyTexture = dummyTexture;
        }

        public override void Update(GameTime gameTime)/*GameTime gameTime, KeyboardState keyState)*/
        {
            //if (keyState.IsKeyDown(Keys.W))
            //{
            //    paddlePos.Y -= paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //    Debug.WriteLine(paddlePos.Y);
            //}

            //if (keyState.IsKeyDown(Keys.S))
            //{
            //    paddlePos.Y += paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //}

            //if (paddlePos.Y > windowHeight - paddleRect.Height / 2)
            //{
            //    paddlePos.Y = windowHeight - paddleRect.Height / 2;
            //}
            //else if (paddlePos.Y < paddleRect.Height / 2)
            //{
            //    paddlePos.Y = paddleRect.Height / 2;
            //}
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dummyTexture, paddlePos, paddleRect, Color.Red);
        }
    }
}
