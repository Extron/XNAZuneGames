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
    public class MenuComponent : DrawableGameComponent
    {
        #region Fields
        /// <summary>
        /// An event handler to add any additional commands to the program when exiting, e.i. saving the game
        /// </summary>
        public EventHandler<EventArgs> Exit;

        /// <summary>
        /// This event handler is called when the game is exited and the user is returned to the main menu.
        /// </summary>
        public EventHandler<EventArgs> ExitGame;

        /// <summary>
        /// This event handler is used to pause the game, and is called when the back button is pressed (default).
        /// </summary>
        public EventHandler<EventArgs> Pause;

        /// <summary>
        /// This event handler is used to end the game
        /// </summary>
        public EventHandler<EventArgs> GameOver;

        /// <summary>
        /// This event handler is used to start the game
        /// </summary>
        public EventHandler<EventArgs> Start;

        /// <summary>
        /// Menus have their own separate sprite batch so that effects put on the game's sprite batch do not
        /// carry over to the menu's sprite batch.
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// Menus have their own unique input hanlder separate from the main game so that even if the user
        /// changes the game controls, the menu controls remain the same.
        /// </summary>
        InputHandlerComponent input;

        /// <summary>
        /// The transition manager is the overall manager for menu transitions.  Calls can be made to it from the 
        /// menu component, allowing for menu transitions to run at any needed time.
        /// </summary>
        TransitionManager transitionManager;

        /// <summary>
        /// This dictionary contains all registered menus of the game.  If a menu is not registered, it will not appear
        /// in the game.
        /// </summary>
        Dictionary<string, MenuType> menus;

        /// <summary>
        /// Like the menu dictionary, each system must be registered with the component before it can be used. 
        /// </summary>
        Dictionary<string, SystemType> systems;

        /// <summary>
        /// The stacks are used to contain the current menus that need to be updated and displayed. This allows for 
        /// the retaining of any menu chain, e.i. "Title Screen" goes to "Options" goes to "Touch Screen".  This allows
        /// for efficient movement foreward and backward through the menus.  The menu stack contains menus that don't 
        /// require the game to be playing, e.i title screens.  The game menus hold the values for menus that run while
        /// the game is running, e.i. pause menus. 
        /// </summary>
        Stack<string> menuStack;

        /// <summary>
        /// The stacks are used to contain the current menus that need to be updated and displayed. This allows for 
        /// the retaining of any menu chain, e.i. "Title Screen" goes to "Options" goes to "Touch Screen".  This allows
        /// for efficient movement foreward and backward through the menus.  The menu stack contains menus that don't 
        /// require the game to be playing, e.i title screens.  The game menus hold the values for menus that run while
        /// the game is running, e.i. pause menus. 
        /// </summary>
        Stack<string> gameStack;

        /// <summary>
        /// This bool allows the game to determine if the user is actually playing the game, or is just moving through 
        /// the menus. 
        /// </summary>
        bool isPlaying;
        #endregion

        #region Properties
        /// <summary>
        /// Returns the identifier of the current menu, getting the menu from which ever stack, 
        /// main menus or game menus, is currently activated
        /// </summary>
        public Stack<string> Stack
        {
            get
            {
                if (isPlaying)
                    return gameStack;
                else
                    return menuStack;
            }

            set
            {
                if (isPlaying)
                    gameStack = value;
                else
                    menuStack = value;
            }
        }

        /// <summary>
        /// Returns a bool identifying if the game needs to update the main game loop.
        /// </summary>
        public bool IsPlaying
        {
            //Checks whether the game is active and the there is no menus in front of the game.
            get { return (isPlaying && (gameStack.Peek() == "Playing Game") && !transitionManager.HasTransitions); }
            set { isPlaying = value; }
        }

        /// <summary>
        /// This allows for the user to set the input to touch, which affects both the game input and 
        /// the menu input, as both can use the touch pad as opposed to pressing the buttons.
        /// </summary>
        public bool Touch
        {
            get { return input.Touch; }
            set { input.Touch = value; }
        }
        #endregion

        #region Constructors
        public MenuComponent(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            input = new InputHandlerComponent(game);
            transitionManager = new TransitionManager();
            menus = new Dictionary<string, MenuType>();
            systems = new Dictionary<string, SystemType>();
            menuStack = new Stack<string>();
            gameStack = new Stack<string>();
        }
        #endregion

        #region Overridden Methods
        public override void Initialize()
        {
            //Initialize the input handler
            input.Initialize();

            //hook up the needed event methods
            Start += StartGame;
            ExitGame += EndGame;

            isPlaying = false;

            //Initialize the menu stack to start at the title screen
            menuStack.Push("Title Screen");

            //Initialize the game stack to start with playing the game
            gameStack.Push("Playing Game");

            foreach (MenuType menu in menus.Values)
            {
                //Initialize each menu
                menu.initialize();

                //Set all menu options to add the Select event method
                menu.setEvents(Select);

                //Register the menu with the transition manager if it has any transitions
                if (menu.HasIntro || menu.HasExit)
                    transitionManager.addMenu(menu.State, menu);
            }

            foreach (SystemType system in systems.Values)
            {
                //Initialize each system
                system.initialize();
            }

            //Run the transition for the title screen, since that is the first menu to be displayed
            transitionManager.runTransition(TransitionState.intro, "Title Screen", getMenu("Title Screen").Queue);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Load all registered assets
            AssetManager.LoadAssets(Game.Content);

            //Load each menu
            foreach (MenuType menu in menus.Values)
            {
                menu.load();
            }

            //Load each system
            foreach (SystemType system in systems.Values)
            {
                system.load();
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //If the transition manager is running any transactions, do not update the menus.
            if (transitionManager.HasTransitions)
            {
                //Update any transitions.
                transitionManager.update();
            }
            //Else get the current menu and update it.
            else
            {
                MenuType currentMenu = getMenu(Stack.Peek());

                //Force the input to update.
                input.Update(gameTime);

                //If there is a registered menu, update that menu.
                if (currentMenu != null)
                {
                    currentMenu.update(input);
                }
                //Else check to see if any systems are registered.
                else
                {
                    SystemType system = getSystem(Stack.Peek());

                    //If there is a system, update it.
                    if (system != null)
                    {
                        back(system);

                        system.update(input);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //If there are transitions running, do not draw any menus.
            if (transitionManager.HasTransitions)
            {
                //Draw any transitions.
                transitionManager.draw(spriteBatch);
            }
            //Else draw the current menu.
            else
            {
                MenuType currentMenu = getMenu(Stack.Peek());

                //If there is a menu registered, draw it.
                if (currentMenu != null)
                {
                    currentMenu.draw(spriteBatch);
                }
                //Else check to see if there are any systems registered.
                else
                {
                    SystemType system = getSystem(Stack.Peek());

                    //If there is a system, draw it.
                    if (system != null)
                    {
                        system.draw(spriteBatch);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// This function registers a menu with the component, allowing it to be called upon at any time.
        /// </summary>
        /// <param name="menu">The menu to be registered</param>
        public void add(MenuType menu)
        {
            menus.Add(menu.State, menu);

            menu.Back += Back;
        }

        /// <summary>
        /// This function registers a menu system with the component, allowing it and its menus to be called upon at
        /// any time
        /// </summary>
        /// <param name="system">the system to be registered</param>
        public void addSystem(SystemType system)
        {
            systems.Add(system.state, system);
        }

        /// <summary>
        /// This function returns any menu within the component by using the menu's string identifier
        /// </summary>
        /// <param name="identifier">The string identifier of the desired menu</param>
        /// <returns>Returns a menu from within the registered menus of the compoennt</returns>
        public MenuType getMenu(string identifier)
        {
            try
            {
                return menus[identifier];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// This function returns any system within the component by using the system's string identifier
        /// </summary>
        /// <param name="identifier">The string identifier of the desired system</param>
        /// <returns>Returns a system from within the registered systems of the component</returns>
        public SystemType getSystem(string identifier)
        {
            try
            {
                return systems[identifier];
            }
            catch
            {
                return null;
            }
        }

        public void back(SystemType system)
        {
            MenuFunctions.back(input, system, menuStack);

            if (menuStack.Count == 0)
                Exit(this, new EventArgs());
        }

        /// <summary>
        /// This function sets the menu component to display the paused menu.  This can be hooked into the main game
        /// in order to pause it and access any menus attached to the pause menu.
        /// </summary>
        /// <returns>Returns a bool value indicating whether the menus have been paused</returns>
        public bool pause()
        {
            if (input.getButton("Back", true))
            {
                transitionManager.runTransition(TransitionState.intro, "Paused", getMenu("Paused").Queue);
                gameStack.Push("Paused");

                if (Pause != null)
                    Pause(this, new EventArgs());

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// This functions sets the menu component to display the game over menu.  This can be hooked into the main game
        /// in order to set the state to game over when the game's end conditions are met.
        /// </summary>
        public void gameOver()
        {
            transitionManager.runTransition(TransitionState.intro, "Game Over", getMenu("Game Over").Queue);

            isPlaying = false;

            menuStack.Push("Game Over");

            if (GameOver != null)
                GameOver(this, new EventArgs());
        }

        /// <summary>
        /// This function allows the menu to take the background textures of the game and set them to the background of
        /// any menu.  For example, this would be called when the game is paused so that the game is still "displayed"
        /// behind the paused menu.  This only works if the menu implements the IInGame interface.
        /// </summary>
        /// <param name="menuName">The identifier of the menu</param>
        /// <param name="texture">The game background converted as a texture.  If there are multiple items to be drawn
        /// from the game, use a RenderTarget and retrieve a combined texture of the game</param>
        /// <param name="color">The color of the texture, allowing for "dimming" effects by tinting the background</param>
        public void setBackground(string menuName, Texture2D texture, Color color)
        {
            MenuType menu = getMenu(menuName);
            IComponent component;

            if (menu.components.TryGetValue("In-Game", out component))
            {
                IInGame inGameComponent = component as IInGame;

                inGameComponent.setBackground(texture, color);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// This event is passed to each registered menu and attached to all options' event handlers within that menu
        /// </summary>
        /// <param name="sender">The option that has been activated</param>
        /// <param name="e">A standard blank EventArgs</param>
        public void Select(object sender, EventArgs e)
        {
            //Get the option that raised the event.
            OptionType option = sender as OptionType;

            //Run the current menu's select transition, if it has one.
            if (option.ActivateSelected)
                transitionManager.runTransition(TransitionState.selected, Stack.Peek(), getMenu(Stack.Peek()).Queue);

            //Determine the option action
            switch (option.Action)
            {
                case OptionAction.next:
                    Stack.Push(option.MenuLink);
                    getMenu(option.MenuLink).dynamicInitialize();
                    break;

                case OptionAction.previous:
                    Stack.Pop();
                    break;

                case OptionAction.startGame:
                    if (Start != null)
                        Start(this, new EventArgs());
                    break;

                case OptionAction.endGame:
                    if (ExitGame != null)
                        ExitGame(this, new EventArgs());
                    break;

                case OptionAction.exit:
                    if (Exit != null)
                        Exit(this, new EventArgs());
                    break;
            }

            //If the option signifies that a transition needs to be run, run the transition of the current menu
            if (option.ActivateTransition)
                transitionManager.runTransition(TransitionState.intro, Stack.Peek(), getMenu(Stack.Peek()).Queue);
        }

        /// <summary>
        /// This event is passed to each registered menu and attached to the menu's back event handler
        /// </summary>
        /// <param name="sender">The menu that has been activated</param>
        /// <param name="e">A standard blank EventArgs</param>
        public void Back(object sender, EventArgs e)
        {
            MenuType menu = sender as MenuType;

            //Pop the stack
            Stack.Pop();

            //Run the exit transition
            transitionManager.runTransition(TransitionState.exit, menu.State, menu.Queue);

            //If the stack is empty, meaning there are no more menus, exit the game
            if (Stack.Count == 0)
                Game.Exit();
            //Else if the menu is not a small menu, i.e. the background does not show behind it, run the intro transition
            else if (!(menu is SmallMenuType))
                transitionManager.runTransition(TransitionState.intro, Stack.Peek(), getMenu(Stack.Peek()).Queue);

            //If the game is not running, force update the input
            if (!(menu is PlayingGameType))
                input.updateInput();
        }

        /// <summary>
        /// This event can be attached to any event within the component, allowing the component to activate the game 
        /// menus within. 
        /// </summary>
        /// <param name="sender">The option that has been activated</param>
        /// <param name="e">A standard blank EventArgs</param>
        public void StartGame(object sender, EventArgs e)
        {
            //If there are transitions, do not start the game.  Instead, add this event to the transition manager's
            //FinishTransition handler, due to the game running in the background during the transition otherwise
            if (sender is TransitionManager)
            {
                //reset the game stack
                gameStack.Clear();
                gameStack.Push("Playing Game");

                isPlaying = true;

                //Remove the event from the transition manager's event handler
                transitionManager.FinishTransition -= StartGame;
            }
            else if (transitionManager.HasTransitions)
                transitionManager.FinishTransition += StartGame;
            //Else start the game
            else
            {
                //reset the game stack
                gameStack.Clear();
                gameStack.Push("Playing Game");

                isPlaying = true;

                //Remove the event from the transition manager's event handler
                transitionManager.FinishTransition -= StartGame;
            }
        }

        /// <summary>
        /// This event can be attached to any event within the component, allowing the component to deactivate the game 
        /// menus within, and activate the main menus. 
        /// </summary>
        /// <param name="sender">The option that has been activated</param>
        /// <param name="e">A standard blank EventArgs</param>
        public void EndGame(object sender, EventArgs e)
        {
            //reset the menu stack
            menuStack.Clear();
            menuStack.Push("Title Screen");

            isPlaying = false;
        }
        #endregion
    }
}