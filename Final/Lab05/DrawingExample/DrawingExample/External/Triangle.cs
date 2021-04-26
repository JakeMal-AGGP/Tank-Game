using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LineDraw;
using DrawingExample;

namespace LineDraw
{
    public class Triangle
    {
        public float lineWidth = 5.0f;
        public Color lineColor = Color.SeaGreen;
        public bool solid = false;
        public Vector2 PointA;
        public Vector2 PointB;
        public Vector2 PointC;

        public Triangle() { }

        public Triangle(Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            PointA = pointA;
            PointB = pointB;
            PointC = pointC;
        }

        public Triangle(Vector2 pointA, Vector2 pointB, Vector2 pointC, float width, Color color)
        {
            PointA = pointA;
            PointB = pointB;
            PointC = pointC;
            lineWidth = width;
            lineColor = color;
        }

        public bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
        {
            Vector3 cp1 = Vector3.Cross(b - a, p1 - a);
            Vector3 cp2 = Vector3.Cross(b - a, p2 - a);

            if(Vector3.Dot(cp1, cp2)>=0) { return true; }
            return false;
        }

        public bool PointInTriangle(Vector2 point)
        {
            Vector3 pv3 = new Vector3(point.X, point.Y, 0);
            Vector3 pav3 = new Vector3(PointA.X, PointA.Y, 0);
            Vector3 pbv3 = new Vector3(PointB.X, PointB.Y, 0);
            Vector3 pcv3 = new Vector3(PointC.X, PointC.Y, 0);

            if(SameSide(pv3, pav3, pbv3, pcv3) && SameSide(pv3, pbv3, pav3, pcv3) && SameSide(pv3, pcv3, pav3, pbv3)) { return true; }

            return false;
        }

        public void fillTriangle(SpriteBatch spriteBatch)
        {
            float[] arrX = { PointA.X, PointB.X, PointC.X };
            float[] arrY = { PointA.Y, PointB.Y, PointC.Y };

            float minX = arrX.Min();
            float maxX = arrX.Max();
            float minY = arrY.Min();
            float maxY = arrY.Max();

            /*
            LineDrawer.DrawLine(spriteBatch, 3, Color.White, new Vector2(minX, minY), new Vector2(maxX, minY));
            LineDrawer.DrawLine(spriteBatch, 3, Color.White, new Vector2(maxX, minY), new Vector2(maxX, maxY));
            LineDrawer.DrawLine(spriteBatch, 3, Color.White, new Vector2(maxX, maxY), new Vector2(minX, maxY));
            LineDrawer.DrawLine(spriteBatch, 3, Color.White, new Vector2(minX, maxY), new Vector2(minX, minY));
            */

            for(int yPoint = (int)minY; yPoint <= maxY; yPoint++)
            {
                for(int xPoint = (int)minX; xPoint < maxX; xPoint++)
                {
                    if(PointInTriangle(new Vector2(xPoint, yPoint) + new Vector2(.5f, .5f)))
                    {
                        LineDrawer.DrawPixel(spriteBatch, lineColor, new Vector2(xPoint, yPoint) + new Vector2(.5f, .5f));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(solid)
            {
                fillTriangle(spriteBatch);
            }
            else
            {
                LinePrimatives.DrawTriangle(spriteBatch, lineWidth, lineColor, PointA, PointB, PointC);
            }
        }
    }
}
