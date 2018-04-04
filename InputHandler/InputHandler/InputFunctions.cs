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
    /// Static class that contains functions used by the input manager.
    /// </summary>
    public static class InputFunctions
    {
        /// <summary>
        /// This function generates a list of the current buttons pressed on the game pad.
        /// </summary>
        /// <param name="pad">The current game pad state.</param>
        /// <param name="oldPad">The previous game pad state.</param>
        /// <returns>Returns a list of the pressed buttons on the game pad.</returns>
        public static List<Buttons> getButtonList(GamePadState pad, GamePadState oldPad)
        {
            List<Buttons> list = new List<Buttons>();

            if (pad.Buttons.A == ButtonState.Pressed && oldPad.Buttons.A == ButtonState.Released)
                list.Add(Buttons.A);

            if (pad.Buttons.B == ButtonState.Pressed && oldPad.Buttons.B == ButtonState.Released)
                list.Add(Buttons.B);

            if (pad.Buttons.X == ButtonState.Pressed && oldPad.Buttons.X == ButtonState.Released)
                list.Add(Buttons.X);

            if (pad.Buttons.Y == ButtonState.Pressed && oldPad.Buttons.Y == ButtonState.Released)
                list.Add(Buttons.Y);

            if (pad.Buttons.Back == ButtonState.Pressed && oldPad.Buttons.Back == ButtonState.Released)
                list.Add(Buttons.Back);

            if (pad.DPad.Down == ButtonState.Pressed && oldPad.DPad.Down == ButtonState.Released)
                list.Add(Buttons.DPadDown);

            if (pad.DPad.Up == ButtonState.Pressed && oldPad.DPad.Up == ButtonState.Released)
                list.Add(Buttons.DPadUp);

            if (pad.DPad.Left == ButtonState.Pressed && oldPad.DPad.Left == ButtonState.Released)
                list.Add(Buttons.DPadLeft);

            if (pad.DPad.Right == ButtonState.Pressed && oldPad.DPad.Right == ButtonState.Released)
                list.Add(Buttons.DPadRight);

            if (pad.Buttons.LeftShoulder == ButtonState.Pressed && oldPad.Buttons.LeftShoulder == ButtonState.Released)
                list.Add(Buttons.LeftShoulder);

            if (pad.Buttons.RightShoulder == ButtonState.Pressed && oldPad.Buttons.RightShoulder == ButtonState.Released)
                list.Add(Buttons.RightShoulder);

            return list;
        }
    }
}
