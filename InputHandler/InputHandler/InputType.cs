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
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace ZHandler
{
    /// <summary>
    /// A class that contains a specific button or key, as well as methods to check if the button is pressed or released, as well
    /// as including two different check methods: Level detect, which determines if a button is pressed or released, and 
    /// edge detect, which determines if the button state has changed, i.e. if the button was released and is now pressed.
    /// </summary>
    public class InputType
    {
        /// <summary>
        /// The button that the input state holds information for.
        /// </summary>
        Buttons assignedButton;

        /// <summary>
        /// The key that the input state holds information for.
        /// </summary>
        Keys assignedKey;

        /// <summary>
        /// Creates and input state that moniters a speicific game pad button.
        /// </summary>
        /// <param name="buttonIn">The button that the input state moniters.</param>
        public InputType(Buttons buttonIn)
        {
            assignedButton = buttonIn;
        }

        /// <summary>
        /// Creates and input state that moniters a speicific keyboard key.
        /// </summary>
        /// <param name="keyIn">The key that the input state moniters.</param>
        public InputType(Keys keyIn)
        {
            assignedKey = keyIn;
        }

        /// <summary>
        /// Returns the current button or assigns a new button.
        /// </summary>
        public Buttons Button
        {
            get { return assignedButton; }
            set { assignedButton = value; }
        }

        /// <summary>
        /// Determines if the button is being pressed.
        /// </summary>
        /// <param name="pad">The current game pad state.</param>
        /// <returns>Returns true if the button is pressed, false otherwise.</returns>
        public bool levelDetect(GamePadState pad)
        {
            return pad.IsButtonDown(assignedButton);
        }

        /// <summary>
        /// Determines if the key is being pressed.
        /// </summary>
        /// <param name="pad">The current keyboard state.</param>
        /// <returns>Returns true if the key is pressed, false otherwise.</returns>
        public bool levelDetect(KeyboardState keyboard)
        {
            return keyboard.IsKeyDown(assignedKey);
        }

        /// <summary>
        /// Determines if the button has been pressed.
        /// </summary>
        /// <param name="pad">The current game pad state.</param>
        /// <param name="oldPad">The previous game pad state.</param>
        /// <returns>Returns true if the button is pressed and was not pressed in the previous game pad state, 
        /// and returns false otherwise.</returns>
        public bool edgeDetect(GamePadState pad, GamePadState oldPad)
        {
            return (pad.IsButtonDown(assignedButton) && oldPad.IsButtonUp(assignedButton));
        }

        /// <summary>
        /// Determines if the key has been pressed.
        /// </summary>
        /// <param name="pad">The current keyboard state.</param>
        /// <param name="oldPad">The previous keyboard state.</param>
        /// <returns>Returns true if the key is pressed and was not pressed in the previous keyboard state, 
        /// and returns false otherwise.</returns>
        public bool edgeDetect(KeyboardState keyboard, KeyboardState oldKeyboard)
        {
            return (keyboard.IsKeyDown(assignedKey) && oldKeyboard.IsKeyUp(assignedKey));
        }

        /// <summary>
        /// Determines if the button is not being pressed.
        /// </summary>
        /// <param name="pad">The current game pad state.</param>
        /// <returns>Returns true if the button is released, false otherwise.</returns>
        public bool releasedDetect(GamePadState pad)
        {
            return pad.IsButtonUp(assignedButton);
        }

        /// <summary>
        /// Determines if the key is not being pressed.
        /// </summary>
        /// <param name="pad">The current keyboard state.</param>
        /// <returns>Returns true if the key is released, false otherwise.</returns>
        public bool releasedDetect(KeyboardState keyboard)
        {
            return keyboard.IsKeyUp(assignedKey);
        }

        /// <summary>
        /// Determines if the button has been released.
        /// </summary>
        /// <param name="pad">The current game pad state.</param>
        /// <param name="oldPad">The previous game pad state.</param>
        /// <returns>Returns true if the button is released and was pressed in the previous game pad state, 
        /// and returns false otherwise.</returns>
        public bool releasedEdgeDetect(GamePadState pad, GamePadState oldPad)
        {
            return (pad.IsButtonUp(assignedButton) && oldPad.IsButtonDown(assignedButton));
        }

        /// <summary>
        /// Determines if the key has been released.
        /// </summary>
        /// <param name="pad">The current keyboard state.</param>
        /// <param name="oldPad">The previous keyboard state.</param>
        /// <returns>Returns true if the key is released and was pressed in the previous keyboard state, 
        /// and returns false otherwise.</returns>
        public bool releasedEdgeDetect(KeyboardState keyboard, KeyboardState oldKeyboard)
        {
            return (keyboard.IsKeyUp(assignedKey) && oldKeyboard.IsKeyDown(assignedKey));
        }
    }
}
