using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LineDraw; 

namespace DrawingExample
{
    class GameMode : GameApp
    {

        float offset = 0;
        //bool MouseShow = false;

        Line theline;

        Grid2D theGrid;

        XMark xMark; 

        /// <summary>
        /// Default Constructor. Most of the work you need to do should be in Initalize
        /// </summary>
        public GameMode() : base()
        {
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // TODO: use this.Content to load your game content here

            // Setting up Screen Resolution
            // Read more here: http://rbwhitaker.wikidot.com/changing-the-window-size
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            theline = new Line();
            theline.width *= 3;
            theline.color = Color.GreenYellow;
            theline.startPoint = Vector2.Zero;
            theline.endPoint = new Vector2(10, 10); 


            theGrid = new Grid2D();
            xMark = new XMark();
            xMark.Location = new Vector2(3, 5); 
            xMark.LineColorLocal(Color.Red);
            xMark.LineWidthLocal(4f); 


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
           
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // If you create textures on the fly, you need to unload them. 
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void GameUpdate(GameTime gameTime)
        {

            offset += gameTime.ElapsedGameTime.Milliseconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            int deltaScrollWheel = mousePrevious.ScrollWheelValue - mouseCurrent.ScrollWheelValue; 
            if (deltaScrollWheel != 0)
            {
                theGrid.GridSize += (Math.Abs(deltaScrollWheel) / deltaScrollWheel) * 2; 
            }
           
            if (IsKeyPressed(Keys.Q))
            {
                theGrid.isDrawingLines = !theGrid.isDrawingLines; 
            }
            if (IsKeyPressed(Keys.W))
            {
                theGrid.isDrawingDivsions = !theGrid.isDrawingDivsions;
            }
            if (IsKeyPressed(Keys.E))
            {
                theGrid.IsDrawingAxis = !theGrid.IsDrawingAxis;
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clearColor);

            spriteBatch.Begin();

            //theGrid.Draw(spriteBatch);


            // Draw the line as normal
            //.color = Color.Red;
            //theline.Draw(spriteBatch);

            // Now Draw to grid size

            theline.color = Color.LightBlue;
            theGrid.DrawLine(spriteBatch, theline);

            // Draw on Grid, but not scaled to grid Size

           // theline.color = Color.LightBlue;
           // theGrid.DrawLine(spriteBatch, theline);

            // Draw a line object on screen
            theGrid.DrawLineObject(spriteBatch, xMark);

            LinePrimatives.DrawCircle(spriteBatch, 10f, Color.White, theGrid.Grid2ScreenPoint(Vector2.Zero, true), 10, 12);
            LinePrimatives.DrawPoint(spriteBatch, 10f, Color.Blue, theGrid.Grid2ScreenPoint(new Vector2(3, 5), true));

            LinePrimatives.DrawPoint(spriteBatch, theGrid.ScaleGrid2Screen(2), Color.Green, theGrid.Grid2ScreenPoint(new Vector2(-10, -10), true));

            LinePrimatives.DrawCircle(spriteBatch, 3f, Color.White, theGrid.Grid2ScreenPoint(new Vector2 (-5, 5) , true), theGrid.ScaleGrid2Screen(2), 6);

            LinePrimatives.DrawPoint(spriteBatch, 10f, Color.Green, theGrid.Grid2ScreenPoint(LinePrimatives.CircleRadiusPoint(new Vector2(-5, 5), 0, 2)));
            LinePrimatives.DrawPoint(spriteBatch, 10f, Color.Green, theGrid.Grid2ScreenPoint(LinePrimatives.CircleRadiusPoint(new Vector2(-5, 5), 60, 2)));
            LinePrimatives.DrawPoint(spriteBatch, 10f, Color.Green, theGrid.Grid2ScreenPoint(LinePrimatives.CircleRadiusPoint(new Vector2(-5, 5), 120, 2)));
            LinePrimatives.DrawPoint(spriteBatch, 10f, Color.Green, theGrid.Grid2ScreenPoint(LinePrimatives.CircleRadiusPoint(new Vector2(-5, 5), 180, 2)));
            LinePrimatives.DrawPoint(spriteBatch, 10f, Color.Green, theGrid.Grid2ScreenPoint(LinePrimatives.CircleRadiusPoint(new Vector2(-5, 5), 240, 2)));
            LinePrimatives.DrawPoint(spriteBatch, 10f, Color.Green, theGrid.Grid2ScreenPoint(LinePrimatives.CircleRadiusPoint(new Vector2(-5, 5), 300, 2)));
            //LinePrimatives.CircleRadiusPoint(theGrid.Grid2ScreenPoint(new Vector2(-5, 5), true), -60, 40); 

            Vector2 Pos = LinePrimatives.CircleRadiusPoint(new Vector2(graphics.PreferredBackBufferWidth / 2f, graphics.PreferredBackBufferHeight / 2f), offset/120, theGrid.GridSize*7.5f);
            LinePrimatives.DrawPoint(spriteBatch, theGrid.ScaleGrid2Screen(2), Color.Wheat, Pos);
            


            theGrid.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
