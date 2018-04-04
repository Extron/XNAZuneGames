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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using ZHandler;

namespace GameMenus
{
    public abstract class MenuType
    {
        #region Variables
        /// <summary>
        /// This event handler is raised when the user presses the back button.
        /// </summary>
        public Select Back;

        /// <summary>
        /// A dictionary of the menu's various components, accessible by a standardized unique string identification.
        /// </summary>
        public Dictionary<string, IComponent> components;

        /// <summary>
        /// The current state of the menu's transitions.  Possible values are intro, exit, and select transitions, as well as 
        /// an idle menu state and an exiting final state.
        /// </summary>
        TransitionState transitionState;

        /// <summary>
        /// A unique string identifier for the menu to be accessed in a dictionary
        /// </summary>
        string state;

        /// <summary>
        /// A value that determines of the menu's transitions are queuable or listable.  Queuable transitions execute one at a time,
        /// in the order that they are sent to the transition manager.  No overlapping takes place with these transitions.  Listable
        /// transitions execute at the same time, and any that are sent to the manager are executed.  The transitions overlap, which
        /// is useful for designing fade and scroll transitions.
        /// </summary>
        bool queue;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the menu's current transition state.
        /// </summary>
        public TransitionState TransitionState
        {
            get { return transitionState; }
            set { transitionState = value; }
        }

        /// <summary>
        /// Determines if any of the menu's components are registered with intro transitions. 
        /// </summary>
        public bool HasIntro
        {
            get
            {
                bool temp = false;

                foreach (IComponent component in components.Values)
                {
                    if (component.HasIntro)
                    {
                        temp = true;
                        break;
                    }
                }

                return temp;
            }
        }

        /// <summary>
        /// Determines if any of the menu's components are registered with select transitions. 
        /// </summary>
        public bool HasSelected
        {
            get
            {
                bool temp = false;

                foreach (IComponent component in components.Values)
                {
                    if (component.HasSelect)
                    {
                        temp = true;
                        break;
                    }
                }

                return temp;
            }
        }

        /// <summary>
        /// Determines if any of the menu's components are registered exit intro transitions. 
        /// </summary>
        public bool HasExit
        {
            get
            {
                bool temp = false;

                foreach (IComponent component in components.Values)
                {
                    if (component.HasExit)
                    {
                        temp = true;
                        break;
                    }
                }

                return temp;
            }
        }

        /// <summary>
        /// Checks to see if all of the menu's components have completed their transitions.
        /// </summary>
        public bool CompletedTransitions
        {
            get
            {
                bool temp = false;

                foreach (IComponent component in components.Values)
                {
                    if (component.completedTransitions(transitionState))
                    {
                        temp = true;
                        continue;
                    }
                    else
                    {
                        temp = false;
                        break;
                    }
                }

                return temp;
            }
        }

        /// <summary>
        /// Gets or sets the menu's unique identifier.
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Gets or sets the queue variable, which determines of the menu's transitions are queuable or listable.
        /// </summary>
        public bool Queue
        {
            get { return queue; }
            set { queue = value; }
        }
        #endregion

        #region Constructors
        public MenuType(string current)
        {
            components = new Dictionary<string, IComponent>();
            state = current;
            transitionState = TransitionState.menu;
            queue = true;
        }
        #endregion        

        #region Overridable Functions
        /// <summary>
        /// Initializes all menu components at the menu's startup.
        /// </summary>
        public virtual void initialize()
        {
            foreach (IComponent component in components.Values)
                component.initialize();
        }

        /// <summary>
        /// Loads all graphical data for the components at the menu's startup.
        /// </summary>
        public virtual void load()
        {
            foreach (IComponent component in components.Values)
                component.load();
        }

        /// <summary>
        /// Updates all of the menu's components, as well as checking if the user has pressed the back button.
        /// </summary>
        /// <param name="i">The game's menu input handler.</param>
        public virtual void update(InputHandlerComponent i)
        {
            switch (transitionState)
            {
                case TransitionState.menu:
                    foreach (IComponent component in components.Values)
                        component.update(i);

                    back(i);

                    break;
            }
        }

        /// <summary>
        /// Draws all the menu's components.
        /// </summary>
        /// <param name="spriteBatch">The game's menu sprite batch.</param>
        public virtual void draw(SpriteBatch spriteBatch)
        {
            foreach (IComponent component in components.Values)
                component.draw(spriteBatch);
        }

        /// <summary>
        /// Dynamically initializes specific items when the menu is accessed within the game.  This menu can be overrided within
        /// the game's menu to initialize components that require dynamic information from the game.
        /// </summary>
        public virtual void dynamicInitialize()
        {
        }

        /// <summary>
        /// Checks to see if the user has pressed the back button, and if so, raises the Back event and resets the menu data.
        /// </summary>
        /// <param name="i">The game's menu input handler.</param>
        public virtual void back(InputHandlerComponent i)
        {
            if (i.getButton("Back", true))
            {
                if (Back != null)
                    Back(this, new EventArgs());

                reset();
            }
        }

        /// <summary>
        /// Resets all of the menu components to their default values.
        /// </summary>
        public virtual void reset()
        {
            foreach (IComponent component in components.Values)
                component.reset();
        }
        #endregion

        #region Menu Options
        /// <summary>
        /// Checks to see if any of the menu components are option components, and adds the event at the specified option.
        /// </summary>
        /// <param name="method">The event that needs to be added.</param>
        /// <param name="optionIndex">The option that needs the event.</param>
        public void setEvent(Select method, int optionIndex)
        {
            foreach (object component in components.Values)
            {
                if (component is IOptions)
                {
                    IOptions options = component as IOptions;

                    options.setEvent(method, optionIndex);
                }
            }
        }

        /// <summary>
        /// Checks to see if any of the menu components are option components, and adds the event to all options.
        /// </summary>
        /// <param name="method">The event that needs to be added.</param>
        public void setEvents(Select method)
        {
            foreach (object component in components.Values)
            {
                if (component is IOptions)
                {
                    IOptions options = component as IOptions;

                    options.setEvents(method);
                }
            }
        }

        /// <summary>
        /// Updates all of the components' transitions.
        /// </summary>
        public void updateTransitions()
        {
            foreach (IComponent component in components.Values)
                component.updateTransitions(transitionState);
        }

        /// <summary>
        /// Draw's all of the components' transitions.
        /// </summary>
        /// <param name="spriteBatch">The game's menu sprite batch.</param>
        public virtual void drawTransitions(SpriteBatch spriteBatch)
        {
            foreach (IComponent component in components.Values)
                component.drawTransitions(spriteBatch, transitionState);
        }

        /// <summary>
        /// Adds a IComponent implemented component to the menu's list of components.
        /// </summary>
        /// <param name="component">The component to be added.</param>
        protected void addComponent(IComponent component)
        {
            components.Add(component.Identifier, component);
        }
        #endregion
    }
}
