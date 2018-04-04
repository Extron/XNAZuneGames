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

namespace GameMenus
{
    public class HighlightType
    {
        public Rectangle rect;
        public Texture2D text;
        public Color color;
        public Vector2 origin;
        public string asset;

        #region Properties
        public Vector2 Vector
        {
            get { return new Vector2(rect.X, rect.Y); }
            set
            {
                rect.X = (int)value.X;

                rect.Y = (int)value.Y;
            }
        }

        public bool Centered
        {
            set
            {
                if (value)
                    center();
                else
                    origin = new Vector2(0);
            }

        }
        #endregion

        #region Constructors
        public HighlightType(Rectangle rect, Texture2D text, Color color)
        {
            this.rect = rect;
            this.text = text;
            this.color = color;
            origin = new Vector2(0);
        }

        public HighlightType(Vector2 vector, string textureAsset, Color tint)
        {
            rect = new Rectangle((int)vector.X, (int)vector.Y, 0, 0);
            asset = textureAsset;
            color = tint;
        }

        public HighlightType(Texture2D texture)
        {
            text = texture;
            rect = new Rectangle(0, 0, text.Width, text.Height);
            color = Color.White;
            origin = new Vector2(0);
        }

        public HighlightType(Vector2 vector)
        {
            rect = new Rectangle((int)vector.X, (int)vector.Y, 0, 0);
            color = Color.White;
        }

        public HighlightType(Rectangle rectangle)
        {
            rect = rectangle;
            color = Color.White;
        }
        #endregion

        #region Virtual Functions
        public virtual void load(ContentManager content, string fileName)
        {
            text = content.Load<Texture2D>(fileName);

            if (rect.Width == 0)
                rect.Width = text.Width;

            if (rect.Height == 0)
                rect.Height = text.Height;
        }

        public virtual void load()
        {
            text = AssetManager.getTexture(asset);

            if (rect.Width == 0)
                rect.Width = text.Width;

            if (rect.Height == 0)
                rect.Height = text.Height;
        }

        public virtual void update()
        {
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, rect, null, color, 0f, origin, SpriteEffects.None, 0.0f);
        }        
        #endregion

        #region Class Functions
        public void setSize(int x, int y)
        {
            rect.Width = x;
            rect.Height = y;
        }

        public void setTexture(Texture2D texture)
        {
            text = texture;
        }

        public void right()
        {
            origin = new Vector2();
        }

        public void center()
        {
            origin.X = text.Width / 2;
        }

        public void left()
        {
            origin.X = text.Width;
        }
        #endregion
    }
}
