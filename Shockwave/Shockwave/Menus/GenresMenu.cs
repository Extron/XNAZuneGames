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
using Shockwave.Enumerations;
using Microsoft.Xna.Framework.Media;
using Reverb.Components.Background;
using Reverb.Transitions;
using Reverb.Components.Titles;
using Reverb.Components.Scrolling;
using Reverb.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Arguments;
using Reverb.Enumerations;
using Reverb.Components.Options;
using Reverb.Screens;

namespace Shockwave.Menus
{
    public class GenresMenu : MenuType
    {
        GenreCollection genreCollection;

        public GenresMenu()
            : base("Genres")
        {
        }

        public void setSongList(MediaLibrary library)
        {
            genreCollection = library.Genres;
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
            ScrollOptionsComponent optionList = new ScrollOptionsComponent(fontAsset, boudingBox);

            for (int i = 0; i < genreCollection.Count; i++)
            {
                OptionType option = new OptionType(genreCollection[i].Name, new Vector2(boudingBox.X, boudingBox.Y + optionFactor * i),
                                                   color, new Rectangle(boudingBox.X, boudingBox.Y + optionFactor * i, boudingBox.Width, optionFactor),
                                                   OptionAction.next, "Collection Songs", highlighterAsset, new Vector2(highlighterLocation.X, highlighterLocation.Y + i * optionFactor));

                option.Selected += SetSubMenu;

                optionList.addOption(option);
            }

            optionList.setTransitions(intro, revert, select, exit);

            addComponent(optionList);
        }

        public void SetSubMenu(OptionArgs args)
        {
            MediaComponent.collectionSongs.setSongs(genreCollection[args.index].Name, genreCollection[args.index].Songs);
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
    }
}

