using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    public class LineObject
    {
        public float Roation = 0;
        public Vector2 Scale = Vector2.One;
        public Vector2 Location = Vector2.Zero;
        public List<Line> Lines = new List<Line>();

        public LineObject()
        {
            Initalize();
        }

        public virtual void Initalize()
        { // Intended to be overriden in sub classes 
        }

        public void Add(Line NewLine)
        {
            Lines.Add(NewLine);
        }

        public void UpdateTranslations(Vector2 location, float roation, Vector2 scale)
        {
            Location = location;
            Roation = roation;
            Scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].Draw(spriteBatch, Location, Roation, Scale);
                }
            }
        }

        // these methods ALTER the object
        public void ScaleLocal(float scale)
        {
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].startPoint = Lines[i].startPoint * scale;
                    Lines[i].endPoint = Lines[i].endPoint * scale;
                }
            }
        }

        public void ScaleLocal(float scaleX, float scaleY)
        {
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].startPoint.X = Lines[i].startPoint.X * scaleX;
                    Lines[i].startPoint.Y = Lines[i].startPoint.Y * scaleY;
                    Lines[i].endPoint.X = Lines[i].endPoint.X * scaleX;
                    Lines[i].endPoint.Y = Lines[i].endPoint.Y * scaleY;
                }
            }
        }

        public void ScaleLocal(Vector2 scale)
        {
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].startPoint = Lines[i].startPoint * scale;
                    Lines[i].endPoint = Lines[i].endPoint * scale;
                }
            }
        }

        public void TranslateLocal(float X, float Y)
        {
            TranslateLocal(new Vector2(X, Y));
        }

        public void TranslateLocal(Vector2 translate)
        {
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].startPoint = Lines[i].startPoint - translate;
                    Lines[i].endPoint = Lines[i].endPoint - translate;
                }
            }
        }

        public void RotateLocal(float angle)
        {
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].startPoint = LinePrimatives.RotatePoint(Vector2.Zero, angle, Lines[i].startPoint);
                    Lines[i].endPoint = LinePrimatives.RotatePoint(Vector2.Zero, angle, Lines[i].endPoint);
                }
            }
        }

        public void LineWidthLocal(float LineWidth)
        {
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].width = LineWidth;
                }
            }
        }

        public void LineColorLocal(Color lineColor)
        {
            if (Lines.Count != 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].color = lineColor;
                }
            }
        }

    }
}
