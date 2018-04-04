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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameMenus
{
    public class MenuButtonType
    {
        public Rectangle rect;
        public Texture2D text;
        public Texture2D pressed;
        public bool selected;

        string textureAsset;
        string pressedAsset;
        float rotation;

        public bool Flip
        {
            set
            {
                if (value)
                    rotation = (float)Math.PI;
                else
                    rotation = 0;
            }
        }

        public MenuButtonType(Rectangle target, string textName, string pressedName)
        {
            rect = target;
            textureAsset = textName;
            pressedAsset = pressedName;
            selected = false;
            rotation = 0;
        }

        public void load()
        {
            text = AssetManager.getTexture(textureAsset);
            pressed = AssetManager.getTexture(pressedAsset);
        }

        public void draw(SpriteBatch spriteBatch)
        {
                if (selected)
                    spriteBatch.Draw(pressed, rect, null, Color.White, rotation, new Vector2(0), SpriteEffects.None, 0.0f);
                else
                    spriteBatch.Draw(text, rect, null, Color.White, rotation, new Vector2(0), SpriteEffects.None, 0.0f);
        }

        public void update(ref int counter, int counterMax)
        {
            if (selected && counter == counterMax)
            {
                selected = false;
                counter = 0;
            }
        }
    }
}
