/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of GameMenus.
 * 
 * GameMenus is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * GameMenus is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GameMenus.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class SmallMenuType : MenuType
    {
        MenuType baseMenu;

        public SmallMenuType(MenuType menu, string current)
            : base(current)
        {
            baseMenu = menu;
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            baseMenu.draw(spriteBatch);

            base.draw(spriteBatch);
        }

        public override void  drawTransitions(SpriteBatch spriteBatch)
        {
            baseMenu.draw(spriteBatch);

 	        base.drawTransitions(spriteBatch);
        }
    }
}
