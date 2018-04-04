#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of QuatrixHD.
 * 
 * QuatrixHD is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * QuatrixHD is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with QuatrixHD.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Components.Titles;
using Reverb.Components.Background;
using Reverb.Components.Graphics;
using Reverb.Elements;
using Reverb.Components.Selections;
using Reverb.Components.Slides;
using QuatrixHD.Blocks;
using Reverb.Components.Text;
using Reverb.Transitions;
using Reverb.Components;
using Reverb.Components.Options;
using Reverb.Enumerations;
using Reverb.Arguments;
using QuatrixHD.Storage;
using Reverb.Screens;
using Reverb.Elements.Buttons;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu to edit the colors of the blocks.
    /// </summary>
    class BrickColors : MenuType
    {
        BlockType currentBlock = new OBlock();

        public BrickColors()
            : base("Brick Colors")
        {
            Queue = false;
        }

        public override void initialize()
        {
            setBackground();

            setFrame();

            setTitle();

            setOptions();

            base.initialize();
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("Fonts/MaturaTitle", "Colors", new Vector2(136, 50), Color.Red);

            title.setAlignment(TextAlignment.center);

            title.setTransitions(new MoveComponent(null, new Vector2(-200, 0), true, 15), new MoveComponent(null, new Vector2(200, 0), false, 15));

            addComponent(title);
        }

        private void setBackground()
        {
            BackgroundComponent background = new BackgroundComponent("Menus/Standard Menu");

            background.setTransitions(new FadeComponent(15, 1), new FadeComponent(15, -1));

            addComponent(background);
        }

        private void setFrame()
        {
            GraphicComponent frame = new GraphicComponent("Menus/Option Frame", new Rectangle(39, 105, 196, 360));

            frame.setTransitions(new FadeComponent(15, 1), new FadeComponent(15, -1));

            addComponent(frame);
        }

        private void setOptions()
        {
            SelectionComponent selections = new SelectionComponent("Fonts/MaturaOptions", new Vector2(95, 120), Color.Red);

            selections.addSelection("O Block");
            selections.addSelection("T Block");
            selections.addSelection("I Block");
            selections.addSelection("J Block");
            selections.addSelection("L Block");
            selections.addSelection("S Block");
            selections.addSelection("Z Block");

            selections.addButton(new ButtonType("Menus/Arrow Button", "Menus/Arrow Pressed", new Vector2(60, 120)), 0);
            selections.addButton(new ButtonType("Menus/Arrow Button", "Menus/Arrow Pressed", new Vector2(194, 120), new Rectangle(194, 120, 20, 20), SpriteEffects.FlipHorizontally), 1);
            
            selections.Change += ChangeBlock;

            selections.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(selections);

            TextListComponent text = new TextListComponent("Fonts/MaturaOptions");
            text.addText("Red:", new Vector2(50, 155), Color.Red);
            text.addText("Green:", new Vector2(50, 215), Color.Red);
            text.addText("Blue:", new Vector2(50, 275), Color.Red);

            text.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            addComponent(text);

            NumberSlideComponent slide = new NumberSlideComponent("Menus/Meter", "Menus/Knob", new Vector2(50, 180), new Rectangle(59, 180, 157, 25));
            slide.SetValue += SetRed;
            slide.ChangeValue += ChangeRed;

            slide.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(slide);

            slide = new NumberSlideComponent("Menus/Meter", "Menus/Knob", new Vector2(50, 240), new Rectangle(59, 240, 157, 25));
            slide.SetValue += SetGreen;
            slide.ChangeValue += ChangeGreen;

            slide.Identifier = "Number Slide 2";

            slide.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(slide);

            slide = new NumberSlideComponent("Menus/Meter", "Menus/Knob", new Vector2(50, 300), new Rectangle(59, 300, 157, 25));
            slide.SetValue += SetBlue;
            slide.ChangeValue += ChangeBlue;

            slide.Identifier = "Number Slide 3";

            slide.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(slide);

            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            OptionType option = new OptionType(new OptionType("Save", new Vector2(136, 390), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 390)));
            option.Selected += SaveColors;
            options.addOption(option);

            option = new OptionType(new OptionType("Cancel", new Vector2(136, 420), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 420)));
            option.Selected += DiscardColors;
            options.addOption(option);

            options.setAlignment(TextAlignment.center);

            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            
            addComponent(options);
        }

        private void SaveColors(OptionArgs args)
        {
            ColorData.saveColors();
        }

        private void DiscardColors(OptionArgs args)
        {
            OBlock.resetColor();
            IBlock.resetColor();
            TBlock.resetColor();
            JBlock.resetColor();
            LBlock.resetColor();
            SBlock.resetColor();
            ZBlock.resetColor();

            ColorData.saveColors();
        }

        private void ChangeBlock(string block)
        {
            switch (block)
            {
                case "O Block":
                    currentBlock = new OBlock();
                    break;

                case "T Block":
                    currentBlock = new TBlock();
                    break;

                case "I Block":
                    currentBlock = new IBlock();
                    break;

                case "J Block":
                    currentBlock = new JBlock();
                    break;

                case "L Block":
                    currentBlock = new LBlock();
                    break;

                case "S Block":
                    currentBlock = new SBlock();
                    break;

                case "Z Block":
                    currentBlock = new ZBlock();
                    break;
            }

            ComponentType slide;

            if (components.TryGetValue("Number Slide", out slide))
                slide.reset();

            if (components.TryGetValue("Number Slide 2", out slide))
                slide.reset();

            if (components.TryGetValue("Number Slide 3", out slide))
                slide.reset();
        }

        private void SetRed(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            slide.Value = currentBlock.getColor().R / (float)255;
        }

        private void ChangeRed(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            Color newColor = currentBlock.getColor();
            
            newColor.R = (byte)(255 * slide.Value);

            currentBlock.changeColor(newColor);
        }

        private void SetGreen(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            slide.Value = currentBlock.getColor().G / (float)255;
        }

        private void ChangeGreen(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            Color newColor = currentBlock.getColor();

            newColor.G = (byte)(255 * slide.Value);

            currentBlock.changeColor(newColor);
        }

        private void SetBlue(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            slide.Value = currentBlock.getColor().B / (float)255;
        }

        private void ChangeBlue(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            Color newColor = currentBlock.getColor();

            newColor.B = (byte)(255 * slide.Value);

            currentBlock.changeColor(newColor);
        }
    }
}
