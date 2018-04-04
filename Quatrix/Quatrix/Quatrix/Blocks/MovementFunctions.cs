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

namespace Quatrix
{
    static class MovementFunctions
    {
        public static int BounderyCoordinate(int coordinate)
        {
            while (coordinate > 99)
                coordinate = coordinate - 100;

            while (coordinate > 9)
                coordinate = coordinate - 10;

            return coordinate;
        }
        
        public static bool LeftBounderyCollision(GameBlockNew block)
        {
            if (BounderyCoordinate(block.Brick1.Coordinate) == 1)
                return false;

            if (BounderyCoordinate(block.Brick2.Coordinate) == 1)
                return false;

            if (BounderyCoordinate(block.Brick3.Coordinate) == 1)
                return false;

            if (BounderyCoordinate(block.Brick4.Coordinate) == 1)
                return false;
            else
                return true;
        }

        public static bool RightBounderyCollision(GameBlockNew block)
        {
            if (BounderyCoordinate(block.Brick1.Coordinate) == 0)
                return false;

            if (BounderyCoordinate(block.Brick2.Coordinate) == 0)
                return false;

            if (BounderyCoordinate(block.Brick3.Coordinate) == 0)
                return false;

            if (BounderyCoordinate(block.Brick4.Coordinate) == 0)
                return false;
            else
                return true;
        }

        public static bool RightSideCollision(GameBlockNew block, GameGrid grid)
        {
            int i = 0;
            int j = 0;

            if (block.Brick2.Rect.X < 138)
            {
                CalculateCoordinates(block.Brick1.Coordinate, out i, out j);
                if (grid.GetCell(i + 1, j).squareFilled)
                    return false;

                CalculateCoordinates(block.Brick2.Coordinate, out i, out j);
                if (grid.GetCell(i + 1, j).squareFilled)
                    return false;

                CalculateCoordinates(block.Brick3.Coordinate, out i, out j);
                if (grid.GetCell(i + 1, j).squareFilled)
                    return false;

                CalculateCoordinates(block.Brick4.Coordinate, out i, out j);
                if (grid.GetCell(i + 1, j).squareFilled)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        public static bool LeftSideCollision(GameBlockNew block, GameGrid grid)
        {
            int i = 0;
            int j = 0;

            if (block.Brick1.Rect.X > 24)
            {
                CalculateCoordinates(block.Brick1.Coordinate, out i, out j);
                if (grid.GetCell(i - 1, j).squareFilled)
                    return false;

                CalculateCoordinates(block.Brick2.Coordinate, out i, out j);
                if (grid.GetCell(i - 1, j).squareFilled)
                    return false;

                CalculateCoordinates(block.Brick3.Coordinate, out i, out j);
                if (grid.GetCell(i - 1, j).squareFilled)
                    return false;

                CalculateCoordinates(block.Brick4.Coordinate, out i, out j);
                if (grid.GetCell(i - 1, j).squareFilled)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        public static void CalculateCoordinates(int coordinate, out int iIn, out int jIn)
        {
            iIn = 0;
            jIn = 0;

            if (coordinate > 100)
            {
                jIn = jIn + 10;
                coordinate = coordinate - 100;
            }

            if (coordinate <= 100 && coordinate > 10)
            {
                if (coordinate % 10 != 0)
                {
                    jIn = jIn + (coordinate / 10);
                    coordinate = coordinate - ((coordinate / 10) * 10);
                }
                else if (coordinate % 10 == 0)
                {
                    jIn = jIn + ((coordinate / 10) - 1);
                    coordinate = coordinate - ((coordinate / 10) * 10);
                }
            }

            if (coordinate <= 10 && coordinate > 0)
            {
                iIn = iIn + (coordinate - 1);
            }
            else
                iIn = iIn + 9;
        }
        
        public static bool BlockCoordinateCount(int iIn, int coordinate)
        {
            int coordinateCount = 0;

            if (coordinate % 10 != 0)
                coordinateCount = coordinate / 10;

            else
                coordinateCount = (coordinate / 10) - 1;

            if (coordinateCount == iIn)
                return true;
            else
                return false;
        }

        public static bool BlockDropCollision(GameBlockNew block, GameGrid grid)
        {
            int i = 0;
            int j = 0;

            CalculateCoordinates(block.Brick1.Coordinate, out i, out j);
            if (grid.GetCell(i, j + 1).squareFilled)
                return false;

            CalculateCoordinates(block.Brick2.Coordinate, out i, out j);
            if (grid.GetCell(i, j + 1).squareFilled)
                return false;

            CalculateCoordinates(block.Brick3.Coordinate, out i, out j);
            if (grid.GetCell(i, j + 1).squareFilled)
                return false;

            CalculateCoordinates(block.Brick4.Coordinate, out i, out j);
            if (grid.GetCell(i, j + 1).squareFilled)
                return false;
            else
                return true;
        }

        public static bool RotationCollision(GameBlockNew block, GameGrid grid)
        {
            GameBlockNew rotatedBlock;
            bool temp = true;
            int i = 0;
            int j = 0;

            if (block.Brick1.Coordinate <= 10 || block.Brick2.Coordinate > 180)
            {
                temp = false;
                return temp;
            }

            rotatedBlock = block.Copy();

            rotatedBlock.Rotate();

            CalculateCoordinates(rotatedBlock.Brick1.Coordinate, out i, out j);
            if (grid.GetCell(i, j).squareFilled)
                temp = false;

            CalculateCoordinates(rotatedBlock.Brick2.Coordinate, out i, out j);
            if (grid.GetCell(i, j).squareFilled)
                temp = false;

            CalculateCoordinates(rotatedBlock.Brick3.Coordinate, out i, out j);
            if (grid.GetCell(i, j).squareFilled)
                temp = false;

            CalculateCoordinates(rotatedBlock.Brick4.Coordinate, out i, out j);
            if (grid.GetCell(i, j).squareFilled)
                temp = false;

            return temp;
        }
    }
}
