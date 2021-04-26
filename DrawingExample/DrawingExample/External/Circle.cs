using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LineDraw;
using DrawingExample;


namespace LineDraw
{
    public class Circle : Ellipse
    {

        public bool solid = false;
        public float rad; // radius

        public Circle() { }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            rad = radius;
            SetRadius(radius);
        }
        
        public Circle(Vector2 center, float radius, int sides, float width, Color lineColor) 
        {
            Center = center;
            Sides = sides;
            Width = width;
            color = lineColor;
            rad = radius;
            SetRadius(radius);
        }

        public void SetRadius(float radius)
        {
            //Radius = radius;
            Axis.X = radius;
            Axis.Y = radius;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(solid)
            {
                LinePrimatives.DrawSolidCircle(spriteBatch, color, Center, Axis.X);
            }
            else
            {
                LinePrimatives.DrawCircle(spriteBatch, Width, color, Center, Axis.X, Sides);
            }
        }

    }

}
