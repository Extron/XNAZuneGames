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
using ZHandler;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public abstract class SystemType
    {
        public Dictionary<string, MenuType> menus;
        public string state;

        protected string current;

        public SystemType(string systemState)
        {
            state = systemState;
            menus = new Dictionary<string, MenuType>();
        }

        public virtual void add(MenuType menu)
        {
            if (menus.Count == 0)
            {
                current = menu.State;
            }

            menus.Add(menu.State, menu);
        }

        public MenuType getMenu(string state)
        {
            return menus[state];
        }

        public virtual void initialize()
        {
            foreach (MenuType menu in menus.Values)
            {
                menu.initialize();
            }
        }

        public virtual void load()
        {
            foreach (MenuType menu in menus.Values)
            {
                menu.load();
            }
        }

        public virtual void update(InputHandlerComponent i)
        {
            getMenu(current).update(i);
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            getMenu(current).draw(spriteBatch);
        }
    }
}
