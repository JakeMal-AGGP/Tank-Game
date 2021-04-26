using Microsoft.Xna.Framework;


namespace LineDraw
{
    public class Circle : Ellipse
    {

        //public float Radius; 

        public Circle() { }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            SetRadius(radius);
        }
        
        public Circle(Vector2 center, float radius, int sides, float width, Color lineColor) 
        {
            Center = center;
            Sides = sides;
            Width = width;
            color = lineColor;
            SetRadius(radius);
        }

        public void SetRadius(float radius)
        {
            //Radius = radius;
            Axis.X = radius;
            Axis.Y = radius;
        }
        
    }

}
