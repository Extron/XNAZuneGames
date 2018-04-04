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
using Reverb.Components.Background;
using Reverb.Elements;
using Reverb.Enumerations;
using Reverb.Components.InGame;
using Reverb.Transitions;
using Reverb.Components.Options;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a pause menu.
    /// </summary>
    class Pause : MenuType
    {
        public Pause()
            : base("Paused")
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
            TitleComponent title = new TitleComponent("Fonts/MaturaInGame", "Paused", new Vector2(136, 110), Color.Red);

            title.setAlignment(TextAlignment.center);

            title.setTransitions(new MoveComponent(null, new Vector2(0, 340), true, 20), new MoveComponent(null, new Vector2(200, 0), true, 15),
                                 new MoveComponent(null, new Vector2(-200, 0), false, 15), new MoveComponent(null, new Vector2(0, -340), false, 20));

            addComponent(title);
        }

        private void setBackground()
        {
            InGameComponent inGameBackground = new InGameComponent();
            inGameBackground.setTransitions(new DimComponent(new Vector3(100), 20, -1), new FadeComponent(15, 1),
                                            new FadeComponent(15, -1), new DimComponent(new Vector3(100), 20, 1));

            addComponent(inGameBackground);


            BackgroundComponent background = new BackgroundComponent("Menus/Pause Menu");

            background.Position = new Vector2(52, 100);

            background.setTransitions(new MoveComponent(null, new Vector2(0, 340), true, 20), new MoveComponent(null, new Vector2(200, 0), true, 15),
                                      new MoveComponent(null, new Vector2(-200, 0), false, 15), new MoveComponent(null, new Vector2(0, -340), false, 20)); 

            addComponent(background);

        }

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            options.addOption(new OptionType("Return", new Vector2(136, 160), Color.Red, OptionAction.previous, false, true, "Menus/Highlighter", new Vector2(136, 160)));
            options.addOption(new OptionType("Media Player", new Vector2(136, 190), Color.Red, OptionAction.next, "Media Player Menu", "Menus/Highlighter", new Vector2(136, 190)));
            options.addOption(new OptionType("Options", new Vector2(136, 220), Color.Red, OptionAction.next, "Options", "Menus/Highlighter", new Vector2(136, 220)));
            options.addOption(new OptionType("Quit", new Vector2(136, 250), Color.Red, OptionAction.endGame, true, true, "Menus/Highlighter", new Vector2(136, 250)));

            options.setAlignment(TextAlignment.center);

            options.setTransitions(new MoveCollection(new Vector2(0, 340), 20, true), new MoveCollection(new Vector2(200, 0), 15, true),
                                   new MoveCollection(new Vector2(-200, 0), 15, false), new MoveCollection(new Vector2(0, -340), 20, false));

            addComponent(options);
        }
    }
}
