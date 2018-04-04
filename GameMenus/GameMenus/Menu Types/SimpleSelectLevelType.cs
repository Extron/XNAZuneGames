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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZHandler;

namespace GameMenus
{
    public abstract class SimpleSelectLevelType : MenuType
    {
        public EventHandler<EventArgs> Selected;

        protected SpriteFont font;
        protected string fontAsset;

        Vector2 textVector;
        MenuButtonType left;
        MenuButtonType right;
        int level;
        int levelMax;
        int counter;
        int counterMax;

        #region Properties
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        #endregion

        #region Constructors
        public SimpleSelectLevelType(string state)
            : base(state)
        {
            level = 1;
            levelMax = 1;
            counter = 0;
            counterMax = 10;
        }
        #endregion

        #region Class Functions
        public void setText(Vector2 vector)
        {
            textVector = vector;
        }

        public void setButtons(MenuButtonType lButton, MenuButtonType rButton)
        {
            left = lButton;
            right = rButton;
        }

        public void setButtons(MenuButtonType lButton, MenuButtonType rButton, string side)
        {
            left = lButton;
            right = rButton;

            switch (side)
            {
                case "left":
                    left.Flip = true;
                    break;

                case "right":
                    right.Flip = true;
                    break;

                default:
                    right.Flip = true;
                    break;
            }
        }

        public void setColor(Color color)
        {
            //base.Color = color;
        }

        public void setCounterMax(int x)
        {
            counterMax = x;
        }

        public void setLevelMax(int x)
        {
            levelMax = x;
        }
        #endregion

        #region Overridden Functions
        public override void load()
        {
            left.load();
            right.load();

            font = AssetManager.getFont(fontAsset);

            base.load();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            left.draw(spriteBatch);
            right.draw(spriteBatch);

            //spriteBatch.DrawString(font, level.ToString(), textVector, Color);
        }

        public override void update(InputHandlerComponent i)
        {
            base.update(i);

            left.update(ref counter, counterMax);
            right.update(ref counter, counterMax);

            select(i);

            counter++;

            if (counter > counterMax)
                counter = 0;
        }

        public /*override*/ void movement(InputHandlerComponent i)
        {
            if (i.getButton("Left", true) && !left.selected && !right.selected && level > 1)
            {
                level--;
                left.selected = true;
            }

            if (i.getButton("Right", true) && !left.selected && !right.selected && level < levelMax)
            {
                level++;
                right.selected = true;
            }
        }

        public void select(InputHandlerComponent i)
        {
            if (i.getButton("Select", true))
            {
                Selected(this, new EventArgs());
            }
        }
        #endregion
    }
}
