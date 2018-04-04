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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using ZHandler;

namespace ZMediaPlayer
{
    public class MediaLibraryPlayerType : MenuType
    {
        public EventHandler<EventArgs> PlayPause;
        public EventHandler<EventArgs> GetNext;
        public EventHandler<EventArgs> GetPrevious;
        public EventHandler<EventArgs> SetDisplay;

        Texture2D albumArt;
        Texture2D noArt;
        Rectangle rectangle;
        Vector2 shuffleVec, repeatVec;
        Vector2 artistVec, albumVec, songVec;
        string shuffleStatus;
        string repeatStatus;
        string songName;
        string artist;
        string album;
        bool shuffleOn;
        bool repeatOn;

        #region Properties
        public bool ShuffleOn
        {
            get 
            {
                return shuffleOn;
            }
            set
            {
                if (value)
                    shuffleStatus = "Shuffle: On";
                else
                    shuffleStatus = "Shuffle: Off";

                shuffleOn = value;
            }
        }

        public bool RepeatOn
        {
            get
            {
                return repeatOn;
            }
            set
            {
                if (value)
                    repeatStatus = "Repeat: On";
                else
                    repeatStatus = "Repeat: Off";

                repeatOn = value;
            }
        }
        #endregion

        #region Constructors
        public MediaLibraryPlayerType(ContentManager content, string current, string previous, string fontName, string highlightName, Rectangle artPosition)
            : base(content, current, previous, fontName, highlightName, 0, 0)
        {
            rectangle = artPosition;
        }
        #endregion

        #region Overridden Functions
        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            SetDisplay(this, new EventArgs());

            spriteBatch.Draw(albumArt, rectangle, Color.White);

            drawText(spriteBatch);

            drawIndicators(spriteBatch);
        }

        public override void movement(InputHandlerComponent input)
        {
            if (input.getButton(ButtonType.b, true))
            {
                PlayPause(this, new EventArgs());
            }

            if (input.getButton(ButtonType.left, true))
            {
                GetPrevious(this, new EventArgs());
            }

            if (input.getButton(ButtonType.right, true))
            {
                GetNext(this, new EventArgs());
            }

            if (input.getButton(ButtonType.up, true))
            {
                MediaPlayer.Volume += 0.1f;
            }

            if (input.getButton(ButtonType.down, true))
            {
                MediaPlayer.Volume -= 0.1f;
            }

            base.movement(input);
        }
        #endregion

        #region Class Functions
        public void setNoArt(Texture2D blankArt)
        {
            noArt = blankArt;
        }

        public void setArt(Song song, GameServiceContainer s)
        {
            if (song.Album.GetAlbumArt(s) != null)
            {
                albumArt = song.Album.GetAlbumArt(s);
            }
            else
            {
                albumArt = noArt;
            }
        }

        public void setText(Song song)
        {
            if (song.Artist.Name != null)
                artist = song.Artist.Name;
            else
                artist = "Unknown Artist";

            if (song.Album.Name != null)
                album = song.Album.Name;
            else
                album = "Unknown Album";

            if (song.Name != null)
                songName = song.Name;
            else
                songName = "Unknown Song";
        }

        public void setIndicators(Vector2 shuffle, Vector2 repeat)
        {
            shuffleVec = shuffle;
            repeatVec = repeat;

            shuffleStatus = "Shuffle: Off";
            repeatStatus = "Repeat: Off";
        }

        public void setDisplay(Song song, GameServiceContainer s)
        {
            setArt(song, s);
            setText(song);
        }

        public void setVectors(Vector2 songName, Vector2 artist, Vector2 album)
        {
            songVec = songName;
            artistVec = artist;
            albumVec = album;
        }

        public void setColor(Color color)
        {
            Color = color;
        }

        public void drawText(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.DrawString(Font, artist, artistVec, Color);
            }
            catch
            {
            }

            try
            {
                spriteBatch.DrawString(Font, album, albumVec, Color);
            }
            catch
            {
            }

            try
            {
                spriteBatch.DrawString(Font, songName, songVec, Color);
            }
            catch
            {
            }
        }

        public void drawIndicators(SpriteBatch spriteBatch)
        {
            if (shuffleStatus != null)
            {
                updateShuffle();
                spriteBatch.DrawString(Font, shuffleStatus, shuffleVec, Color);
            }

            if (repeatStatus != null)
            {
                updateRepeat();
                spriteBatch.DrawString(Font, repeatStatus, repeatVec, Color);
            }
        }

        public void updateShuffle()
        {
            ShuffleOn = MediaPlayer.IsShuffled;
        }

        public void updateRepeat()
        {
            RepeatOn = MediaPlayer.IsRepeating;
        }
        #endregion
    }
}
