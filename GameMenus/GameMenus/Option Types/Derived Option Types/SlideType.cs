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
using ZHandler;

namespace GameMenus
{
    public class SlideType
    {
        StringType text;
        SpriteFont font;
        HighlightType highlighter;
        SliderType slider;
        Vector2 numberVector;
        OptionState state;
        string asset;
        bool drawNumber;
        int number;
        int minimum;
        int maximum;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public OptionState State
        {
            get { return state; }
            set { state = value; }
        }

        public SlideType(StringType slideText, HighlightType textHighlighter, SliderType sliderObject, string fontAsset, int max)
        {
            text = slideText;
            highlighter = textHighlighter;
            slider = sliderObject;

            asset = fontAsset;
            maximum = max;
            minimum = 0;

            drawNumber = false;
        }

        public void load()
        {
            font = AssetManager.getFont(asset);

            slider.load();

            highlighter.load();
        }

        public void update(InputHandlerComponent i)
        {
            if (state == OptionState.highlighted)
                movement(i);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (state == OptionState.highlighted)
                highlighter.draw(spriteBatch);

            text.draw(spriteBatch, font);

            if (drawNumber)
                spriteBatch.DrawString(font, number.ToString(), numberVector, text.color);

            if (slider != null)
                slider.draw(spriteBatch);
        }

        public void movement(InputHandlerComponent i)
        {
            if (i.getButton("Left", false) && number > minimum)
            {
                number--;

                if (slider != null)
                    slider.move(number, maximum);
            }

            if (i.getButton("Right", false) && number < maximum)
            {
                number++;
                
                if (slider != null)
                    slider.move(number, maximum);
            }
        }

        public void setSlider(SliderType optionSlider)
        {
            slider = optionSlider;
        }
    }
}
