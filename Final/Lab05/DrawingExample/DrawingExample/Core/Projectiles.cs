using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LineDraw;

namespace Tools
{
    public class Projectiles
    {
        public int type;
        public Vector2 velocity;
        public int damage;
        public Vector2 position;

        //Trail Properties
        public float size;
        public float transparency;


        public Projectiles()
        {
            type = 1;

            size = 5;

            setProperties();
        }

        public Projectiles(int TYPE)
        {
            type = TYPE;

            size = 5;

            setProperties();
        }

        private void setProperties()
        {
            switch(type)
            {
                case 0:
                    damage = 0;
                    break;
                case 1:
                    damage = 34;
                    break;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {

            if(type == 0)
            {
                size -= .2f;

                if(size < .5f)
                {
                    return;
                }

                LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, position, size, 15);
            }
            else
            {
                LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, position, size, 15);
            }
        }
    }
}
