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
    class MediaPlayerMenu : MenuType
    {
        public MediaPlayerMenu(ContentManager content, string fileName)
            : base(content, "Media Player Menu", "Title Screen", "Matura MT Script Capitals", "Highlight", 101, 30)
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            OptionType temp = new OptionType("Player", Font, Color.Red, new Vector2(25, 90), "Media Player", 20, 85);
            options.Add(temp);

            temp = new OptionType("Playlist", Font, Color.Red, new Vector2(25, 130), "Playlist Menu", 20, 125);
            options.Add(temp);

            temp = new OptionType("Shuffle", Font, Color.Red, new Vector2(25, 170), "Shuffle", 20, 165);
            options.Add(temp);

            temp = new OptionType("Repeat", Font, Color.Red, new Vector2(25, 210), "Repeat", 20, 205);
            options.Add(temp);

            Color = Color.Red;
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("Media Player", content.Load<SpriteFont>("Matura Title"), Color.Red, new Vector2(15, 20));

            base.load(content, fileName);
        }
    }
}
