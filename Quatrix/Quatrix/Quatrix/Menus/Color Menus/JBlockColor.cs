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
using GameMenus;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quatrix
{
    class JBlockColor : ColorChooserType
    {
        public JBlock block;

        public JBlockColor(ContentManager content, string fileName)
            : base(content, "J-Block Color", "Block List", "Matura MT Script Capitals", "Highlight", 101, 30)
        {
            initialize();
            load(content, fileName);
        }

        public override void initialize()
        {
            block = new JBlock();

            block.Preview();

            color = JBlock.color;

            Color = Color.Red;
        }

        public override void load(ContentManager content, string fileName)
        {
            setTitle("J-Block", content.Load<SpriteFont>("Matura Title"), Color.Red, new Vector2(15, 20));

            SlideType temp = new SlideType("Red", Font, Color.Red, new Vector2(25, 100), new Vector2(), "J-Block Color", 0, 255, 20, 95);
            temp.setSlider(new SliderType(content.Load<Texture2D>("Slide 2"), new Vector2(130, 100),
                content.Load<Texture2D>("Standard Slider"), new Vector2(130, 103), Color.White));
            temp.Selected += SetRed;
            options.Add(temp);

            temp = new SlideType("Blue", Font, Color.Red, new Vector2(25, 140), new Vector2(), "J-Block Color", 0, 255, 20, 135);
            temp.setSlider(new SliderType(content.Load<Texture2D>("Slide 2"), new Vector2(130, 140),
                content.Load<Texture2D>("Standard Slider"), new Vector2(130, 143), Color.White));
            temp.Selected += SetBlue;
            options.Add(temp);

            temp = new SlideType("Green", Font, Color.Red, new Vector2(25, 180), new Vector2(), "J-Block Color", 0, 255, 20, 175);
            temp.setSlider(new SliderType(content.Load<Texture2D>("Slide 2"), new Vector2(130, 180),
                content.Load<Texture2D>("Standard Slider"), new Vector2(130, 183), Color.White));
            temp.Selected += SetGreen;
            options.Add(temp);

            OptionType option = new OptionType("Save", Font, Color.Red, new Vector2(25, 220), "Block List", 20, 215);
            option.Selected += SetColor;
            options.Add(option);

            base.load(content, fileName);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            block.Draw(spriteBatch, color);
        }

        public void SetColor(object sender, EventArgs e)
        {
            block.changeColor(color);
        }
    }
}
