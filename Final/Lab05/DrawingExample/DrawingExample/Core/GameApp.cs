using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LineDraw; 

namespace DrawingExample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameApp : Game
    {
        /// <summary>
        /// Singleton Reference
        /// </summary>
        public static GameApp instance;

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        protected KeyboardState keyboardCurrent;
        protected KeyboardState keyboardPrevious;
        protected MouseState mouseCurrent;
        protected MouseState mousePrevious;
        protected List<GamePadState> gamePadsCurrrent;
        protected List<GamePadState> gamePadsPrevious;
        /// <summary>
        /// Max number of Game Pads to look for
        /// </summary>
        protected int MaxGamePads= 4; 

        /// <summary>
        /// Color to Clear the Screen With
        /// </summary>
        public Color clearColor = Color.Black; 

        public GameApp()
        {
            instance = this; 
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initalizing previous states with valid information
            keyboardPrevious = Keyboard.GetState();
            mousePrevious = Mouse.GetState();
            gamePadsCurrrent = new List<GamePadState>();
            gamePadsPrevious = new List<GamePadState>();
            for (int i = 0; i < MaxGamePads; i++)
            {
                gamePadsPrevious.Add(GamePad.GetState(i));
            }

            // Initalizing the spritebatch for drawing
            LineDrawer.InitateLineDrawer(GraphicsDevice); 
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Stub, Write your code in GameMode
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Stub, Write your code in GameMode
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardCurrent = Keyboard.GetState();
            mouseCurrent = Mouse.GetState();
            if (MaxGamePads > 0)
            {
                gamePadsCurrrent.Clear();
                for (int i = 0; i < MaxGamePads; i++)
                {
                    gamePadsCurrrent.Add(GamePad.GetState(i));
                }
            }

            // This is the real heart of the Update Function
            // Seperate Function used here so we perform getting input before
            // and then assign previous to the current after. 
            GameUpdate(gameTime);

            // Setting Current to Previous for next game tick
            keyboardPrevious = keyboardCurrent;
            mousePrevious = mouseCurrent;
            gamePadsPrevious = gamePadsCurrrent;

            base.Update(gameTime);
        }

        protected virtual void GameUpdate(GameTime gameTime)
        {
            // Exit Application Code should be in GameMode

        }

        //Utility Functions 

        /// <summary>
        /// Returns true if key on keyboard was JUST PRESSED in current tick. 
        /// </summary>
        /// <param name="key">Which key to check, use Keys.'keyname"</param>
        /// <returns>bool</returns>
        public bool IsKeyPressed(Keys key)
        {
            return (keyboardCurrent.IsKeyDown(key) && keyboardPrevious.IsKeyUp(key)); 
        }

        /// <summary>
        /// Returns true if key on keyboard was JUST RELEASED in current ticks.
        /// </summary>
        /// <param name="key">Which key to check, use Keys.'keyname"</param>
        /// <returns></returns>
        public bool IsKeyReleased(Keys key)
        {
            return (keyboardCurrent.IsKeyUp(key) && keyboardPrevious.IsKeyDown(key));
        }

        /// <summary>
        /// Returns true if key on keyboard has been HELD down 2 ticks. 
        /// </summary>
        /// <param name="key">Which key to check, use Keys.'keyname"</param>
        /// <returns>bool</returns>
        public bool IsKeyHeld(Keys key)
        {
            return (keyboardCurrent.IsKeyDown(key) && keyboardPrevious.IsKeyDown(key));
        }


        /// <summary>
        /// Returns true if button on mouse was JUST PRESSED in current tick. 
        /// <para>Note use the property name from the MouseState exactly as typed. When typed wrong, will throw a null object error</para>
        /// </summary>
        /// <param name="ButtonName">Which Mouse Button To Check</param>
        /// <returns>bool</returns>
        public bool MouseButtonIsPressed(string ButtonName)
        {
            // using vars cause these two lines are messy. 
            ButtonState cmouse = (ButtonState)mouseCurrent.GetType().GetProperty(ButtonName).GetValue(mouseCurrent);
            ButtonState pmouse = (ButtonState)mousePrevious.GetType().GetProperty(ButtonName).GetValue(mousePrevious);

            return ((cmouse == ButtonState.Pressed) && (pmouse == ButtonState.Released));
            /*
            var result = ((cmouse == ButtonState.Pressed) && (pmouse == ButtonState.Released));
            Console.WriteLine(result + " " + ButtonName + " C:"+cmouse + " - P:" + pmouse); 
            return result;
            */
        }

        /// <summary>
        /// Returns true if button on mouse was JUST Released in current tick. 
        /// <para>Note use the property name from the MouseState exactly as typed. When typed wrong, will throw a null object error</para>
        /// </summary>
        /// <param name="ButtonName">Which Mouse Button To Check</param>
        /// <returns>bool</returns>
        public bool MouseButtonIsReleased(string ButtonName)
        {
            // using vars cause these two lines are messy. 
            ButtonState cmouse = (ButtonState)mouseCurrent.GetType().GetProperty(ButtonName).GetValue(mouseCurrent);
            ButtonState pmouse = (ButtonState)mousePrevious.GetType().GetProperty(ButtonName).GetValue(mousePrevious);

            return ((cmouse == ButtonState.Released) && (pmouse == ButtonState.Pressed));
        }

        /// <summary>
        /// Returns true if button on mouse is held down. 
        /// <para>Note use the property name from the MouseState exactly as typed. When typed wrong, will throw a null object error</para>
        /// </summary>
        /// <param name="ButtonName">Which Mouse Button To Check</param>
        /// <returns>bool</returns>
        public bool MouseButtonIsHeld(string ButtonName)
        {
            // using vars cause these two lines are messy. 
            ButtonState cmouse = (ButtonState)mouseCurrent.GetType().GetProperty(ButtonName).GetValue(mouseCurrent);
            ButtonState pmouse = (ButtonState)mousePrevious.GetType().GetProperty(ButtonName).GetValue(mousePrevious);

            return ((cmouse == ButtonState.Pressed) && (pmouse == ButtonState.Pressed));
        }

        /// <summary>
        /// Returns true if button on gamepad was JUST PRESSED in current tick. 
        /// <para>Note use the property name from the GamepadState exactly as typed. When typed wrong, will throw a null object error</para>
        /// </summary>
        /// <param name="playerNum">Which gamepad to check, Zero is First</param>
        /// <param name="ButtonName">Which Mouse Button To Check</param>
        /// <returns>bool</returns>
        public bool GamePadButtonIsPressed(int playerNum, string ButtonName)
        {
            // using vars cause these two lines are messy. 
            ButtonState cgamepad = (ButtonState)gamePadsCurrrent[playerNum].GetType().GetProperty(ButtonName).GetValue(gamePadsCurrrent[playerNum]);
            ButtonState pgamepad = (ButtonState)gamePadsPrevious[playerNum].GetType().GetProperty(ButtonName).GetValue(gamePadsPrevious[playerNum]);

            return ((cgamepad == ButtonState.Pressed) && (pgamepad == ButtonState.Released));
        }

        /// <summary>
        /// Returns true if button on gamepad was JUST RELEASED in current tick. 
        /// <para>Note use the property name from the GamepadState exactly as typed. When typed wrong, will throw a null object error</para>
        /// </summary>
        /// <param name="playerNum">Which gamepad to check, Zero is First</param>
        /// <param name="ButtonName">Which Mouse Button To Check</param>
        /// <returns>bool</returns>
        public bool GamePadButtonIsReleased(int playerNum, string ButtonName)
        {
            // using vars cause these two lines are messy. 
            ButtonState cgamepad = (ButtonState)gamePadsCurrrent[playerNum].GetType().GetProperty(ButtonName).GetValue(gamePadsCurrrent[playerNum]);
            ButtonState pgamepad = (ButtonState)gamePadsPrevious[playerNum].GetType().GetProperty(ButtonName).GetValue(gamePadsPrevious[playerNum]);

            return ((cgamepad == ButtonState.Released) && (pgamepad == ButtonState.Pressed));
        }

        /// <summary>
        /// Returns true if button on gamepad is HELD DOWN. 
        /// <para>Note use the property name from the GamepadState exactly as typed. When typed wrong, will throw a null object error</para>
        /// </summary>
        /// <param name="playerNum">Which gamepad to check, Zero is First</param>
        /// <param name="ButtonName">Which Mouse Button To Check</param>
        /// <returns>bool</returns>
        public bool GamePadButtonIsHeld(int playerNum, string ButtonName)
        {
            // using vars cause these two lines are messy. 
            ButtonState cgamepad = (ButtonState)gamePadsCurrrent[playerNum].GetType().GetProperty(ButtonName).GetValue(gamePadsCurrrent[playerNum]);
            ButtonState pgamepad = (ButtonState)gamePadsPrevious[playerNum].GetType().GetProperty(ButtonName).GetValue(gamePadsPrevious[playerNum]);

            return ((cgamepad == ButtonState.Released) && (pgamepad == ButtonState.Pressed));
        }
    }
}
