using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Shared;

namespace Pong.Entities
{
    public class Brick : Entity
    {
        public bool IsDestroyed { get; private set; }
        public int Health { get; private set; }
        public Vector2 Position { get; private set; }
        public Color BrickColor { get; private set; }

        private int width;
        private int height;

        private Texture2D texture = Globals.dummyTexture;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    width,
                    height
                );
            }
        }

        public Brick(Vector2 position, int width, int height, Color brickColor, int health = 1)
        {
            Position = position;
            this.width = width;
            this.height = height;
            BrickColor = brickColor;
            Health = health;
            IsDestroyed = false;
        }

        public override void Update(GameTime gameTime)
        {
            // Later: animations, hit flash, etc.
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsDestroyed)
                return;

            spriteBatch.Draw(texture, Bounds, BrickColor);
        }

        public void TakeHit()
        {
            Health--;

            if (Health <= 0)
                IsDestroyed = true;
        }
    }
}