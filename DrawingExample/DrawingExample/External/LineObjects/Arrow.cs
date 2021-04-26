using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    class Arrow : LineObject
    {
        public Arrow()
            : base()
        { }

        public override void Initalize()
        {
            Lines.Add(new Line(new Vector2(0, 10), new Vector2(0, 20), 3.5f, Color.Red));
            Lines.Add(new Line(new Vector2(0, 20), new Vector2(20, 0), 3.5f, Color.Red));
            Lines.Add(new Line(new Vector2(20, 0), new Vector2(0, -20), 3.5f, Color.Red));
            Lines.Add(new Line(new Vector2(0, -20), new Vector2(0, -10), 3.5f, Color.Red));
            Lines.Add(new Line(new Vector2(0, -10), new Vector2(-20, -10), 3.5f, Color.Red));
            Lines.Add(new Line(new Vector2(-20, -10), new Vector2(-20, 10), 3.5f, Color.Red));
            Lines.Add(new Line(new Vector2(-20, 10), new Vector2(0, 10), 3.5f, Color.Red));
    
        }
    }
}
