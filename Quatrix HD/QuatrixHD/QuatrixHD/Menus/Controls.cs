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
using QuatrixHD.Quatrix;
using Reverb.Components.Titles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Transitions;
using Reverb.Components.Background;
using Reverb.Components.Graphics;
using Reverb.Components.Selections;
using Reverb.Components.Options;
using Reverb.Elements;
using Reverb.Arguments;
using Reverb.Enumerations;
using Reverb.Screens;
using Reverb.Elements.Buttons;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu to edit the control scheme of the game.
    /// </summary>
    class Controls : MenuType
    {
        ControlType controlType;

        public Controls()
            : base("Controls")
        {
            Queue = false;

            controlType = ControlType.motion;
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
            TitleComponent title = new TitleComponent("Fonts/MaturaTitle", "Controls", new Vector2(136, 50), Color.Red);

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

            selections.addSelection("Motion");
            selections.addSelection("Buttons");

            selections.addButton(new ButtonType("Menus/Arrow Button", "Menus/Arrow Pressed", new Vector2(60, 120)), 0);
            selections.addButton(new ButtonType("Menus/Arrow Button", "Menus/Arrow Pressed", new Vector2(194, 120), new Rectangle(194, 120, 20, 20), SpriteEffects.FlipHorizontally), 1);
            
            selections.Change += ChangeControls;

            selections.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(selections);

            GraphicSelectionComponent graphics = new GraphicSelectionComponent();
            graphics.addGraphic(new GraphicType("Menus/Control Scheme 1", new Vector2(66, 145)));
            graphics.addGraphic(new GraphicType("Menus/Control Scheme 2", new Vector2(66, 145)));

            graphics.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(graphics);

            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            OptionType option = new OptionType(new OptionType("Save", new Vector2(136, 400), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 400)));
            option.Selected += SaveControls;
            options.addOption(option);

            option = new OptionType(new OptionType("Cancel", new Vector2(136, 430), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 430)));
            options.addOption(option);

            options.setAlignment(TextAlignment.center);

            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            
            addComponent(options);
        }

        private void SaveControls(OptionArgs args)
        {
            GameManager.controlType = controlType;
        }

        private void ChangeControls(string controls)
        {
            switch (controls)
            {
                case "Motion":
                    controlType = ControlType.motion;
                    break;

                case "Buttons":
                    controlType = ControlType.buttons;
                    break;
            }

            GraphicSelectionComponent graphics = components["Graphic Selections"] as GraphicSelectionComponent;

            graphics.progress();
        }
    }
}
