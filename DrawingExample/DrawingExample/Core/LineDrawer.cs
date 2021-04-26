/*  XNA Line Drawer Class
 *  
 *  Author: Gregory Walek
 * 
 *  Based on the Code Located at 
 *  http://www.xnawiki.com/index.php?title=Drawing_2D_lines_without_using_primitives
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    public static class LineDrawer
    {
        static Texture2D blank;

        public static void InitateLineDrawer(GraphicsDevice GraphicsDevice)
        {
            blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
        }

        public static void DrawPixel(SpriteBatch spriteBatch, Color color, Vector2 screenPoint)
        {
            spriteBatch.Draw(blank, screenPoint, color); 
        }

        public static void DrawLine(SpriteBatch spriteBatch, float width, Color color, Vector2 point1, Vector2 point2)
        {
            if (blank == null)
            {
                InitateLineDrawer(spriteBatch.GraphicsDevice);
            }

            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            Vector2 offset = new Vector2(0.0f, 0.5f);

            spriteBatch.Draw(blank, point1, null, color,
                       angle, offset, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }

        public static void DrawLine(SpriteBatch spriteBatch, float width, Color color, Line line)
        {
            DrawLine(spriteBatch, width, color, line.startPoint, line.endPoint);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Line line)
        {
            line.Draw(spriteBatch);
        }

        public static void DrawLine2(SpriteBatch spriteBatch, float width, Color color, Vector2 point1, Vector2 point2)
        {
            if (blank == null)
            {
                InitateLineDrawer(spriteBatch.GraphicsDevice);
            }

            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            spriteBatch.Draw(blank, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }

        public static void DrawLine2(SpriteBatch spriteBatch, float width, Color color, Line line)
        {
            DrawLine2(spriteBatch, width, color, line.startPoint, line.endPoint);
        }

    }
 
}