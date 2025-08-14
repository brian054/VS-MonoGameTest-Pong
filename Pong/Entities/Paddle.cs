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
using Pong.Shared;

namespace Pong.Entities
{
    public class Paddle : Entity
    {

        public Vector2 paddlePos;
        public int paddleWidth;
        public int paddleHeight;
        private int paddleSpeed = 500; // pixels per second

        // private Vector2 paddleSize; 
        private Texture2D dummyTexture;

        private int windowHeight = Globals.PreferredBackBufferHeight;

        public Rectangle paddleRect =>
            new Rectangle(
                (int)MathF.Round(paddlePos.X),
                (int)MathF.Round(paddlePos.Y),
                paddleWidth,
                paddleHeight
             );
             
        public Paddle(Rectangle rect, Texture2D dummyTexture)
        {
            paddleWidth = rect.Width;
            paddleHeight = rect.Height;
            paddlePos = new Vector2(rect.X, rect.Y);
            this.dummyTexture = dummyTexture;
        }

        public override void Update(GameTime gameTime)/*GameTime gameTime, KeyboardState keyState)*/
        {
            // Movement
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
            {
                paddlePos.Y -= paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                paddlePos.Y += paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Bounds
            if (paddlePos.Y > windowHeight - paddleRect.Height)
            {
                paddlePos.Y = windowHeight - paddleRect.Height;
            }
            else if (paddlePos.Y < 0)
            {
                paddlePos.Y = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dummyTexture, paddlePos, paddleRect, Color.Red);
        }
    }
}
