/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of GameMenus.
 * 
 * GameMenus is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * GameMenus is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GameMenus.  If not, see <http://www.gnu.org/licenses/>.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using ZHandler;

namespace GameMenus
{
    public abstract class SideToSideType : MenuType
    {
        public EventHandler<DirectionArgs> Scroll;

        string leftState;
        string rightState;
        StaticArrowType leftArrow;
        StaticArrowType rightArrow;

        public string Left
        {
            get { return leftState; }
            set { leftState = value; }
        }

        public string Right
        {
            get { return rightState; }
            set { rightState = value; }
        }

        public SideToSideType(string state) 
            : base(state)
        {
            leftState = "None";
            rightState = "None";
        }

        #region Class Methods
        public void setSubStates(string lState, string rState)
        {
            leftState = lState;
            rightState = rState;
        }

        public void setArrows(Vector2 lVec, Vector2 rVec, string lName, string rName)
        {
            leftArrow = new StaticArrowType(lVec, lName);
            rightArrow = new StaticArrowType(rVec, rName);
        }

        public void clearArrow(Direction direction)
        {
            if (direction == Direction.right)
                rightArrow = null;
            else if (direction == Direction.left)
                leftArrow = null;
        }

        public void scroll(InputHandlerComponent i)
        {
            if (i.getButton("Left", true) && leftState != "None")
            {
                if (leftState != "None")
                    Scroll(this, new DirectionArgs(Direction.left));
            }
            else if (i.getButton("Right", true) && rightState != "None")
            {
                if (rightState != "None")
                    Scroll(this, new DirectionArgs(Direction.right));
            }
        }
        #endregion

        #region Overrided Methods
        public override void load()
        {
            if (leftArrow != null)
                leftArrow.load();

            if (rightArrow != null)
                rightArrow.load();

            base.load();
        }

        public override void update(InputHandlerComponent i)
        {
            scroll(i);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            if (leftArrow != null)
                leftArrow.draw(spriteBatch);

            if (rightArrow != null)
                rightArrow.draw(spriteBatch);
        }
        #endregion
    }
}
