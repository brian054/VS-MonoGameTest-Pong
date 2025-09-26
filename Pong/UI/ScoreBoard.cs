using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Pong.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Pong.Entities;

namespace Pong.UI
{
    public class ScoreBoard
    {
        private Vector2 playerScorePos;
        private Vector2 villainScorePos;

        public int playerScore { get; set; } = 0;
        public int villainScore { get; set; } = 0;

        public ScoreBoard()
        {
            playerScorePos = new Vector2(0, 0);
            villainScorePos = new Vector2(100, 100);
        }

        public void ResetScore()
        {
            playerScore = 0;
            villainScore = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // player text
            spriteBatch.DrawString(Globals.DefaultFont, playerScore.ToString(), playerScorePos, Color.White);
            // villain text
            spriteBatch.DrawString(Globals.DefaultFont, villainScore.ToString(), villainScorePos, Color.White);
        }
    }
}
