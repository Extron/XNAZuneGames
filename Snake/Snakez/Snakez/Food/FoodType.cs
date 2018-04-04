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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ZSounds;
using Snakez.Levels;
using Snakez.AI;

namespace Snakez
{
    /// <summary>
    /// An enum which defines a food item's abilities and intelligence
    /// </summary>
    enum FoodClassification
    {
        standard,
        quick,
        smart,
        threat
    }

    /// <summary>
    /// A class which all food objects are derived from.  This class contains functions to update, move, and draw the
    /// food item, as well as all variables to manipulate the item.
    /// </summary>
    class FoodType
    {
        #region Variables
        /// <summary>
        /// An integer that represents the amount granted to the user's score upon eating the food.
        /// </summary>
        protected int score;

        /// <summary>
        /// An integer that represents the amount of segments added to the snake upon eating the food.
        /// </summary>
        protected int growth;

        /// <summary>
        /// The AI agent that drives the food, deducing movement and threat level.
        /// </summary>
        IAgent ai;

        /// <summary>
        /// The food texture used to draw the food.
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// The location of the food within the level's grid.
        /// </summary>
        Vector2 vector;

        /// <summary>
        /// The rotation of the sprite, dependant on the food's orientation
        /// </summary>
        float rotation;
        #endregion

        #region Properties
        public Texture2D Texutre
        {
            get { return texture; }
        }

        public Vector2 Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        public OrientationType Orientation
        {
            get { return ai.Orientation; }
            set { ai.Orientation = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public int Score
        {
            get { return score; }
        }

        public int Growth
        {
            get { return growth; }
        }
        #endregion

        #region Constructors
        public FoodType(FoodClassification classification)
        {
            if (classification == FoodClassification.threat)
                ai = new HarmfulAI(classification);
            else
                ai = new EdibleAI(classification);

            rotation = 0;
        }
        #endregion

        #region Game Functions
        /// <summary>
        /// This function gets a random location for the food item, and determines if the food item can actually 
        /// spawn there.  If not, it continues to get random locations until a suitable one is found.  
        /// </summary>
        /// <param name="grid">The level's grid, which contains all objects within the level.</param>
        public void spawn(GameGrid grid)
        {
            int x = 0, y = 0;

            do
            {
                x = RandomGenerator.randomNumber(0, 24);
                y = RandomGenerator.randomNumber(0, 32);
            }
            while (grid.GetCell(x, y).content != CellContent.empty);

            vector = new Vector2(x, y);
        }

        /// <summary>
        /// Initializes the food item's AI agent and the rotation of the food item.
        /// </summary>
        public void initialize()
        {
            ai.initialize();

            rotation = MovementFunctions.calculateRotation(ai.Orientation);
        }

        /// <summary>
        /// Loads the graphical texture of the food item.
        /// </summary>
        /// <param name="content">The game's content manager</param>
        /// <param name="fileName">The name of the texutre file within the Content folder.</param>
        public void load(ContentManager content, string fileName)
        {
            texture = content.Load<Texture2D>(fileName);
        }

        /// <summary>
        /// Updates the food item's AI agent, which moves the food.  
        /// </summary>
        /// <param name="snake">The snake, which is controlled by the user.</param>
        /// <param name="level">The current level.</param>
        public void update(SnakeType snake, LevelType level)
        {
            ai.move(snake, this, level);
        }

        /// <summary>
        /// Acts as the food item's draw method, but instead of drawing straight to the screen, it fills the level's grid
        /// with its texture, which is drawn when the grid is drawn.
        /// </summary>
        /// <param name="grid">The level's grid.</param>
        public void fillGrid(LevelType level)
        {
            rotation = MovementFunctions.calculateRotation(ai.Orientation);

            PortalType portal;

            if (level.tryGetPortal(vector, out portal))
            {
                portal.fillGrid(level);
            }

            fill(level.Grid);
        }

        public void fill(GameGrid grid)
        {
            grid.GetCell(vector).fillCell(CellContent.food, texture, rotation, Color.White);
        }
        #endregion
    }
}
