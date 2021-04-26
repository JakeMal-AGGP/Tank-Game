using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace DrawingExample
{
    public class Sprite
    {
        /// <summary>
        /// Texture2D, Texture to Draw
        /// </summary>
        public Texture2D texture = null;    //  sprite texture 

        /// <summary>
        /// <para>Color, Tint color to draw with, Texture is Multiplied by this color</para> 
        /// <para>Color.White = draw the texture as is (with out a tint)</para>
        /// <para>Color.Black = draw the texture 100% black</para>
        /// </summary>
        public Color tint = Color.White;    // color Tint

        /// <summary>
        /// Vector2, Where to draw on screen 
        /// </summary>
        public Vector2 position = Vector2.Zero; // Position

        /// <summary>
        /// Vector2, Ultility Vector to help draw sprites. 
        /// </summary>
        public Vector2 positionOffset = Vector2.Zero;

        /// <summary>
        /// Rectangle, what part of the texture to show. null = draw entire texture 
        /// </summary>
        public Rectangle? sourceRec = null;

        /// <summary>
        /// Float, Rotation of the Object in radians, 
        /// </summary>
        public float rotation = 0;
        // rotation is a good canidate for a property 
        // get\set in degrees, store internally as radians, 
        // also a secon property to get\set in vector2

        /// <summary>
        /// Vector2, The Origin represents the center of the sprite. 
        /// </summary>
        public Vector2 origin = Vector2.Zero;

        /// <summary>
        ///  Float, Scales the sprite. 
        ///  <para></para><para></para>
        ///  When this value goes above 2, the spite degrades in quality quickly.
        /// </summary>
        public float scale = 1.0f;
        // Scale is a good canidate for a Property to autogenerate a forward and side vector 

        /// <summary>
        /// SpriteEffects, options for drawing fipped horizontal or vertial, Default is None.
        /// <para></para><para></para>
        /// To draw both horizontal and veritical use the '|' operator with both. 
        /// </summary>
        public SpriteEffects effect = SpriteEffects.FlipHorizontally;

        /// <summary>
        /// Float, sets Layer Depth of this sprite. Defaults to 0.
        /// </summary>
        public float layerDepth = 0;

        /// <summary>
        /// Bool, Turns off drawing on this sprite. 
        /// </summary>
        public bool dontDraw = false; 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Texture">Texture2d, texture to draw</param>
        public Sprite(Texture2D Texture)
        {
            texture = Texture;
        }

        /// <summary>
        /// Constructor, will load asset from string
        /// </summary>
        /// <param name="textureName">String, texture to draw </param>
        public Sprite(string textureName)
        {
            LoadTexture(textureName);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Texture">Texture2d, texture to draw</param>
        /// <param name="Position">Vector2, where to place the sprite</param>
        public Sprite(Texture2D Texture, Vector2 Position)
        {
            texture = Texture;
            position = Position;
        }

        /// <summary>
        /// Constructor, will load asset from string
        /// </summary>
        /// <param name="textureName">String, texture to draw </param>
        /// <param name="Position">Vector2, where to place the sprite</param>
        public Sprite(string textureName, Vector2 Position)
        {
            LoadTexture(textureName);
            position = Position;

        }

        /// <summary>
        /// Loads the Texture2D with given name
        /// </summary>
        /// <param name="content">Content Manager, use Game's Content member</param>
        /// <param name="textureName">string, name of texture to load.</param>
        public void LoadTexture(string textureName)
        {
            texture = GameApp.instance.Content.Load<Texture2D>(textureName);
        }

        /// <summary>
        /// Draws this Sprite, SpriteBatch.begin must have been called already!
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (dontDraw) { return; } // don't draw if the switch one
            spriteBatch.Draw(texture, (position + positionOffset), sourceRec, tint, rotation, origin, scale, effect, layerDepth);
        }

    }
}
