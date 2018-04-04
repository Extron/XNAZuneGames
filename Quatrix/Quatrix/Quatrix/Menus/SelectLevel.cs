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

namespace Quatrix
{
    class SelectLevel : SimpleSelectLevelType
    {
        public SelectLevel(ContentManager content, string fileName)
            : base(content, "Select Level", "Title Screen", "LindseyLarge", "Clear Brick")
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            setText(new Vector2(105, 165));

            setColor(Color.Red);

            setLevelMax(9);
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("Select Level", content.Load<SpriteFont>("Matura Title"), Color.Red, new Vector2(35, 70));

            setButtons(new MenuButtonType(content, new Rectangle(45, 180, 32, 32), "Arrow", "Arrow Pressed"),
                       new MenuButtonType(content, new Rectangle(195, 210, 32, 32), "Arrow", "Arrow Pressed"), "right");

            base.load(content, fileName);
        }
    }
}
