/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of GameMenus.
 * 
 * GameMenus is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * GameMenus is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GameMenus.  If not, see <http://www.gnu.org/licenses/>.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ZHandler;
using Microsoft.Xna.Framework.Content;

namespace GameMenus
{
    public delegate void Select(object sender, EventArgs e);

    public class OptionType
    {
        public Select Selected;

        protected StandardState option;
        protected HighlightState highlighted;
        protected HitState hit;
        protected OptionState state;
        string menuLink;
        TextAlignment alignment;
        OptionAction action;
        bool hasSwitch;
        bool activateTransition;
        bool activateSelected;

        #region Properties
        public Vector2 Highligher
        {
            get { return highlighted.Highlight; }
            set { highlighted.Highlight = value; }
        }

        public Vector2 TextVector
        {
            get { return option.Vector; }
            set { option.Vector = value; }
        }

        public Color TextColor
        {
            get { return option.TextColor; }
            set { option.TextColor = value; }
        }

        public string Text
        {
            get { return option.Text; }
            set { option.Text = value; }
        }

        public string MenuLink
        {
            get { return menuLink; }
            set { menuLink = value; }
        }

        public bool Switch
        {
            get { return hasSwitch; }
            set { hasSwitch = value; }
        }

        public bool ActivateTransition
        {
            get { return activateTransition; }
            set { activateTransition = value; }
        }

        public bool ActivateSelected
        {
            get { return activateSelected; }
            set { activateSelected = value; }
        }

        public OptionState State
        {
            get { return state; }
            set { state = value; }
        }

        public OptionAction Action
        {
            get { return action; }
            set { action = value; }
        }

        public TextAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }
        #endregion

        #region Constructors
        public OptionType(string stringText, string fontName, Color color, Vector2 stringVector, string rState, Rectangle highlightRect, string highlightName)
        {
            option = new StandardState(new StringType(stringText, stringVector, color), fontName);
            highlighted = new HighlightState(option, highlightRect, highlightName);
            menuLink = rState;
            action = OptionAction.next;
            alignment = TextAlignment.right;
            activateTransition = true;
            activateSelected = true;

            state = OptionState.standard;
        }

        public OptionType(string stringText, string fontName, Color color, Vector2 stringVector, OptionAction optionAction, bool activate, Rectangle highlightRect, string highlightName)
        {
            option = new StandardState(new StringType(stringText, stringVector, color), fontName);
            highlighted = new HighlightState(option, highlightRect, highlightName);
            menuLink = state.ToString();
            action = optionAction;
            alignment = TextAlignment.right;
            activateTransition = activate;
            activateSelected = true;

            state = OptionState.standard;
        }

        public OptionType(string stringText, string fontName, Color color, Vector2 stringVector, string rState)
        {
            option = new StandardState(new StringType(stringText, stringVector, color), fontName);
            highlighted = new HighlightState(option);
            menuLink = rState;
            action = OptionAction.next;
            alignment = TextAlignment.right;
            activateTransition = true;
            activateSelected = true;

            state = OptionState.standard;
        }

        public OptionType(string stringText, string fontName, Color color, Vector2 stringVector, string rState, Rectangle highlightRect, string highlightName, TextAlignment textAlignment)
        {
            option = new StandardState(new StringType(stringText, stringVector, color), fontName);
            highlighted = new HighlightState(option, highlightRect, highlightName);
            menuLink = rState;
            action = OptionAction.next;
            alignment = textAlignment;
            activateTransition = true;
            activateSelected = true;

            state = OptionState.standard;
        }
        #endregion

        #region Virtual Functions
        public virtual void load()
        {
            option.load();

            highlighted.load();
            
            if (hit != null)
                hit.load();

            switch (alignment)
            {
                case TextAlignment.right:
                    option.right();
                    highlighted.right();
                    break;

                case TextAlignment.center:
                    option.center();
                    highlighted.center();
                    break;

                case TextAlignment.left:
                    option.left();
                    highlighted.left();
                    break;                   
            }
        }

        public virtual void draw(SpriteBatch spriteBatch) 
        {
            switch (state)
            {
                case OptionState.standard:
                    option.draw(spriteBatch);
                    break;

                case OptionState.highlighted:
                    highlighted.draw(spriteBatch);
                    break;

                case OptionState.pressed:
                    hit.draw(spriteBatch);
                    break;
            }
        }

        public virtual void update(InputHandlerComponent i)
        {
            switch (state)
            {
                case OptionState.standard:
                    option.update();
                    break;

                case OptionState.highlighted:
                    highlighted.update();
                    select(i);
                    break;

                case OptionState.pressed:
                    hit.update();
                    break;
            }
        }

        public virtual void select(InputHandlerComponent i, ref string state)
        {
            if (i.getButton("Select", true))
            {
                if (Selected != null)
                    Selected(this, new EventArgs());

                state = menuLink;
            }
        }

        public virtual void select(InputHandlerComponent i)
        {
            if (i.getButton("Select", true))
            {
                if (Selected != null)
                    Selected(this, new EventArgs());
            }
        }
        #endregion

        #region Class Funtions
        public void setFont(SpriteFont font)
        {
            option.Font = font;
            highlighted.Font = font;
            //hit.Font = font;
        }

        public void setHighlight(Texture2D texture)
        {
            highlighted.Texture = texture;
        }

        public void wrapText()
        {
            highlighted.wrapText();
            //hit.wrapText();
        }
        #endregion
    }

    public enum OptionAction
    {
        next,
        previous,
        startGame,
        endGame,
        exit
    }
}
