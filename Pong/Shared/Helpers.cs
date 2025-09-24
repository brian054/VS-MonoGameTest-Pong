using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Shared
{
    public static class Helpers
    {
        public static Texture2D CreateCircleTexture(GraphicsDevice graphicsDevice, int radius)
        {
            int diameter = radius * 2;
            Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);

            Color[] data = new Color[diameter * diameter];
            float radiusSquared = radius * radius;

            for (int y = 0; y < diameter; y++)
            {
                for (int x = 0; x < diameter; x++)
                {
                    int index = y * diameter + x;
                    Vector2 distance = new Vector2(x - radius, y - radius);
                    if (distance.LengthSquared() <= radiusSquared)
                        data[index] = Color.White;
                    else
                        data[index] = Color.Transparent;
                }
            }

            texture.SetData(data);
            return texture;
        }

        //public static bool IsCircleRectColliding(Vector2 circleCenter, float radius, Rectangle rect)
        //{
        //    float nearestX = MathHelper.Clamp(circleCenter.X, rect.Left, rect.Right);
        //    float nearestY = MathHelper.Clamp(circleCenter.Y, rect.Top, rect.Bottom);

        //    float dx = circleCenter.X - nearestX;
        //    float dy = circleCenter.Y - nearestY;

        //    return (dx * dx + dy * dy) < (radius * radius);
        //}

        //public static bool AABB_Collision(Rectangle r1, Rectangle r2)
        //{
        //    if (r1.Intersects(r2))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
