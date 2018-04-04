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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class ColorChooserType : MenuType
    {
        Texture2D preview;
        Vector2 previewVector;
        protected Color color;

        public ColorChooserType(string current, string preview)
            : base(current)
        {
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            if (preview != null)
                spriteBatch.Draw(preview, previewVector, color);
        }

        public void setPreview(Texture2D texture, Vector2 vector)
        {
            preview = texture;

            previewVector = vector;
        }

        public void SetRed(object sender, EventArgs e)
        {
            SlideType option = sender as SlideType;

            if (option != null)
            {
                color.R = (byte)option.Number;
            }
        }

        public void SetBlue(object sender, EventArgs e)
        {
            SlideType option = sender as SlideType;

            if (option != null)
            {
                color.B = (byte)option.Number;
            }
        }

        public void SetGreen(object sender, EventArgs e)
        {
            SlideType option = sender as SlideType;

            if (option != null)
            {
                color.G = (byte)option.Number;
            }
        }

        public void setSliders(SliderType slider)
        {
            /*
            for (int i = 0; i < options.Count; i++)
            {
                SlideType temp = options[i] as SlideType;

                if (temp != null)
                {
                    temp.setSlider(slider);

                    options[i] = temp;
                }
             
            }
            */
        }
    }
}
