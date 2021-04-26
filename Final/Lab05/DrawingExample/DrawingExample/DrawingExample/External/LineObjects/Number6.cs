using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    class Number6 : LineObject
    {
        public Number6()
            : base()
        {
            ScaleLocal(.5f, -.5f);
            TranslateLocal(0, -10);
        }

        public override void Initalize()
        {
            // 8 Segment Display (10,20)
            Lines.Add(new Line(new Vector2(0, 0), new Vector2(10, 0) ));
            Lines.Add(new Line(new Vector2(10, 0), new Vector2(10, 10) ));
            Lines.Add(new Line(new Vector2(0, 0), new Vector2(0, 10) ));
            Lines.Add(new Line(new Vector2(10, 10), new Vector2(0, 10) ));
            Lines.Add(new Line(new Vector2(0, 10), new Vector2(0, 20) ));
            //Lines.Add(new Line(new Vector2(10, 10), new Vector2(10, 20) ));
            Lines.Add(new Line(new Vector2(0, 20), new Vector2(10, 20) ));
        }
    }
}
