/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of Snakez.
 * 
 * Snakez is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Snakez is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Snakez.  If not, see <http://www.gnu.org/licenses/>.
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
using ZHUD;
using ZSounds;
using ZStorage;

namespace Snakez
{
    public class Snakez : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameTime time;
        MenuComponent menus;
        InputHandlerComponent input;
        StorageComponent storage;
        HighScoreData scores;
        LevelProgressData currentLevel;
        LevelType level;
        HUDComponent hud;
        MessageType startMessage;
        int lives = 3;
        int score = 0;
        int levelNumber = 1;
        int timer = 0;
        int number = RandomGenerator.randomNumber(150, 300);
        bool advancedLevel = false;
        bool started;
        bool initialized;

        //This region contains data for the main gameplay
        #region Playing Game Data
        /// <summary>
        /// Initialize all non graphical game assets.
        /// </summary>
        void initializeGame()
        {
            //Create and initialize level one
            level = new LevelType(LevelManager.backgroundAsset, LevelManager.wallAsset);
            level.initialize(LevelManager.getLevel(levelNumber));

            //Set the level's events
            level.setGameOverEvent(SnakeCollision);
            level.setEatEvent(EatenFood);
            level.setObjectEvent(NextLevel, "Key");
            level.setObjectEvent(ExtraLife, "Extra Life");

            //Initialize the start message
            initializeStartMessage();

            //Set the start message to display the proper text
            setStartMessage();

            //Set the text to display the proper text
            resetText();

            //Set the variable to signify that initialization is complete
            initialized = true;
        }

        /// <summary>
        /// Load all graphical game assets.
        /// </summary>
        void loadGame()
        {
            startMessage.load(Content);
            level.load(Content);
        }

        /// <summary>
        /// Reset the level if the snake dies.
        /// </summary>
        void resetLevel()
        {
            //Reset the level
            level.resetLevel();

            //Set the variable to signify complete initialization
            initialized = true;
        }

        /// <summary>
        /// Sets the game at a new level.  This is used for level-ups.
        /// </summary>
        /// <param name="n">The level number to set the game at.</param>
        void setLevel(int n)
        {
            string levelAsset = "Level Data/Level " + n;
            level = new LevelType(LevelManager.backgroundAsset, LevelManager.wallAsset);
           
            //Initialize and load the new level
            level.initialize(LevelManager.getLevel(levelNumber));
            level.load(Content);

            //Set the level's events
            level.setGameOverEvent(SnakeCollision);
            level.setEatEvent(EatenFood);
            level.setObjectEvent(NextLevel, "Key");
            level.setObjectEvent(ExtraLife, "Extra Life");

            //Set the variable to tell that initialzation is complete
            initialized = true;

            //Set the variable that initializing a new level is complete
            advancedLevel = false;
        }

        /// <summary>
        /// The main game update loop.
        /// </summary>
        void playingGame()
        {
            //If the game has been started, run the normal loop
            if (started)
            {
                //Update the level
                level.update(input, score);

                //Play a squeak at a random interval
                randomSqueak();

                //If the pause button is pressed, call the menu pause method
                menus.pause();
            }
            //Else run the start message update loop
            else
            {
                //If the start button is pressed, start the game
                if (input.getButton("Play/Pause", true))
                {
                    started = true;

                    if (!initialized)
                        resetLevel();
                }
            }
        }

        /// <summary>
        /// The main game draw method, which draws all in-game objects.
        /// </summary>
        void playingGameDraw()
        {
            //Draw the main level background
            level.draw(spriteBatch);

            //If the game has not been started, draw the start message
            if (!started)
                startMessage.draw(spriteBatch);
        }

        /// <summary>
        /// Initializes the start message for the game.
        /// </summary>
        void initializeStartMessage()
        {
            startMessage = new MessageType();

            InGameMessage temp;

            temp = new InGameMessage("Press the Play/Pause \n     button to start", "Fonts/Lindsey", new Vector2(30, 70), Color.Red);
            startMessage.addMessage(temp, "Press Start");

            temp = new InGameMessage("You lost a life", "Fonts/Lindsey", new Vector2(70, 140), Color.Black);
            temp.displayMessage(false);
            startMessage.addMessage(temp, "Lost Life");

            temp = new InGameMessage("Next Level!", "Fonts/Lindsey", new Vector2(70, 140), Color.Black);
            temp.displayMessage(false);
            startMessage.addMessage(temp, "Next Level");
        }

        /// <summary>
        /// Sets the start message with the proper text, depending on the variables of the game.
        /// </summary>
        void setStartMessage()
        {
            started = false;

            if (lives == 3 && !advancedLevel)
            {
                startMessage.displayMessage("Next Level", false);
                startMessage.displayMessage("Lost Life", false);
            }

            if (lives != 3)
            {
                startMessage.displayMessage("Next Level", false);
                startMessage.displayMessage("Lost Life", true);
            }

            if (advancedLevel)
            {
                startMessage.displayMessage("Next Level", true);
                startMessage.displayMessage("Lost Life", false);
            }
        }

        /// <summary>
        /// Draws all the in-game objects to a render target, which produces a texture of the game.  This is used for setting
        /// in game menus to display the game in the background.
        /// </summary>
        /// <returns>A generated texture of the game's objects at their current position.</returns>
        Texture2D BackgroundTexture()
        {
            RenderTarget2D renderTarget = new RenderTarget2D(GraphicsDevice, 240, 320, 1, SurfaceFormat.Color);

            GraphicsDevice.SetRenderTarget(0, renderTarget);

            spriteBatch.Begin();
            playingGameDraw();
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(0, null);

            return renderTarget.GetTexture();
        }
        #endregion

        //This region contains data for the sound effects
        #region Sound Data
        void initializeSounds()
        {
            SoundManager.addSound("Munch");
            SoundManager.addSound("Game Over");
            SoundManager.addSound("Game Over 1");
            SoundManager.addSound("Key Jingle 3");
            SoundManager.addSound("Level Up 2");
            SoundManager.addSound("Squeak 1");
        }

        void loadSounds()
        {
            SoundManager.load(Content, "Sound");
        }
        #endregion

        //This region contains data for the food
        #region Food Data
        #region Functions
        void randomSqueak()
        {
            if (number == timer)
            {
                SoundManager.playSound("Squeak 1");

                number = RandomGenerator.randomNumber(150, 300);
                timer = 0;
            }

            if (timer > number)
                timer = 0;

            timer++;
        }
        #endregion
        #endregion

        //This region contains all menu data
        #region Menu Data
        #region Load and Initialize Methods
        void initializeMenus()
        {
            menus = new MenuComponent(this);
            Components.Add(menus);

            menus.Exit += Exit;

            menus.add(new PlayingGameType());
            menus.add(new Options());
            menus.add(new About());
            menus.add(new TitleScreen());
            menus.add(new GameOver());
            menus.add(new Paused());
            menus.add(new TouchPad(menus.getMenu("Options")));
            menus.add(new Sound(menus.getMenu("Options")));
            menus.add(new SnakeOptions());
            menus.add(new SnakeColor());
            menus.add(new SelectLevel(SelectLevel));
            menus.add(new HighScores());

            loadControlsSystem();
        }

        void initializeAssets()
        {
            AssetManager.addFontAsset("Lindsey");
            AssetManager.addFontAsset("LindseyLarge");
            AssetManager.addFontAsset("LindseySmall");
            AssetManager.addFontAsset("LindseyAbout");

            AssetManager.addTextureAsset("Menus/Title Screen");
            AssetManager.addTextureAsset("Menus/Menu Background");
            AssetManager.addTextureAsset("Menus/Small Menu");
            AssetManager.addTextureAsset("Menus/In Game Menu");
            AssetManager.addTextureAsset("Menus/Highlight");
            AssetManager.addTextureAsset("Menus/Center Highlight");
            AssetManager.addTextureAsset("Sliders/Slider");
            AssetManager.addTextureAsset("Sliders/Slide Nub");
            AssetManager.addTextureAsset("Previews/Snake Preview");
        }

        void setEvents()
        {
            menus.Pause += PauseGame;
            menus.GameOver += GameOver;
            menus.Start += StartGame;
            menus.ExitGame += QuitGame;

            menus.getMenu("Touch Pad").setEvent(TouchOn, 0);
            menus.getMenu("Touch Pad").setEvent(TouchOff, 1);

            menus.getMenu("Sound").setEvent(SoundOn, 0);
            menus.getMenu("Sound").setEvent(SoundOff, 1);

            menus.getMenu("Options").setEvent(LoadScores, 3);

            menus.getMenu("High Scores").setEvent(ClearScores, 0);
        }

        void loadControlsSystem()
        {
            /*
            SideToSideSystemType system = new SideToSideSystemType("How to Play");
            system.add(new Controls());
            system.add(new Goals1());
            system.add(new Goals2());

            menus.addSystem(system);
            */
        }
        #endregion

        #region Event Handlers
        void StartGame(object sender, EventArgs e)
        {
            initializeGame();
            loadGame();
            enableHUD(true);
        }

        void QuitGame(object sender, EventArgs e)
        {
            level = new LevelType(LevelManager.backgroundAsset, LevelManager.wallAsset);
            level.initialize(LevelManager.getLevel(levelNumber));
            level.load(Content);
            score = 0;
            lives = 3;
            levelNumber = 1;
            resetText();
            enableHUD(false);
        }

        void TouchOn(object sender, EventArgs e)
        {
            input.Touch = true;
        }

        void TouchOff(object sender, EventArgs e)
        {
            input.Touch = false;
        }

        void SoundOn(object sender, EventArgs e)
        {
            SoundManager.IsEnabled = true;
        }

        void SoundOff(object sender, EventArgs e)
        {
            SoundManager.IsEnabled = false;
        }

        void ClearScores(object sender, EventArgs e)
        {
            scores.clearScores();

            HighScores menu = menus.getMenu("High Scores") as HighScores;

            if (menu != null)
                menu.loadDisplayList();
        }

        void LoadScores(object sender, EventArgs e)
        {
            HighScores menu = menus.getMenu("High Scores") as HighScores;

            if (menu != null)
                menu.loadDisplayList();
        }

        void SelectLevel(object sender, SelectLevelArgs args)
        {
            score = LevelManager.getScore(args.level - 1);

            lives = 3;

            levelNumber = args.level;
        }

        void PauseGame(object sender, EventArgs e)
        {
            menus.setBackground("Paused", BackgroundTexture(), Color.Gray);
        }

        void GameOver(object sender, EventArgs e)
        {
            menus.setBackground("Game Over", BackgroundTexture(), Color.Gray);

            lives = 3;
            score = LevelManager.getScore(levelNumber - 1);
        }

        void Exit(object sender, EventArgs e)
        {
            this.Exit();
        }
        #endregion
        #endregion

        //This region contains all in-game events
        #region Events
        void EatenFood(object sender, FoodArgs args)
        {
            SnakeType snake = sender as SnakeType;

            FoodType foodEaten = args.getFood(snake.Vector);

            snake.addSegment(foodEaten.Growth);

            foodEaten.spawn(level.Grid);

            SoundManager.playSound("Munch");

            updateScore(foodEaten);
        }

        void SnakeCollision(object sender, EventArgs e)
        {
            if (lives > 0)
            {
                lives--;

                setStartMessage();

                SoundManager.playSound("Game Over 1");

                initialized = false;
            }
            else if (lives == 0)
            {
                menus.gameOver();

                scores.addScore(score);

                saveHighScores();

                SoundManager.playSound("Game Over");

                initialized = false;
            }

            hud.getText("Lives Number").changeNumber(lives);
        }

        void NextLevel(object sender, EventArgs e)
        {
            SoundManager.playSound("Level Up 2");

            levelNumber++;

            hud.getText("Level Number").changeNumber(levelNumber);

            advancedLevel = true;

            setStartMessage();

            setLevel(levelNumber);

            saveCurrentLevel();
        }

        void ExtraLife(object sender, EventArgs e)
        {
            lives++;

            hud.getText("Lives Number").changeNumber(lives);
        }
        #endregion

        //This region contains all HUD data
        #region HUD data
        void initializeHUD()
        {
            hud = new HUDComponent(this, GraphicsDevice);

            BaseTextType temp;

            temp = new BaseTextType("Score:", "Fonts/LindseySmall", new Vector2(10, 5), Color.Blue);
            hud.addText(temp);

            temp = new BaseTextType("Score Number", "Fonts/LindseySmall", new Vector2(55, 5), Color.Blue);
            temp = new NumberWrapper(temp, score);
            hud.addText(temp);

            temp = new BaseTextType("Lives:", "Fonts/LindseySmall", new Vector2(100, 5), Color.Blue);
            hud.addText(temp);

            temp = new BaseTextType("Lives Number", "Fonts/LindseySmall", new Vector2(145, 5), Color.Blue);
            temp = new NumberWrapper(temp, lives);
            hud.addText(temp);

            temp = new BaseTextType("Level:", "Fonts/LindseySmall", new Vector2(170, 5), Color.Blue);
            hud.addText(temp);

            temp = new BaseTextType("Level Number", "Fonts/LindseySmall", new Vector2(220, 5), Color.Blue);
            temp = new NumberWrapper(temp, levelNumber);
            hud.addText(temp);

            temp = new BaseTextType("Add Score Number", "Fonts/LindseySmall", new Vector2(60, 15), Color.Yellow);
            temp = new NumberWrapper(temp, 0);
            temp = new FloatWrapper(temp, new Vector2(0, 3), 1.0f);
            temp = new FadeWrapper(temp, 1.0f, -1);
            temp = new ScaleWrapper(temp, 1.02f, 1.0f, 1.0f);
            hud.addText(temp);

            Components.Add(hud);
        }

        void enableHUD(bool enabled)
        {
            hud.Enabled = enabled;
            hud.Visible = enabled;
        }

        void updateScore(FoodType food)
        {
            BaseTextType text = hud.getText("Add Score Number");

            if (!text.DrawText)
            {
                text.changeNumber(food.Score);
                text.startText(time);
            }
            else
                text.addToNumber(food.Score);

            score += food.Score;

            hud.getText("Score Number").changeNumber(score);
        }

        void resetText()
        {
            hud.getText("Score Number").changeNumber(score);
            hud.getText("Lives Number").changeNumber(lives);
            hud.getText("Level Number").changeNumber(levelNumber);
        }
        #endregion

        //This region contains all storage data
        #region Storage
        void initializeStorage()
        {
            storage = new StorageComponent("Snakez");
            scores = new HighScoreData();
            currentLevel = new LevelProgressData();
        }

        void loadStorage()
        {
            //Load the high scores
            //If the storage manager cannot find a file, create and save one
            if (!(scores.populate(storage.LoadData<int>("highscores.lst"))))
            {
                DataType<int> scoreList = new DataType<int>(10);

                scores.populate(scoreList);

                storage.SaveData<int>(scoreList, "highscores.lst");
            }

            //Load the current level
            //If the storage manager cannot find a file, create and save one
            if (!(currentLevel.populate(storage.LoadData<int>("level.lst"))))
            {
                DataType<int> savedLevel = new DataType<int>(1);
                savedLevel.list.Add(1);

                currentLevel.populate(savedLevel);

                storage.SaveData<int>(currentLevel.CurrentLevelData, "level.lst");
            }
        }

        void saveHighScores()
        {
            storage.SaveData<int>(scores.Scores, "highscores.lst");
        }

        void saveCurrentLevel()
        {
            currentLevel.setCurrentLevel(levelNumber);

            storage.SaveData<int>(currentLevel.CurrentLevelData, "level.lst");
        }
        #endregion

        public Snakez()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 240;
            graphics.PreferredBackBufferHeight = 320;

            graphics.ApplyChanges();

            input = new InputHandlerComponent(this);

            Components.Add(input);
        }

        protected override void Initialize()
        {
            initializeMenus();
            initializeAssets();
            initializeHUD();
            enableHUD(false);
            initializeSounds();
            initializeStorage();
            loadStorage();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            setEvents();
            loadSounds();

            LevelManager.loadLevels(Content);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            time = gameTime;

            if (menus.IsPlaying)
                playingGame();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (menus.IsPlaying)
                playingGameDraw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
