using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * abstract class: a base class that cannot be instantiated on its own. It's meant to be inherited by other classes.
 * 
 * An abstract function cannot have functionality. You're basically saying, any child class MUST give their own 
 * version of this method, however it's too general to even try to implement in the parent class.
 * 
 * abstract functions are also required to be implemented in subclasses.
 * 
 * A virtual function, is basically saying look, here's the functionality that may or may not be good enough for 
 * the child class (so it's optional). Can also be empty. So if it is good enough, use this method, if not, then override 
 * me, and provide your own functionality.
 */

namespace Pong.Entities
{
    public abstract class Entity
    {
        //public Vector2 Position { get; set; }
        //public Vector2 Speed { get; set; }

        public Entity()
        {
            //Position = Position;
            //Speed = Vector2.Zero;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
