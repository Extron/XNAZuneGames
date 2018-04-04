/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of Quatrix.
 * 
 * Quatrix is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Quatrix is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Quatrix.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GameMenus;
using ZHandler;

namespace Quatrix.Menus
{
    class Controls : ControlMenuType
    {

        public Controls(ContentManager content, InputHandlerComponent i, string fileName)
            : base(content, "Controls", "Options", "Matura MT Script Capitals", "Highlight", 101, 30)
        {
            gameInput = i;

            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            OptionType temp = new OptionType("Fast Drop", Font, Color.Red, new Vector2(25, 90), "Controls");
            options.Add(temp);

            temp = new OptionType("Rotate", Font, Color.Red, new Vector2(25, 120), "Controls");
            options.Add(temp);

            temp = new OptionType("Left", Font, Color.Red, new Vector2(25, 150), "Controls");
            options.Add(temp);

            temp = new OptionType("Right", Font, Color.Red, new Vector2(25, 180), "Controls");
            options.Add(temp);

            temp = new OptionType("Pause", Font, Color.Red, new Vector2(25, 210), "Controls");
            options.Add(temp);

            temp = new OptionType("Save", Font, Color.Red, new Vector2(25, 250), "Options");
            options.Add(temp);

            ControlType control = new ControlType(Font, Color.Red, new Vector2(130, 90), "Controls", ButtonType.down, Buttons.DPadDown);
            controls.Add(control);

            control = new ControlType(Font, Color.Red, new Vector2(130, 120), "Controls", ButtonType.a, Buttons.A);
            controls.Add(control);

            control = new ControlType(Font, Color.Red, new Vector2(130, 150), "Controls", ButtonType.left, Buttons.DPadLeft);
            controls.Add(control);

            control = new ControlType(Font, Color.Red, new Vector2(130, 180), "Controls", ButtonType.right, Buttons.DPadRight);
            controls.Add(control);

            control = new ControlType(Font, Color.Red, new Vector2(130, 210), "Controls", ButtonType.back, Buttons.Back);
            controls.Add(control);

            base.initialize();
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("Controls", content.Load<SpriteFont>("Matura Title"), Color.Red, new Vector2(15, 20));

            base.load(content, fileName);
        }
    }
}
