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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using ZHandler;
using Microsoft.Xna.Framework.Graphics;

namespace ZMediaPlayer
{
    public class MediaLibraryMenuType : MenuType
    {
        public EventHandler<EventArgs> Selected;
        public MediaLibrarySubMenuType subMenu;
        public bool activeSubMenu;

        MediaLibrary mediaLibrary;   
        int itemNumber;
        int index;
        
        protected int x;
        protected int y;
        protected int verticalFactor;

        #region Properties
        public MediaLibrary Library
        {
            get { return mediaLibrary; }
        }

        public int CurrentSong
        {
            get 
            {
                if (!activeSubMenu)
                    return index;
                else
                    return subMenu.CurrentSong;
            }
            
            set 
            {
                if (!activeSubMenu)
                    index = value;
                else
                    subMenu.CurrentSong = value; 
            }
        }

        public int ItemNumber
        {
            get { return itemNumber; }
            set { itemNumber = value; }
        }

        public EventHandler<EventArgs> SubMenuSelected
        {
            get { return subMenu.Selected; }
            set { subMenu.Selected = value; }
        }
        #endregion

        #region Contructors
        public MediaLibraryMenuType(ContentManager content, string current, string prev,
            string fontName, string highlightName, int x, int y, int number)
            : base(content, current, prev, fontName, highlightName, x, y)
        {
            subMenu = new MediaLibrarySubMenuType(content, current, current, fontName, highlightName, x, y, number);
            activeSubMenu = false;
            itemNumber = number;
        }
        #endregion

        #region Overridden Functions
        public override void initialize()
        {
            for (int i = 0; i < itemNumber; i++)
                options.Add(null);

            subMenu.setCoordinates(x, y, verticalFactor);
            subMenu.initialize();
            subMenu.Color = Color;
            subMenu.Back += ResetSubMenu;

            base.initialize();
        }

        public override void load(ContentManager content, string fileName)
        {
            subMenu.load(content, fileName);
            base.load(content, fileName);
        }

        public override void update(InputHandlerComponent i, ref string state)
        {
            if (activeSubMenu)
                subMenu.update(i, ref state);
            else
                base.update(i, ref state);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (!activeSubMenu)
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
            else
                subMenu.draw(spriteBatch);
        }

        public override void movement(InputHandlerComponent input)
        {
            if (!activeSubMenu)
            {
                if (input.getButton(ButtonType.down, false) && (Position != options.Count) && (Counter > 10) && 
                    MediaLibraryFunctions.canMoveDown(State, mediaLibrary, index))
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
            else
                subMenu.movement(input);
        }

        public override void reset(ref string current)
        {
            if (activeSubMenu)
                subMenu.reset(ref current);
            else
            {
                index = 0;

                base.reset(ref current);
            }
        }
        #endregion

        #region Class Functions
        public void setMediaLibrary(MediaLibrary m)
        {
            mediaLibrary = m;
        }

        public void setCoordinates(int horizontal, int vertical, int factor)
        {
            x = horizontal;
            y = vertical;
            verticalFactor = factor;
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

                string temp = MediaLibraryFunctions.getItemName(State, mediaLibrary, i + index);

                options[i] = new OptionType(temp, Font, Color, new Vector2(x, n) , State, m, n - 5);
                options[i].Selected += ActivateSubMenu;
            }
        }

        protected virtual void getItemsUp()
        {
            for (int i = 0; i < itemNumber; i++)
            {
                int m, n;

                m = x - 5;
                n = y + verticalFactor * (itemNumber - 1 - i);

                string temp = MediaLibraryFunctions.getItemName(State, mediaLibrary, index - i);

                options[itemNumber - 1 - i] = new OptionType(temp, Font, Color, new Vector2(x, n), State, m, n - 5);
                options[itemNumber - 1 - i].Selected += ActivateSubMenu;
            }
        }

        protected void setSubMenu()
        {
            subMenu.setSongs(MediaLibraryFunctions.getSongs(State, mediaLibrary, index), Title, 
                MediaLibraryFunctions.getItemName(State, mediaLibrary, index));

            activeSubMenu = true;
        }
        #endregion

        #region Event Handlers
        public void AddSong(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                if (!activeSubMenu)
                    Selected(this, new EventArgs());
                else
                    subMenu.Selected(this, new EventArgs());
            }
        }

        public void ActivateSubMenu(object sender, EventArgs e)
        {
            if (State == "Songs" || activeSubMenu)
                AddSong(sender, e);
            else
                setSubMenu();
        }

        public void ResetSubMenu(object sender, EventArgs e)
        {
            activeSubMenu = false;

            Title.Text = State;
        }
        #endregion
    }
}
