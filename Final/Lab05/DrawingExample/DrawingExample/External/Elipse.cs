using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{

    public class Ellipse
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Center = Vector2.Zero;
        public Vector2 Axis = Vector2.One;
        public float Roation = 0;
        public int Sides = 32;
        public float Width = 2.0f;
        public Color color = Color.White;

        public Ellipse() { }

        public Ellipse(Rectangle axis)
        {
            Position.X = axis.Center.X;
            Position.Y = axis.Center.Y;
            Axis.X = (axis.Width / 2.0f);
            Axis.Y = (axis.Height / 2.0f);

            // Ensure the Axis is not negative values. 
            if (Axis.X < 0) { Axis.X = -Axis.X; }
            if (Axis.Y < 0) { Axis.Y = -Axis.Y; }
        }

        public Ellipse(Vector2 position, Vector2 axis)
        {
            Position = position;
            Axis = axis;
        }

        public Ellipse(Vector2 position, float radius)
        {
            Position = position;
            Axis = new Vector2(radius,radius);
        }

        public Ellipse(Vector2 position, Vector2 axis, float roation, int sides, float width, Color colorIN)
        {
            Position = position;
            Axis = axis;
            Roation = roation;
            Sides = sides;
            Width = width;
            color = colorIN;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LinePrimatives.DrawEllipse(spriteBatch, Width, color, Position, Axis, Roation, Center, Sides);
        }

        public LineObject ConvertToLineObject()
        {
            LineObject result = new LineObject();
            // TO DO: WRITE CODE!
            return result;
        }

        public float Radius(float angle)
        {
            return LinePrimatives.AngleToEllipseV2(angle + Roation, Axis).Length();  
        }

    }
}
