/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of Snakez.
 * 
 * Snakez is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Snakez is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Snakez.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Snakez
{
    static class MovementFunctions
    {
        public static float calculateRotation(OrientationType orientation)
        {
            float rotation = 0;

            switch (orientation)
            {
                case OrientationType.right:
                    rotation = (float)(Math.PI / 2);
                    break;

                case OrientationType.left:
                    rotation = (float)(Math.PI / -2);
                    break;

                case OrientationType.down:
                    rotation = (float)(Math.PI);
                    break;

                case OrientationType.up:
                    rotation = 0;
                    break;
            }

            return rotation;
        }

        
        public static float calculateCurveRotation(OrientationType oldOrientation, OrientationType newOrientation)
        {
            float rotation = float.NaN;

            switch (newOrientation)
            {
                case OrientationType.right:
                    if (oldOrientation == OrientationType.up)
                        rotation = (float)(Math.PI / 2);
                    else if (oldOrientation == OrientationType.down)
                        rotation = 0;
                    break;

                case OrientationType.left:
                    if (oldOrientation == OrientationType.up)
                        rotation = (float)Math.PI;
                    else if (oldOrientation == OrientationType.down)
                        rotation = (float)(Math.PI / -2);
                    break;

                case OrientationType.up:
                    if (oldOrientation == OrientationType.right)
                        rotation = (float)(Math.PI / -2);
                    else if (oldOrientation == OrientationType.left)
                        rotation = 0;
                    break;

                case OrientationType.down:
                    if (oldOrientation == OrientationType.right)
                        rotation = (float)Math.PI;
                    else if (oldOrientation == OrientationType.left)
                        rotation = (float)(Math.PI / 2);
                    break;
            }

            return rotation;
        }
        
        public static Vector2 calculateLocation(OrientationType orientation)
        {
            Vector2 vector = new Vector2();

            switch (orientation)
            {
                case OrientationType.up:
                    vector =  new Vector2(0, -1);
                    break;

                case OrientationType.down:
                    vector =  new Vector2(0, 1);
                    break;

                case OrientationType.left:
                    vector =  new Vector2(-1, 0);
                    break;

                case OrientationType.right:
                    vector =  new Vector2(1, 0);
                    break;
            }

            return vector;
        }

        public static bool calculateMovement(GameGrid grid, FoodType food, OrientationType orientation)
        {
            bool move = false;

            int x = (int)food.Vector.X;
            int y = (int)food.Vector.Y;

            switch (orientation)
            {
                case OrientationType.up:
                    if (grid.GetCell(x, y - 1).content == CellContent.empty)
                        move = true;
                    break;

                case OrientationType.down:
                    if (grid.GetCell(x, y + 1).content == CellContent.empty)
                        move = true;
                    break;

                case OrientationType.right:
                    if (grid.GetCell(x + 1, y).content == CellContent.empty)
                        move = true;
                    break;

                case OrientationType.left:
                    if (grid.GetCell(x - 1, y).content == CellContent.empty)
                        move = true;
                    break;
            }

            return move;
        }
    }
}
