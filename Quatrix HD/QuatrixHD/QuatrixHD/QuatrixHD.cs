#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of QuatrixHD.
 * 
 * QuatrixHD is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * QuatrixHD is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with QuatrixHD.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
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
using QuatrixHD.Grid;
using QuatrixHD.Blocks;
using QuatrixHD.Quatrix;
using Reverb;
using QuatrixHD.Menus;
using Reverb.Screens;
using Shockwave;
using Reverb.Elements;
using Reverb.Enumerations;
using Reverb.Transitions;
using ZStorage;
using QuatrixHD.Storage;
using Mach;

namespace QuatrixHD
{
    public class QuatrixHD : Microsoft.Xna.Framework.Game
    {
        #region Fields
        /// <summary>
        /// Standard game graphics manager.
        /// </summary>
        GraphicsDeviceManager graphics;

        /// <summary>
        /// The menus component for the game, allowing a powerful managed menu system.
        /// </summary>
        MenuComponent menus;

        /// <summary>
        /// The media player component for the game, allowing access to the media library, including the ability
        /// to play any song within the library.
        /// </summary>
        MediaComponent mediaPlayer;

        /// <summary>
        /// The game manager for the game, containing any data pertinent to the actual game play.
        /// </summary>
        GameManager game;

        /// <summary>
        /// The storage component for the game, allowing for storage of data from the game.
        /// </summary>
        StorageComponent storage;

        /// <summary>
        /// The audio manager for the game, allowing audio to be loaded and distributed throughout the game.
        /// </summary>
        SoundManager audio;
        #endregion

        #region Game Functions
        public QuatrixHD()
        {
            IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 272;
            graphics.PreferredBackBufferHeight = 480;

            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            game = new GameManager(this);

            game.deactivate();

            game.GameOver += GameOver;

            game.GameOver += EndGame;

            game.Pause += PauseGame;

            Components.Add(game);

            initializeMenus();

            initializeMediaPlayer();

            initializeStorage();

            initializeAudio();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            audio.load(Content);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            saveStorage();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
        #endregion

        #region Menus
        void initializeMenus()
        {
            menus = new MenuComponent(this);

            #if WINDOWS
            //menus.setSplashScreen(new SplashScreen("Video/Splash Screen"));
            #endif

            #if ZUNE
            menus.setSplashScreen(new SplashScreen("Splash Screen/Information", 240));
            #endif

            menus.Start += StartGame;
            menus.End += EndGame;
            menus.Exit += ExitGame;
            menus.Resume += ResumeGame;

            menus.add(new TitleScreen());
            menus.add(new NewGame());
            menus.add(new Options());
            menus.add(new Sound());
            menus.add(new BrickSettings());
            menus.add(new BrickColors());
            menus.add(new MediaPlayerMenu());
            menus.add(new Pause());
            menus.add(new GameOver());
            menus.add(new Player());
            menus.add(new HighScores());
            menus.add(new BrickTextures());
            menus.add(new MediaPlaylist());
            menus.add(new Controls());

            Components.Add(menus);
        }
        #endregion

        #region Media Player
        void initializeMediaPlayer()
        {
            mediaPlayer = new MediaComponent(this);

            mediaPlayer.setMenuBackgrounds("Menus/Standard Menu", new Vector2(), Color.White, new FadeComponent(15, 1), new FadeComponent(15, -1));

            mediaPlayer.setMenuTitles("Fonts/MaturaTitle", new Vector2(60, 40), Color.Red, new FadeComponent(15, 1), new FadeComponent(15, -1));

            mediaPlayer.setMenuDisplayLists("Fonts/MaturaOptions", "Menus/Highlighter", new Rectangle(30, 100, 212, 320), Color.Red,
                                            new Vector2(30, 100), 30, new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(null, new Vector2(200, 0), 15, true),
                                            new MoveCollection(new Vector2(-200, 0), 15, false), new MoveCollection(new Vector2(200, 0), 15, false));

            mediaPlayer.setMenuBackOption("Fonts/MaturaOptions", new Vector2(136, 440), Color.Red, "Menus/Highlighter", new Vector2(136, 440), TextAlignment.center, 
                                          new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(null, new Vector2(200, 0), 15, true),
                                          new MoveCollection(new Vector2(-200, 0), 15, false), new MoveCollection(new Vector2(200, 0), 15, false));

            mediaPlayer.setMenuQueue(false);

            mediaPlayer.addMenus(menus);

            Components.Add(mediaPlayer);
        }
        #endregion

        #region Storage
        void initializeStorage()
        {
            storage = new StorageComponent("Quatrix HD");

            HighScoreData.load(storage);

            ColorData.load(storage);

            TextureData.load(storage);
        }

        void saveStorage()
        {
            HighScoreData.save(storage);

            ColorData.save(storage);

            TextureData.save(storage);
        }
        #endregion

        #region Audio
        void initializeAudio()
        {
            audio = new SoundManager();

            audio.addSound("Audio/Bloop");
            audio.addSound("Audio/Row Deleted");
        }
        #endregion

        #region Events
        /// <summary>
        /// This event gets linked to the menu's Start event, which is raised when the user chooses an option to
        /// start the game.  This event starts the game and disables the menus.
        /// </summary>
        /// <param name="sender">The MenuComponent object</param>
        /// <param name="e">A default EventArgs object</param>
        void StartGame(object sender, EventArgs e)
        {
            game.start();

            menus.EnableMenus = false;
        }

        /// <summary>
        /// This event gets linked to the game manager's Pause event, which is raised when the user activates the pause button
        /// within the game manager.  This event enables the menus and pauses the game.
        /// </summary>
        /// <param name="sender">The GameManager object</param>
        /// <param name="e">A default EventArgs object</param>
        void PauseGame(object sender, EventArgs e)
        {
            menus.EnableMenus = true;

            menus.pause();

            menus.setBackground("Paused", game.getBackground(), Color.White);
        }

        /// <summary>
        /// This event gets linked to the menu's Resume event, which is raised when teh user chooses an option that
        /// resume's the game.  This event disables the menus and re-enables the game.
        /// </summary>
        /// <param name="sender">The MenuComponent object</param>
        /// <param name="e">A default EventArgs object</param>
        void ResumeGame(object sender, EventArgs e)
        {
            menus.EnableMenus = false;

            game.resume();
        }

        /// <summary>
        /// This event gets linked to the menu's End event, which is raised when the user chooses an option that 
        /// closes the game and accesses the menus.  This event enables the menus, deactivates the game, and resets the 
        /// game's data.
        /// </summary>
        /// <param name="sender">The MenuComponent object</param>
        /// <param name="e">A default EventArgs object</param>
        void EndGame(object sender, EventArgs e)
        {
            menus.EnableMenus = true;

            game.deactivate();

            game.unload();
        }

        /// <summary>
        /// This event gets linked to the menu component's Exit event, which is raised when the user chooses an exiting 
        /// option.  This event closes the entire application.
        /// </summary>
        /// <param name="sender">The MenuComponent object</param>
        /// <param name="e">A default EventArgs object</param>
        void ExitGame(object sender, EventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// This event gets linked to the game manager's GameOver event, and accesses the menu's game over method.
        /// </summary>
        /// <param name="sender">The GameManager object</param>
        /// <param name="e">A default EventArgs object</param>
        void GameOver(object sender, EventArgs e)
        {
            menus.EnableMenus = true;

            menus.gameOver();

            menus.setBackground("Game Over", game.getBackground(), Color.White);
        }
        #endregion
    }
}
