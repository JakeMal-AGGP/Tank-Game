using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    public static class LinePrimatives
    {
        public static Vector2 CircleRadiusPoint(Vector2 Origin, float angle, float radius)
        {
            //all calculations go here

            double r = angle * (Math.PI / 180);

            double x = (Origin.X + (radius * Math.Cos(r)));
            double y = (Origin.Y + (radius * Math.Sin(r)));

            Vector2 RadiusPoint = new Vector2((float)x, (float)y);

            return RadiusPoint;

        }
        public static void DrawPoint(SpriteBatch spriteBatch, float width, Color color, Vector2 location)
        {
             LinePrimatives.DrawCircle(spriteBatch, width/2, color, location, width/2, (int) width*2);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, float width, Color color, Rectangle Rec)
        {
            LineDrawer.DrawLine(spriteBatch, width, color, new Vector2((Rec.X), (Rec.Y)), new Vector2((Rec.X + Rec.Width), (Rec.Y)));
            LineDrawer.DrawLine(spriteBatch, width, color, new Vector2((Rec.X + Rec.Width), Rec.Y), new Vector2((Rec.X + Rec.Width), (Rec.Y + Rec.Height)));
            LineDrawer.DrawLine(spriteBatch, width, color, new Vector2((Rec.X + Rec.Width), (Rec.Y + Rec.Height)), new Vector2((Rec.X), (Rec.Y + Rec.Height)));
            LineDrawer.DrawLine(spriteBatch, width, color, new Vector2((Rec.X), (Rec.Y + Rec.Height)), new Vector2((Rec.X), (Rec.Y)));
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, float width, Color color, Rectangle Rec, float roation)
        {
            Vector2 Center = new Vector2(Rec.Center.X, Rec.Center.Y);

            DrawRectangle(spriteBatch, width, color, Rec, roation, Center);

        }

        public static void DrawRectangle(SpriteBatch spriteBatch, float width, Color color, Rectangle Rec, float Roation, Vector2 Center)
        {
            LineDrawer.DrawLine(spriteBatch, width, color, RotatePoint(Center, Roation, new Vector2((Rec.X), (Rec.Y))), LinePrimatives.RotatePoint(Center, Roation, new Vector2((Rec.X + Rec.Width), (Rec.Y))));
            LineDrawer.DrawLine(spriteBatch, width, color, RotatePoint(Center, Roation, new Vector2((Rec.X + Rec.Width), Rec.Y)), LinePrimatives.RotatePoint(Center, Roation, new Vector2((Rec.X + Rec.Width), (Rec.Y + Rec.Height))));
            LineDrawer.DrawLine(spriteBatch, width, color, RotatePoint(Center, Roation, new Vector2((Rec.X + Rec.Width), (Rec.Y + Rec.Height))), LinePrimatives.RotatePoint(Center, Roation, new Vector2((Rec.X), (Rec.Y + Rec.Height))));
            LineDrawer.DrawLine(spriteBatch, width, color, RotatePoint(Center, Roation, new Vector2((Rec.X), (Rec.Y + Rec.Height))), LinePrimatives.RotatePoint(Center, Roation, new Vector2((Rec.X), (Rec.Y))));
        }

        public static void DrawSolidRectangle(SpriteBatch spriteBatch, Color color, Rectangle Rec)
        {
            float width = Rec.Width;
            LineDrawer.DrawLine(spriteBatch, width, color, new Vector2(Rec.Center.X, Rec.Bottom), new Vector2(Rec.Center.X, Rec.Top));

        }

        public static void DrawSolidRectangle(SpriteBatch spriteBatch, Color color, Rectangle Rec, float Roation, Vector2 Center)
        {
            float width = Rec.Width;
            LineDrawer.DrawLine(spriteBatch, width, color, RotatePoint(Center, Roation, new Vector2(Rec.Center.X, Rec.Bottom)), RotatePoint(Center, Roation, new Vector2(Rec.Center.X, Rec.Top)));

        }

        public static void DrawSolidRectangle(SpriteBatch spriteBatch, Color color, Rectangle Rec, float Roation)
        {
            DrawSolidRectangle(spriteBatch, color, Rec, Roation, new Vector2(Rec.Center.X, Rec.Center.Y));
        }

        public static void DrawCircle(SpriteBatch spriteBatch, float width, Color color, Vector2 Position, float Radius, int Sides)
        {
            Vector2 Starting = new Vector2(Radius, 0);
            Vector2 Next;
            Vector2 Previous = Starting;
            float degrees = 360 / Sides;

            //Console.WriteLine(degrees); 

            if (Sides < 3)
            {
                Sides = 36;
            }

            for (int i = 1; i < Sides; i++)
            {
                /// This is drawing the circle Counter Clock Wise) 

                Next = AngleToV2((i * degrees), Radius);
                LineDrawer.DrawLine2(spriteBatch, width, color, Position + Previous, Position + Next);

                // Debug Line
                //Console.WriteLine(i+"*" + (i * degrees) + "::" + Previous + "::" + Next); 

                Previous = Next;

            }

            LineDrawer.DrawLine2(spriteBatch, width, color, Position + Previous, Position + Starting);

        }

        public static void DrawSolidCircle(SpriteBatch spriteBatch, Color color, Vector2 Center, float RadiusIN)
        {
            if (RadiusIN == 0) { return; } // No Radius, No Circle

            Rectangle circlebox = new Rectangle();
            float Radius = RadiusIN;
            if (Radius < 0) { Radius = -Radius; }

            int Sides = (int)(Radius / 10);
            if (Sides < 10) { Sides = 10; }

            circlebox.Height = (int)(Radius * 2);
            circlebox.Width = 32;

            if (Radius < 100)
            {
                float val = Radius;
                if (val < 20) { val = 20; }
                circlebox.Width = (int)(32 * (Radius / 100));
            }

            circlebox.X = (int)(Center.X - circlebox.Width / 2);
            circlebox.Y = (int)(Center.Y - Radius);

            //Rectangle circlebox = new Rectangle((int)location.X - 16, (int)location.Y - 100, 32, 200);

            for (int i = 0; i < Sides; i++)
            {
                DrawSolidRectangle(spriteBatch, Color.Red, circlebox, i * (180.0f / Sides));
            }
        }

        public static void DrawArc(SpriteBatch spriteBatch, float width, Color color, Vector2 Position, float OuterRadius, float InnerRadius, float Arc, float Rotation, int Sides)
        {
            float degrees = Arc / Sides;

            Vector2 OuterStart = AngleToV2(Rotation - Arc / 2, OuterRadius);
            Vector2 OuterEnd = AngleToV2(Rotation + Arc / 2, OuterRadius);
            Vector2 InnerStart = AngleToV2(Rotation - Arc / 2, InnerRadius);
            Vector2 InnerEnd = AngleToV2(Rotation + Arc / 2, InnerRadius);

            if (Sides < 3)
            {
                Sides = 12;
            }

            // Draw the Side of the Arc
            LineDrawer.DrawLine(spriteBatch, width, color, Position + OuterStart, Position + InnerStart);
            LineDrawer.DrawLine(spriteBatch, width, color, Position + OuterEnd, Position + InnerEnd);

            if (InnerRadius > 0)
            {
                LineDrawer.DrawLine(spriteBatch, width, color, Position + InnerStart, Position + InnerEnd);
            }

            Vector2 Next;
            Vector2 Previous = OuterStart;

            for (int i = 1; i < Sides; i++)
            {
                /// This is drawing the circle Counter Clock Wise) 

                Next = AngleToV2((i * degrees) + (Rotation - Arc / 2), OuterRadius);
                LineDrawer.DrawLine(spriteBatch, width, color, Position + Previous, Position + Next);

                // Debug Line
                //Console.WriteLine(i+"*" + (i * degrees) + "::" + Previous + "::" + Next); 

                Previous = Next;

            }
            LineDrawer.DrawLine(spriteBatch, width, color, Position + Previous, Position + OuterEnd);


        }

        public static void DrawEllipse(SpriteBatch spriteBatch, float width, Color color, Vector2 Position, Vector2 Axis, int Sides)
        {
            Vector2 Starting = new Vector2(Axis.X, 0);
            Vector2 Next;
            Vector2 Previous = Starting;
            float degrees = 360 / Sides;

            //Console.WriteLine(degrees); 

            if (Sides < 3)
            {
                Sides = 36;
            }

            for (int i = 1; i < Sides; i++)
            {
                /// This is drawing the circle Counter Clock Wise) 

                Next = AngleToEllipseV2((i * degrees), 1.0f, Axis);
                LineDrawer.DrawLine2(spriteBatch, width, color, Position + Previous, Position + Next);

                // Debug Line
                //Console.WriteLine(i+"*" + (i * degrees) + "::" + Previous + "::" + Next); 

                Previous = Next;

            }

            LineDrawer.DrawLine2(spriteBatch, width, color, Position + Previous, Position + Starting);

        }

        public static void DrawEllipse(SpriteBatch spriteBatch, float width, Color color, Vector2 Position, Vector2 Axis, float Rotation, int Sides)
        {
            DrawEllipse(spriteBatch, width, color, Position, Axis, Rotation, Vector2.Zero, Sides);
        }

        public static void DrawEllipse(SpriteBatch spriteBatch, float width, Color color, Vector2 Position, Vector2 Axis, float Rotation, Vector2 Center, int Sides)
        {
            Vector2 RoationCenter = Vector2.Zero;
            if (Center != Vector2.Zero)
                RoationCenter = Center - Position;

            Vector2 Starting = RotatePoint(RoationCenter, Rotation, new Vector2(Axis.X, 0));
            Vector2 Next;
            Vector2 Previous = Starting;
            float degrees = 360 / Sides;

            //Console.WriteLine(degrees); 

            if (Sides < 3)
            {
                Sides = 36;
            }

            for (int i = 1; i < Sides; i++)
            {
                /// This is drawing the circle Counter Clock Wise) 

                Next = RotatePoint(RoationCenter, Rotation, AngleToEllipseV2((i * degrees), 1.0f, Axis));
                LineDrawer.DrawLine2(spriteBatch, width, color, Position + Previous, Position + Next);

                // Debug Line
                //Console.WriteLine(i+"*" + (i * degrees) + "::" + Previous + "::" + Next); 

                Previous = Next;

            }

            LineDrawer.DrawLine2(spriteBatch, width, color, Position + Previous, Position + Starting);

        }

        public static void DrawEllipse(SpriteBatch spriteBatch, float width, Color color, Rectangle Rec, int sides)
        {
            Vector2 Center = Vector2.Zero;
            Vector2 Axis = Vector2.Zero;

            Center.X = Rec.Center.X;
            Center.Y = Rec.Center.Y;
            Axis.X = Rec.Width / 2.0f;
            Axis.Y = Rec.Height / 2.0f;

            DrawEllipse(spriteBatch, width, color, Center, Axis, sides);
        }

        public static void DrawEllipse(SpriteBatch spriteBatch, float width, Color color, Rectangle Rec, float roation, int sides)
        {
            DrawEllipse(spriteBatch, width, color, Rec, roation, Vector2.Zero, sides);
        }

        public static void DrawEllipse(SpriteBatch spriteBatch, float width, Color color, Rectangle Rec, float roation, Vector2 RotCenter, int Sides)
        {
            Vector2 DrawCenter = Vector2.Zero;
            Vector2 Axis = Vector2.Zero;

            DrawCenter.X = Rec.Center.X;
            DrawCenter.Y = Rec.Center.Y;
            Axis.X = Rec.Width / 2.0f;
            Axis.Y = Rec.Height / 2.0f;

            DrawEllipse(spriteBatch, width, color, DrawCenter, Axis, roation, RotCenter, Sides);
        }

        public static Vector2 AngleToV2(float angle, float length)
        {
            Vector2 direction = Vector2.Zero;
            direction.X = (float)Math.Cos(MathHelper.ToRadians(angle)) * length;
            direction.Y = (float)Math.Sin(MathHelper.ToRadians(angle)) * length;
            return direction;
        }

        public static Vector2 AngleToEllipseV2(float angle, float length, Vector2 Axis)
        {
            Vector2 direction = Vector2.Zero;
            direction.X = (float)Math.Cos(MathHelper.ToRadians(angle)) * Axis.X * length;
            direction.Y = (float)Math.Sin(MathHelper.ToRadians(angle)) * Axis.Y * length;
            return direction;
        }

        public static Vector2 AngleToEllipseV2(float angle, Vector2 Axis)
        {
            return AngleToEllipseV2(angle, 1f, Axis);
        }

        public static float V2ToAngle(Vector2 startPoint, Vector2 endPoint)
        {
            float xDiff = startPoint.X - endPoint.X;
            float yDiff = startPoint.Y - endPoint.Y;
            return (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
        }


        public static Vector2 RotatePoint(Vector2 Center, float angle, Vector2 pointIN)
        {
            Vector2 point = new Vector2(pointIN.X, pointIN.Y);
            float sin = (float)Math.Sin(MathHelper.ToRadians(angle));
            float cos = (float)Math.Cos(MathHelper.ToRadians(angle));

            // translate point back to origin:
            point.X -= Center.X;
            point.Y -= Center.Y;

            // rotate point
            float xnew = point.X * cos - point.Y * sin;
            float ynew = point.X * sin + point.Y * cos;

            // translate point back:
            point.X = xnew + Center.X;
            point.Y = ynew + Center.Y;

            return point;
        }
    }
}

