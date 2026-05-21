using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Shared
{
    public static class Helpers
    {
        // TODO: can we remove this?
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

        // To generalize the ball and paddle collision zones code to determine Ball direction angle.
        public static float GetBounceOffsetFromZones(float t, int zoneCount, float maxOffset)
        {
            if (zoneCount <= 1)
                return 0f;

            t = MathHelper.Clamp(t, 0f, 1f);

            int zone = (int)(t * zoneCount);

            if (zone >= zoneCount)
                zone = zoneCount - 1;

            float normalizedZone = zone / (float)(zoneCount - 1);

            return MathHelper.Lerp(-maxOffset, maxOffset, normalizedZone);
        }

        // Potentially could move all eventManager methods into here

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
