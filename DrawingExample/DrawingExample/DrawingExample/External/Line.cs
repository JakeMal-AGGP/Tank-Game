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

        public Vector2? Intersect(Line other)
        {
            Vector2? Result = null;

            float ua = (other.endPoint.X - other.startPoint.X) * (this.startPoint.Y - other.startPoint.Y) -
                            (other.endPoint.Y - other.startPoint.Y) * (this.startPoint.X - other.startPoint.X);
            float ub = (this.endPoint.X - this.startPoint.X) * (this.startPoint.Y - other.startPoint.Y) -
                            (this.endPoint.Y - this.startPoint.Y) * (this.startPoint.X - other.startPoint.X);
            float denominator = (other.endPoint.Y - other.startPoint.Y) * (this.endPoint.X - this.startPoint.X) -
                            (other.endPoint.X - other.startPoint.X) * (this.endPoint.Y - this.startPoint.Y);


            if (Math.Abs(denominator) <= PERCISION)
            {
                if (Math.Abs(ua) <= PERCISION && Math.Abs(ub) <= PERCISION)
                {
                    Result = (this.startPoint + this.endPoint) / 2;
                }
            }
            else
            {
                ua /= denominator;
                ub /= denominator;

                if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
                {
                    Result = new Vector2?(
                                new Vector2(
                                        this.startPoint.X + ua * (this.endPoint.X - this.startPoint.X),
                                        this.startPoint.Y + ua * (this.endPoint.Y - this.startPoint.Y)
                                            )
                                        );
                }
            }

            return Result;
        }

        public Vector2? Intersect(Rectangle other)
        {
            // TO DO: CONVERT THIS USING THE LINEOBJECT... USE THIS A STARTING POINT
            // Returns Intersect point. None: returns Null. 2 pts: returns closer to startpoint.  
            Vector2? next;
            List<Vector2?> points = new List<Vector2?>();

            Line LineA = new Line(other.Left, other.Top, other.Right, other.Top);
            Line LineB = new Line(other.Left, other.Top, other.Left, other.Bottom);
            Line LineC = new Line(other.Right, other.Top, other.Right, other.Bottom);
            Line LineD = new Line(other.Left, other.Bottom, other.Right, other.Bottom);

            next = this.Intersect(LineA);
            if (next != null) { points.Add(next.Value); }
            next = this.Intersect(LineB);
            if (next != null) { points.Add(next.Value); }
            next = this.Intersect(LineC);
            if (next != null) { points.Add(next.Value); }
            next = this.Intersect(LineD);
            if (next != null) { points.Add(next.Value); }

            if (points.Count == 0) { return null; }
            if (points.Count == 1) { return points[0]; }

            if (Vector2.Distance(startPoint, points[0].Value) <= Vector2.Distance(startPoint, points[1].Value))
            { return points[0]; }
            else
            { return points[1]; }
        }

        public Vector2? Intersect(Ellipse other)
        {
            // If the ellipse or line segment are empty, (that is smaller than the percision) forget the work and return no intersections.
            if ((this.Length() <= PERCISION) && (other.Axis.X <= PERCISION) && (other.Axis.Y <= PERCISION))
            { return null; }

            // Make sure the axis has non-negative width and height.
            if (other.Axis.X < 0) { other.Axis.X = -other.Axis.X; }
            if (other.Axis.Y < 0) { other.Axis.Y = -other.Axis.Y; }



            // Need to translate the start and end points of the line
            // the need 
            Vector2 tstartPoint = startPoint - other.Position;
            Vector2 tendPoint = endPoint - other.Position;

            float Offsetroation = -other.Roation;

            if (other.Roation != 0)
            {
                tstartPoint = LinePrimatives.RotatePoint(Vector2.Zero, Offsetroation, tstartPoint);
                tendPoint = LinePrimatives.RotatePoint(Vector2.Zero, Offsetroation, tendPoint);
            }

            Vector2 AxisLocation = Vector2.Zero;
            AxisLocation.X = other.Axis.X;
            AxisLocation.Y = other.Axis.Y;

            if (AxisLocation.X < 0) { AxisLocation.X = -AxisLocation.X; }
            if (AxisLocation.Y < 0) { AxisLocation.Y = -AxisLocation.Y; }
            // Calculate the quadratic parameters.

            float A = (tendPoint.X - tstartPoint.X) * (tendPoint.X - tstartPoint.X) / AxisLocation.X / AxisLocation.X +
                          (tendPoint.Y - tstartPoint.Y) * (tendPoint.Y - tstartPoint.Y) / AxisLocation.Y / AxisLocation.Y;
            float B = 2 * tstartPoint.X * (tendPoint.X - tstartPoint.X) / AxisLocation.X / AxisLocation.X +
                          2 * tstartPoint.Y * (tendPoint.Y - tstartPoint.Y) / AxisLocation.Y / AxisLocation.Y;
            float C = tstartPoint.X * tstartPoint.X / AxisLocation.X / AxisLocation.X + tstartPoint.Y * tstartPoint.Y / AxisLocation.Y / AxisLocation.Y - 1;


            // Make a list of t values.
            List<float> t_values = new List<float>();

            // Calculate the discriminant.
            float discriminant = B * B - 4 * A * C;
            if (discriminant == 0)
            {
                // One real solution.
                t_values.Add(-B / 2 / A);

            }
            else if (discriminant > 0)
            {
                // Two real solutions.
                t_values.Add((float)((-B + Math.Sqrt(discriminant)) / 2 / A));
                t_values.Add((float)((-B - Math.Sqrt(discriminant)) / 2 / A));

            }

            //Console.WriteLine(t_values.Count);
            // Convert the t values into points.
            List<Vector2?> points = new List<Vector2?>();
            foreach (float t in t_values)
            {
                // If the points are on the segment add them to the list.
                if (((t >= 0f) && (t <= 1f)))
                {
                    float x = tstartPoint.X + (tendPoint.X - tstartPoint.X) * t;
                    float y = tstartPoint.Y + (tendPoint.Y - tstartPoint.Y) * t;
                    Vector2 tVec = new Vector2(x, y);
                    if (other.Roation != 0)
                    {
                        tVec = LinePrimatives.RotatePoint(Vector2.Zero, -Offsetroation, tVec);

                    }
                    tVec += other.Position;
                    points.Add(tVec);

                }
            }


            // Return the closest point to the line start point if an intersection exists
            if (points.Count == 0) { return null; }
            if (points.Count == 1) { return points[0]; }


            if (Vector2.Distance(startPoint, points[0].Value) <= Vector2.Distance(startPoint, points[1].Value))
            { return points[0]; }
            else
            { return points[1]; }

        }

        public Vector2? Intersect(Circle other)
        {
            return Intersect(other.Center, other.Radius(Angle()));
        }

        public Vector2? Intersect(Vector2 Center, float Radius)
        {
           return Intersect(new Ellipse(Center, Radius)); 
        }
        
        public bool Intersects(Vector2 p)
        {
            if (DistanceLineSegmentToPoint(startPoint, endPoint, p) <= PERCISION) { return true; }
            else { return false; }
        }

        public bool Intersects(Line other)
        {
            if (this.Intersect(other) != null) { return true; }
            else { return false; }
        }

        public bool Intersects(Rectangle other)
        {
            if (this.Intersect(other) != null) { return true; }
            else { return false; }
        }

        public bool Intersects(Ellipse other)
        {
            if (this.Intersect(other) != null) { return true; }
            else { return false; }
        }

        public bool Intersects(Circle other)
        {
            if (this.Intersect(other.Center, other.Radius(Angle())) != null) { return true; }
            else { return false; }
        }

        public bool Intersects(Vector2 Center, float Radius)
        {
            if (this.Intersect(Center, Radius) != null) { return true; }
            else { return false; }
        }

        private float DistanceLineSegmentToPoint(Vector2 A, Vector2 B, Vector2 p)
        {
            //get the normalized line segment vector
            Vector2 v = B - A;
            v.Normalize();

            //determine the point on the line segment nearest to the point p
            float distanceAlongLine = Vector2.Dot(p, v) - Vector2.Dot(A, v);
            Vector2 nearestPoint;
            if (distanceAlongLine < 0)
            {
                //closest point is A
                nearestPoint = A;
            }
            else if (distanceAlongLine > Vector2.Distance(A, B))
            {
                //closest point is B
                nearestPoint = B;
            }
            else
            {
                //closest point is between A and B... A + d  * ( ||B-A|| )
                nearestPoint = A + distanceAlongLine * v;
            }

            //Calculate the distance between the two points
            float actualDistance = Vector2.Distance(nearestPoint, p);
            return actualDistance;
        }

        public float LineAngle()
        {
            return LinePrimatives.V2ToAngle(startPoint, endPoint);
        }

    }
}
