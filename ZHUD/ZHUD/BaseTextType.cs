/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of ZHUD.
 * 
 * ZHUD is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * ZHUD is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with ZHUD.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ZHUD
{
    public class BaseTextType
    {
        protected string text;
        protected SpriteFont font;
        protected Vector2 vector;
        protected Color color;
        protected bool drawText;
        protected string asset;

        #region Properties
        public virtual string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public virtual SpriteFont Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
            }
        }

        public string Asset
        {
            get { return asset; }
            set { asset = value; }
        }

        public virtual Vector2 Vector
        {
            get
            {
                return vector;
            }
            set
            {
                vector = value;
            }
        }

        public virtual Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public virtual bool DrawText
        {
            get
            {
                return drawText;
            }
            set
            {
                drawText = value;
            }
        }
        #endregion

        public BaseTextType()
        {
        }

        public BaseTextType(string itemText, string fontAsset, Vector2 textVector, Color textColor)
        {
            text = itemText;
            asset = fontAsset;
            vector = textVector;
            color = textColor;
            drawText = true;
        }

        public virtual void load(ContentManager content)
        {
            font = content.Load<SpriteFont>(asset);
        }

        public virtual void update(GameTime time)
        {
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (drawText)
                spriteBatch.DrawString(font, text, vector, color);
        }

        public virtual void drawScale(SpriteBatch spriteBatch, Vector2 origin, float scale)
        {
            if (drawText)
                spriteBatch.DrawString(font, text, vector, color, 0.0f, origin, scale, SpriteEffects.None, 1.0f);
        }

        public virtual void changeText(string str)
        {
            text = str;
        }

        public virtual void addToText(string str)
        {
            text += str;
        }

        public virtual void addToNumber(int n)
        {
        }

        public virtual void changeNumber(int n)
        {
        }

        public virtual void startText(GameTime time)
        {
            if (!drawText)
                drawText = true;
        }

        public virtual Vector2 centerText(string text)
        {
            Vector2 vector = font.MeasureString(text) / 2;

            return vector;
        }
    }
}
