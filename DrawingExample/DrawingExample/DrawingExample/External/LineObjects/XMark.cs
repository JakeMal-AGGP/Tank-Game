using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineDraw
{
    class XMark : LineObject
    {
        public XMark()
            : base()
        { }

        public override void Initalize()
        {
            Lines.Add(new Line(new Vector2(-5, 5), new Vector2(5, -5), 3.5f, Color.Red));
            Lines.Add(new Line(new Vector2(5, 5), new Vector2(-5, -5), 3.5f, Color.Red));
        }
    }
}
