using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pong.Shared;
using System.Diagnostics;

namespace Pong.Entities
{
    public class Ball : Entity
    {
        public Vector2 Position { get; private set; } 
        private Vector2 PrevPos { get; set; }
        public Vector2 Direction { get; private set; }
        public int Size { get; private set; }
        public int CurrSpeed { get; private set; }  // was 250, pixels per second
                                     // private Texture2D ballTexture;

        // private readonly bool isPong;
        /*
            The above just gets messy over time, what if we wanna add a volleyball style game using the same Ball entity, we need to 
            use Strategy pattern for that? or some other way lets think on this.  
        */

        public Rectangle ballRect =>
            new Rectangle(
                (int)MathF.Round(Position.X), 
                (int)MathF.Round(Position.Y),
                Size,
                Size
             );

        public Ball(Rectangle rect, Vector2 dir, int speed)
        {
           Position = new Vector2(rect.X, rect.Y);
           PrevPos = Position;
           Size = rect.Width; 
 
           Direction = dir; 

           CurrSpeed = speed;
        }

        public void Reset(Vector2 position, Vector2 direction, int speed)
        {
            Position = position;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            Direction = direction;
            CurrSpeed = speed;
        }

        public override void Update(GameTime gameTime) 
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            PrevPos = Position;
            Position += Direction * CurrSpeed * dt;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.dummyTexture, Position, ballRect, Color.White);
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetSpeed(int speed)
        {
            CurrSpeed = speed;
        }

        public void SetDirection(Vector2 dir)
        {
            Direction = dir;
        }

        public void ReverseX()
        {
            Direction = new Vector2(-Direction.X, Direction.Y);
        }

        public void ReverseY()
        {
            Direction = new Vector2(Direction.X, -Direction.Y);
        }


    }
}
