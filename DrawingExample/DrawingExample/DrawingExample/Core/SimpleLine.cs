using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    public class SimpleLine
    {
        public Vector2 startPoint = Vector2.Zero;
        public Vector2 endPoint = Vector2.Zero;
        public float width = 2.0f;
        public Color color = Color.White;

        // constructors
        public SimpleLine()
        {
            // Default constructor
        }
        public SimpleLine(Vector2 start, Vector2 end)
        {
            startPoint = start;
            endPoint = end;
        }
        public SimpleLine(Vector2 start, Vector2 end, float Width)
        {
            startPoint = start;
            endPoint = end;
            width = Width;
        }
        public SimpleLine(Vector2 start, Vector2 end, Color LineColor)
        {
            startPoint = start;
            endPoint = end;

            color = LineColor;
        }
        public SimpleLine(Vector2 start, Vector2 end, float Width, Color LineColor)
        {
            startPoint = start;
            endPoint = end;
            width = Width;
            color = LineColor;
        }
        public SimpleLine(float startX, float startY, float endX, float endY)
        {
            startPoint = new Vector2(startX, startY);
            endPoint = new Vector2(endX, endY);
        }
        public SimpleLine(float startX, float startY, float endX, float endY, float Width)
        {
            startPoint = new Vector2(startX, startY);
            endPoint = new Vector2(endX, endY);
            width = Width;
        }
        public SimpleLine(float startX, float startY, float endX, float endY, Color LineColor)
        {
            startPoint = new Vector2(startX, startY);
            endPoint = new Vector2(endX, endY);
            color = LineColor;
        }
        public SimpleLine(float startX, float startY, float endX, float endY, float Width, Color LineColor)
        {
            startPoint = new Vector2(startX, startY);
            endPoint = new Vector2(endX, endY);
            width = Width;
            color = LineColor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LineDrawer.DrawLine(spriteBatch, width, color, startPoint, endPoint);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Offset)
        {
            // TO DO: Add in code for Rotation around a Point that is not the Origin
            LineDrawer.DrawLine(spriteBatch, width, color, startPoint + Offset, endPoint + Offset);
        }

        public float Length()
        {
            return ((endPoint - startPoint).Length());
        }

        public void Invert()
        {
            // Inverts line by switching end point and start point. 
            Vector2 temp = startPoint;
            startPoint = endPoint;
            endPoint = startPoint;
        }

    }
}
