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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class DisplayListType : MenuType
    {
        List<string> stringList;
        List<Vector2> vectorList;

        protected SortDirection direction;

        public DisplayListType(string current)
            : base(current) 
        {
            stringList = new List<string>();

            vectorList = new List<Vector2>();

            direction = SortDirection.up;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            for (int i = 0; i < stringList.Count; i++)
            {
                //spriteBatch.DrawString(Font, stringList[i], vectorList[i], Color);
            }
        }

        public void setText<Type>(List<Type> list)
        {
            stringList.Clear();

            if (direction == SortDirection.up)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string temp = list[i].ToString();

                    stringList.Add(temp);
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    string temp = list[i].ToString();

                    stringList.Add(temp);
                }
            }
        }

        public void setVectors(int x, int y, int factor)
        {
            vectorList.Clear();

            for (int i = 0; i < stringList.Count; i++)
            {
                Vector2 temp = new Vector2(x, y + (factor * i));

                vectorList.Add(temp);
            }
        }

        public void resetDisplay()
        {
            stringList.Clear();

            vectorList.Clear();
        }

        public enum SortDirection
        {
            up,
            down
        }
    }
}
