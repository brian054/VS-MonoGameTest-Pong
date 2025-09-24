using Pong.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Pong.Shared;

// To manage collisions so I can clean up the Game1 and Ball files.

namespace Pong.Managers
{
    public class CollisionManager
    { 
        public CollisionManager() 
        {
            // hmmm
        }

        public void HandleCollisions(Rectangle playerRect, Rectangle villainRect, Ball theBall)
        {
            // check ball collision window
            if (theBall.ballRect.Y < 0 || theBall.ballRect.Y + theBall.ballSize > Globals.PreferredBackBufferHeight)
            {
                theBall.ballVelocity = new Vector2(theBall.ballVelocity.X, -theBall.ballVelocity.Y);
            }

            if (theBall.ballRect.X < 0 || theBall.ballRect.X + theBall.ballSize > Globals.PreferredBackBufferWidth)
            {
                theBall.ballVelocity = new Vector2(-theBall.ballVelocity.X, theBall.ballVelocity.Y);
            }

            // check player paddle collision ball

            // check enemy paddle collision ball
        }
        
    }
}
