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
using QuatrixHD.Blocks;
using QuatrixHD.Grid;
using Microsoft.Xna.Framework;

namespace QuatrixHD.Quatrix
{
    /// <summary>
    /// Contains static methods for collision detection and resolution.
    /// </summary>
    static class Collision
    {
        /// <summary>
        /// Determines if a block is touching the floor of a grid with 19 rows.
        /// </summary>
        /// <param name="block">The block that is being checked.</param>
        /// <returns>Returns true if the block is touching the floor, returns false otherwise.</returns>
        public static bool floorCollision(BlockType block)
        {
            bool returnValue = false;

            if (block.Brick1.Vector.Y == 18)
                returnValue = true;
            else if (block.Brick2.Vector.Y == 18)
                returnValue = true;
            else if (block.Brick3.Vector.Y == 18)
                returnValue = true;
            else if (block.Brick4.Vector.Y == 18)
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Determines if a falling progression of a block will result in overlap with other blocks within the grid.
        /// </summary>
        /// <param name="block">The block to be checked.</param>
        /// <param name="grid">The grid that the block is being filled to.</param>
        /// <returns>Returns true if the current block will overlap another block in the grid if it falls one row.</returns>
        public static bool blockCollision(BlockType block, GridType grid)
        {
            bool returnValue = false;

            Vector2 newVector = block.Brick1.Vector;

            newVector.Y++;

            if (newVector.Y < 19)
            {
                if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                    returnValue = true;
            }

            newVector = block.Brick2.Vector;

            newVector.Y++;

            if (newVector.Y < 19)
            {
                if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                    returnValue = true;
            }

            newVector = block.Brick3.Vector;

            newVector.Y++;

            if (newVector.Y < 19)
            {
                if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                    returnValue = true;
            }

            newVector = block.Brick4.Vector;

            newVector.Y++;

            if (newVector.Y < 19)
            {
                if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                    returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if a block will overlap another block within the grid if it 
        /// moves one space in any horizontal direction.
        /// </summary>
        /// <param name="block">The block to be checked.</param>
        /// <param name="grid">The grid that the block is being filled to.</param>
        /// <param name="direction">The direction to check for overlapping blocks.</param>
        /// <returns>Returns true if a progression of one space to the specified direction of the current block 
        /// results in an overlap with another block in the grid.</returns>
        public static bool blockCollision(BlockType block, GridType grid, Direction direction)
        {
            bool returnValue = false;

            Vector2 newVector;

            if (direction == Direction.left)
            {
                newVector = block.Brick1.Vector;
                newVector.X--;

                if (newVector.X > -1)
                {
                    if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                        returnValue = true;
                }
                else
                    returnValue = true;

                newVector = block.Brick2.Vector;
                newVector.X--;

                if (newVector.X > -1)
                {
                    if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                        returnValue = true;
                }
                else
                    returnValue = true;

                newVector = block.Brick3.Vector;
                newVector.X--;

                if (newVector.X > -1)
                {
                    if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                        returnValue = true;
                }
                else
                    returnValue = true;

                newVector = block.Brick4.Vector;
                newVector.X--;

                if (newVector.X > -1)
                {
                    if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                        returnValue = true;
                }
                else
                    returnValue = true;
            }
            else
            {
                newVector = block.Brick1.Vector;
                newVector.X++;

                if (newVector.X < 10)
                {
                    if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                        returnValue = true;
                }
                else
                    returnValue = true;

                newVector = block.Brick2.Vector;
                newVector.X++;

                if (newVector.X < 10)
                {
                    if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                        returnValue = true;
                }
                else
                    returnValue = true;

                newVector = block.Brick3.Vector;
                newVector.X++;

                if (newVector.X < 10)
                {
                    if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                        returnValue = true;
                }
                else
                    returnValue = true;

                newVector = block.Brick4.Vector;
                newVector.X++;

                if (newVector.X < 10)
                {
                    if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                        returnValue = true;
                }
                else
                    returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if any part of the block is at the designated vector, esentially determining if it has 
        /// collided with that location.
        /// </summary>
        /// <param name="block">The block to be checked.</param>
        /// <param name="vector">The location of the "collision".</param>
        /// <returns>Returns true if the block has "collided" with the location.</returns>
        public static bool brickCollision(BlockType block, Vector2 vector)
        {
            bool returnType = false;

            if (block.Brick1.Vector == vector)
                returnType = true;
            else if (block.Brick2.Vector == vector)
                returnType = true;
            else if (block.Brick3.Vector == vector)
                returnType = true;
            else if (block.Brick4.Vector == vector)
                returnType = true;

            return returnType;
        }

        /// <summary>
        /// Determines if there are blocks within the area that another block is supposed to spawn at.
        /// </summary>
        /// <param name="spawnData"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool overlapCollision(SpawnData spawnData, GridType grid)
        {
            bool returnValue = false;

            if (grid.getCell(spawnData.brick1Vector).IsFull)
                returnValue = true;
            else if (grid.getCell(spawnData.brick2Vector).IsFull)
                returnValue = true;
            else if (grid.getCell(spawnData.brick3Vector).IsFull)
                returnValue = true;
            else if (grid.getCell(spawnData.brick4Vector).IsFull)
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Determines of a block is touching one of the two sides of the game board.
        /// </summary>
        /// <param name="block">The block to be checked.</param>
        /// <param name="direction">The side of the game board to check.</param>
        /// <returns>Returns true if the block is touching the designated side.</returns>
        public static bool sideCollision(BlockType block, Direction direction)
        {
            bool returnValue = false;

            if (direction == Direction.left)
            {
                if (block.Brick1.Vector.X == 0)
                    returnValue = true;
                else if (block.Brick2.Vector.X == 0)
                    returnValue = true;
                else if (block.Brick3.Vector.X == 0)
                    returnValue = true;
                else if (block.Brick4.Vector.X == 0)
                    returnValue = true;

            }
            else
            {
                if (block.Brick1.Vector.X == 9)
                    returnValue = true;
                else if (block.Brick2.Vector.X == 9)
                    returnValue = true;
                else if (block.Brick3.Vector.X == 9)
                    returnValue = true;
                else if (block.Brick4.Vector.X == 9)
                    returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if a progression of the rotation state of a block will result in an overlap with another 
        /// block in the grid.
        /// </summary>
        /// <param name="block">The block to be checked.</param>
        /// <param name="grid">The grid that the block is being filled to.</param>
        /// <returns>Returns true if a rotation by the current block will result in an overlap with another
        /// block in the game grid.</returns>
        public static bool rotationCollision(BlockType block, GridType grid)
        {
            bool returnValue = false;

            RotationData nextRotation;

            nextRotation = block.getRotation(block.Rotation);

            Vector2 newVector = block.Brick1.Vector + nextRotation.brick1Location;

            if ((newVector.X < 0 || newVector.X > 9) || (newVector.Y < 0 || newVector.Y > 18))
                returnValue = true;
            else if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                returnValue = true;

            newVector = block.Brick2.Vector + nextRotation.brick2Location;

            if ((newVector.X < 0 || newVector.X > 9) || (newVector.Y < 0 || newVector.Y > 18))
                returnValue = true;
            else if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                returnValue = true;

            newVector = block.Brick3.Vector + nextRotation.brick3Location;

            if ((newVector.X < 0 || newVector.X > 9) || (newVector.Y < 0 || newVector.Y > 18))
                returnValue = true;
            else if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                returnValue = true;

            newVector = block.Brick4.Vector + nextRotation.brick4Location;

            if ((newVector.X < 0 || newVector.X > 9) || (newVector.Y < 0 || newVector.Y > 18))
                returnValue = true;
            else if (grid.getCell(newVector).IsFull && !brickCollision(block, newVector))
                returnValue = true;

            return returnValue;
        }
    }
}
