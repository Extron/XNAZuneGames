/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of Quatrix.
 * 
 * Quatrix is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Quatrix is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Quatrix.  If not, see <http://www.gnu.org/licenses/>.
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
using System.Diagnostics;
using Quatrix.Menus;
using ZHandler;
using GameMenus;
using ZHUD;
using ZMediaPlayer;
using ZStorage;

namespace Quatrix
{
    public class Quatrix : Microsoft.Xna.Framework.Game
    {
        //This region contains all of the variables of the game
        #region Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHandlerComponent input;
        MenuComponent menus;
        HUDComponent hud;
        StorageComponent storage;
        HighScoreData highScores;
        ColorData colors;
        TextureData texture;
        SoundEffect bloop;
        SoundEffect gameOver;
        SoundEffect rowDeleted;
        SpriteFont font;
        Texture2D background;
        GameBlockNew block;
        GameBlockNew preview;
        Texture2D clearBrick;
        Random random = new Random();
        GameGrid grid = new GameGrid();
        Color backgroundColor = Color.White;
        int[] rowCounter = new int[19];
        int randomNumber;
        int movementNumber;
        int movementCounter = 0;
        int updateCounter = 0;
        int dropCounter = 0;
        int accelCounter = 0;
        int scoreIn = 0;
        int score = 0;
        int multiplier = 1;
        int level = 1;
        int dropTime = 45;
        int sensitivity = 8;
        float multiplierExpiration;
        bool setNormalbool = true;
        bool rotation = false;
        bool playSound = true;
        bool increaseMultiplier = false;

        MediaLibraryComponent mediaLibrary;
        #endregion

        //This region contains code for the Update and Draw methods for playing the game.
        #region Playing Game Data
        void playingGame(GameTime gameTime)
        {
            setPreview();

            if (setNormalbool)
                setNormal();

            if (dropCounter == dropTime)
            {
                setCoordinates();
                blockDrop();
                dropCounter = 0;
            }

            if (dropCounter > dropTime)
                dropCounter = 0;

            if (input.getButton(ButtonType.a, true) && rotation)
            {
                setCoordinates();
                blockRotation();
                
                if (playSound)
                    bloop.Play();
            }

            if (input.getButton(ButtonType.down, false) && accelCounter == 5)
            {
                setCoordinates();
                accelDrop();
                accelCounter = 0;
            }

            if (accelCounter > 5)
                accelCounter = 0;

            if (movementCounter == sensitivity)
            {
                setCoordinates();
                blockMovement();
                movementCounter = 0;
            }

            setCoordinates();

            setNormalbool = false;

            rotation = true;

            update(gameTime);
            
            if (input.getButton(ButtonType.back, true))
            {
                enableMenus(true);
                menus.Current = "Paused";
                rotation = false;
            }
        }

        void playingGameDraw()
        {
            spriteBatch.Draw(background, new Vector2(0), backgroundColor);
            drawBlocks();
            drawPreview();
            drawGrid();
        }

        void loadGame()
        {
            initializeGrid();
            initializeHUD();
            initializeBlocks();

            loadSounds();
            loadText();

            background = this.Content.Load<Texture2D>("Quatrix Background");

            enableHUD(true);
        }
        #endregion

        //This region contains code for the Update and Draw methods for the menus, as well as any movement functions.
        #region Menu Data

        #region Load and Initialize Methods
        void initializeMenus()
        {
            menus = new MenuComponent(this);
            enableMenus(true);
            Components.Add(menus);
        }

        void loadMenus()
        {
            //Load menus with null event handlers           
            menus.add(new Options(Content, @"Menus\Standard Menu"));
            menus.add(new About(Content, @"Menus\About Menu"));
            menus.add(new Controls(Content, input, @"Menus\Standard Menu"));

            //Load menus with non-null event handlers
            menus.add(new TitleScreen(Content, @"Menus\Main Menu"));
            menus.getMenu("Title Screen").options[0].Selected += StartGame;

            menus.add(new TouchPad(menus.getMenu("Options"), Content, @"Menus\Small Menu"));
            menus.getMenu("Touch Pad").options[0].Selected += TouchOn;
            menus.getMenu("Touch Pad").options[1].Selected += TouchOff;

            menus.add(new Sound(menus.getMenu("Options"), Content, @"Menus\Small Menu"));
            menus.getMenu("Sound").options[0].Selected += SoundOn;
            menus.getMenu("Sound").options[1].Selected += SoundOff;

            menus.add(new Fail(Content, @"Menus\In Game Menu"));
            menus.getMenu("Game Over").options[0].Selected += RestartGame;
            menus.getMenu("Game Over").options[1].Selected += QuitGame;

            menus.add(new HighScores(Content, @"Menus\Standard Menu"));
            menus.getMenu("High Scores").options[0].Selected += ResetHighScores;

            menus.add(new MediaPlayerMenu(Content, @"Menus\Standard Menu"));
            menus.getMenu("Media Player Menu").options[0].Selected += AccessMediaPlayer;
            menus.getMenu("Media Player Menu").options[1].Selected += ManagePlaylist;

            menus.add(new Shuffle(menus.getMenu("Media Player Menu"), Content, @"Menus\Small Menu"));
            menus.getMenu("Shuffle").options[0].Selected += ShuffleOn;
            menus.getMenu("Shuffle").options[1].Selected += ShuffleOff;

            menus.add(new Repeat(menus.getMenu("Media Player Menu"), Content, @"Menus\Small Menu"));
            menus.getMenu("Repeat").options[0].Selected += RepeatOn;
            menus.getMenu("Repeat").options[1].Selected += RepeatOff;

            menus.add(new PlaylistMenu(Content, @"Menus\Media Menu"));
            menus.getMenu("Playlist Menu").options[0].Selected += AccessPlaylist;
            menus.getMenu("Playlist Menu").options[1].Selected += AccessArtists;
            menus.getMenu("Playlist Menu").options[2].Selected += AccessAlbums;
            menus.getMenu("Playlist Menu").options[3].Selected += AccessSongs;
            menus.getMenu("Playlist Menu").options[4].Selected += AccessGenres;
            menus.getMenu("Playlist Menu").options[5].Selected += ClearPlayList;

            menus.add(new Colors(Content, @"Menus\Standard Menu"));
            menus.getMenu("Colors").options[2].Selected += ResetColors;

            menus.add(new BrickStyles(Content, @"Menus\Standard Menu"));
            menus.getMenu("Brick Styles").options[0].Selected += SetCatEye;
            menus.getMenu("Brick Styles").options[1].Selected += SetQuatrix;
            menus.getMenu("Brick Styles").options[2].Selected += SetRound;
            menus.getMenu("Brick Styles").options[3].Selected += SetClassic;

            //Load special menus
            SelectLevel temp = new SelectLevel(Content, @"Menus\Select Level Menu");
            temp.Selected += LevelSelect;
            menus.add(temp);

            //Load Color Menus
            menus.add(new OBlockColor(Content, @"Menus\Standard Menu"));
            menus.getMenu("O-Block Color").options[3].Selected += SaveColors;

            menus.add(new TBlockColor(Content, @"Menus\Standard Menu"));
            menus.getMenu("T-Block Color").options[3].Selected += SaveColors;

            menus.add(new IBlockColor(Content, @"Menus\Standard Menu"));
            menus.getMenu("I-Block Color").options[3].Selected += SaveColors;

            menus.add(new ZBlockColor(Content, @"Menus\Standard Menu"));
            menus.getMenu("Z-Block Color").options[3].Selected += SaveColors;

            menus.add(new SBlockColor(Content, @"Menus\Standard Menu"));
            menus.getMenu("S-Block Color").options[3].Selected += SaveColors;

            menus.add(new LBlockColor(Content, @"Menus\Standard Menu"));
            menus.getMenu("L-Block Color").options[3].Selected += SaveColors;

            menus.add(new JBlockColor(Content, @"Menus\Standard Menu"));
            menus.getMenu("J-Block Color").options[3].Selected += SaveColors;

            menus.add(new BlockList(Content, @"Menus\Standard Menu"));
        }

        void enableMenus(bool value)
        {
            menus.Enabled = value;
        }
        #endregion

        #region Event Handler Methods
        void StartGame(object sender, EventArgs e)
        {
            loadGame();
            enableMenus(false);
        }

        void QuitGame(object sender, EventArgs e)
        {
            backgroundColor = Color.White;
            clearGrid();
            clearBlock();
            resetText();
            enableHUD(false);
        }

        void ResumeGame(object sender, EventArgs e)
        {
            backgroundColor = Color.White;
            enableMenus(false);
        }

        void RestartGame(object sender, EventArgs e)
        {
            backgroundColor = Color.White;
            playSound = true;
            clearGrid();
            clearBlock();
            resetText();
            enableMenus(false);
        }

        void SaveColors(object sender, EventArgs e)
        {
            colors.setColors();

            saveColors();
        }

        void ResetColors(object sender, EventArgs e)
        {
            colors.setDefault();

            saveColors();

            GameBlockNew.texture = Content.Load<Texture2D>(@"Bricks\Cat-Eye 2 Brick");

            texture.setTexture(@"Bricks\Cat-Eye 2 Brick");

            saveTexture();
        }

        void ResetHighScores(object sender, EventArgs e)
        {
            HighScores menu = sender as HighScores;

            highScores.clearHighScores();

            if (storage.SaveData<int>(highScores.data, "highscores.lst"))
                menu.resetDisplay();
        }

        void TouchOn(object sender, EventArgs e)
        {
            input.Touch = true;
            menus.Touch = true;
            sensitivity = 12;
        }

        void TouchOff(object sender, EventArgs e)
        {
            input.Touch = false;
            menus.Touch = false;
            sensitivity = 8;
        }

        void SoundOn(object sender, EventArgs e)
        {
            playSound = true;
        }

        void SoundOff(object sender, EventArgs e)
        {
            playSound = false;
        }

        void ShuffleOn(object sender, EventArgs e)
        {
            mediaLibrary.Shuffle = true;
        }

        void ShuffleOff(object sender, EventArgs e)
        {
            mediaLibrary.Shuffle = false;
        }

        void RepeatOn(object sender, EventArgs e)
        {
            mediaLibrary.Repeat = true;
        }

        void RepeatOff(object sender, EventArgs e)
        {
            mediaLibrary.Repeat = false;
        }

        void LevelSelect(object sender, EventArgs e)
        {
            SelectLevel temp = sender as SelectLevel;

            loadGame();
            level = temp.Level;
            levelSetup();
            menus.Current = "Playing Game";
            enableMenus(false);
        }

        void SetCatEye(object sender, EventArgs e)
        {
            GameBlockNew.texture = Content.Load<Texture2D>(@"Bricks\Cat-Eye 2 Brick");

            texture.setTexture(@"Bricks\Cat-Eye 2 Brick");

            saveTexture();
        }

        void SetQuatrix(object sender, EventArgs e)
        {
            GameBlockNew.texture = Content.Load<Texture2D>(@"Bricks\Quatrix Brick");

            texture.setTexture(@"Bricks\Quatrix Brick");

            saveTexture();
        }

        void SetRound(object sender, EventArgs e)
        {
            GameBlockNew.texture = Content.Load<Texture2D>(@"Bricks\Round Brick");

            texture.setTexture(@"Bricks\Round Brick");

            saveTexture();
        }

        void SetClassic(object sender, EventArgs e)
        {
            GameBlockNew.texture = Content.Load<Texture2D>(@"Bricks\Blank Brick");

            texture.setTexture(@"Bricks\Blank Brick");

            saveTexture();
        }
        void ManagePlaylist(object sender, EventArgs e)
        {
            mediaLibrary.Visible = true;

            mediaLibrary.updateGameState("Playlist Menu");
        }      

        void AccessMediaPlayer(object sender, EventArgs e)
        {
            mediaLibrary.Visible = true;

            mediaLibrary.updateGameState("Media Player");
        }

        void AccessPlaylist(object sender, EventArgs e)
        {
            mediaLibrary.Visible = true;

            mediaLibrary.updateGameState("Playlist");

            ((MediaLibraryPlaylistMenu)mediaLibrary.menus.getMenu("Playlist")).getItems(ListDirection.down);
        }

        void AccessArtists(object sender, EventArgs e)
        {
            mediaLibrary.Visible = true;

            mediaLibrary.updateGameState("Artists");

            ((MediaLibraryMenuType)mediaLibrary.menus.getMenu("Artists")).getItems(ListDirection.down);
        }

        void AccessAlbums(object sender, EventArgs e)
        {
            mediaLibrary.Visible = true;

            mediaLibrary.updateGameState("Albums");

            ((MediaLibraryMenuType)mediaLibrary.menus.getMenu("Albums")).getItems(ListDirection.down);
        }

        void AccessSongs(object sender, EventArgs e)
        {
            mediaLibrary.Visible = true;

            mediaLibrary.updateGameState("Songs");

            ((MediaLibraryMenuType)mediaLibrary.menus.getMenu("Songs")).getItems(ListDirection.down);
        }

        void AccessGenres(object sender, EventArgs e)
        {
            mediaLibrary.Visible = true;

            mediaLibrary.updateGameState("Genres");

            ((MediaLibraryMenuType)mediaLibrary.menus.getMenu("Genres")).getItems(ListDirection.down);
        }

        void ClearPlayList(object sender, EventArgs e)
        {
            mediaLibrary.clearPlaylist();

            ((Playlist)menus.getMenu("Playlist")).clearList();
        }
        #endregion

        #region Update and Draw Functions
        void exitGame()
        {
            if (menus.Current == "Exit Game")
                Exit();
        }

        void pausedDraw()
        {
            playingGameDraw();
        }

        void failedDraw()
        {
            playingGameDraw();
        }
        #endregion

        #endregion

        //This region contains code for the HUD component, such as initializing and loading, as well as Event Handlers
        #region HUD Data
        void initializeHUD()
        {
            hud = new HUDComponent(this, GraphicsDevice);

            BaseTextType temp;

            temp = new TextType("Score", new Vector2(190, 90), Color.Red);
            hud.addText(temp);

            temp = new TextType("Score Number", new Vector2(190, 105), Color.Red);
            temp = new NumberWrapper(temp, score);
            hud.addText(temp);

            temp = new TextType("Level", new Vector2(190, 130), Color.Red);
            hud.addText(temp);

            temp = new TextType("Level Number", new Vector2(210, 145), Color.Red);
            temp = new NumberWrapper(temp, level);
            hud.addText(temp);

            temp = new TextType("Bonus", new Vector2(190, 170), Color.Red);
            hud.addText(temp);

            temp = new TextType("Multiplier Number", new Vector2(210, 185), Color.Red);
            temp = new NumberWrapper(temp, multiplier);
            hud.addText(temp);

            temp = new TextType("High\nScore", new Vector2(190, 210), Color.Red);
            hud.addText(temp);

            temp = new TextType("High Score Number", new Vector2(190, 245), Color.Red);
            temp = new NumberWrapper(temp, highScores.getHighScore());
            hud.addText(temp);

            temp = new TextType("Next Level", new Vector2(25, 160), Color.White);
            temp = new FloatWrapper(temp, new Vector2(0, -3), 3.0f);
            hud.addText(temp);

            temp = new TextType("Next Level Number", new Vector2(90, 190), Color.White);
            temp = new NumberWrapper(temp, level);
            temp = new FloatWrapper(temp, new Vector2(0, -3), 3.0f);
            hud.addText(temp);

            temp = new TextType("Add Score Number", new Vector2(80, 240), Color.Yellow);
            temp = new NumberWrapper(temp, 0);
            temp = new FloatWrapper(temp, new Vector2(0, -3), 1.0f);
            hud.addText(temp);

            temp = new TextType("Multiplier Bonus", new Vector2(100, 100), Color.Orange);
            temp = new ScaleWrapper(temp, 1, 1.5f, 0.5f, 3);
            hud.addText(temp);

            temp = new TextType("Add Multiplier Number", new Vector2(100, 130), Color.Orange);
            temp = new NumberWrapper(temp, level);
            temp = new ScaleWrapper(temp, 1, 1.5f, 0.5f, 3);
            hud.addText(temp);

            temp = new TextType("Bonus Expired", new Vector2(25, 120), Color.Orange);
            temp = new FadeWrapper(temp, 4);
            hud.addText(temp);

            Components.Add(hud);
        }

        void loadText()
        {
            //Load Standard Font
            hud.loadFont(font);

            //Load Special Font
            hud.loadFont(Content.Load<SpriteFont>("LindseyMedium"), "Next Level");
            hud.loadFont(Content.Load<SpriteFont>("LindseyMedium"), "Next Level Number");
            hud.loadFont(Content.Load<SpriteFont>("Lindsey"), "Multiplier Bonus");
            hud.loadFont(Content.Load<SpriteFont>("Lindsey"), "Add Multiplier Number");
            hud.loadFont(Content.Load<SpriteFont>("Lindsey"), "Bonus Expired");

            //Center text
            hud.getText("Score Number").centerText("Score Number");
            hud.getText("Level Number").centerText("Level Number");
            hud.getText("Multiplier Number").centerText("Multiplier Number");
        }

        void enableHUD(bool value)
        {
            if (value)
            {
                hud.Enabled = true;
                hud.Visible = true;
            }
            else
            {
                hud.Enabled = false;
                hud.Visible = false;
            }
        }

        void updateScore(GameTime gameTime)
        {
            hud.getText("Score Number").addToNumber(scoreIn * multiplier);

            if (scoreIn > 0)
            {
                updateFloatingScore();
                hud.getText("Add Score Number").changeNumber(scoreIn * multiplier);
                hud.getText("Add Score Number").startText(gameTime);
            }

            score += scoreIn * multiplier;
        }

        void updateMultiplier(GameTime gameTime)
        {
            if (scoreIn > 0)
            {
                if (increaseMultiplier)
                {
                    multiplier++;

                    hud.getText("Add Multiplier Number").changeNumber(multiplier);
                    hud.getText("Add Multiplier Number").startText(gameTime);

                    hud.getText("Multiplier Bonus").startText(gameTime);

                    multiplierExpiration = (float)gameTime.TotalGameTime.TotalSeconds;
                }
                else
                    increaseMultiplier = true;
            }
            else
                increaseMultiplier = false;

            hud.getText("Multiplier Number").changeNumber(multiplier);
        }

        void reduceMultiplier(GameTime gameTime)
        {
            int time = (int)(gameTime.TotalGameTime.TotalSeconds - multiplierExpiration);
           
            if (time > 15 && multiplier > 1)
            {
                multiplier--;
                hud.getText("Multiplier Number").changeNumber(multiplier);

                hud.getText("Bonus Expired").startText(gameTime);

                if (multiplier == 1)
                    multiplierExpiration = 0;
                else
                    multiplierExpiration = (float)gameTime.TotalGameTime.TotalSeconds;
            }
        }

        void updateLevel(GameTime gameTime)
        {
            hud.getText("Level Number").addToNumber(1);
            hud.getText("Next Level Number").addToNumber(1);

            hud.getText("Next Level").startText(gameTime);
            hud.getText("Next Level Number").startText(gameTime);

            level += 1;
        }

        void updateFloatingScore()
        {
            for (int i = rowCounter.Length - 1; i >= 0; i--)
            {
                if (rowCounter[i] == 0)
                {
                    hud.getText("Add Score Number").Vector = new Vector2(80, (10 * (i)));
                    break;
                }
            }
        }

        void updateHighScore()
        {
            hud.getText("High Score Number").changeNumber(highScores.getHighScore());
        }

        void resetText()
        {
            hud.getText("Score Number").changeNumber(0);
            hud.getText("Level Number").changeNumber(1);
            hud.getText("Multiplier Number").changeNumber(1);
            hud.getText("Next Level Number").changeNumber(1);
        }
        #endregion

        //This region contains initialization and load code for blocks, sound, and the grid.
        #region Initialization and Load Data
        void initializeInput()
        {
            input = new InputHandlerComponent(this);
            Components.Add(input);
            input.Touch = false;
            input.Enabled = false;
        }

        void initializeGrid()
        {
            int coordinateCounter = 1;

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    grid.GetCell(j, i).square = new Rectangle(9 + (16 * j), 10 + (16 * i), 16, 16);
                    grid.GetCell(j, i).coordinate = coordinateCounter;
                    grid.GetCell(j, i).brick = this.Content.Load<Texture2D>("Clear Brick");
                    coordinateCounter++;
                }
            }
        }

        void initializeBlocks()
        {
            block = randomize();
            preview = randomize();
        }

        void loadSounds()
        {
            bloop = this.Content.Load<SoundEffect>("Bloop1");
            gameOver = this.Content.Load<SoundEffect>("Game Over");
            rowDeleted = this.Content.Load<SoundEffect>("Row Deleted");
        }
        #endregion

        //This region contains the code that randomizez the blocks. 
        #region Randomizing Code

        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        GameBlockNew randomize()
        {
            randomNumber = RandomNumber(1, 8);
            GameBlockNew blockIn;

            switch (randomNumber)
            {
                case 1:
                    blockIn = new OBlock();
                    break;

                case 2:
                    blockIn = new IBlock();
                    break;

                case 3:
                    blockIn = new TBlock();
                    break;

                case 4:
                    blockIn = new LBlock();
                    break;

                case 5:
                    blockIn = new JBlock();
                    break;

                case 6:
                    blockIn = new SBlock();
                    break;

                case 7:
                default:
                    blockIn = new ZBlock();
                    break;
            }
            blockIn.load(Content);
            return blockIn;
        }

        #endregion

        //This region contains code for block movement, such as drop, accelerated drop, and rotation.
        #region Block Movement Code

        void blockDrop()
        {
            block.Drop(grid);
        }

        void accelDrop()
        {
            block.Drop(grid);
        }

        void blockMovement()
        {
            if (input.getButton(ButtonType.left, false))
                movementNumber = -16;
            else if (input.getButton(ButtonType.right, false))
                movementNumber = 16;
            else movementNumber = 0;

            block.Move(movementNumber, grid);
        }

        void blockRotation()
        {
            if (MovementFunctions.RotationCollision(block, grid))
            {
                block.Rotate();
            }
        }
        #endregion  

        //This region contains drawing methods that draw the block preview, the block, and text.
        #region Block Draw Code

        void drawBlocks()
        {
            block.Draw(spriteBatch);
        }

        void drawPreview()
        {
            preview.Draw(spriteBatch);
        }

        void setPreview()
        {
            preview.Preview();
        }

        void setNormal()
        {
            block.Normal();
        }

        #endregion

        //This region contains any code pertaining to the game grid, coordinate system, and the row counter.
        #region Grid, Coordinate, and Row Data
        //Convert block to the game grid
        void gridFill()
        {
            block.FillGridCells(grid);
        }

        //Assigns each grid cell a coordinate
        void setCoordinates()
        {
            for (int j = 0; j < 19; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        block.SetBlockCoordinates(grid.GetCell(k, j));
                    }
                }
        }

        //Removes any full rows, moves all blocks above, and adds score
        void deleteBlocks()
        {
            grid.deleteRow(rowCounter, clearBrick, out scoreIn, rowDeleted, playSound);
        }

        //Goes through each row and counts how many blocks are in the row
        void countRows()
        {
            grid.rowCount(rowCounter);
        }

        //Draws the game grid
        void drawGrid()
        {
            grid.drawGrid(spriteBatch);
        }

        //Clears the grid values, used for game restarts
        void clearGrid()
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    grid.GetCell(j, i).brick = this.Content.Load<Texture2D>("Clear Brick");
                    grid.GetCell(j, i).squareFilled = false;
                    grid.GetCell(j, i).isCounted = false;
                    grid.GetCell(j, i).color = Color.White;
                }

                rowCounter[i] = 0;
            }
        }

        //Clears and resets the block data
        void clearBlock()
        {
            block = randomize();
            preview = randomize();
            setNormalbool = true;
            score = 0;
            level = 1;
            multiplier = 1;
            multiplierExpiration = 0;
            dropTime = 45;
            dropCounter = 0;
        }
        #endregion

        //This region contains the code for the main game update mehtod, fail state checking, and level setup and update.  
        #region Update, Fail State, and Level Data
        void update(GameTime gameTime)
        {
            //If the block has reached the bootom or hit another block, check counter
            if (block.Brick4.Rect.Y == 298 || !MovementFunctions.BlockDropCollision(block, grid))
            {
                //If the update counter reaches 30, convert current block to grid, get new block, and check rows
                if (updateCounter == 30)
                {
                    gridFill(); //Converts the block to the grid
                    block = preview; //Sets the preview block to the main block
                    preview = randomize(); //Randomizes the next preview block
                    setNormalbool = true;
                    countRows(); //Conts the number of blocks in a row
                    deleteBlocks(); //Deletes any full rows
                    updateMultiplier(gameTime); //Updates the multiplier
                    updateScore(gameTime); //Updates the score
                    levelUpdate(gameTime); //Update the level
                    updateCounter = 0; //Resets the counter
                }
                //Else update the counter
                else
                    updateCounter++;             
            }
            //Else update counters
            else
            {
                reduceMultiplier(gameTime);
                dropCounter++;
                accelCounter++;
            }

            movementCounter++;
            failState();
        }

        void failState()
        {
            if (block.Brick1.Coordinate <= 10 && !MovementFunctions.BlockDropCollision(block, grid))
            {
                enableMenus(true);
                menus.Current = "Game Over";
                gameOver.Play();
                saveHighScore();
                updateHighScore();
            }
        }

        void levelUpdate(GameTime gameTime)
        {
            if ((level * 1000) <= score && level < 9)
            {
                updateLevel(gameTime);
                dropTime -= 5;
            }

            if ((level * 1000) <= score && level >= 9)
            {
                updateLevel(gameTime);
            }
        }

        void levelSetup()
        {
            hud.getText("Level Number").changeNumber(level);
            hud.getText("Next Level Number").changeNumber(level);
            dropTime = dropTime - 5 * (level - 1);
        }

        #endregion

        //This region contains the code for any media player applications
        #region Media Library Code
        void initializeLibrary()
        {
            mediaLibrary = new MediaLibraryComponent(this, menus);

            Components.Add(mediaLibrary);
        }

        void loadLibrary()
        {
            mediaLibrary.addMenu(new ArtistsMenu(Content, @"Menus\Media Menu"));
            mediaLibrary.addMenu(new AlbumsMenu(Content, @"Menus\Media Menu"));
            mediaLibrary.addMenu(new SongsMenu(Content, @"Menus\Media Menu"));
            mediaLibrary.addMenu(new GenresMenu(Content, @"Menus\Media Menu"));
            mediaLibrary.addMenu(new MediaPlayer(Content, @"Menus\Main Menu"));
            mediaLibrary.addMenu(new PausedMediaPlayer(Content, @"Menus\Blank Menu"));
            mediaLibrary.addMenu(new Playlist(Content, @"Menus\Media Menu"));

            loadPauseMenu();
        }

        void updateLibrary()
        {
            mediaLibrary.updateInput(menus.Input);

            switch (menus.Current)
            {
                case "Paused":
                case "Media Player":
                case "Artists":
                case "Albums":
                case "Songs":
                case "Genres":
                    mediaLibrary.Visible = true;
                    break;

                default:
                    mediaLibrary.Visible = false;
                    break;
            }
        }

        void loadPauseMenu()
        {
            Pause temp = new Pause(Content, @"Menus\In Game Menu");

            temp.player = menus.getMenu("Paused Player") as PausedMediaPlayer;

            menus.add(temp);
            menus.getMenu("Paused").options[0].Selected += ResumeGame;
            menus.getMenu("Paused").options[1].Selected += QuitGame;
        }
        #endregion

        //This region contains the code for the storage device, such as high scores, color schemes, etc.
        #region Storage Code
        void initializeStorage()
        {
            storage = new StorageComponent("Quatrix");          
        }

        void loadStorage()
        {
            #region Load High Scores
            highScores = new HighScoreData();

            highScores.data = storage.LoadData<int>("highscores.lst");

            if (highScores.data == null)
            {
                highScores.data = new DataType<int>(10);

                storage.SaveData<int>(highScores.data, "highscores.lst");
            }

            HighScores menu = menus.getMenu("High Scores") as HighScores;

            menu.setDisplay(highScores.data.list);
            #endregion

            #region Load Colors
            colors = new ColorData();

            colors.data.redData = storage.LoadData<int>("redcolors.lst");
            colors.data.greenData = storage.LoadData<int>("greencolors.lst");
            colors.data.blueData = storage.LoadData<int>("bluecolors.lst");

            if (colors.data.redData == null || colors.data.greenData == null || colors.data.blueData == null)
            {
                colors.data.redData = new DataType<int>(7);
                colors.data.greenData = new DataType<int>(7);
                colors.data.blueData = new DataType<int>(7);

                colors.loadData();

                storage.SaveData<int>(colors.data.redData, "redcolors.lst");
                storage.SaveData<int>(colors.data.greenData, "greencolors.lst");
                storage.SaveData<int>(colors.data.blueData, "bluecolors.lst");
            }
            else
                colors.assignColors();
            #endregion

            #region Load Block Texture
            texture = new TextureData();

            texture.data = storage.LoadData<string>("texture.lst");

            if (texture.data == null)
            {
                texture.data = new DataType<string>(1);

                texture.loadData();

                texture.setTexture(@"Bricks\Cat-Eye 2 Brick");

                storage.SaveData<string>(texture.data, "texture.lst");
            }
            else
                texture.assignTexture(Content);
            #endregion
        }

        void saveHighScore()
        {
            if (highScores.addHighScore(score))
            {
                storage.SaveData<int>(highScores.data, "highscores.lst");

                HighScores menu = menus.getMenu("High Scores") as HighScores;

                menu.setDisplay(highScores.data.list);
            }
        }

        void saveColors()
        {
            storage.SaveData<int>(colors.data.redData, "redcolors.lst");
            storage.SaveData<int>(colors.data.greenData, "greencolors.lst");
            storage.SaveData<int>(colors.data.blueData, "bluecolors.lst");
        }

        void saveTexture()
        {
            storage.SaveData<string>(texture.data, "texture.lst");
        }
        #endregion

        //All of the main overrided methods of the game class
        #region Main Game Methods
        public Quatrix()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 240;
            graphics.PreferredBackBufferHeight = 320;

            graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            initializeMenus();
            initializeInput();
            initializeLibrary();
            initializeStorage();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Lindsey");

            clearBrick = this.Content.Load<Texture2D>("Clear Brick");

            GameBlockNew.texture = Content.Load<Texture2D>(@"Bricks\Cat-Eye 2 Brick");

            loadMenus();

            loadLibrary();

            loadStorage();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            updateLibrary();

            if (menus.Current == "Playing Game")
            {
                playingGame(gameTime);
                input.Enabled = true;
            }
            else
                input.Enabled = false;

            exitGame();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (menus.Current)
            {
                case "Playing Game":
                    playingGameDraw();
                    break;

                case "Paused":
                    pausedDraw();
                    break;

                case "Game Over":
                    failedDraw();
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
