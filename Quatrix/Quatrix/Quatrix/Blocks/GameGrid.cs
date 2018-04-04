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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace Quatrix
{
    class GameGrid
    {
        public GameGrid()
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cells[j, i] = new GameGridCell();
                }
            }    
        }
        
        GameGridCell[,] cells = new GameGridCell[10, 19];

        public GameGridCell GetCell(int x, int y)
        {
            return cells[x, y];
        }

        public void deleteRow(int[] rowCounter, Texture2D clearBrick, out int scoreIn, SoundEffect sound, bool playSound)
        {
            bool soundOnce = true;
            scoreIn = 0;

            for (int m = 0; m < 19; m++)
            {
                // row counter tracks how many bricks are in the row - a full row is 10 bricks
                if (rowCounter[m] == 10)
                {
                    for (int j = m; j > 0; j--)
                    {
                        // copy higher rows down one row
                        for (int i = 0; i < 10; i++)
                        {
                            GetCell(i, j).brick = this.GetCell(i, j - 1).brick;
                            GetCell(i, j).squareFilled = this.GetCell(i, j - 1).squareFilled;
                            GetCell(i, j).coordinate = this.GetCell(i, j - 1).coordinate + 10;
                            GetCell(i, j).isCounted = this.GetCell(i, j - 1).isCounted;
                            GetCell(i, j).color = this.GetCell(i, j - 1).color;
                            rowCounter[j] = rowCounter[j - 1];
                        }
                    }

                    // initialize the top row
                    for (int k = 0; k < 10; k++)
                    {
                        GetCell(k, 0).squareFilled = false;
                        GetCell(k, 0).brick = clearBrick;
                        GetCell(k, 0).color = Color.White;
                    }

                    scoreIn = scoreIn + 100;

                    if (playSound && soundOnce)
                    {
                        sound.Play();
                        soundOnce = false;
                    }
                }
            }
        }

        public void rowCount(int[] rowCounter)
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.GetCell(j, i).squareFilled && !this.GetCell(j, i).isCounted)
                    {
                        rowCounter[i]++;
                        this.GetCell(j, i).isCounted = true;
                    }                    
                }                
            }
        }

        public void drawGrid(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 10; j++)
                    spriteBatch.Draw(GetCell(j, i).brick, GetCell(j, i).square, GetCell(j, i).color);
            }
        }
    }

    class GameGridCell
    {
        public Rectangle square;
        public Texture2D brick;
        public Color color;
        public bool squareFilled;        
        public bool isCounted;
        public int coordinate;        
    }
}
