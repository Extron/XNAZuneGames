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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using GameMenus;
using ZHandler;

namespace ZMediaPlayer
{
    public class MediaLibraryComponent : DrawableGameComponent
    {
        public MenuComponent menus;
        public InputHandlerComponent input;

        SpriteBatch spriteBatch;
        MediaLibrary mediaLibrary;
        List<Song> playList;
        Song currentSong;
        Random random;
        bool[] shuffledSongs;
        bool active;
        int index;

        #region Properties
        public string Current
        {
            get
            {
                return menus.Current;
            }
            set
            {
                menus.Current = value;
            }
        }

        public bool Shuffle
        {
            get
            {
                return MediaPlayer.IsShuffled;
            }
            set
            {
                MediaPlayer.IsShuffled = value;
            }
        }

        public bool Repeat
        {
            get
            {
                return MediaPlayer.IsRepeating;
            }
            set
            {
                MediaPlayer.IsRepeating = value;
            }
        }
        #endregion

        #region Constructors
        public MediaLibraryComponent(Game game, MenuComponent m)
            : base(game)
        {
            menus = m;
            input = new InputHandlerComponent(game);
            playList = new List<Song>();
            random = new Random();
            
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
        #endregion

        #region Overridden Functions
        public override void Initialize()
        {
            mediaLibrary = new MediaLibrary();
            MediaPlayer.Volume = 1.0f;
            index = 0;
            active = false;
            shuffledSongs = new bool[mediaLibrary.Songs.Count];

            currentSong = mediaLibrary.Songs[0];

            menus.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            input.Update(gameTime);

            playNext();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            menus.Draw(gameTime);
            base.Draw(gameTime);
        }
        #endregion

        #region Class Functions
        public void enable()
        {
            if (!Enabled)
            {
                input.Enabled = true;
                menus.Enabled = true;
                Enabled = true;
            }
            if (!Visible)
            {
                menus.Visible = true;
                Visible = true;
            }
        }

        public void disable()
        {
            if (Enabled)
            {
                input.Enabled = false;
                menus.Enabled = false;
                Enabled = false;
            }
            if (Visible)
            {
                menus.Visible = false;
                Visible = false;
            }
        }

        public void visible()
        {
            Visible = !Visible;
        }

        public void updateGameState(string state)
        {
            menus.Current = state;
        }

        public void updateInput(InputHandlerComponent i)
        {
            input = i;
        }

        public void addMenu(MediaLibraryMenuType menu)
        {
            menu.setMediaLibrary(mediaLibrary);

            menu.Selected += SelectMusic;

            menu.subMenu.Selected += SelectMusic;

            menus.add(menu);
        }

        public void addMenu(MediaLibraryPlaylistMenu menu)
        {
            menu.setMediaLibrary(mediaLibrary);

            menu.Selected += RemoveMusic;

            menu.setPlaylist(playList);

            menus.add(menu);
        }

        public void addMenu(MediaLibraryPlayerType menu)
        {
            menu.PlayPause += PlayPauseMusic;
            menu.GetNext += NextSong;
            menu.GetPrevious += PreviousSong;
            menu.SetDisplay += UpdateDisplay;

            menus.add(menu);
        }

        public void setSong()
        {
            if (playList.Count != 0)
            {
                index = 0;

                if (Shuffle)
                {
                    shuffleSong();
                }

                currentSong = playList[index];
            }
            else
            {
                currentSong = mediaLibrary.Songs[index];
            }
        }

        public void playNext()
        {
            if (MediaPlayer.State == MediaState.Stopped && active)
            {
                if (!Repeat)
                    NextSong(null, null);
                else if (Shuffle)
                    NextSong(null, null);
                else
                    MediaPlayer.Play(currentSong);
            }
        }

        public void clearPlaylist()
        {
            playList.Clear();       
        }

        private void shuffleSong()
        {
            bool temp = true;
            int max = 0;

            if (playList.Count != 0)
                max = playList.Count;
            else
                max = mediaLibrary.Songs.Count;

            for (int i = 0; i < max; i++)
            {
                if (!shuffledSongs[i])
                    temp = false;
            }

            if (temp)
            {
                for (int i = 0; i < max; i++)
                {
                    shuffledSongs[i] = false;
                }
            }

            do
            {
                index = RandomNumber(0, max);
            }
            while (shuffledSongs[index]);

            if (!MediaPlayer.IsRepeating)
                shuffledSongs[index] = true;
        }

        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
        #endregion

        #region Event Handlers
        public void SelectMusic(object sender, EventArgs e)
        {
            MediaLibrarySubMenuType menu = sender as MediaLibrarySubMenuType;

            if (menu != null)
            {
                if (menu.CurrentSong == 0)
                    MediaLibraryFunctions.addCollection(playList, menu.songs);
                else
                    MediaLibraryFunctions.addSong(playList, menu.songs[menu.CurrentSong - 1]);
            }
            else
            {
                MediaLibraryMenuType songMenu = sender as MediaLibraryMenuType;

                MediaLibraryFunctions.addToPlaylist(songMenu, playList);
            }
        }

        public void RemoveMusic(object sender, EventArgs e)
        {
            MediaLibraryPlaylistMenu menu = sender as MediaLibraryPlaylistMenu;

            MediaLibraryFunctions.removeFromPlaylist(menu, playList);
        }

        public void PlayPauseMusic(object sender, EventArgs e)
        {
            switch (MediaPlayer.State)
            {
                case MediaState.Paused:
                    MediaPlayer.Resume();
                    break;

                case MediaState.Playing:
                    MediaPlayer.Pause();
                    break;

                case MediaState.Stopped:
                    setSong();
                    active = true;
                    MediaPlayer.Play(currentSong);
                    break;
            }
        }

        public void NextSong(object sender, EventArgs e)
        {
            if (Shuffle)
            {
                shuffleSong();
            }
            else
                index++;

            if (index < playList.Count && playList.Count != 0)
            {
                currentSong = playList[index];
            }
            else
            {
                if (playList.Count == 0)
                {
                    currentSong = mediaLibrary.Songs[index];
                }
                else
                {
                    index = 0;
                    currentSong = playList[index];
                }
            }

            if (active)
                MediaPlayer.Play(currentSong);
        }

        public void PreviousSong(object sender, EventArgs e)
        {
            if (Shuffle)
            {
                shuffleSong();
            }
            else
                index--;

            if (index != -1)
            {
                if (playList.Count != 0)
                    currentSong = playList[index];
                else
                    currentSong = mediaLibrary.Songs[index];
            }
            else
            {
                if (playList.Count == 0)
                {
                    index = mediaLibrary.Songs.Count - 1;
                    currentSong = mediaLibrary.Songs[index];
                }
                else
                {
                    index = playList.Count - 1;
                    currentSong = playList[index];
                }
            }

            if (active)
                MediaPlayer.Play(currentSong);
        }

        public void StopSong(object sender, EventArgs e)
        {
            if (MediaPlayer.State != MediaState.Stopped)
                MediaPlayer.Stop();
        }

        public void UpdateDisplay(object sender, EventArgs e)
        {
            MediaLibraryPlayerType menu = sender as MediaLibraryPlayerType;

            menu.setDisplay(currentSong, Game.Services);
        }
        #endregion
    }
}
