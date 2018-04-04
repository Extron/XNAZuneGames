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
using Microsoft.Xna.Framework.Media;
using GameMenus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZHandler;

namespace ZMediaPlayer
{
    public class MediaLibrarySubMenuType : MenuType
    {
        public EventHandler<EventArgs> Selected;
        public EventHandler<EventArgs> Back;
        public SongCollection songs;

        int itemNumber;
        int index;

        protected int x;
        protected int y;
        protected int verticalFactor;

        public int CurrentSong
        {
            get { return index; }
            set{ index = value; }
        }

        public MediaLibrarySubMenuType(ContentManager content, string current, string prev,
            string fontName, string highlightName, int x, int y, int number)
            : base(content, current, prev, fontName, highlightName, x, y)
        {
            itemNumber = number;
        }

        #region Overridden Functions
        public override void initialize()
        {
            for (int i = 0; i < itemNumber; i++)
                options.Add(null);

            base.initialize();
        }

        public override void load(ContentManager content, string fileName)
        {
            base.load(content, fileName);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Menu, Vector, Color.White);

            for (int i = 0; i < options.Count; i++)
            {
                try
                {
                    options[i].draw(spriteBatch, Highlighter);
                }
                catch
                {
                }
            }

            if (Title != null)
                Title.draw(spriteBatch);
        }

        public override void movement(ZHandler.InputHandlerComponent input)
        {
            if (input.getButton(ButtonType.down, false) && (Position != options.Count) && (Counter > 10) && (index != songs.Count))
            {
                options[Position].Highlight = false;

                if (Position == itemNumber - 1)
                {
                    index++;
                    getItemsDown();
                    Position = 0;
                }
                else
                {
                    if (options[Position + 1] != null)
                    {
                        index++;
                        Position++;
                    }
                    else
                        return;
                }

                options[Position].Highlight = true;
                Counter = 0;
                return;
            }
            else if (input.getButton(ButtonType.up, false) && (index != 0) && (Counter > 10))
            {
                options[Position].Highlight = false;

                if (Position == 0)
                {
                    if (index != 0)
                    {
                        index--;
                        getItemsUp();
                        Position = itemNumber - 1;
                    }
                }
                else
                {
                    if (options[Position - 1] != null)
                    {
                        index--;
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

        public override void reset(ref string current)
        {
            for (int i = 0; i < options.Count; i++)
                options[i] = null;

            index = 0;

            if (Back != null)
                Back(this, new EventArgs());

            base.reset(ref current);
        }
        #endregion

        #region Class Functions
        public void setCoordinates(int horizontal, int vertical, int factor)
        {
            x = horizontal;
            y = vertical;
            verticalFactor = factor;
        }

        public void setSongs(SongCollection songCollection, TitleType title, string name)
        {
            songs = songCollection;

            Title = title;

            Title.Text = name;

            getItems(ListDirection.down);
        }

        public virtual void getItems(ListDirection direction)
        {
            switch (direction)
            {
                case ListDirection.down:
                    getItemsDown();
                    break;

                case ListDirection.up:
                    getItemsUp();
                    break;

                default:
                    getItemsDown();
                    break;
            }
        }

        protected virtual void getItemsDown()
        {
            for (int i = 0; i < itemNumber; i++)
            {
                int m, n;

                m = x - 5;
                n = y + verticalFactor * i;

                string temp;

                if (i + index == 0)
                    temp = "(Select All)";
                else
                {
                    try
                    {
                        temp = MediaLibraryFunctions.getItemName(State, songs, (i - 1) + index);
                    }
                    catch
                    {
                        options[i] = null;
                        continue;
                    }
                }

                options[i] = new OptionType(temp, Font, Color, new Vector2(x, n), State, m, n - 5);
                options[i].Selected += AddSong;
            }
        }

        protected virtual void getItemsUp()
        {
            for (int i = itemNumber - 1; i >= 0; i--)
            {
                int m, n;

                m = x - 5;
                n = y + verticalFactor * i;

                string temp = "";

                if (i == 0 && index < itemNumber)
                    temp = "(Select All)";
                else
                    temp = MediaLibraryFunctions.getItemName(State, songs, (index) - (itemNumber - i));
                

                options[i] = new OptionType(temp, Font, Color, new Vector2(x, n), State, m, n - 5);
                options[i].Selected += AddSong;
            }
        }
        #endregion

        #region Event Handlers
        public void AddSong(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected(this, new EventArgs());
        }
        #endregion
    }
}
