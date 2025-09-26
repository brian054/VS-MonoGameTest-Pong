
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pong.Shared
{
    public static class Globals
    {
        public const int PreferredBackBufferWidth = 960;
        public const int PreferredBackBufferHeight = 540;

        public static Texture2D dummyTexture;

        public static readonly Random Random = new Random();

        public static SpriteFont DefaultFont;

    }
}
