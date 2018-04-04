#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of QuatrixHD.
 * 
 * QuatrixHD is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * QuatrixHD is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with QuatrixHD.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace QuatrixHD.Grid
{
    /// <summary>
    /// Represents a single drawable cell to hold one brick.
    /// </summary>
    class CellType
    {
        Texture2D sprite;
        Vector2 location;
        Color color;
        bool isFull;

        public Vector2 Location
        {
            get { return location; }
        }

        public bool IsFull
        {
            get { return isFull; }
        }

        public CellType(Vector2 cellLocation)
        {
            location = cellLocation;
            color = Color.White;
            isFull = false;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
                spriteBatch.Draw(sprite, location, color);
        }

        public void fillCell(Texture2D texture, Color spriteColor)
        {
            sprite = texture;
            color = spriteColor;
            isFull = true;
        }

        public void fillCell(CellType cell)
        {
            sprite = cell.sprite;
            color = cell.color;
            isFull = cell.isFull;
        }

        public void emptyCell()
        {
            sprite = null;
            color = Color.White;
            isFull = false;
        }
    }
}
