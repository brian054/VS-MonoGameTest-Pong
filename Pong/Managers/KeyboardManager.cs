using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Managers
{
    public static class KeyboardManager
    {
        public static KeyboardState CurrentState { get; private set; }
        public static KeyboardState PreviousState { get; private set; }

        public static void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        public static bool WasKeyPressed(Keys key)
        {
            return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }
    }
}
