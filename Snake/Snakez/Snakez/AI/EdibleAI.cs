using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Snakez.Levels;
using Microsoft.Xna.Framework.Graphics;
using Snakez.AI;

namespace Snakez
{
    class EdibleAI : IAgent
    {
        FoodClassification classification;
        ThreatLevel threat;
        OrientationType orientation;
        bool moveFood;
        int innerZone;
        int outerZone;
        int moveBound;
        int upperBound;
        int random;

        public OrientationType Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public EdibleAI(FoodClassification food)
        {
            classification = food;
            threat = ThreatLevel.low;
            moveFood = true;
        }

        public void initialize()
        {
            switch (classification)
            {
                case FoodClassification.standard:
                    innerZone = 2;
                    outerZone = 4;
                    moveBound = 7;
                    upperBound = 2;
                    break;

                case FoodClassification.quick:
                    innerZone = 5;
                    outerZone = 10;
                    moveBound = 20;
                    upperBound = 3;
                    break;

                case FoodClassification.smart:
                    innerZone = 10;
                    outerZone = 15;
                    moveBound = 20;
                    upperBound = 6;
                    break;
            }
        }

        public void move(SnakeType snake, FoodType food, LevelType level)
        {
            PortalType portal;

            random = RandomGenerator.generator.Next();

            deduceThreat(snake, food);

            deduceMove();

            if (moveFood)
            {
                deduceOrientation(snake, food);

                if (level.tryGetPortal(food.Vector, out portal))
                {
                    if (orientation == portal.Entrance)
                    {
                        if (!portal.Link.IsFull)
                        {
                            portal.teleport();
                            portal.clearGrid(level);
                        }
                    }
                    else
                        movement(food, level);
                }
                else
                    movement(food, level);
            }
        }

        private void deduceThreat(SnakeType snake, FoodType food)
        {
            int yClose = (int)(snake.Vector.Y - food.Vector.Y);
            int xClose = (int)(snake.Vector.X - food.Vector.X);

            if ((Math.Abs(yClose) <= innerZone) || (Math.Abs(xClose) <= innerZone))
            {
                threat = ThreatLevel.high;
            }
            else if ((Math.Abs(yClose) <= outerZone) || (Math.Abs(xClose) <= outerZone))
            {
                threat = ThreatLevel.medium;
            }
            else if ((Math.Abs(yClose) > outerZone) || (Math.Abs(xClose) > outerZone))
            {
                threat = ThreatLevel.low;
            }
        }

        private void deduceOrientation(SnakeType snake, FoodType food)
        {
            int number = random % upperBound;

            switch (threat)
            {
                case ThreatLevel.low:
                    if (number == 0)
                        randomOrientation();
                    else if (number == 1)
                        directionalOrientation(food);
                    else
                        runOrientation(snake, food);
                    break;

                case ThreatLevel.medium:
                    if (number == 0 || number == 1)
                        randomOrientation();
                    else if (number == 2)
                        runOrientation(snake, food);
                    else
                        smartOrientation(snake);
                    break;

                case ThreatLevel.high:
                    if (number == 0 || number == 1)
                        runOrientation(snake, food);
                    else
                        smartOrientation(snake);
                    break;
            }
        }

        private void deduceMove()
        {
            moveFood = true;

            int number = random % moveBound;

            if (number == 0)
                moveFood = false;
            else
            {
                switch (threat)
                {
                    case ThreatLevel.low:
                        if ((number % 2) == 0)
                            moveFood = false;
                        break;

                    case ThreatLevel.medium:
                        if ((number % 3) == 0)
                            moveFood = false;
                        break;

                    case ThreatLevel.high:
                        if (number == 3)
                            moveFood = false;
                        break;
                }
            }
        }

        private void movement(FoodType food, LevelType level)
        {
            PortalType portal;

            //If the food is in a portal, clear the portal
            if (level.tryGetPortal(food.Vector, out portal))
            {
                portal.clearGrid(level);
            }
            //Else empty the old grid cell
            else
                level.Grid.GetCell(food.Vector).emptyCell();

            //Add the movement vector to the current mouse vector
            Vector2 newLocation = food.Vector + MovementFunctions.calculateLocation(orientation);

            if (level.tryGetPortal(newLocation, out portal))
            {
                if (!portal.IsFull)
                {
                    food.Vector = newLocation;
                    portal.fillPortal(food);
                }
            }
            else if (level.Grid.GetCell(newLocation).content == CellContent.empty)
                food.Vector = newLocation;

            food.fillGrid(level);
        }

        //Returns the same orientation as the snake, simulating the food running from the snake
        private void runOrientation(SnakeType snake, FoodType food)
        {
            bool temp = false;

            //Determine if food is in front of snake
            switch (snake.Orientation)
            {
                case OrientationType.up:
                    if (snake.Vector.Y > food.Vector.Y)
                        temp = true;
                    break;

                case OrientationType.down:
                    if (snake.Vector.Y < food.Vector.Y)
                        temp = true;
                    break;

                case OrientationType.left:
                    if (snake.Vector.X > food.Vector.X)
                        temp = true;
                    break;

                case OrientationType.right:
                    if (snake.Vector.X < food.Vector.X)
                        temp = true;
                    break;
            }

            //If the food is in front of snake, run, else random
            if (temp)
                orientation = snake.Orientation;
            else
                randomOrientation();
        }

        //Returns a perpenticular orientation to the snake, simulating the food "zig-zagging"
        private void smartOrientation(SnakeType snake)
        {
            int number = random % 2;

            switch (snake.Orientation)
            {
                case OrientationType.up:
                case OrientationType.down:
                    orientation = (OrientationType)(2 + number);
                    break;

                case OrientationType.left:
                case OrientationType.right:
                    orientation = (OrientationType)(number);
                    break;
            }
        }

        //Returns a random orientation
        private void randomOrientation()
        {
            orientation = (OrientationType)(random % 4);
        }

        //Returns an orientation to move the mouse away from a wall and into the center of the level
        private void directionalOrientation(FoodType food)
        {
            switch (orientation)
            {
                case OrientationType.up:
                    if (food.Vector.Y < 4)
                        orientation = OrientationType.down;
                    break;

                case OrientationType.down:
                    if (food.Vector.Y > 28)
                        orientation = OrientationType.up;
                    break;

                case OrientationType.left:
                    if (food.Vector.X < 4)
                        orientation = OrientationType.right;
                    break;

                case OrientationType.right:
                    if (food.Vector.X > 20)
                        orientation = OrientationType.left;
                    break;
            }
        }
    }
}
