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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GameMenus;

namespace Quatrix.Menus
{
    class Sound : SmallMenuType
    {
        public Sound(MenuType menu, ContentManager content, string fileName)
            : base(menu, content, "Sound", "Options", "Matura MT Script Capitals", "Highlight", 84, 30)
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            Vector = new Vector2(125, 110);

            OptionType temp = new OptionType("On", Font, Color.Red, new Vector2(135, 125), "Options", 128, 120);
            options.Add(temp);

            temp = new OptionType("Off", Font, Color.Red, new Vector2(135, 155), "Options", 128, 150);
            options.Add(temp);

            Color = Color.Red;
        }
    }
}
