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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Shockwave.Menus;
using Reverb;
using Reverb.Transitions;
using Reverb.Components.Titles;
using Reverb.Components.Background;
using Reverb.Enumerations;

namespace Shockwave
{
    public class MediaComponent : GameComponent
    {
        public static SubSongMenu collectionSongs;
        public static List<Song> playlist;
        public static bool playing;
        public static int currentSong;

        static GameServiceContainer services;
        static MediaLibrary mediaLibrary;
        static Random random;
        static List<bool> songsPlayed;
        ArtistsMenu artists;
        AlbumsMenu albums;
        GenresMenu genres;
        PlaylistsMenu playlists;
        SongsMenu songs;

        public static Song CurrentSong
        {
            get
            {
                if (playlist.Count > 0)
                    return playlist[currentSong];
                else
                    return mediaLibrary.Songs[currentSong];
            }
        }

        public static List<string> Playlist
        {
            get
            {
                List<string> newList = new List<string>();

                foreach (Song song in playlist)
                    newList.Add(song.Name);

                return newList;
            }
        }

        public MediaComponent(Game game)
            : base(game)
        {
            mediaLibrary = new MediaLibrary();
            artists = new ArtistsMenu();
            albums = new AlbumsMenu();
            genres = new GenresMenu();
            playlists = new PlaylistsMenu();
            songs = new SongsMenu();
            collectionSongs = new SubSongMenu();

            playlist = new List<Song>();
            songsPlayed = new List<bool>();
            playing = false;

            random = new Random();

            services = game.Services;

            artists.setSongList(mediaLibrary);
            albums.setSongList(mediaLibrary);
            genres.setSongList(mediaLibrary);
            playlists.setSongList(mediaLibrary);
            songs.setSongs(mediaLibrary);
        }

        public override void Initialize()
        {
            for (int i = 0; i < mediaLibrary.Songs.Count; i++)
                songsPlayed.Add(false);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            play();

            base.Update(gameTime);
        }

        public static void addSong(Song song)
        {
            playlist.Add(song);
        }

        public static void start()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                playing = true;
                currentSong = 0;

                MediaPlayer.Play(CurrentSong);
            }
            else
            {
                MediaPlayer.Resume();
            }
        }

        public static void stop()
        {
            playing = false;
            currentSong = 0;
            MediaPlayer.Stop();
        }

        public static void pause()
        {
            MediaPlayer.Pause();
        }

        public static void moveNextSong()
        {
            if (MediaPlayer.IsShuffled)
                shuffle();
            else
                moveNext();
            
            if (playing)
                MediaPlayer.Play(CurrentSong);
        }

        public static void movePreviousSong()
        {
            if (MediaPlayer.IsShuffled)
                shuffle();
            else
                movePrevious();

            if (playing)
                MediaPlayer.Play(CurrentSong);
        }

        public static void removeSong(string name)
        {
            if (playlist.Count > 0)
            {
                for (int i = 0; i < playlist.Count; i++)
                {
                    if (name == playlist[i].Name)
                        playlist.RemoveAt(i);
                }
            }
        }

        public static Texture2D getArt()
        {
            if (CurrentSong.Album.HasArt)
                return CurrentSong.Album.GetAlbumArt(services);
            else
                return null;
        }

        public static void getInfo(out string artist, out string album, out string song)
        {
            artist = CurrentSong.Artist.Name;
            album = CurrentSong.Album.Name;
            song = CurrentSong.Name;
        }

        public void addMenus(MenuComponent menus)
        {
            menus.add(artists);
            menus.add(albums);
            menus.add(genres);
            menus.add(playlists);
            menus.add(songs);
            menus.add(collectionSongs);
        }

        public void setMenuBackgrounds(string textureAsset, Vector2 location, Color color, ITransition intro, ITransition revert, ITransition select, ITransition exit)
        {
            artists.setBackground(textureAsset, intro, revert, select, exit);

            albums.setBackground(textureAsset, intro, revert, select, exit);

            genres.setBackground(textureAsset, intro, revert, select, exit);

            playlists.setBackground(textureAsset, intro, revert, select, exit);

            songs.setBackground(textureAsset, intro, revert, select, exit);

            collectionSongs.setBackground(textureAsset, intro, revert, select, exit);
        }

        public void setMenuBackgrounds(string textureAsset, Vector2 location, Color color, ITransition intro, ITransition exit)
        {
            artists.setBackground(textureAsset, intro, intro, exit, exit);

            albums.setBackground(textureAsset, intro, intro, exit, exit);

            genres.setBackground(textureAsset, intro, intro, exit, exit);

            playlists.setBackground(textureAsset, intro, intro, exit, exit);

            songs.setBackground(textureAsset, intro, intro, exit, exit);

            collectionSongs.setBackground(textureAsset, intro, intro, exit, exit);
        }

        public void setMenuTitles(string fontAsset, Vector2 location, Color color, ITransition intro, ITransition revert, ITransition select, ITransition exit)
        {
            artists.setTitle(new TitleComponent(fontAsset, "Artists", location, color), intro, revert, select, exit);

            albums.setTitle(new TitleComponent(fontAsset, "Albums", location, color), intro, revert, select, exit);

            genres.setTitle(new TitleComponent(fontAsset, "Genres", location, color), intro, revert, select, exit);

            playlists.setTitle(new TitleComponent(fontAsset, "Playlists", location, color), intro, revert, select, exit);

            songs.setTitle(new TitleComponent(fontAsset, "Songs", location, color), intro, revert, select, exit);

            collectionSongs.setTitle(new TitleComponent(fontAsset, "", location, color), intro, revert, select, exit);
        }

        public void setMenuTitles(string fontAsset, Vector2 location, Color color, ITransition intro, ITransition exit)
        {
            artists.setTitle(new TitleComponent(fontAsset, "Artists", location, color), intro, intro, exit, exit);

            albums.setTitle(new TitleComponent(fontAsset, "Albums", location, color), intro, intro, exit, exit);

            genres.setTitle(new TitleComponent(fontAsset, "Genres", location, color), intro, intro, exit, exit);

            playlists.setTitle(new TitleComponent(fontAsset, "Playlists", location, color), intro, intro, exit, exit);

            songs.setTitle(new TitleComponent(fontAsset, "Songs", location, color), intro, intro, exit, exit);

            collectionSongs.setTitle(new TitleComponent(fontAsset, "", location, color), intro, intro, exit, exit);
        }

        public void setMenuDisplayLists(string fontAsset, string highlightAsset, Rectangle boundingBox, Color color, Vector2 highlighterLocation, int factor, ITransition intro, ITransition revert, ITransition select, ITransition exit)
        {
            artists.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, revert, select, exit);

            albums.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, revert, select, exit);

            genres.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, revert, select, exit);

            playlists.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, revert, select, exit);

            songs.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, revert, select, exit);

            collectionSongs.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, revert, select, exit);
        }

        public void setMenuDisplayLists(string fontAsset, string highlightAsset, Rectangle boundingBox, Color color, Vector2 highlighterLocation, int factor, ITransition intro, ITransition exit)
        {
            artists.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, intro, exit, exit);

            albums.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, intro, exit, exit);

            genres.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, intro, exit, exit);

            playlists.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, intro, exit, exit);

            songs.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, intro, exit, exit);

            collectionSongs.setDisplayList(fontAsset, highlightAsset, boundingBox, color, highlighterLocation, factor, intro, intro, exit, exit);
        }

        public void setMenuBackOption(string fontAsset, Vector2 location, Color color, string highlighterAsset, Vector2 highlighterLocation, TextAlignment alignment, ITransition intro, ITransition revert, ITransition select, ITransition exit)
        {
            artists.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, revert, select, exit);

            albums.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, revert, select, exit);

            genres.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, revert, select, exit);

            playlists.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, revert, select, exit);

            songs.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, revert, select, exit);

            collectionSongs.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, revert, select, exit);
        }

        public void setMenuBackOption(string fontAsset, Vector2 location, Color color, string highlighterAsset, Vector2 highlighterLocation, TextAlignment alignment, ITransition intro, ITransition exit)
        {
            artists.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, intro, exit, exit);

            albums.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, intro, exit, exit);

            genres.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, intro, exit, exit);

            playlists.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, intro, exit, exit);

            songs.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, intro, exit, exit);

            collectionSongs.setBack(fontAsset, location, color, highlighterAsset, highlighterLocation, alignment, intro, intro, exit, exit);
        }

        public void setMenuQueue(bool menusQueueOnTransition)
        {
            artists.Queue = menusQueueOnTransition;

            albums.Queue = menusQueueOnTransition;

            genres.Queue = menusQueueOnTransition;

            playlists.Queue = menusQueueOnTransition;

            songs.Queue = menusQueueOnTransition;

            collectionSongs.Queue = menusQueueOnTransition;
        }

        private void play()
        {
            if (MediaPlayer.State == MediaState.Stopped && playing)
            {
                if (MediaPlayer.IsShuffled)
                {
                    shuffle();
                }
                else if (!MediaPlayer.IsRepeating)
                {
                    moveNext();
                }

                if (playlist.Count > 0)
                    MediaPlayer.Play(playlist[currentSong]);
                else
                    MediaPlayer.Play(mediaLibrary.Songs[currentSong]);
            }
        }

        private static void shuffle()
        {
            int nextSong;
            int counter = 1;

            do
            {
                if (playlist.Count > 0)
                {
                    if (counter == playlist.Count)
                    {
                        for (int i = 0; i < songsPlayed.Count; i++)
                            songsPlayed[i] = false;
                    }

                    nextSong = random.Next(0, playlist.Count);
                }
                else
                {
                    if (counter == mediaLibrary.Songs.Count)
                    {
                        for (int i = 0; i < songsPlayed.Count; i++)
                            songsPlayed[i] = false;
                    }

                    nextSong = random.Next(0, mediaLibrary.Songs.Count);
                }

                counter++;
            }
            while (songsPlayed[nextSong]);

            if (!MediaPlayer.IsRepeating)
                songsPlayed[nextSong] = false;

            currentSong = nextSong;
        }

        private static void moveNext()
        {
            if (playlist.Count > 0)
            {
                if (currentSong == playlist.Count - 1)
                    currentSong = 0;
                else
                    currentSong++;
            }
            else
            {
                if (currentSong == mediaLibrary.Songs.Count - 1)
                    currentSong = 0;
                else
                    currentSong++;
            }
        }

        private static void movePrevious()
        {
            if (playlist.Count > 0)
            {
                if (currentSong == 0)
                    currentSong = playlist.Count - 1;
                else
                    currentSong--;
            }
            else
            {
                if (currentSong == 0)
                    currentSong = mediaLibrary.Songs.Count - 1;
                else
                    currentSong--;
            }
        }
    }
}
