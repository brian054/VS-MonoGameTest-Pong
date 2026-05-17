using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private readonly bool isVerticalMovement;
        private readonly int windowWidth = Globals.PreferredBackBufferWidth;

        public Rectangle paddleRect =>
            new Rectangle(
                (int)MathF.Round(paddlePos.X),
                (int)MathF.Round(paddlePos.Y),
                paddleWidth,
                paddleHeight
             );
             
        public Paddle(Rectangle rect, bool isVerticalMovement)
        {
            paddleWidth = rect.Width;
            paddleHeight = rect.Height;
            paddlePos = new Vector2(rect.X, rect.Y);
            this.isVerticalMovement = isVerticalMovement;
        }
        
        // TODO: finish
        public override void Update(GameTime gameTime)/*GameTime gameTime, KeyboardState keyState)*/
        {
            // move out into method
            if(isVerticalMovement)
            {
                UpdateVerticalMovement(gameTime);
            }
            else 
            {
                UpdateHorizontalMovement(gameTime);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.dummyTexture, paddlePos, paddleRect, Color.Red);
        }


        /*
            Note: maybe redo all this: Use Strategy pattern if this stuff starts getting messy

            We have more than 2 movement strategies:
            - vertical 
            - horizontal
            - AI movement

            This might be overkill, so for now I'll leave it as is. 
        */
        private void UpdateVerticalMovement(GameTime gameTime)
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
            if (paddlePos.Y > windowWidth - paddleRect.Height)
            {
                paddlePos.Y = windowWidth - paddleRect.Height;
            }
            else if (paddlePos.Y < 0)
            {
                paddlePos.Y = 0;
            }
        }

        private void UpdateHorizontalMovement(GameTime gameTime)
        {
            // Movement
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
            {
                paddlePos.X -= paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                paddlePos.X += paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Bounds
            if (paddlePos.X + paddleRect.Width > windowWidth)
            {
                paddlePos.X = windowWidth - paddleRect.Width;
            }
            else if (paddlePos.X < 0)
            {
                paddlePos.X = 0;
            }
        }
    }
}
