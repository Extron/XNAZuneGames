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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameMenus;
using ZHandler;

namespace Quatrix
{
    class Pause : MenuType
    {
        public PausedMediaPlayer player;

        public Pause(ContentManager content, string fileName)
            : base(content, "Paused", "Playing Game", "Matura MT Script Capitals", "Highlight", 134, 30)
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            Vector = new Vector2(50, 25);

            OptionType temp = new OptionType("Resume", Font, Color.Red, new Vector2(120, 60), "Playing Game", 109, 55);
            options.Add(temp);

            temp = new OptionType("Quit", Font, Color.Red, new Vector2(120, 95), "Title Screen", 109, 90);
            options.Add(temp);

            center();

            Color = Color.Red;
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("Paused", Font, Color.Red, new Vector2(90, 30));

            base.load(content, fileName);
        }

        public override void update(InputHandlerComponent i, ref string state)
        {
            player.update(i, ref state);

            base.update(i, ref state);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            player.draw(spriteBatch);

            base.draw(spriteBatch);
        }

        public override void movement(InputHandlerComponent input)
        {
            player.movement(input);

            base.movement(input);
        }
    }
}
