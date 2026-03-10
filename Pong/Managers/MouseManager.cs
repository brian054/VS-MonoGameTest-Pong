using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Managers
{
    public static class MouseManager
    {
        public static MouseState CurrentState { get; private set; }
        public static MouseState PreviousState { get; private set; }

        public static void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Mouse.GetState();
        }

        public static bool LeftClicked()
        {
            return CurrentState.LeftButton == ButtonState.Pressed &&
                   PreviousState.LeftButton == ButtonState.Released;
        }
    }
}
