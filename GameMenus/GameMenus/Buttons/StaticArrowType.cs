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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameMenus
{
    public class StaticArrowType
    {
        Vector2 vect;
        Texture2D text;
        string textureAsset;

        public StaticArrowType(Vector2 vector, string fileName)
        {
            textureAsset = fileName;

            vect = vector; 
        }

        public void load()
        {
            text = AssetManager.getTexture(textureAsset);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, vect, Color.White);
        }
    }
}
