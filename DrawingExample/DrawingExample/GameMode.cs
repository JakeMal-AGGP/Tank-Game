using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LineDraw;
using Tools;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using DrawingExample.Core;

namespace DrawingExample
{
    class GameMode : GameApp
    {
        float offset = 0;
        Vector2 mousePosition;
        Vector2 screenSize;
        Terrain terrain;
        Tank Player1;
        Tank Player2;
        Tank currentPlayer;
        Vector2 lastPower = new Vector2(80, 70);
        bool showControls = false;
        List<Explosion> explosions = new List<Explosion>();
        Delete objectDelete = new Delete();

        //Music and sound effects
        Song mainTheme;
        List<SoundEffect> soundEffects = new List<SoundEffect>();

        SpriteFont font;
        string variableString;

        List<Projectiles> projectiles = new List<Projectiles>();

        Random globalRandom = new Random();

        /// <summary>
        /// Public contstructor... Does need to do anything at all. Those are the best constructors. 
        /// </summary>
        public GameMode() : base() { }

        protected override void Initialize()
        {
            base.Initialize();

            // Setting up Screen Resolution
            // Read more here: http://rbwhitaker.wikidot.com/changing-the-window-size
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;
            screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);



            terrain = new Terrain(screenSize, 50, 25, 75, 25);

            Player1 = new Tank(1, terrain, globalRandom, explosions, soundEffects);
            Player2 = new Tank(2, terrain, globalRandom, explosions, soundEffects);

            Player1.enemyTank = Player2;
            Player2.enemyTank = Player1;

            currentPlayer = Player1;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            font = Content.Load<SpriteFont>("Font");

            mainTheme = Content.Load<Song>("mainTheme");

            soundEffects.Add(Content.Load<SoundEffect>("explosion"));
            soundEffects.Add(Content.Load<SoundEffect>("shoot"));
            soundEffects.Add(Content.Load<SoundEffect>("hit"));

            SoundEffect.MasterVolume = .25f;

            MediaPlayer.Play(mainTheme);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = .25f;

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

            

            mousePosition = new Vector2(mouseCurrent.X, mouseCurrent.Y);

            if(currentPlayer.turnOver == true)
            {
                //Check for winner
                if(currentPlayer.gameWin)
                {
                    // Quit Application
                    if (IsKeyPressed(Keys.N))
                    {
                        Exit();
                    }
                    else if(IsKeyPressed(Keys.Y)) // Restart Game
                    {
                        Program.restart = true;
                        Exit();
                    }

                    return;
                }

                if (currentPlayer.player_num == 1)
                {
                    currentPlayer = Player2;
                }
                else
                {
                    currentPlayer = Player1;
                }
                currentPlayer.turnOver = false;
                currentPlayer.canFire = true;
                currentPlayer.fuel = 100;
            }


            offset += gameTime.ElapsedGameTime.Milliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            int deltaScrollWheel = mousePrevious.ScrollWheelValue - mouseCurrent.ScrollWheelValue;

            if(!currentPlayer.isSelectingTank)
            {
                if (IsKeyHeld(Keys.A) && currentPlayer.quickMove == true)
                {
                    currentPlayer.angle += .01f;
                }

                if (IsKeyPressed(Keys.A) && currentPlayer.quickMove == false)
                {
                    currentPlayer.angle += .01f;
                }

                //Move Tank Right
                if (IsKeyHeld(Keys.E))
                {
                    currentPlayer.moveTank(.5f);
                }

                //Move Tank Left
                if (IsKeyHeld(Keys.Q))
                {
                    currentPlayer.moveTank(-.5f);
                }


                // Increase Power
                if (IsKeyHeld(Keys.W))
                {
                    if (currentPlayer.power - .1f < .9f)
                    {

                    }
                    else
                    {
                        currentPlayer.power -= .05f;
                    }
                }

                // Decrease Power
                if (IsKeyHeld(Keys.S))
                {
                    if (currentPlayer.power + .1f > 10f)
                    {

                    }
                    else
                    {
                        currentPlayer.power += .05f;
                    }
                }

                // Adjust firing angle
                if (IsKeyHeld(Keys.D) && currentPlayer.quickMove == true)
                {
                    currentPlayer.angle -= .01f;
                }

                if (IsKeyPressed(Keys.D) && currentPlayer.quickMove == false)
                {
                    currentPlayer.angle -= .01f;
                }

                // Fire projectile
                if (IsKeyPressed(Keys.F))
                {
                    currentPlayer.fire();
                }

                // Toggle Showing Controls
                if (IsKeyHeld(Keys.Tab))
                {
                    showControls = true;
                }
                else
                {
                    showControls = false;
                }

                if (IsKeyPressed(Keys.Space))
                {
                    if (currentPlayer.quickMove == true)
                    {
                        currentPlayer.quickMove = false;
                    }
                    else
                    {
                        currentPlayer.quickMove = true;
                    }
                }
            }
            else
            {
                if(IsKeyPressed(Keys.Left) || IsKeyPressed(Keys.A))
                {
                    currentPlayer.cycleTankType(false);
                }
                if(IsKeyPressed(Keys.Right) || IsKeyPressed(Keys.D))
                {
                    currentPlayer.cycleTankType(true);
                }
                if(IsKeyPressed(Keys.Enter))
                {
                    currentPlayer.isSelectingTank = false;
                    currentPlayer.turnOver = true;
                }
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

            terrain.drawTerrain(spriteBatch);

            Player1.enemyHitbox = Player2.hitbox;
            Player2.enemyHitbox = Player1.hitbox;

            Player1.draw(spriteBatch);
            Player2.draw(spriteBatch);

            if (explosions.Count > 0)
            {

                for(int x = 0; x < explosions.Count;)
                {
                    explosions[x].Draw(spriteBatch);

                    if (explosions[x].width <= 0)
                    {
                        objectDelete.DeleteExplosion(explosions[x]);
                        explosions.Remove(explosions[x]);
                    }

                    x++;
                }
            }

            if (!currentPlayer.gameWin)
            {
                //HUD
                if (currentPlayer.quickMove)
                {
                    variableString = "ON";
                }
                else
                {
                    variableString = "OFF";
                }

                if(currentPlayer.isSelectingTank)
                {

                    spriteBatch.DrawString(font, "PLAYER " + currentPlayer.player_num.ToString() + ", SELECT YOUR TANK", new Vector2((screenSize.X / 2) - 100, screenSize.Y / 6), Color.Cyan);

                    switch (currentPlayer.tankType)
                    {
                        case 0:
                            spriteBatch.DrawString(font, "Speedy Tank: More movement, Weaker armor", new Vector2((screenSize.X / 2) - 100, screenSize.Y / 4), Color.Yellow);
                            break;
                        case 1:
                            spriteBatch.DrawString(font, "Heafty Tank: Less movement, Thicker armor", new Vector2((screenSize.X / 2) - 100, screenSize.Y / 4), Color.Yellow);
                            break;
                        case 2:
                            spriteBatch.DrawString(font, "Punchy Tank: Average movement and armor, Powerful cannon", new Vector2((screenSize.X / 2) - 100, screenSize.Y / 4), Color.Yellow);
                            break;
                    }
                }

                spriteBatch.DrawString(font, "Quickmove: " + variableString, new Vector2(25, 25), Color.Yellow);

                spriteBatch.DrawString(font, "Power: ", new Vector2(25, 50), Color.Yellow);
                LineDrawer.DrawLine(spriteBatch, 10, Color.Yellow, new Vector2(80, 60), (new Vector2(80, 60) + new Vector2((10 - currentPlayer.power) * 10, 0)));
                LineDrawer.DrawLine(spriteBatch, 3, Color.Red, new Vector2(80, 70), (new Vector2(80, 70) + new Vector2((10 - currentPlayer.lastPower) * 10, 0)));

                spriteBatch.DrawString(font, "Fuel: ", new Vector2(25, 90), Color.Green);
                LineDrawer.DrawLine(spriteBatch, 10, Color.Yellow, new Vector2(80, 100), (new Vector2(80, 100) + new Vector2((currentPlayer.fuel), 0)));
            }

            if(currentPlayer.gameWin)
            {
                spriteBatch.DrawString(font, "GAME OVER, WINNER: PLAYER " + currentPlayer.player_num.ToString(), new Vector2(25, 5), Color.Yellow);
                spriteBatch.DrawString(font, "REPLAY? Y / N", new Vector2(25, 25), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(font, "PLAYER " + currentPlayer.player_num.ToString(), new Vector2(25, 5), Color.Yellow);
            }

            if(showControls)
            {
                LinePrimatives.DrawSolidRectangle(spriteBatch, Color.Black, new Rectangle((int)screenSize.X / 4, (int)screenSize.Y / 4, (int)screenSize.X / 2, (int)screenSize.Y / 2));
                LinePrimatives.DrawRectangle(spriteBatch, 10, Color.Red, new Rectangle((int)screenSize.X / 4, (int)screenSize.Y / 4, (int)screenSize.X / 2, (int)screenSize.Y / 2));

                spriteBatch.DrawString(font, "TANK CONTROLS", new Vector2((screenSize.X / 2) - 75, (screenSize.Y / 2) / 1.75f), Color.Red);
                spriteBatch.DrawString(font, "Q / E to move", new Vector2((screenSize.X / 2) - 75, (screenSize.Y / 2) / 1.25f), Color.Red);
                spriteBatch.DrawString(font, "W / S to adjust power", new Vector2((screenSize.X / 2) - 75, (screenSize.Y / 2) / 1.15f), Color.Red);
                spriteBatch.DrawString(font, "A / D to rotate cannon", new Vector2((screenSize.X / 2) - 75, (screenSize.Y / 2) / 1.05f), Color.Red);
                spriteBatch.DrawString(font, "SPACE to toggle quick-rotate", new Vector2((screenSize.X / 2) - 75, (screenSize.Y / 2) / .95f), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
