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
using Reverb.Components.Titles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Transitions;
using Reverb.Components.Background;
using Reverb.Elements;
using Reverb.Enumerations;
using Reverb.Components.Options;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates the main title screen of the game.
    /// </summary>
    class TitleScreen : MenuType
    {
        public TitleScreen()
            : base("Title Screen")
        {
            Queue = false;
        }

        public override void initialize()
        {
            setBackground();

            setTitle();

            setOptions();

            base.initialize();
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("Fonts/MaturaTitle", "Quatrix", new Vector2(136, 130), Color.Red);

            title.setAlignment(TextAlignment.center);

            title.setTransitions(new MoveComponent(null, new Vector2(-200, 0), true, 15), new MoveComponent(null, new Vector2(200, 0), true, 15), 
                                 new MoveComponent(null, new Vector2(-200, 0), false, 15), new MoveComponent(null, new Vector2(200, 0), true, 15));

            addComponent(title);
        }

        private void setBackground()
        {
            BackgroundComponent background = new BackgroundComponent("Menus/Main Menu");

            background.setTransitions(new FadeComponent(15, 1), new FadeComponent(15, -1));

            addComponent(background);
        }

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            options.addOption(new OptionType("New Game", new Vector2(136, 250), Color.Red, OptionAction.next, "New Game", "Menus/Highlighter", new Vector2(136, 250)));
            options.addOption(new OptionType("Media Player", new Vector2(136, 280), Color.Red, OptionAction.next, "Media Player Menu", "Menus/Highlighter", new Vector2(136, 280)));
            options.addOption(new OptionType("High Scores", new Vector2(136, 310), Color.Red, OptionAction.next, "High Scores", "Menus/Highlighter", new Vector2(136, 310)));
            options.addOption(new OptionType("Options", new Vector2(136, 340), Color.Red, OptionAction.next, "Options", "Menus/Highlighter", new Vector2(136, 340)));
            options.addOption(new OptionType("Exit", new Vector2(136, 370), Color.Red, OptionAction.exit, "Quit", "Menus/Highlighter", new Vector2(136, 370)));

            options.setAlignment(TextAlignment.center);

            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, true),
                                   new MoveCollection(new Vector2(-200, 0), 15, false), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(options);
        }
    }
}
