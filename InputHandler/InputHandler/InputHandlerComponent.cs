/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of ZHandler.
 * 
 * ZHandler is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * ZHandler is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with ZHandler.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ZHandler
{
    /// <summary>
    /// This class manages input for a game.  It can be used for keyboards, mice, or game pads, and by extention, Zune controls.
    /// The manager contains a list of input states, each with a specific button to monitor and determine it's state.  There are
    /// input states added to the list by default, including directional inputs, which map to either the directional keys on a
    /// keyboard or the d-pad on a game pad.  Other input state include a select state, which is Enter or A, Play/Pause, which 
    /// is RightShift or B, and back, which is Escape or Back.  This input manager can also set the directional states to be
    /// touch sensitive, responding to the Zune touch pad, and therefore remaps the input state to use the left analog stick instead
    /// of the d-pad.  Finally, this input manager can dynamically alter the input states, thus allowing the user to customeize
    /// in-game controls.
    /// </summary>
    public class InputHandlerComponent : GameComponent
    {
        #region Variables
        /// <summary>
        /// Keyboard state to get and contain current keyboard data.
        /// </summary>
        KeyboardState keyboard;

        /// <summary>
        /// Keyboard state to contain previous keyboard data.  Used for edge detect methods.
        /// </summary>
        KeyboardState oldKeyboard;

        /// <summary>
        /// Mouse state to get and contain current mouse data.
        /// </summary>
        MouseState mouse;

        /// <summary>
        /// Game pad state to get and contain current game pad data.
        /// </summary>
        GamePadState pad;

        /// <summary>
        /// Game pad state to contain previous game pad data.  Used for edge detect methods.
        /// </summary>
        GamePadState oldPad;

        /// <summary>
        /// A list of all input states that have been created for the game.  Each input state manages a specific button or key.
        /// </summary>
        Dictionary<string, InputType> inputStates;

        /// <summary>
        /// The vector of the left analog stick, or mouse if no game pad is being used.
        /// </summary>
        Vector2 leftAnalog;

        /// <summary>
        /// The vector of the right analog stick.
        /// </summary>
        Vector2 rightAnalog;

        /// <summary>
        /// A variable that declares if the Zune touch pad is being used.
        /// </summary>
        bool touch;

        /// <summary>
        /// A variable that declares if the keyboard is being used instead of a game pad.
        /// </summary>
        bool usingKeyboard;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a standard input manager for the game, designating if a keyboard or a game pad is being used.
        /// </summary>
        /// <param name="game">The game that the input manager is attached to.</param>
        public InputHandlerComponent(Game game)
            : base(game)
        {
            pad = new GamePadState();
            oldPad = new GamePadState();

#if WINDOWS
            if (!pad.IsConnected)
            {
                usingKeyboard = true;
                keyboard = new KeyboardState();
                oldKeyboard = new KeyboardState();
                mouse = new MouseState();
            }
#else
            usingKeyboard = false;
#endif
        }
        #endregion

        #region Properties
        /// <summary>
        /// Property to manually assign the touch pad settings. 
        /// </summary>
        public bool Touch
        {
            get
            {
                return touch;
            }
            set
            {
                touch = value;
                setTouch();
            }
        }

        public KeyboardState KBoard
        {
            get { return keyboard; }
            set { keyboard = value; }
        }

        public KeyboardState OldKBoard
        {
            get { return oldKeyboard; }
            set { oldKeyboard = value; }
        }

        public GamePadState OldPad
        {
            get { return oldPad; }
            set { oldPad = value; }
        }

        public GamePadState Pad
        {
            get { return pad; }
            set { pad = value; }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Overridden fuction that initializes the input state list and adds some standard input states.  
        /// This function is called with the main game initialize method.
        /// </summary>
        public override void Initialize()
        {
            inputStates = new Dictionary<string, InputType>();

            if (usingKeyboard)
            {
                inputStates.Add("Left", new InputType(Keys.Left));
                inputStates.Add("Right", new InputType(Keys.Right));
                inputStates.Add("Up", new InputType(Keys.Up));
                inputStates.Add("Down", new InputType(Keys.Down));
                inputStates.Add("Back", new InputType(Keys.Escape));
                inputStates.Add("Select", new InputType(Keys.Enter));
                inputStates.Add("Play/Pause", new InputType(Keys.RightShift));

                touch = false;
            }
            else
            {
                inputStates.Add("Left", new InputType(Buttons.DPadLeft));
                inputStates.Add("Right", new InputType(Buttons.DPadRight));
                inputStates.Add("Up", new InputType(Buttons.DPadUp));
                inputStates.Add("Down", new InputType(Buttons.DPadDown));
                inputStates.Add("Back", new InputType(Buttons.Back));
                inputStates.Add("Select", new InputType(Buttons.A));
                inputStates.Add("Play/Pause", new InputType(Buttons.B));

                touch = false;
            }

            base.Initialize();
        }

        /// <summary>
        /// Overridden function that updates with the main game update method.
        /// </summary>
        /// <param name="gameTime">The main game time variable.</param>
        public override void Update(GameTime gameTime)
        {
            //Set the previous input state
            oldPad = pad;

            //Get the current input state
            pad = GamePad.GetState(PlayerIndex.One);

            //Set the vectors
            leftAnalog = new Vector2(pad.ThumbSticks.Left.X, pad.ThumbSticks.Left.Y);

            rightAnalog = new Vector2(pad.ThumbSticks.Right.X, pad.ThumbSticks.Right.Y);

            //If the program is running in Windows, update keyboard states
            if (usingKeyboard)
            {
                oldKeyboard = keyboard;

                keyboard = Keyboard.GetState();

                mouse = Mouse.GetState();

                leftAnalog = new Vector2(mouse.X, mouse.Y);
            }

            base.Update(gameTime);
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// Adds a new input state containing a specific button to the dictionary.
        /// </summary>
        /// <param name="button">The identifier of the new input state.</param>
        /// <param name="assignment">The button the input state will contain.</param>
        public void addButton(string button, Buttons assignment)
        {
            inputStates.Add(button, new InputType(assignment));
        }

        /// <summary>
        /// Adds a new input state containing a specific keyboard key to the dictionary.
        /// </summary>
        /// <param name="button">The identifier of the new input state.</param>
        /// <param name="assignment">The key the input state will contain.</param>
        public void addButton(string button, Keys assignment)
        {
            inputStates.Add(button, new InputType(assignment));
        }

        /// <summary>
        /// Determines if a button is pressed.
        /// </summary>
        /// <param name="button">The key of the input state that contains the button.</param>
        /// <param name="edgeDetect">If this value is true, the button will be checked using edge detect methods.
        /// If the value is false, the button will be checked using level detect methods.</param>
        /// <returns>Returns true if the button is pressed, depending on the check method.</returns>
        public bool getButton(string button, bool edgeDetect)
        {
            bool temp = false;

            try
            {
                if (usingKeyboard)
                {
                    if (edgeDetect)
                        temp = inputStates[button].edgeDetect(keyboard, oldKeyboard);
                    else
                        temp = inputStates[button].levelDetect(keyboard);
                }
                else
                {
                    if (edgeDetect)
                        temp = inputStates[button].edgeDetect(pad, oldPad);
                    else
                        temp = inputStates[button].levelDetect(pad);
                }
            }
            catch
            {
                temp = false;
            }

            return temp;
        }


        /// <summary>
        /// Determines if a button is released.
        /// </summary>
        /// <param name="button">The key of the input state that contains the button.</param>
        /// <param name="edgeDetect">If this value is true, the button will be checked using edge detect methods.
        /// If the value is false, the button will be checked using level detect methods.</param>
        /// <returns>Returns a true value if the button is released, depending on the check method.</returns>
        public bool getReleased(string button, bool edgeDetect)
        {
            bool temp = false;

            try
            {
                if (usingKeyboard)
                {
                    if (edgeDetect)
                        temp = inputStates[button].releasedEdgeDetect(keyboard, oldKeyboard);
                    else
                        temp = inputStates[button].releasedDetect(keyboard);
                }
                else
                {
                    if (edgeDetect)
                        temp = inputStates[button].releasedEdgeDetect(pad, oldPad);
                    else
                        temp = inputStates[button].releasedDetect(pad);
                }
            }
            catch
            {
                temp = false;
            }

            return temp;
        }

        /// <summary>
        /// Returns a vector from one of the two analog sticks on the standard controller.  If the mouse
        /// is being used instead, the function returns the vector of the mouse according to the lop left
        /// corner of the screen.
        /// </summary>
        /// <param name="vectorType">Specifies which vector, either the left analog or the right analog
        /// vector, to return.</param>
        /// <returns></returns>
        public Vector2 getVector(AnalogType vectorType)
        {
            Vector2 vector = new Vector2();

            switch (vectorType)
            {
                case AnalogType.leftAnalog:
                    vector = leftAnalog;
                    break;

                case AnalogType.rightAnalog:
                    vector = rightAnalog;
                    break;
            }

            return vector;
        }

        /// <summary>
        /// Returns a vector referring to the mouse position as an emulation of the standard 
        /// analog stick.  Uses an origin point to calculate the vector of the mouse pointer 
        /// from that point.
        /// </summary>
        /// <param name="origin">A point that the mouse pointer centers around</param>
        /// <returns>Returns a mouse vector based on an origin other than the top-left corner.</returns>
        public Vector2 getVector(Vector2 origin)
        {
            //Get the displacement of the mouse vector from the origin, emulating the standard coordinate plane
            Vector2 vector = leftAnalog - origin;

            //Invert the y coordinate to convert the coordinates to a standard coordinate plane.
            vector.Y = -vector.Y;

            //Convert the vectors to unit circle coordinates
            vector /= vector.Length();

            return vector;
        }

        /// <summary>
        /// Assigns a button from the game pad to any of the input state objects within the component.
        /// </summary>
        /// <param name="button">The key of the input state that the new button needs to be attached to.</param>
        /// <param name="assignment">The button that is being attached to the input state.</param>
        public void setButton(string button, Buttons assignment)
        {
            //Try to get an input state from the input state dictionary and assign the button to it
            try
            {
                inputStates[button].Button = assignment;
            }
            //If the input state is not in the list, create a new input state and add it to the dictionary
            catch
            {
                inputStates.Add(button, new InputType(assignment));
            }
        }

        /// <summary>
        /// Sets the four directional input states to register either analog stick movements, such as the touch pad on the Zune,
        /// or the d-pad buttons.
        /// </summary>
        public void setTouch()
        {
            //Try to get a directional input state from the list.  If the state does not exist, add it to the list
            try
            {
                if (touch)
                    inputStates["Left"].Button = Buttons.LeftThumbstickLeft;
                else
                    inputStates["Left"].Button = Buttons.DPadLeft;
            }
            catch
            {
                if (touch)
                    inputStates.Add("Left", new InputType(Buttons.LeftThumbstickLeft));
                else
                    inputStates.Add("Left", new InputType(Buttons.DPadLeft));
            }

            try
            {
                if (touch)
                    inputStates["Right"].Button = Buttons.LeftThumbstickRight;
                else
                    inputStates["Right"].Button = Buttons.DPadRight;
            }
            catch
            {
                if (touch)
                    inputStates.Add("Right", new InputType(Buttons.LeftThumbstickRight));
                else
                    inputStates.Add("Right", new InputType(Buttons.DPadRight));
            }

            try
            {
                if (touch)
                    inputStates["Up"].Button = Buttons.LeftThumbstickUp;
                else
                    inputStates["Up"].Button = Buttons.DPadUp;
            }
            catch
            {
                if (touch)
                    inputStates.Add("Up", new InputType(Buttons.LeftThumbstickUp));
                else
                    inputStates.Add("Up", new InputType(Buttons.DPadUp));
            }

            try
            {
                if (touch)
                    inputStates["Down"].Button = Buttons.LeftThumbstickDown;
                else
                    inputStates["Down"].Button = Buttons.DPadDown;
            }
            catch
            {
                if (touch)
                    inputStates.Add("Down", new InputType(Buttons.LeftThumbstickDown));
                else
                    inputStates.Add("Down", new InputType(Buttons.DPadDown));
            }
        }

        /// <summary>
        /// Gets the next pressed button on a game pad and assigns it to a specific input state.
        /// </summary>
        /// <param name="button">The input state that needs to be changed.</param>
        /// <returns>Returns a true value if the button change has occured, and returns a false value if none of the
        /// buttons on the game pad have been pressed.</returns>
        public bool changeButton(string button)
        {
            List<Buttons> list = new List<Buttons>();

            if (oldPad != pad)
            {
                list = InputFunctions.getButtonList(pad, oldPad);

                if (list.Count > 0)
                {
                    setButton(button, list[0]);

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns the current button assigned to an input state.
        /// </summary>
        /// <param name="button">The input state that contains the button to be returned.</param>
        /// <returns>Returns the current button.</returns>
        public Buttons getAssignedButton(string button)
        {
            Buttons returnButton = Buttons.A;

            try
            {
                returnButton = inputStates[button].Button;
            }
            catch
            {
            }

            return returnButton;
        }

        /// <summary>
        /// Forcefully updates the input states, which otherwise update from the main game update method.
        /// </summary>
        public void updateInput()
        {
            pad = GamePad.GetState(PlayerIndex.One);

            oldPad = pad;

            if (usingKeyboard)
            {
                keyboard = Keyboard.GetState();

                oldKeyboard = keyboard;
            }
        }
        #endregion
    }
}
