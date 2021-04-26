using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    public class Line
    {
        public static float PERCISION = 0.0001f;
        public Vector2 startPoint = Vector2.Zero;
        public Vector2 endPoint = Vector2.Zero;
        public float width = 2.0f;
        public Color color = Color.White;

        public Line()
        {
            // Default constructor
        }
        public Line(Vector2 start, Vector2 end)
        {
            startPoint = start;
            endPoint = end;
        }
        public Line(Vector2 start, Vector2 end, float Width)
        {
            startPoint = start;
            endPoint = end;
            width = Width;
        }
        public Line(Vector2 start, Vector2 end, Color LineColor)
        {
            startPoint = start;
            endPoint = end;

            color = LineColor;
        }
        public Line(Vector2 start, Vector2 end, float Width, Color LineColor)
        {
            startPoint = start;
            endPoint = end;
            width = Width;
            color = LineColor;
        }
        public Line(float startX, float startY, float endX, float endY)
        {
            startPoint = new Vector2(startX, startY);
            endPoint = new Vector2(endX, endY);
        }
        public Line(float startX, float startY, float endX, float endY, float Width)
        {
            startPoint = new Vector2(startX, startY);
            endPoint = new Vector2(endX, endY);
            width = Width;
        }
        public Line(float startX, float startY, float endX, float endY, Color LineColor)
        {
            startPoint = new Vector2(startX, startY);
            endPoint = new Vector2(endX, endY);
            color = LineColor;
        }
        public Line(float startX, float startY, float endX, float endY, float Width, Color LineColor)
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
            LineDrawer.DrawLine(spriteBatch, width, color, startPoint + Offset, endPoint +Offset);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Offset, float Rotation, Vector2 Scale)
        {
            // TO DO: Add in code for Rotation around a Point that is not the Origin
            LineDrawer.DrawLine(spriteBatch, width, color, LinePrimatives.RotatePoint(Vector2.Zero, Rotation, startPoint * Scale) + Offset, LinePrimatives.RotatePoint(Vector2.Zero, Rotation, endPoint * Scale) + Offset);
        }

        public float Length()
        {
            return ((endPoint - startPoint).Length());
        }

        public Vector2 MidPoint()
        {
            return ((startPoint + endPoint)/2); 
        }

        public float Slope()
        {
            return (( endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X));
        }

        public float SlopePerpendicular()
        {
            return -((endPoint.X - startPoint.X) / (endPoint.Y - startPoint.Y));
        }

        public float Angle()
        {
            return (LinePrimatives.V2ToAngle(startPoint, endPoint));
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
