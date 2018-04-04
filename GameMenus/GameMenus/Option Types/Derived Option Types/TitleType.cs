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

namespace GameMenus
{
    public class TitleType
    {
        StringType title;
        SpriteFont font;
        TextAlignment alignment;
        string fontAsset;

        #region Properties
        public string Text
        {
            get { return title.text; }
            set { title.text = value; }
        }

        public Vector2 Vector
        {
            get { return title.vector; }
            set { title.vector = value; }
        }

        public Color TextColor
        {
            get { return title.color; }
            set { title.color = value; }
        }

        public TextAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }
        #endregion

        #region Constructors
        public TitleType(string text, string fontName, Color textColor, Vector2 stringVector)
        {
            title = new StringType(text, stringVector, textColor);

            fontAsset = fontName;

            alignment = TextAlignment.right;
        }

        public TitleType(string text, string fontName, Color textColor, Vector2 stringVector, TextAlignment textAlignment)
        {
            title = new StringType(text, stringVector, textColor);

            fontAsset = fontName;

            alignment = textAlignment;
        }
        #endregion

        #region Class Functions
        public void load()
        {
            font = AssetManager.getFont(fontAsset);

            switch (alignment)
            {
                case TextAlignment.right:
                    right();
                    break;

                case TextAlignment.center:
                    center();
                    break;

                case TextAlignment.left:
                    left();
                    break;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            title.draw(spriteBatch, font);
        }

        private void right()
        {
            title.origin = new Vector2();
        }

        private void center()
        {
            Vector2 temp = font.MeasureString(title.text);
            title.origin.X = temp.X / 2;
        }

        private void left()
        {
            Vector2 temp = font.MeasureString(title.text);
            title.origin.X = temp.X;
        }
        #endregion
    }
}
