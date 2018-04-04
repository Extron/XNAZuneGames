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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameMenus;

namespace Quatrix
{
    class TitleScreen : MenuType
    {
        public TitleScreen(ContentManager content, string fileName)
            : base(content, "Title Screen", "Exit Game", "Matura MT Script Capitals", "Highlight", 161, 30) 
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {

            OptionType temp = new OptionType("New Game", Font, Color.Red, new Vector2(120, 160), "Playing Game", 121, 155);
            options.Add(temp);

            temp = new OptionType("Select Level", Font, Color.Red, new Vector2(120, 185), "Select Level", 121, 180);
            options.Add(temp);

            temp = new OptionType("Media Player", Font, Color.Red, new Vector2(120, 210), "Media Player Menu", 121, 205);
            options.Add(temp);

            temp = new OptionType("Options", Font, Color.Red, new Vector2(120, 235), "Options", 121, 230);
            options.Add(temp);

            temp = new OptionType("Quit", Font, Color.Red, new Vector2(120, 260), "Exit Game", 121, 255);
            options.Add(temp);

            center();

            Color = Color.Red;
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("Quatrix", content.Load<SpriteFont>("Matura Title"), Color.Red, new Vector2(65, 70));
            base.load(content, fileName);
        }
    }
}
