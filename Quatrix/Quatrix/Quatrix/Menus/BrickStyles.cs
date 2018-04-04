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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quatrix
{
    class BrickStyles : PreviewType
    {
        public BrickStyles(ContentManager content, string fileName)
            : base(content, "Brick Styles", "Colors", "Matura MT Script Capitals", "Highlight", 101, 30)
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            OptionType temp = new OptionType("Cat-Eye", Font, Color.Red, new Vector2(25, 100), "Colors", 20, 95);
            options.Add(temp);

            temp = new OptionType("Quatrix", Font, Color.Red, new Vector2(25, 140), "Colors", 20, 135);
            options.Add(temp);

            temp = new OptionType("Round", Font, Color.Red, new Vector2(25, 180), "Colors", 20, 175);
            options.Add(temp);

            temp = new OptionType("Classic", Font, Color.Red, new Vector2(25, 220), "Colors", 20, 215);
            options.Add(temp);

            setColor(Color.Red);

            setVector(new Vector2(150, 90));

            Color = Color.Red;
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("Brick Style", content.Load<SpriteFont>("Matura Title"), Color.Red, new Vector2(15, 20));

            addPreview(content.Load<Texture2D>(@"Bricks\Cat-Eye 2 Brick Preview"));

            addPreview(content.Load<Texture2D>(@"Bricks\Quatrix Brick Preview"));

            addPreview(content.Load<Texture2D>(@"Bricks\Round Brick Preview"));

            addPreview(content.Load<Texture2D>(@"Bricks\Blank Brick Preview"));

            base.load(content, fileName);
        }
    }
}
