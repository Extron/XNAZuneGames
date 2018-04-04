/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of ZMediaPlayer.
 * 
 * ZMediaPlayer is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * ZMediaPlyaer is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with ZMediaPlayer.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using GameMenus;
using Microsoft.Xna.Framework;
using ZHandler;

namespace ZMediaPlayer
{
    public class MediaLibraryPlaylistMenu : MediaLibraryMenuType
    {
        List<Song> playlist;

        #region Constructors
        public MediaLibraryPlaylistMenu(ContentManager content, string current, string previous, string fontName, string highlightName,
            int x, int y, int number)
            : base(content, current, previous, fontName, highlightName, x, y, number)
        {
            playlist = new List<Song>();
        }
        #endregion

        #region Overridden Functions
        public override void movement(InputHandlerComponent input)
        {
            if (input.getButton(ButtonType.down, false) && (Position != options.Count) && (Counter > 10) && (CurrentSong != playlist.Count - 1))
            {
                options[Position].Highlight = false;

                if (Position == ItemNumber - 1)
                {
                    CurrentSong++;
                    getItemsDown();
                    Position = 0;
                }
                else
                {
                    if (options[Position + 1] != null)
                    {
                        CurrentSong++;
                        Position++;
                    }
                    else
                        return;
                }

                options[Position].Highlight = true;
                Counter = 0;
                return;
            }
            else if (input.getButton(ButtonType.up, false) && (CurrentSong != 0) && (Counter > 10))
            {
                options[Position].Highlight = false;

                if (Position == 0)
                {
                    if (CurrentSong != 0)
                    {
                        CurrentSong--;
                        getItemsUp();
                        Position = ItemNumber - 1;
                    }
                }
                else
                {
                    if (options[Position - 1] != null)
                    {
                        CurrentSong--;
                        Position--;
                    }
                    else
                        return;
                }

                options[Position].Highlight = true;
                Counter = 0;
                return;
            }
        }

        protected override void getItemsDown()
        {
            for (int i = 0; i < ItemNumber; i++)
            {
                int m, n;

                m = x - 5;
                n = y + verticalFactor * i;

                string temp;

                if (playlist.Count != 0)
                {
                    if (i + CurrentSong < playlist.Count)
                        temp = playlist[i + CurrentSong].Name;
                    else
                    {
                        options[i] = null;
                        continue;
                    }
                }
                else
                {
                    temp = "No Songs in Playlist";
                    options[0] = new OptionType(temp, Font, Color, new Vector2(x, n), State, m, n - 5);
                    break;
                }

                options[i] = new OptionType(temp, Font, Color, new Vector2(x, n), State, m, n - 5);
                options[i].Selected += RemoveSong;
            }
        }

        protected override void getItemsUp()
        {
            for (int i = 1; i <= ItemNumber; i++)
            {
                int m, n;

                m = x - 5;
                n = y + verticalFactor * (ItemNumber - i);

                string temp = "";

                if (playlist.Count != 0)
                {
                    if (CurrentSong - (i - 1) >= 0)
                        temp = playlist[CurrentSong - (i - 1)].Name;
                }
                else
                {
                    temp = "No Songs in Playlist";
                    options[0] = new OptionType(temp, Font, Color, new Vector2(x, n), State, m, n - 5);
                    break;
                }

                options[ItemNumber - i] = new OptionType(temp, Font, Color, new Vector2(x, n), State, m, n - 5);
                options[ItemNumber - i].Selected += RemoveSong;
            }
        }
        #endregion

        #region Class Functions
        public void setPlaylist(List<Song> list)
        {
            playlist = list;
        }

        public void clearList()
        {
            playlist.Clear();

            for (int i = 0; i < options.Count; i++)
            {
                options[i] = null;
            }
        }
        #endregion

        #region Event Handlers
        public void RemoveSong(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, new EventArgs());
                CurrentSong = 0;
                Position = 0;
                getItems(ListDirection.down);
            }
        }
        #endregion
    }
}
