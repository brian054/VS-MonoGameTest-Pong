using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong {
    internal class PlayerPaddle {

        public Rectangle playerRect;
        public Vector2 playerPos;
        private int playerSpeed = 350; // pixels per second
        private Texture2D playerTexture;

        public PlayerPaddle(Texture2D emptyTexture, Rectangle rect) {
            playerRect = rect;
            playerPos = new Vector2(rect.X, rect.Y);
            playerTexture = emptyTexture;

        }

        public void Update(KeyboardState kb, float dt) {
            Vector2 movement = Vector2.Zero;
            if (kb.IsKeyDown(Keys.W)) movement.Y -= 1; // Up
            if (kb.IsKeyDown(Keys.S)) movement.Y += 1; // Down
            if (kb.IsKeyDown(Keys.A)) movement.X -= 1; // Left
            if (kb.IsKeyDown(Keys.D)) movement.X += 1; // Right

            // normalize for diagonal movement
            if (movement.LengthSquared() > 1) {
                movement.Normalize();
            }

            // update pos
            playerPos += movement * playerSpeed * dt;

            // update rect pos - for collision ig
            playerRect.X = (int) playerPos.X;
            playerRect.Y = (int) playerPos.Y;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(playerTexture, playerRect, Color.Red);
        }
    }
}
