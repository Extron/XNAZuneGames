using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snakez.AI;
using Snakez.Levels;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class HarmfulAI : IAgent
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

        public HarmfulAI(FoodClassification food)
        {
            classification = food;
            threat = ThreatLevel.low;
            moveFood = true;
        }

        public void initialize()
        {
            switch (classification)
            {
                case FoodClassification.threat:
                    innerZone = 4;
                    outerZone = 8;
                    moveBound = 7;
                    upperBound = 2;
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
                        movement(snake, food, level);
                }
                else
                    movement(snake, food, level);
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
                        attackOrientation(snake, food);
                    break;

                case ThreatLevel.medium:
                    if (number == 0 || number == 1)
                        randomOrientation();
                    else if (number == 2)
                        attackOrientation(snake, food);
                    else
                        smartOrientation(snake);
                    break;

                case ThreatLevel.high:
                    if (number == 0 || number == 1)
                        attackOrientation(snake, food);
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

        private void movement(SnakeType snake, FoodType food, LevelType level)
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
            else if (level.Grid.GetCell(newLocation).content == CellContent.snake)
            {
                if (classification == FoodClassification.threat)
                    snake.GameOver(this, new EventArgs());
            }
            else if (level.Grid.GetCell(newLocation).content == CellContent.empty)
                food.Vector = newLocation;

            food.fillGrid(level);
        }

        //Returns an orientation facing the snake, giving the food a predatory movement
        private void attackOrientation(SnakeType snake, FoodType food)
        {
            switch (snake.Orientation)
            {
                case OrientationType.up:
                case OrientationType.down:
                    if (snake.Vector.X < food.Vector.X)
                        orientation = OrientationType.left;
                    else
                        orientation = OrientationType.right;
                    break;

                case OrientationType.left:
                case OrientationType.right:
                    if (snake.Vector.Y < food.Vector.Y)
                        orientation = OrientationType.up;
                    else
                        orientation = OrientationType.down;
                    break;
            }
        }

        //Returns an orientation that allows the AI to get in front of the snake
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