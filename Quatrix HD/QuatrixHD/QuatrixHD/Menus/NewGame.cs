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
using Reverb.Components.Options;
using Reverb.Elements;
using Reverb.Transitions;
using Reverb.Enumerations;
using QuatrixHD.Quatrix;
using QuatrixHD.GameTypes;
using Reverb.Arguments;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu to start a new game from a set of game types.
    /// </summary>
    class NewGame : MenuType
    {
        public NewGame()
            : base("New Game")
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
            TitleComponent title = new TitleComponent("Fonts/MaturaTitle", "Options", new Vector2(136, 50), Color.Red);

            title.setAlignment(TextAlignment.center);

            title.setTransitions(new MoveComponent(null, new Vector2(-200, 0), true, 15), new MoveComponent(null, new Vector2(200, 0), true, 15),
                                 new MoveComponent(null, new Vector2(-200, 0), false, 15), new MoveComponent(null, new Vector2(200, 0), false, 15));

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
            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            OptionType option = new OptionType("Infinity", new Vector2(136, 120), Color.Red, OptionAction.startGame, false, true, "Menus/Highlighter", new Vector2(137, 120));
            option.Selected += Infinity;
            options.addOption(option);

            option = new OptionType("Sprint", new Vector2(136, 150), Color.Red, OptionAction.startGame, false, true, "Menus/Highlighter", new Vector2(137, 150));
            option.Selected += Sprint;
            options.addOption(option);

            option = new OptionType("Race", new Vector2(136, 180), Color.Red, OptionAction.startGame, false, true, "Menus/Highlighter", new Vector2(137, 180));
            option.Selected += Race;
            options.addOption(option);

            options.addOption(new OptionType("Back", new Vector2(136, 420), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 420)));

            options.setAlignment(TextAlignment.center);

            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(null, new Vector2(200, 0), 15, true),
                                   new MoveCollection(new Vector2(-200, 0), 15, false), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(options);
        }

        private void Infinity(OptionArgs args)
        {
            GameManager.gameType = new InfinityData();
        }

        private void Sprint(OptionArgs args)
        {
            GameManager.gameType = new SprintData();
        }

        private void Race(OptionArgs args)
        {
            GameManager.gameType = new RaceData();
        }
    }
}
