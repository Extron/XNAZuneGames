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
using GameMenus;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace ZMediaPlayer
{
    public static class MediaLibraryFunctions
    {
        public static string getItemName(string state, MediaLibrary mediaLibrary, int i)
        {
            string name = "";

            switch (state)
            {
                case "Artists":
                    name = mediaLibrary.Artists[i].Name;
                    break;

                case "Albums":
                    name = mediaLibrary.Albums[i].Name;
                    break;

                case "Genres":
                    name = mediaLibrary.Genres[i].Name;
                    break;

                case "Songs":
                    name = mediaLibrary.Songs[i].Name;
                    break;
            }

            return name;
        }

        public static string getItemName(string state, SongCollection songs, int i)
        {
            string name = "";

            switch (state)
            {
                case "Artists":
                    name = songs[i].Name;
                    break;

                case "Albums":
                    name = songs[i].Name;
                    break;

                case "Genres":
                    name = songs[i].Name;
                    break;
            }

            return name;
        }

        public static Song getSong(string state, MediaLibrary mediaLibrary, int i)
        {
            Song song = null;

            switch (state)
            {
                case "Songs":
                    song = mediaLibrary.Songs[i];
                    break;
            }

            return song;
        }

        public static SongCollection getSongs(string state, MediaLibrary mediaLibrary, int i)
        {
            SongCollection songs = null;

            switch (state)
            {
                case "Artists":
                    songs = mediaLibrary.Artists[i].Songs;
                    break;

                case "Albums":
                    songs = mediaLibrary.Albums[i].Songs;
                    break;

                case "Genres":
                    songs = mediaLibrary.Genres[i].Songs;
                    break;
            }

            return songs;
        }

        public static void addToPlaylist(MediaLibraryMenuType menu, List<Song> list)
        {
            switch (menu.State)
            {
                case "Artists":
                    addCollection(list, getSongs(menu.State, menu.Library, menu.CurrentSong));
                    break;

                case "Albums":
                    addCollection(list, getSongs(menu.State, menu.Library, menu.CurrentSong));
                    break;

                case "Genres":
                    addCollection(list, getSongs(menu.State, menu.Library, menu.CurrentSong));
                    break;

                case "Songs":
                    addSong(list, getSong(menu.State, menu.Library, menu.CurrentSong));
                    break;
            }
        }

        public static void removeFromPlaylist(MediaLibraryPlaylistMenu menu, List<Song> list)
        {
            list.RemoveAt(menu.CurrentSong);
        } 

        public static void addCollection(List<Song> list, SongCollection songs)
        {
            if (songs != null)
            {
                for (int i = 0; i < songs.Count; i++)
                {
                    list.Add(songs[i]);
                }
            }
        }

        public static void addSong(List<Song> list, Song song)
        {
            if (song != null)
            {
                list.Add(song);
            }
        }

        public static void getSongList(MediaLibraryMenuType menu)
        {
            menu.getItems(ListDirection.down);
        }

        public static bool canMoveDown(string state, MediaLibrary m, int index)
        {
            bool temp = true;

            switch (state)
            {
                case "Artists":
                    if (index == m.Artists.Count - 1)
                        temp = false;
                    break;

                case "Albums":
                    if (index == m.Albums.Count - 1)
                        temp = false;
                    break;

                case "Genres":
                    if (index == m.Genres.Count - 1)
                        temp = false;
                    break;

                case "Songs":
                    if (index == m.Songs.Count - 1)
                        temp = false;
                    break;
            }

            return temp;
        }
    }
}
