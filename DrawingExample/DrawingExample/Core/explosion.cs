using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LineDraw;
using Microsoft.Xna.Framework.Audio;

namespace DrawingExample.Core
{
    public class Explosion
    {

        public float radius;
        public float width;
        public Vector2 position;
        public bool markForDeletion = false;

        public Explosion(Vector2 pos, SoundEffect sound)
        {
            radius = 5;
            width = 10;
            position = pos;
            sound.CreateInstance().Play();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LinePrimatives.DrawCircle(spriteBatch, width, Color.Orange, position, radius, 25);

            radius += 2f;
            width -= .5f;
        }


    }
}
