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
using ZStorage;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Quatrix
{
    class TextureData
    {
        public DataType<string> data;

        public void loadData()
        {
            string temp = null;

            data.list.Add(temp);
        }

        public void assignTexture(ContentManager content)
        {
            GameBlockNew.texture = content.Load<Texture2D>(data.list[0]);
        }

        public void setTexture(string fileName)
        {
            data.list[0] = fileName;
        }
    }
}
