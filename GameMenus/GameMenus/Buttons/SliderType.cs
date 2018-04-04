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
    public class SliderType
    {
        //The slide is the background strip on which the slider moves
        Texture2D slide;
        Vector2 slideVector;
        string slideAsset;

        //The slider is the little texture that moves along the slide
        Texture2D slider;
        Vector2 sliderVector;
        string sliderAsset;

        Color color;

        public SliderType(string slideAssetName, Vector2 slideVec, string sliderAssetName, Vector2 sliderVec, Color sliderColor)
        {
            slideAsset = slideAssetName;
            slideVector = slideVec;

            sliderAsset = sliderAssetName;
            sliderVector = sliderVec;

            color = sliderColor;
        }

        public void load()
        {
            slide = AssetManager.getTexture(slideAsset);
            slider = AssetManager.getTexture(sliderAsset);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(slide, slideVector, color);

            spriteBatch.Draw(slider, sliderVector, color);
        }

        public void move(int number, int maximum)
        {
            int x = (slide.Width * number) / maximum;

            sliderVector.X = slideVector.X + x;
        }
    }
}
