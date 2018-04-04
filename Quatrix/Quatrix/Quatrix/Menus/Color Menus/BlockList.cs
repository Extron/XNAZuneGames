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
using GameMenus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Quatrix
{
    class BlockList : MenuType
    {
        public BlockList(ContentManager content, string fileName)
            : base(content, "Block List", "Colors", "Matura MT Script Capitals", "Highlight", 101, 30)
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            OptionType temp = new OptionType("O-Block", Font, Color.Red, new Vector2(25, 80), "O-Block Color");
            options.Add(temp);

            temp = new OptionType("T-Block", Font, Color.Red, new Vector2(25, 110), "T-Block Color");
            options.Add(temp);

            temp = new OptionType("I-Block", Font, Color.Red, new Vector2(25, 140), "I-Block Color");
            options.Add(temp);

            temp = new OptionType("Z-Block", Font, Color.Red, new Vector2(25, 170), "Z-Block Color");
            options.Add(temp);

            temp = new OptionType("S-Block", Font, Color.Red, new Vector2(25, 200), "S-Block Color");
            options.Add(temp);

            temp = new OptionType("L-Block", Font, Color.Red, new Vector2(25, 230), "L-Block Color");
            options.Add(temp);

            temp = new OptionType("J-Block", Font, Color.Red, new Vector2(25, 260), "J-Block Color");
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
