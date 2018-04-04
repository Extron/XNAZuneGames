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
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class PreviewType : MenuType
    {
        List<Texture2D> previews;
        Vector2 vector;
        Color color;

        #region Constructors
        public PreviewType(string current)
            : base(current)
        {
            previews = new List<Texture2D>();
        }
        #endregion

        #region Overridden Functions
        public override void draw(SpriteBatch spriteBatch)
        { 
            base.draw(spriteBatch);

            //spriteBatch.Draw(previews[Position], vector, Color);           
        }
        #endregion

        #region Class Functions
        public void setVector(Vector2 v)
        {
            vector = v;
        }

        public void setColor(Color c)
        {
            color = c;
        }

        public void addPreview(Texture2D texture)
        {
            previews.Add(texture);
        }
        #endregion
    }
}
