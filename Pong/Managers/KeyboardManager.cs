using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Managers
{
    // Do we truly need this? Hmmmm
    // public static class KeyboardManager
    // {
    //     private static KeyboardState currentKeyState;
    //     private static KeyboardState previousKeyState;

    //     public static void Update()
    //     {
    //         previousKeyState = currentKeyState;
    //         currentKeyState = Keyboard.GetState();
    //     }

    //     public static bool IsKeyPressed(Keys key)
    //     {
    //         return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    //     }

    //     public static bool IsKeyHeld(Keys key)
    //     {
    //         return currentKeyState.IsKeyDown(key);
    //     }

    //     public static bool IsKeyReleased(Keys key)
    //     {
    //         return !currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyDown(key);
    //     }
    // }
}
