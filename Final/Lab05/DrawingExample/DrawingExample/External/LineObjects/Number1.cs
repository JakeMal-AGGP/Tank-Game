using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    class Number1 : LineObject
    {
        public Number1()
            : base()
        {
            ScaleLocal(.5f, -.5f);
            TranslateLocal(0, -10);
        }


        public override void Initalize()
        {
            // 8 Segment Display (10,20)
            Lines.Add(new Line(new Vector2(0, 0), new Vector2(0, 20)));
           
        }
    }
}
