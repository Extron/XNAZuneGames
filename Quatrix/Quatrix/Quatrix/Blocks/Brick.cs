/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of Quatrix.
 * 
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Quatrix
{
    class Brick
    {
        #region Properties
        public Texture2D Text { get; set; }
        public Rectangle Rect;
        public Color color = Color.White;
        public int Coordinate { get; set; }
        public bool IsVisible { get; set; }
        public bool IsCounted { get; set; }
        #endregion

        #region Class Functions
        public void setBrickCoordinates(GameGridCell cell)
        {
            if (Rect.X == cell.square.X && Rect.Y == cell.square.Y)
                Coordinate = cell.coordinate;
        }

        public void fillGridCell(GameGridCell cell)
        {
            if (Rect.X == cell.square.X && Rect.Y == cell.square.Y)
            {
                cell.squareFilled = true;
                cell.brick = Text;
                cell.color = color;
            }
        }

        public void drawBrick(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Text, Rect, color);
        }

        public void drawBrick(SpriteBatch spriteBatch, Color brickColor)
        {
            spriteBatch.Draw(Text, Rect, brickColor);
        }

        public void countRow(int row, ref int rowCounter)
        {
            if (MovementFunctions.BlockCoordinateCount(row, Coordinate) && !IsCounted)
            {
                rowCounter++;
                IsCounted = true;
            }
        }

        public Brick copy()
        {
            Brick copyBrick = new Brick();

            copyBrick.Rect = this.Rect;
            copyBrick.Text = this.Text;
            copyBrick.Coordinate = this.Coordinate;
            copyBrick.IsCounted = this.IsCounted;
            copyBrick.IsVisible = this.IsVisible;

            return copyBrick;
        }
        #endregion
    }
}
