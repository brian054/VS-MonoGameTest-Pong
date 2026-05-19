using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Entities;
using Pong.Services;
using Pong.Shared;
using Pong.UI;

namespace Pong.GameStates
{
    internal class BrickBreakerGameState : IGameState
    {
        private readonly GameServices gameServices;

        private Paddle playerPaddle;
        private Ball ball; 
        private ScoreBoard scoreBoard;

        private Brick[,] bricks;

        private int brickRows = 5;
        private int brickColumns = 10;
        private int brickWidth = 70;
        private int brickHeight = 25;
        private int brickPadding = 8;

        public bool gameStart = false;

        public BrickBreakerGameState(GameServices services)
        {
            gameServices = services;

            playerPaddle = new Paddle(
                new Rectangle(
                    Globals.PreferredBackBufferWidth / 2,
                    Globals.PreferredBackBufferHeight - 80,
                    100,
                    20
                ),
                false
            );

            ball = new Ball(new Rectangle(
                    Globals.PreferredBackBufferWidth / 2,
                    Globals.PreferredBackBufferHeight - 80,
                    50,
                    50
            ));

            scoreBoard = new ScoreBoard();

            InitializeBricks();
        }

        public void Update(GameTime gameTime)
        {
            playerPaddle.Update(gameTime);
            ball.Update(gameTime);

            // UpdateBricks(gameTime);
            foreach (Brick brick in bricks)
            {
                if (brick == null)
                    continue;

                brick.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);

            // DrawBricks(spriteBatch);
            foreach (Brick brick in bricks)
            {
                if (brick == null)
                    continue;

                brick.Draw(spriteBatch);
            }

            // scoreBoard.Draw(spriteBatch);

            if (!gameStart)
            {
                Vector2 promptPosition = new Vector2(40, Globals.PreferredBackBufferHeight - 100);
                spriteBatch.DrawString(Globals.DefaultFont, "Press 'spacebar' to start!", promptPosition, Color.White);
            }
        }

        private void InitializeBricks()
        {
            bricks = new Brick[brickRows, brickColumns];

            int totalGridWidth = brickColumns * brickWidth + (brickColumns - 1) * brickPadding;
            int startX = (Globals.PreferredBackBufferWidth - totalGridWidth) / 2;
            int startY = 80;

            for (int row = 0; row < brickRows; row++)
            {
                for (int column = 0; column < brickColumns; column++)
                {
                    int x = startX + column * (brickWidth + brickPadding);
                    int y = startY + row * (brickHeight + brickPadding);

                    Vector2 brickPosition = new Vector2(x, y);
                    Color brickColor = GetBrickColor(row);

                    bricks[row, column] = new Brick(
                        brickPosition,
                        brickWidth,
                        brickHeight,
                        brickColor
                    );
                }
            }
        }   

        private Color GetBrickColor(int row)
        {
            if (row == 0)
                return new Color(255, 0, 0);

            if (row == 1)
                return new Color(255, 165, 0);

            if (row == 2)
                return new Color(255, 255, 0);

            if (row == 3)
                return new Color(0, 255, 0);

            return new Color(0, 128, 255);
        }

        private void CheckBrickCollisions()
        {
            // Later when the ball exists:
            //
            // foreach (Brick brick in bricks)
            // {
            //     if (brick == null || brick.IsDestroyed)
            //         continue;
            //
            //     if (ball.Rectangle.Intersects(brick.Rectangle))
            //     {
            //         brick.TakeHit();
            //         ball.Bounce();
            //         break;
            //     }
            // }
        }
    }
}