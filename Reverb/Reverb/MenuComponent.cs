using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Reflex;
using Reverb.Components;
using Reverb.Components.InGame;
using Reverb.Enumerations;
using Reverb.Arguments;
using Reverb.Screens;
using Reverb.Transitions;

namespace Reverb
{
    public class MenuComponent : DrawableGameComponent
    {
        #region Fields
        /// <summary>
        /// An event handler to add any additional commands to the program when exiting, e.i. saving the game
        /// </summary>
        public EventHandler Exit;

        /// <summary>
        /// This event handler is called when the game is exited and the user is returned to the main menu.
        /// </summary>
        public EventHandler End;

        /// <summary>
        /// This event handler is used to pause the game, and is called when the back button is pressed (default).
        /// </summary>
        public EventHandler Pause;

        /// <summary>
        /// This event handler is used to end the game
        /// </summary>
        public EventHandler GameOver;

        /// <summary>
        /// This event handler is used to start the game
        /// </summary>
        public EventHandler Start;

        /// <summary>
        /// This event handler is used to resume the game
        /// </summary>
        public EventHandler Resume;

        /// <summary>
        /// Menus have their own separate sprite batch so that effects put on the game's sprite batch do not
        /// carry over to the menu's sprite batch.
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// Menus have their own unique input hanlder separate from the main game so that even if the user
        /// changes the game controls, the menu controls remain the same.
        /// </summary>
        InputHandler input;

        /// <summary>
        /// The transition manager is the overall manager for menu transitions.  Calls can be made to it from the 
        /// menu component, allowing for menu transitions to run at any needed time.
        /// </summary>
        TransitionManager transitionManager;

        /// <summary>
        /// A splash screen that plays while the menus load.
        /// </summary>
        SplashScreen splashScreen;

        /// <summary>
        /// This dictionary contains all registered menus of the game.  If a menu is not registered, it will not appear
        /// in the game.
        /// </summary>
        Dictionary<string, MenuType> menus;

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

        /// <summary>
        /// A bool that determines if the menus have been comletely loaded.
        /// </summary>
        volatile bool isLoaded;
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
        /// Enables or disables the menu component and all components used by the menu component.
        /// </summary>
        public bool EnableMenus
        {
            set
            {
                Enabled = value;
                Visible = value;
                input.Enabled = value;
            }
        }
        #endregion

        #region Constructors
        public MenuComponent(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            input = new InputHandler(game);
            transitionManager = new TransitionManager();
            menus = new Dictionary<string, MenuType>();
            menuStack = new Stack<string>();
            gameStack = new Stack<string>();
        }
        #endregion

        #region Overridden Methods
        public override void Initialize()
        {
            if (splashScreen == null)
            {
                initialize();
            }
            else
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(asyncronousLoad));

                splashScreen.start();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (splashScreen == null)
            {
                load();

                isLoaded = true;
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (splashScreen != null && !splashScreen.IsComplete)
            {
                splashScreen.update(input, isLoaded);
            }
            else
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

                    //If there is a registered menu, update that menu.
                    if (currentMenu != null)
                    {
                        currentMenu.update(input);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (splashScreen != null && !splashScreen.IsComplete)
            {
                splashScreen.draw(spriteBatch);
            }
            else
            {
                if (transitionManager.HasTransitions)
                {
                    transitionManager.draw(spriteBatch);
                }
                else
                {
                    MenuType currentMenu = getMenu(Stack.Peek());

                    //If there is a menu registered, draw it.
                    if (currentMenu != null)
                    {
                        currentMenu.draw(spriteBatch);

                        //If the menu's overlap value singifies that the menu beheath it is visible, draw that menu
                        if (currentMenu.IsOverlap)
                            getMenu(Stack.ToArray()[1]);
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Class Methods
        void initialize()
        {
            //Initialize the input handler
            input.Initialize();

            //hook up the needed event methods
            Start += StartGame;
            End += EndGame;

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

            transitionManager.initialize();

            getMenu("Title Screen").dynamicInitialize();

            Game.Components.Add(input);
        }

        void load()
        {
            //Load each menu
            foreach (MenuType menu in menus.Values)
            {
                menu.load(Game.Content);
            }
        }

        void asyncronousLoad(object stateInfo)
        {
            //Load each menu
            initialize();
            load();

            isLoaded = true;

            //Run the transition for the title screen, since that is the first menu to be displayed
            transitionManager.runTransition(TransitionState.intro, "Title Screen");
        }

        /// <summary>
        /// This function registers a menu with the component, allowing it to be called upon at any time.
        /// </summary>
        /// <param name="menu">The menu to be registered</param>
        public void add(MenuType menu)
        {
            menus.Add(menu.State, menu);

            menu.Back += Back;
        }

        public void setSplashScreen(SplashScreen screen)
        {
            splashScreen = screen;
            screen.initialize();
            screen.load(Game.Content);
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
        /// This function sets the menu component to display the paused menu.  This can be hooked into the main game
        /// in order to pause it and access any menus attached to the pause menu.
        /// </summary>
        public void pause()
        {
            transitionManager.runTransition(TransitionState.intro, "Paused");
            gameStack.Push("Paused");

            if (Pause != null)
                Pause(this, new EventArgs());
        }

        /// <summary>
        /// This functions sets the menu component to display the game over menu.  This can be hooked into the main game
        /// in order to set the state to game over when the game's end conditions are met.
        /// </summary>
        public void gameOver()
        {
            transitionManager.runTransition(TransitionState.intro, "Game Over");

            isPlaying = false;

            if (menuStack.Peek() != "Game Over")
                menuStack.Push("Game Over");

            if (getMenu("Game Over") != null)
                getMenu("Game Over").dynamicInitialize();

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
            ComponentType component;

            if (menu.components.TryGetValue("In Game", out component))
            {
                IInGame inGameComponent = component as IInGame;

                inGameComponent.setBackground(texture);

                inGameComponent.Color = color;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// This event is passed to each registered menu and attached to all options' event handlers within that menu
        /// </summary>
        /// <param name="sender">The option that has been activated</param>
        /// <param name="e">An Object Args that contains various information on the option that raised the event.</param>
        public void Select(OptionArgs args)
        {
            //If the selected option requires the selection transition to run, run that transition
            if (args.activateSelect)
            {
                if (args.action != OptionAction.previous)
                    transitionManager.runTransition(TransitionState.select, Stack.Peek());
                else
                    transitionManager.runTransition(TransitionState.exit, Stack.Peek());
            }

            getMenu(Stack.Peek()).reset();

            //Determine the option action
            switch (args.action)
            {
                case OptionAction.next:
                    Stack.Push(args.menuLink);
                    if (getMenu(args.menuLink) != null)
                        getMenu(args.menuLink).dynamicInitialize();
                    break;

                case OptionAction.previous:
                    Stack.Pop();

                    if (Stack.Peek() == "Playing Game")
                    {
                        if (transitionManager.HasTransitions)
                        {
                            transitionManager.FinishTransition += Resume;
                        }
                        else
                        {
                            if (Resume != null)
                                Resume(this, new EventArgs());
                        }
                    }
                    break;

                case OptionAction.startGame:
                    if (transitionManager.HasTransitions)
                    {
                        transitionManager.FinishTransition += Start;
                    }
                    else
                    {
                        if (Start != null)
                            Start(this, new EventArgs());
                    }
                    break;

                case OptionAction.endGame:
                        if (End != null)
                            End(this, new EventArgs());
                    break;

                case OptionAction.exit:
                    if (Exit != null)
                        Exit(this, new EventArgs());
                    break;
            }

            //If the option signifies that a transition needs to be run, run the transition of the current menu
            if (args.activateInro)
            {
                if (args.action != OptionAction.previous)
                    transitionManager.runTransition(TransitionState.intro, Stack.Peek());
                else
                    transitionManager.runTransition(TransitionState.revert, Stack.Peek());
            }
        }

        /// <summary>
        /// This event is passed to each registered menu and attached to the menu's back event handler
        /// </summary>
        /// <param name="sender">The menu that has been activated</param>
        /// <param name="e">A standard blank EventArgs</param>
        public void Back(MenuArgs args)
        {
            //Pop the stack
            Stack.Pop();

            //If the stack is empty, meaning there are no more menus, exit the game
            if (Stack.Count == 0)
            {
                Game.Exit();
            }
            else
            {
                //Run the exit transition
                if (args.activateExit)
                    transitionManager.runTransition(TransitionState.exit, args.identifier);

                //Else if the menu is not a small menu, i.e. the background does not show behind it, run the intro transition
                if (args.activateIntro)
                    transitionManager.runTransition(TransitionState.revert, Stack.Peek());

                //If the game is not running, force update the input
                if (!isPlaying)
                    input.updateInput();
            }
        }

        /// <summary>
        /// This event can be attached to any event within the component, allowing the component to activate the game 
        /// menus within. 
        /// </summary>
        /// <param name="sender">The option that has been activated</param>
        /// <param name="e">A standard blank EventArgs</param>
        public void StartGame(object sender, EventArgs e)
        {
            //reset the game stack
            gameStack.Clear();
            gameStack.Push("Playing Game");

            isPlaying = true;
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
