#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of Shockwave.
 * 
 * Shockwave is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * Shockwave is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with Shockwave.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb;
using Microsoft.Xna.Framework.Media;
using Reverb.Components.Titles;
using Reverb.Components.Scrolling;
using Reverb.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Enumerations;
using Reverb.Arguments;
using Reverb.Components.Background;
using Reverb.Transitions;
using Reverb.Components.Options;
using Reverb.Screens;

namespace Shockwave.Menus
{
    public class SubSongMenu : MenuType
    {
        SongCollection songs;
        int factor;

        public SubSongMenu()
            : base("Collection Songs")
        {
        }

        public void setBackground(string asset, ITransition intro, ITransition revert, ITransition select, ITransition exit)
        {
            BackgroundComponent background = new BackgroundComponent(asset);

            background.setTransitions(intro, revert, select, exit);

            addComponent(background);
        }

        public void setTitle(TitleComponent title, ITransition intro, ITransition revert, ITransition select, ITransition exit)
        {
            title.setTransitions(intro, revert, select, exit);

            addComponent(title);
        }

        public void setDisplayList(string fontAsset, string highlighterAsset, Rectangle boudingBox, Color color, Vector2 highlighterLocation, int optionFactor, ITransition intro, ITransition revert, ITransition select, ITransition exit)
        {
            factor = optionFactor;

            ScrollOptionsComponent optionList = new ScrollOptionsComponent(fontAsset, boudingBox);

            OptionType option = new OptionType("<Select All>", new Vector2(boudingBox.X, boudingBox.Y), color, new Rectangle(boudingBox.X, boudingBox.Y, boudingBox.Width, factor),
                                               OptionAction.nothing, highlighterAsset, highlighterLocation);

            option.Selected += AddAll;

            optionList.addOption(option);

            optionList.setTransitions(intro, revert, select, exit);

            addComponent(optionList);
        }

        public void setBack(string fontAsset, Vector2 location, Color color, string highlighterAsset, Vector2 highlighterLocation, TextAlignment alignment, ITransition intro, ITransition revert, ITransition select, ITransition exit)
        {
            OptionsComponent options = new OptionsComponent(fontAsset);

            OptionType option = new OptionType("Back", location, color, OptionAction.previous, true, true, highlighterAsset, highlighterLocation);
            option.setAlignment(alignment);

            options.addOption(option);

            options.setTransitions(intro, revert, select, exit);

            addComponent(options);
        }

        public void setSongs(string collectionName, SongCollection songCollection)
        {
            songs = songCollection;

            if (components.ContainsKey("Title"))
            {
                TitleComponent title = components["Title"] as TitleComponent;

                title.Text = collectionName;
            }

            if (components.ContainsKey("Scroll Options"))
            {
                ScrollOptionsComponent optionList = components["Scroll Options"] as ScrollOptionsComponent;

                int i;

                for (i = 1; i <= songs.Count; i++)
                {
                    try
                    {
                        optionList.getOption(i).Text = songs[i - 1].Name;
                    }
                    catch
                    {
                        OptionType option = new OptionType(optionList.getOption(0));

                        option.Text = songs[i - 1].Name;

                        option.Position = new Vector2(option.Position.X, option.Position.Y + i * factor);

                        option.Selected -= AddAll;
                        option.Selected += AddSong;

                        optionList.addOption(option);
                    }
                }

                if (i < optionList.Count - 1)
                    optionList.clearOptions(i);
            }
        }

        public void AddAll(OptionArgs args)
        {
            foreach (Song song in songs)
                MediaComponent.addSong(song);
        }

        public void AddSong(OptionArgs args)
        {
            MediaComponent.addSong(songs[args.index - 1]);
        }
    }
}
