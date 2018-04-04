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

namespace QuatrixHD.Grid
{
    /// <summary>
    /// Creates a grid of cells to hold the bricks of a block.  The grid spaces are filled in by blocks from the 
    /// game, and then drawn to the screen.  The grid also retains the block locations, allowing easy collision
    /// detection.
    /// </summary>
    class GridType
    {
        CellType[,] cells;
        Vector2 origin;
        Vector2 size;
        int x;
        int y;

        public GridType(Vector2 gridOrigin, Vector2 cellSize, int width, int height)
        {
            cells = new CellType[width, height];

            origin = gridOrigin;
            size = cellSize;
            x = width;
            y = height;
        }

        public void initialize()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    cells[i, j] = new CellType(new Vector2(origin.X + size.X * i, origin.Y + size.Y * j));
                }
            }
        }

        public void unload()
        {
            foreach (CellType cell in cells)
                cell.emptyCell();

            cells = null;
            origin = new Vector2();
            size = new Vector2();
            x = 0;
            y = 0;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (CellType cell in cells)
                cell.draw(spriteBatch);
        }

        public void clearGrid()
        {
            foreach (CellType cell in cells)
                cell.emptyCell();
        }

        public CellType getCell(Vector2 location)
        {
            return cells[(int)location.X, (int)location.Y];
        }

        public int deleteRow(ref Vector2 rowVector)
        {
            int returnScore = 0;

            //Check each row to see if it is full
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (!cells[j, i].IsFull)
                        break;

                    if (j == x - 1)
                    {
                        clearRow(i);
                        returnScore += 100;
                        rowVector = cells[0, i].Location;
                    }
                }
            }

            return returnScore;
        }

        private void clearRow(int row)
        {
            //Clear the current row
            for (int i = 0; i < x; i++)
            {
                cells[i, row].emptyCell();
            }

            //For all rows above the row, copy the contents of the cell into the one below it
            for (int i = row - 1; i >= 0; i--)
            {
                for (int j = 0; j < x; j++)
                {
                    cells[j, i + 1].fillCell(cells[j, i]);
                }
            }

            //Empty the top row
            for (int i = 0; i < x; i++)
            {
                cells[i, 0].emptyCell();
            }
        }
    }
}
