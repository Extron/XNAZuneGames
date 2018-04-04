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
using Microsoft.Xna.Framework.Content;


namespace Snakez
{
    //The main food class, which directly interacts with the main game class
    class Food
    {
        public List<FoodType> food;

        int counter;

        public Food()
        {
            food = new List<FoodType>();
        }

        /// <summary>
        /// Creates the food data with defined numbers of food and initializes each.
        /// </summary>
        /// <param name="data">A structure defining the numbers of food to create.</param>
        public void initialize(FoodData data)
        {
            setFood(data);

            foreach (FoodType item in food)
                item.initialize();
        }

        /// <summary>
        /// Loads all graphical content for the food.
        /// </summary>
        /// <param name="content">The game content manager.</param>
        public void load(ContentManager content)
        {
            foreach (FoodType item in food)
            {
                if (item is MouseType)
                    item.load(content, "Food/Mouse 2");
                else if (item is GroundHogType)
                    item.load(content, "Food/Ground Hog 1");
                else if (item is RabbitType)
                    item.load(content, "Food/Rabbit 1");
                else if (item is ScorpionType)
                    item.load(content, "Food/Scorpion 1");
            }
        }

        /// <summary>
        /// Spawns all food items, first checking for collision with other level objects, then filling the grid.
        /// </summary>
        /// <param name="grid">The level's current grid.</param>
        public void spawn(GameGrid grid)
        {
            foreach (FoodType item in food)
            {
                item.spawn(grid);
            }
        }

        public void update(SnakeType snake, LevelType level)
        {
            if (counter == food.Count)
                counter = 0;

            food[counter].update(snake, level);

            if (food.Count > 7)
            {
                if (counter + 7 >= food.Count)
                    food[(counter + 7) - food.Count].update(snake, level);
                else
                    food[counter + 7].update(snake, level);
            }

            counter++;
        }

        public void fillGrid(LevelType level)
        {
            foreach (FoodType item in food)
                item.fillGrid(level);
        }

        public FoodArgs getArgs()
        {
            FoodArgs args = new FoodArgs();

            args.addFood(food);

            return args;
        }

        private void setFood(FoodData data)
        {
            food.Clear();

            int start = 0;

            for (int i = 0; i < data.numberOfMice; i++)
            {
                if (start > 8)
                    start = 0;

                food.Add(new MouseType());

                start++;
            }

            for (int i = 0; i < data.numberOfHogs; i++)
            {
                if (start > 8)
                    start = 0;

                food.Add(new GroundHogType());
                
                start++;
            }

            for (int i = 0; i < data.numberOfRabbits; i++)
            {
                if (start > 8)
                    start = 0;

                food.Add(new RabbitType());

                start++;
            }

            for (int i = 0; i < data.numberOfScorpions; i++)
            {
                if (start > 8)
                    start = 0;

                food.Add(new ScorpionType());

                start++;
            }
        }
    }
}
