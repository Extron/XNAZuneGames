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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuatrixHD.Grid;
using Microsoft.Xna.Framework.Content;

namespace QuatrixHD.Blocks
{
    /// <summary>
    /// A brick class, containing information for an individual square brick, such as texture, location, and color.
    /// </summary>
    class BrickType
    {
        Texture2D texture;
        Vector2 vector;
        Color color;
        string asset;

        #region Properties
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Asset
        {
            get { return asset; }
            set { asset = value; }
        }
        #endregion

        #region Constructors
        public BrickType(string textureAsset)
        {
            asset = textureAsset;
        }
        #endregion

        #region Class Functions
        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(asset);
        }

        public void unload()
        {
            texture = null;
            vector = new Vector2();
            color = Color.White;
            asset = "";
        }

        public void move(GridType grid, Direction direction)
        {
            switch (direction)
            {
                case Direction.left:
                    vector.X--;
                    break;

                case Direction.right:
                    vector.X++;
                    break;
            }

            fillGrid(grid);
        }

        public void drop(GridType grid)
        {
            vector.Y++;

            fillGrid(grid);
        }

        public void fillGrid(GridType grid)
        {
            grid.getCell(vector).fillCell(texture, color);
        }

        public void clearGrid(GridType grid)
        {
            grid.getCell(vector).emptyCell();
        }
        #endregion
    }
}
