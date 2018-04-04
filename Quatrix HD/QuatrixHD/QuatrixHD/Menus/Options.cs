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
using Reverb.Components.Graphics;
using Reverb.Transitions;
using Reverb.Components.Options;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu to access the various settings menus.
    /// </summary>
    class Options : MenuType
    {
        public Options()
            : base("Options")
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

            options.addOption(new OptionType("Sound", new Vector2(136, 120), Color.Red, OptionAction.next, "Sound", "Menus/Highlighter", new Vector2(137, 120)));
            options.addOption(new OptionType("Controls", new Vector2(136, 150), Color.Red, OptionAction.next, "Controls", "Menus/Highlighter", new Vector2(137, 150)));
            options.addOption(new OptionType("Brick Settings", new Vector2(136, 180), Color.Red, OptionAction.next, "Brick Settings", "Menus/Highlighter", new Vector2(137, 180)));
            options.addOption(new OptionType("Back", new Vector2(136, 420), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 420)));

            options.setAlignment(TextAlignment.center);

            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(null, new Vector2(200, 0), 15, true),
                                   new MoveCollection(new Vector2(-200, 0), 15, false), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(options);
        }
    }
}
