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
    class HighScores : DisplayListType
    {
        public HighScores(ContentManager content, string fileName)
            : base(content, "High Scores", "Options", "Matura MT Script Capitals", "Highlight", 101, 30)
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            OptionType temp = new OptionType("Reset Scores", Font, Color.Red, new Vector2(25, 90), "High Scores");
            options.Add(temp);

            temp = new OptionType("Back", Font, Color.Red, new Vector2(25, 120), "Options");
            options.Add(temp);

            Color = Color.Red;

            direction = SortDirection.down;

            base.initialize();
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("High Scores", content.Load<SpriteFont>("Matura Title"), Color.Red, new Vector2(15, 20));

            base.load(content, fileName);
        }

        public void setDisplay(List<int> scores)
        {
            setText<int>(scores);

            setVectors(145, 90, 20);
        }
    }
}
