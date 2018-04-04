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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Quatrix
{
    abstract class GameBlockNew
    {     
        public RotationState rState = RotationState.first;
        public static Texture2D texture;

        #region Properties
        public Brick Brick1 { get; private set; }
        public Brick Brick2 { get; private set; }
        public Brick Brick3 { get; private set; }
        public Brick Brick4 { get; private set; }
        #endregion

        #region Enumerations
        public enum RotationState 
        {
            first,
            second,
            third,
            fourth
        }
        #endregion

        #region Constructors
        public GameBlockNew()
        {
            Brick1 = new Brick { Rect = new Rectangle(320, 240, 16, 16), IsVisible = true, IsCounted = false };
            Brick1.Coordinate = 0;

            Brick2 = new Brick { Rect = new Rectangle(320, 240, 16, 16), IsVisible = true, IsCounted = false };
            Brick2.Coordinate = 0;

            Brick3 = new Brick { Rect = new Rectangle(320, 240, 16, 16), IsVisible = true, IsCounted = false };
            Brick3.Coordinate = 0;

            Brick4 = new Brick { Rect = new Rectangle(320, 240, 16, 16), IsVisible = true, IsCounted = false };
            Brick4.Coordinate = 0;

            setTexture(texture);
        }
        #endregion

        #region Abstract and Virtual Functions
        public virtual void load(ContentManager content)
        {
            Brick1.Text = texture;
            Brick2.Text = texture;
            Brick3.Text = texture;
            Brick4.Text = texture;
        }

        public abstract void Preview();

        public abstract void Normal();

        public abstract void Rotate();

        protected abstract GameBlockNew Create();
        #endregion

        #region Class Functions
        public void Move(int movementNumber, GameGrid grid)
        {
            if (MovementFunctions.LeftBounderyCollision(this) && movementNumber < 0 && MovementFunctions.LeftSideCollision(this, grid))
            {
                Brick1.Rect.X -= 16;
                Brick2.Rect.X -= 16;
                Brick3.Rect.X -= 16;
                Brick4.Rect.X -= 16;
            }

            if (MovementFunctions.RightBounderyCollision(this) && movementNumber > 0 && MovementFunctions.RightSideCollision(this, grid))
            {
                Brick1.Rect.X += 16;
                Brick2.Rect.X += 16;
                Brick3.Rect.X += 16;
                Brick4.Rect.X += 16;
            }
        }

        public void Drop(GameGrid grid)
        {
            if (Brick4.Rect.Y != 298 && MovementFunctions.BlockDropCollision(this, grid))
            {
                Brick1.Rect.Y += 16;
                Brick2.Rect.Y += 16;
                Brick3.Rect.Y += 16;
                Brick4.Rect.Y += 16;
            }
        }

        public void CountRow(int row, ref int rowCounter)
        {
            Brick1.countRow(row, ref rowCounter);
            Brick2.countRow(row, ref rowCounter);
            Brick3.countRow(row, ref rowCounter);
            Brick4.countRow(row, ref rowCounter);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Brick1.drawBrick(spriteBatch);
            Brick2.drawBrick(spriteBatch);
            Brick3.drawBrick(spriteBatch);
            Brick4.drawBrick(spriteBatch);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Brick1.drawBrick(spriteBatch, color);
            Brick2.drawBrick(spriteBatch, color);
            Brick3.drawBrick(spriteBatch, color);
            Brick4.drawBrick(spriteBatch, color);
        }

        public void FillGridCells(GameGrid grid)
        {
            int i, j;
            MovementFunctions.CalculateCoordinates(Brick1.Coordinate, out i, out j); 
            Brick1.fillGridCell(grid.GetCell(i, j));

            MovementFunctions.CalculateCoordinates(Brick2.Coordinate, out i, out j);
            Brick2.fillGridCell(grid.GetCell(i, j));

            MovementFunctions.CalculateCoordinates(Brick3.Coordinate, out i, out j);
            Brick3.fillGridCell(grid.GetCell(i, j));

            MovementFunctions.CalculateCoordinates(Brick4.Coordinate, out i, out j);
            Brick4.fillGridCell(grid.GetCell(i, j));
        }

        public void SetBlockCoordinates(GameGridCell cell)
        {
            Brick1.setBrickCoordinates(cell);
            Brick2.setBrickCoordinates(cell);
            Brick3.setBrickCoordinates(cell);
            Brick4.setBrickCoordinates(cell);
        }

        protected void LoadContentByName(ContentManager content, string contentName)
        {
            Brick1.Text = content.Load<Texture2D>(contentName);
            Brick2.Text = content.Load<Texture2D>(contentName);
            Brick3.Text = content.Load<Texture2D>(contentName);
            Brick4.Text = content.Load<Texture2D>(contentName);
        }

        public void setColor(Color c)
        {
            Brick1.color = c;
            Brick2.color = c;
            Brick3.color = c;
            Brick4.color = c;
        }

        public void setTexture(Texture2D tex)
        {
            texture = tex;

            Brick1.Text = texture;
            Brick2.Text = texture;
            Brick3.Text = texture;
            Brick4.Text = texture;
        }

        public GameBlockNew Copy()
        {
            GameBlockNew otherBlock = this.Create();
            otherBlock.rState = this.rState;

            otherBlock.Brick1 = this.Brick1.copy();
            otherBlock.Brick2 = this.Brick2.copy();
            otherBlock.Brick3 = this.Brick3.copy();
            otherBlock.Brick4 = this.Brick4.copy();

            return otherBlock;
        }
        #endregion
    }
}
