using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.GameStates
{
    public interface IGameState
    {
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch) { }
        // every state has Update(), and probably will have Draw()
    }
}
