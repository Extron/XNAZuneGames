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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GameMenus;

namespace Quatrix.Menus
{
    class Options : MenuType
    {
        public Options(ContentManager content, string fileName)
            : base(content, "Options", "Title Screen", "Matura MT Script Capitals", "Highlight", 101, 30)
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            OptionType temp = new OptionType("Touch Pad", Font, Color.Red, new Vector2(25, 90), "Touch Pad", 20, 85);
            options.Add(temp);

            temp = new OptionType("Sound", Font, Color.Red, new Vector2(25, 120), "Sound");
            options.Add(temp);

            temp = new OptionType("Bricks", Font, Color.Red, new Vector2(25, 150), "Colors");
            options.Add(temp);

            temp = new OptionType("Controls", Font, Color.Red, new Vector2(25, 180), "Controls");
            options.Add(temp);

            temp = new OptionType("High Scores", Font, Color.Red, new Vector2(25, 210), "High Scores");
            options.Add(temp);

            temp = new OptionType("About", Font, Color.Red, new Vector2(25, 240), "About");
            options.Add(temp);

            Color = Color.Red;
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("Options", content.Load<SpriteFont>("Matura Title"), Color.Red, new Vector2(15, 20));

            base.load(content, fileName);
        }
    }
}
