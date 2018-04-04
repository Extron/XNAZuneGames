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
using ZMediaPlayer;
using GameMenus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Quatrix
{
    class MediaPlayer : MediaLibraryPlayerType
    {
        public MediaPlayer(ContentManager content, string fileName)
            : base(content, "Media Player", "Media Player Menu", "Matura MT Script Capitals", "Highlight", new Rectangle(75, 30, 100, 100))
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            setVectors(new Vector2(50, 150), new Vector2(50, 200), new Vector2(50, 250));

            setIndicators(new Vector2(15, 300), new Vector2(125, 300));

            setColor(Color.Red);

            base.initialize();
        }

        public override void load(ContentManager content, string fileName)
        {
            setNoArt(content.Load<Texture2D>("No Art"));
            base.load(content, fileName);
        }
    }
}
