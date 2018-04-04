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
using Reverb.Transitions;
using Microsoft.Xna.Framework;
using Reverb.Components.Background;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Components.Graphics;
using Reverb.Components.Scrolling;
using Reverb.Elements;
using Reverb.Enumerations;
using Shockwave;
using Reverb.Arguments;
using Reverb.Components.Options;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu to display the current playlist of the media player.
    /// </summary>
    class MediaPlaylist : MenuType
    {
        public MediaPlaylist()
            : base("Playlist")
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

        public override void dynamicInitialize()
        {
            ScrollOptionsComponent list = components["Scroll Options"] as ScrollOptionsComponent;

            if (MediaComponent.Playlist.Count > 0)
            {
                if (MediaComponent.Playlist.Count > list.Count)
                {
                    for (int i = list.Count; i < MediaComponent.Playlist.Count; i++)
                    {
                        list.addOption(new OptionType("", new Vector2(30, 100 + 30 * i), Color.Red, OptionAction.nothing, false, false, "Menus/Highlighter", new Vector2(30, 100 + 30 * i)));
                    }
                }
                else if (MediaComponent.Playlist.Count < list.Count)
                {
                    list.clearOptions(MediaComponent.Playlist.Count - 1);
                }

                list.Texts = MediaComponent.Playlist;
                list.setEvents(RemoveSong);
            }
            else
            {
                list.clearOptions(0, true);
                list.addOption(new OptionType("<No Songs Added>", new Vector2(30, 100), Color.Red, OptionAction.nothing, false, false, "Menus/Highlighter", new Vector2(30, 100)));
            }

            list.initializeTransitions();

            base.dynamicInitialize();
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("Fonts/MaturaTitle", "Playlist", new Vector2(136, 50), Color.Red);

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

        private void setOptions()
        {
            ScrollOptionsComponent list = new ScrollOptionsComponent("Fonts/MaturaOptions", new Rectangle(30, 100, 212, 320));

            list.addOption(new OptionType("<No Songs Added>", new Vector2(30, 100), Color.Red, OptionAction.nothing, false, false, "Menus/Highlighter", new Vector2(30, 100)));

            list.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(list);

            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            options.addOption(new OptionType("Back", new Vector2(136, 430), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 430)));

            options.setAlignment(TextAlignment.center);
            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            addComponent(options);
        }

        private void RemoveSong(OptionArgs args)
        {
            MediaComponent.removeSong(args.text);

            dynamicInitialize();
        }
    }
}
