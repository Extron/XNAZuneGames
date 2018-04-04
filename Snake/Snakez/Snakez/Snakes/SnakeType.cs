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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GameMenus;
using ZHandler;
using Snakez.Levels;

namespace Snakez
{
    class SnakeType
    {
        #region Event Handlers
        /// <summary>
        /// This event is raised when the snake's head is in the same cell as a food item.  It raises methods to 
        /// update the score, respawn the food item, and play the sound.
        /// </summary>
        public EventHandler<FoodArgs> EatFood;

        /// <summary>
        /// This event is raised when the snake "eats" an activatable object, i.e. a key.
        /// </summary>
        public EventHandler ActivateObject;

        /// <summary>
        /// This event is raised when the snake hits a wall or tries to eat itself.
        /// </summary>
        public EventHandler GameOver;
        #endregion

        #region Variables
        /// <summary>
        /// A static variable that defines the snake's color.
        /// </summary>
        public static Color color = Color.Red;

        /// <summary>
        /// A static variable that represents the time it takes for a snake to move one space forward.
        /// This can be changed according to difficulty level.
        /// </summary>
        public static int interval = 5;

        /// <summary>
        /// The snake's head object.
        /// </summary>
        HeadType head;

        /// <summary>
        /// A list of the snake body segments.
        /// </summary>
        List<BodyType> body;

        /// <summary>
        /// The snake's tail object.
        /// </summary>
        TailType tail;

        /// <summary>
        /// A curve object that is used as a template to add curves to the curve list.
        /// </summary>
        CurveType temp;

        /// <summary>
        /// A list of current curves in the snake, sorted by their location in the level.
        /// </summary>
        Dictionary<Vector2, CurveType> curves;

        /// <summary>
        /// An interger variable that counts.  It is used as a movement speed, and when the counter reaches 
        /// a set interval, the snake will progress one space forward.
        /// </summary>
        int counter;

        /// <summary>
        /// This variable identifies wether the snake is allowed to turn.  The snake can only turn once in a movement
        /// progression, but the user can register a turn any time, if there are no turns already in effect.
        /// </summary>
        bool canTurn;

        /// <summary>
        /// An integer that represents the index of the segment that is currently moved.
        /// </summary>
        int currentSegment;
        #endregion

        #region Properties
        /// <summary>
        /// The vector of the snake, defined as the current vector of the snake's head.
        /// </summary>
        public Vector2 Vector
        {
            get { return head.Vector; }
            set { head.Vector = value; }
        }

        /// <summary>
        /// The orientation of the snake, defined as the current orientation of the snake's head.
        /// </summary>
        public OrientationType Orientation
        {
            get { return head.Orientation; }
            set { head.Orientation = value; }
        }

        public int Length
        {
            get { return body.Count + 2; }
        }
        #endregion

        #region Constructors
        public SnakeType()
        {
            body = new List<BodyType>();
            curves = new Dictionary<Vector2, CurveType>();
            head = new HeadType();
            tail = new TailType();
            temp = new CurveType();

            for (int i = 0; i < 4; i++)
            {
                BodyType segment = new BodyType(i + 2);
                body.Add(segment);
            }

            currentSegment = 3;
        }
        #endregion

        #region Game Functions
        /// <summary>
        /// Initializes the snake's vectors and orientations depending on the level's spawn point.
        /// </summary>
        /// <param name="spawnPoint">The spawn point of the level, containing orientation and location data.</param>
        public void initialize(SpawnData spawnPoint)
        {
            head.initialize(spawnPoint);

            tail.initialize(spawnPoint);

            foreach (BodyType segment in body)
                segment.initialize(spawnPoint);

            temp.initialize(spawnPoint);
        }

        /// <summary>
        /// Loads all graphical content for the snake.
        /// </summary>
        /// <param name="content">The game's content manager.</param>
        public void load(ContentManager content)
        {
            head.load(content);

            foreach (BodyType segment in body)
                segment.load(content);

            tail.load(content);

            temp.load(content);
        }

        /// <summary>
        /// Updates the snake, based upon user input and level design.  
        /// </summary>
        /// <param name="i">The game's input handler.</param>
        /// <param name="level">The current level the snake is in.</param>
        public void update(InputHandlerComponent i, LevelType level)
        {
            //If the snake needs to progress forward, check collision and react accordingly
            if (counter == interval)
            {
                switch (collision(level))
                {
                    //If the potential cell is empty or a portal, move the snake there.
                    case CellContent.empty:
                    case CellContent.portal:
                        move(level);
                        break;

                    //If the cell is filled with a food item, move there and eat the food.
                    case CellContent.food:
                        move(level);
                        EatFood(this, level.LevelFood.getArgs());
                        break;

                    //If the cell is filled with a level item, i.e. a key, move there and activate the item.
                    case CellContent.item:
                        move(level);
                        ActivateObject(this, new EventArgs());
                        break;

                    //If the cell is a wall or a part of the snake, raise the game over event.
                    case CellContent.snake:
                    case CellContent.wall:
                        GameOver(this, new EventArgs());
                        break;
                }

                counter = 0;
                canTurn = true;
            }

            //If a turn has not already been activated, allow the snake to attempt to turn
            if (canTurn)
                turn(i);

            //Update the counter
            counter++;
        }

        /// <summary>
        /// Fills the grid with the current snake part locations.
        /// </summary>
        /// <param name="grid">The grid of the current level.</param>
        public void spawn(GameGrid grid)
        {
            head.fillGrid(grid, color);

            foreach (BodyType segment in body)
                segment.fillGrid(grid, color);

            tail.fillGrid(grid, color);
        }

        /// <summary>
        /// Resets the snake to the location designated by the level's spawn point and spawns the snake.
        /// </summary>
        /// <param name="grid">The current level's grid.</param>
        /// <param name="spawnPoint">The current level's spawn point.</param>
        public void reset(GameGrid grid, SpawnData spawnPoint)
        {
            //Clear the grid
            grid.GetCell(head.Vector).emptyCell();

            foreach (BodyType segment in body)
                grid.GetCell(segment.Vector).emptyCell();

            foreach (CurveType curve in curves.Values)
                grid.GetCell(curve.Vector).emptyCell();

            grid.GetCell(tail.Vector).emptyCell();

            //Remove all excess snake segments
            body.RemoveRange(4, body.Count - 4);

            //Reset the segment numbers correctly
            for (int i = 2; i <= body.Count + 1; i++)
                body[i - 2].SegmentNumber = i;

            //Reset the current updatable segment
            currentSegment = 3;

            //Reset the curve list
            curves = new Dictionary<Vector2, CurveType>();

            //Set the location data for the snake
            initialize(spawnPoint);

            //Spawn the snake
            spawn(grid);
        }

        /// <summary>
        /// Adds a variable amount of segments to the end of the snake.
        /// </summary>
        /// <param name="number">The number of segments that need to be added.</param>
        public void addSegment(int number)
        {
            //Add a new segment by copying the last segment and inserting that copy at the end of the list
            for (int i = 0; i < number; i++)
            {
                //Create the new segment and equate it to the last segment in the snake.
                BodyType temp = new BodyType(body.Count);
                temp.deepCopy(body[currentSegment]);

                //Insert the segment after the current updatable segment in the list.
                body.Insert(currentSegment, temp);

                //Update the current segment
                currentSegment++;
            }
        }

        /// <summary>
        /// Removes a variable amount of segments from the snake.
        /// </summary>
        /// <param name="level">The current level.</param>
        /// <param name="number">The number of segments to remove.</param>
        public void removeSegment(LevelType level, int number)
        {
            for (int i = 0; i < number; i++)
            {
                //The number of segments can never be lower than 4 segments
                if (body.Count > 4)
                {
                    //Clear the segment from the grid
                    level.Grid.GetCell(body[currentSegment].Vector).emptyCell();

                    if (curves.ContainsKey(body[currentSegment].Vector))
                    {
                        curves.Remove(body[currentSegment].Vector);
                    }

                    //Set the tail orientation to the current segment
                    tail.Orientation = body[currentSegment].Orientation;

                    //Remove the current updateable segment
                    body.RemoveAt(currentSegment);

                    //Update the current segment
                    if (currentSegment == 0)
                        currentSegment = body.Count - 1;
                    else
                        currentSegment--;

                    //Move the tail
                    moveTail(level);
                }
            }
        }
        #endregion

        #region Class Functions
        /// <summary>
        /// Progresses the snake one cell forward within the level.
        /// </summary>
        /// <param name="level">The current level.</param>
        private void move(LevelType level)
        {
            //Progress the tail first
            moveTail(level);

            //Take the last snake segment and move it to the front.  This simulates a progression of all segments
            body[currentSegment].Vector = head.Vector;

            //Move the head last
            moveHead(level);

            //Set the orientation for all of the snake parts
            setOrientation(level);

            //Fill the grid with the updated snake data
            fillGrid(level);

            //Update the currentSegment interger
            if (currentSegment == 0)
                currentSegment = body.Count - 1;
            else
                currentSegment--;
        }

        /// <summary>
        /// Progresses the snake's tail forward.
        /// </summary>
        /// <param name="level">The current level.</param>
        private void moveTail(LevelType level)
        {
            PortalType portal;

            //Clear the cell that contains the old tail position only if it is not in a portal
            if (level.tryGetPortal(tail.Vector, out portal))
            {
                //If the tail is in an entrance portal, telport
                if (tail.Orientation == portal.Entrance)
                    portal.teleport();
                //Else progress the snake normally
                else
                    tail.Vector = body[currentSegment].Vector;

                //Clear the portal
                portal.clearGrid(level);
            }
            else
            {
                //Clear the grid cell
                level.Grid.GetCell(tail.Vector).emptyCell();

                //Progress the tail: Set it to the same coordinate as the segment before it
                tail.Vector = body[currentSegment].Vector;
            }
        }

        /// <summary>
        /// Progress the snake's head forward.
        /// </summary>
        /// <param name="level">The current level.</param>
        private void moveHead(LevelType level)
        {
            PortalType portal;

            //If the snake is in a portal, check to see if it can teleport
            if (level.tryGetPortal(head.Vector, out portal))
            {
                //If the snake's orientation is the same as the portal's, teleport.
                if (head.Orientation == portal.Entrance)
                    portal.teleport();
                //Else clear the portal and move the snake normally
                else
                {
                    portal.clearPortal();

                    head.Vector += MovementFunctions.calculateLocation(Orientation);
                }
            }
            //If the snake is in a normal square, move normally
            else
                head.Vector += MovementFunctions.calculateLocation(Orientation);
        }

        /// <summary>
        /// Turns the snake based on the input from the user.
        /// </summary>
        /// <param name="i">The game's input handler</param>
        private void turn(InputHandlerComponent i)
        {
            //The new orientation of the snake
            OrientationType newOrientation = head.Orientation;

            //Get the new orientation based on the input
            if (i.getButton("Right", true) && head.Orientation != OrientationType.left)
                newOrientation = OrientationType.right;
            else if (i.getButton("Up", true) && head.Orientation != OrientationType.down)
                newOrientation = OrientationType.up;
            else if (i.getButton("Down", true) && head.Orientation != OrientationType.up)
                newOrientation = OrientationType.down;
            else if (i.getButton("Left", true) && head.Orientation != OrientationType.right)
                newOrientation = OrientationType.left;

            //If there has been a change in the orientation, add a curve and change the snake
            if (newOrientation != head.Orientation)
            {
                CurveType curve = new CurveType();
                curve.deepCopy(temp);

                curve.Vector = head.Vector;
                curve.Orientation = newOrientation;
                curve.Rotation = MovementFunctions.calculateCurveRotation(head.Orientation, newOrientation);
                curves.Add(curve.Vector, curve);

                head.Orientation = newOrientation;

                canTurn = false;
            }
        }

        /// <summary>
        /// Fills the level grid with the current positions of the snake.
        /// </summary>
        /// <param name="level">The current level.</param>
        public void fillGrid(LevelType level)
        {
            PortalType portal;

            if (level.tryGetPortal(head.Vector, out portal))
            {
                portal.fillPortal(head);
                portal.fillGrid(level);
            }
            else
                head.fillGrid(level.Grid, color);

            //If there is a curve at the same location as the segment, do not draw that segment
            if (!(curves.ContainsKey(body[currentSegment].Vector)))
            {
                if (level.tryGetPortal(body[currentSegment].Vector, out portal))
                {
                    portal.fillPortal(body[currentSegment]);
                    portal.fillGrid(level);
                }
                else
                    body[currentSegment].fillGrid(level.Grid, color);
            }

            if (level.tryGetPortal(tail.Vector, out portal))
            {
                portal.fillPortal(tail);
                portal.fillGrid(level);
            }
            else
                tail.fillGrid(level.Grid, color);

            foreach (CurveType curve in curves.Values)
            {
                if (level.tryGetPortal(curve.Vector, out portal))
                {
                    portal.fillPortal(curve);
                    portal.fillGrid(level);
                }
                else
                    curve.fillGrid(level.Grid, color);
            }
        }

        /// <summary>
        /// Sets the orientation and rotation of the snake's segments, based on the curves of the snake.
        /// </summary>
        /// <param name="level">The current level.</param>
        private void setOrientation(LevelType level)
        {
            PortalType portal;

            //If the tail is at the same location as a curve, set the tail orientation to the curve orientiation
            //and delete that curve from the list
            if (curves.ContainsKey(tail.Vector))
            {
                tail.Orientation = curves[tail.Vector].Orientation;
                curves.Remove(tail.Vector);
            }

            //If the current segment is at the same location as a curve, set the segment orientation to the curve orientation
            if (curves.ContainsKey(body[currentSegment].Vector))
                body[currentSegment].Orientation = curves[body[currentSegment].Vector].Orientation;
            //Else if the segment is in an exit portal, it has the same orientation as the head,
            //otherwise its orientation becomes the same as the segment behind it.
            else
            {
                if (currentSegment + 1 == body.Count)
                {
                    if (level.tryGetPortal(body[currentSegment].Vector, out portal) && level.tryGetPortal(body[0].Vector, out portal))
                        body[currentSegment].Orientation = head.Orientation;
                    else
                        body[currentSegment].Orientation = body[0].Orientation;
                }
                else
                {
                    if (level.tryGetPortal(body[currentSegment].Vector, out portal) && level.tryGetPortal(body[currentSegment + 1].Vector, out portal))
                        body[currentSegment].Orientation = head.Orientation;
                    else
                        body[currentSegment].Orientation = body[currentSegment + 1].Orientation;
                }
            }
        }

        /// <summary>
        /// This function returns the content of the cell the snake is attempting to move to.
        /// </summary>
        /// <param name="grid">The game grid that contains all object data for the level.</param>
        /// <returns>Returns the content of the cell the snake will move into.  Depending on this, the game will respond 
        /// differently, i.e. if there is a wall, the game wil go to game over.</returns>
        private CellContent collision(LevelType level)
        {
            Vector2 newLocation = MovementFunctions.calculateLocation(Orientation) + Vector;

            try
            {
                return level.Grid.GetCell(newLocation).content;
            }
            catch
            {
                return CellContent.portal;
            }
        }
        #endregion
    }
}
