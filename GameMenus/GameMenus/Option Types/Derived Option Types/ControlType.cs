/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of GameMenus.
 * 
 * GameMenus is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * GameMenus is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GameMenus.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMenus
{
    public class ControlType : OptionType
    {
        public string buttonType;
        public Buttons button;

        public ControlType(string fontName, Color color, Vector2 vector, string state, string assignedButton, Buttons actualButton)
            : base(null, fontName, color, vector, state)
        {
            buttonType = assignedButton;

            button = actualButton;

            Text = setText();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }

        public override void select(InputHandlerComponent i, ref string state)
        {
            if (i.getButton("Select", true))
            {
                if (Selected != null)
                {
                    Text = "Press any key";

                    Selected(this, new EventArgs());
                }
            }
        }

        public string setText()
        {
            string text = "";

            switch (button)
            {
                case Buttons.DPadDown:
                case Buttons.DPadUp:
                case Buttons.DPadLeft:
                case Buttons.DPadRight:
                    text = button.ToString();
                    text = text.Substring(4);
                    break;

                case Buttons.Back:
                    text = button.ToString();
                    break;
                    
                case Buttons.A:
                    text = "Center";
                    break;

                case Buttons.B:
                    text = "Play/Pause";
                    break;
            }

            return text;
        }
    }
}
