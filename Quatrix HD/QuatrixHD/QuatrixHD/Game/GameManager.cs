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
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuatrixHD.Grid;
using QuatrixHD.Blocks;
using QuatrixHD.GameTypes;
using Reflex;
using Reflex.Motions;
using Aftershock;
using Aftershock.Text;
using Aftershock.Effects;
using Aftershock.Graphics;
using System.Threading;
using QuatrixHD.Storage;
using Mach;

namespace QuatrixHD.Quatrix
{
    /// <summary>
    /// Manages the actual running of the game play, from resource management to value data.
    /// </summary>
    class GameManager : DrawableGameComponent
    {
        public static GameTypeData gameType;
        public static ScoreData data;
        public static ControlType controlType;
 
        public EventHandler GameOver;
        public EventHandler Pause;

        SpriteBatch spriteBatch;
        InputHandler input;
        IControls controls;
        HUDComponent hud;
        LoadingScreen loadingScreen;

        Texture2D background;
        Texture2D pauseButton;
        Random random;
        GridType mainGrid;
        GridType previewGrid;
        BlockType mainBlock;
        BlockType previewBlock;
        CounterType dropCounter;
        CounterType accelDropCounter;
        CounterType movementCounter;
        CounterType updateCounter;
        CounterType bonusCounter;
        Vector2 scoreVector;
        TimeSpan time;
        bool increaseBonus;
        int score;
        int level;
        int bonus;
        int totalDeleted;
        int rowsDeleted;
        int increase;

        volatile bool isLoaded;

        public GameManager(Game game)
            : base(game)
        {
            input = new InputHandler(game);
            hud = new HUDComponent(game);

            BlockType.textureAsset = "Game/Bricks/Cat-Eye Brick";

            loadingScreen = new LoadingScreen("Fonts/LindseyMedium");
        }

        #region Game Methods
        public override void Initialize()
        {
            loadingScreen.initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            loadingScreen.load(Game.Content);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!isLoaded)
            {
                loadingScreen.update();
            }
            else
            {
                time += gameTime.ElapsedGameTime;

                if (input.motionActivated("Pause"))
                    pause();

                if (controls.fall(input))
                    accelDropCounter.update();
                else
                    dropCounter.update();

                movementCounter.update();

                rotate();

                if (bonus > 1)
                    bonusCounter.update();

                if (Collision.floorCollision(mainBlock) || Collision.blockCollision(mainBlock, mainGrid))
                {
                    mainBlock.fillGrid(mainGrid);
                    updateCounter.update();
                }

                if (controls.drop(input))
                {
                    drop();
                    Update(null, null);
                }

                if (gameType.TimeLimit > TimeSpan.Zero && time >= gameType.TimeLimit)
                    gameOver("Time Limit\n    Reached");

                if (gameType.MaxRows > 0 && totalDeleted >= gameType.MaxRows)
                    gameOver("All rows\n deleted. Nice!");
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (!isLoaded)
            {
                Game.GraphicsDevice.Clear(Color.Black);

                loadingScreen.draw(spriteBatch);
            }
            else
            {
                spriteBatch.Draw(background, new Vector2(), Color.White);

                mainGrid.draw(spriteBatch);
                previewGrid.draw(spriteBatch);

                controls.draw(spriteBatch);

                spriteBatch.Draw(pauseButton, new Vector2(56, 0), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Resource Management Functions
        public void initialize()
        {
            input.Initialize();
            hud.Initialize();

            switch (controlType)
            {
                case ControlType.buttons:
                    controls = new ButtonControls();
                    break;

                case ControlType.motion:
                    controls = new TapControls();
                    break;
            }

            level = 1;
            score = 0;
            bonus = 1;

            ColorData.setColors();
            TextureData.setTexture();

            mainGrid = new GridType(new Vector2(36, 14), new Vector2(20), 10, 19);
            previewGrid = new GridType(new Vector2(12, 426), new Vector2(20), 4, 2);

            mainGrid.initialize();
            previewGrid.initialize();

            random = new Random();

            mainBlock = randomize();
            previewBlock = randomize();

            controls.initializeInput(input);
            input.addMotion("Pause", new Press(new Rectangle(52, 0, 160, 10)));

            initializeHUD();

            dropCounter = new CounterType(gameType.StartSpeed);
            dropCounter.setEvent(Drop);

            accelDropCounter = new CounterType(5);
            accelDropCounter.setEvent(AccelDrop);

            movementCounter = new CounterType(5);
            movementCounter.setEvent(Move);

            updateCounter = new CounterType(30);
            updateCounter.setEvent(Update);

            bonusCounter = new CounterType(900);
            bonusCounter.setEvent(ReduceBonus);

            Game.Components.Add(input);
            Game.Components.Add(hud);
        }

        public void load()
        {
            background = Game.Content.Load<Texture2D>("Game/Quatrix Background");

            pauseButton = Game.Content.Load<Texture2D>("Menus/Pause Bar");

            mainBlock.load(Game.Content);
            previewBlock.load(Game.Content);

            mainBlock.normal(mainGrid);

            previewBlock.preview(previewGrid);

            hud.load();

            controls.load(Game.Content);
        }

        public void asynchronousLoad(object stateInfo)
        {
            initialize();
            load();

            isLoaded = true;

            hud.Enabled = true;
            hud.Visible = true;

            input.Enabled = true;
        }

        public void unload()
        {
            Game.Components.Remove(input);
            Game.Components.Remove(hud);

            hud.unload();
            input.unload();

            background = null;
            pauseButton = null;
            random = null;
            mainGrid.unload();
            previewGrid.unload();
            mainBlock.unload();
            previewBlock.unload();

            dropCounter = null;
            accelDropCounter = null;
            movementCounter = null;
            updateCounter = null;
            bonusCounter = null;
            time = TimeSpan.Zero;
            increaseBonus = false;
            score = 0;
            level = 0;
            bonus = 0;
            totalDeleted = 0;
            rowsDeleted = 0;
            increase = 0;

            isLoaded = false;
        }

        public void start()
        {
            if (!isLoaded)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(asynchronousLoad));
            }
            else
            {
                hud.Enabled = true;
                hud.Visible = true;

                input.Enabled = true;

                mainBlock = randomize();
                previewBlock = randomize();

                previewBlock.preview(previewGrid);
                mainBlock.normal(mainGrid);
            }

            Enabled = true;
            Visible = true;
        }

        public void pause()
        {
            Enabled = false;
            Visible = false;
            hud.Enabled = false;
            hud.Visible = false;
            input.Enabled = false;

            Pause(this, new EventArgs());
        }

        public void resume()
        {
            Enabled = true;
            Visible = true;
            hud.Enabled = true;
            hud.Visible = true;
            input.Enabled = true;
        }

        public void gameOver(string message)
        {
            data.setData(message, time, score, totalDeleted, level);

            HighScoreData.addScore(data);

            GameOver(this, new EventArgs());
        }

        public void reset()
        {
            mainGrid.clearGrid();
            previewGrid.clearGrid();

            score = 0;
            level = 1;
            bonus = 1;
            increase = 0;

            movementCounter.reset();
            updateCounter.reset();
            accelDropCounter.reset();
            dropCounter.reset();
            bonusCounter.reset();

            dropCounter.changeInterval(45);

            increaseBonus = false;
        }

        public void deactivate()
        {
            Enabled = false;
            Visible = false;

            hud.Enabled = false;
            hud.Visible = false;

            input.Enabled = false;
        }

        void initializeHUD()
        {
            initializeStaticText();

            initializeContainers();

            initializeBindText();
        }

        void initializeStaticText()
        {
            if (gameType.ScoreValue == ScoreValue.score)
            {
                hud.addText(new TextType("Fonts/Lindsey", "Level:", new Vector2(110, 410), Color.Red, true));
                hud.addText(new TextType("Fonts/Lindsey", "Score:", new Vector2(110, 430), Color.Red, true));
                hud.addText(new TextType("Fonts/Lindsey", "Bonus: x", new Vector2(190, 410), Color.Red, true));
            }
            else if (gameType.ScoreValue == ScoreValue.time)
            {
                hud.addText(new TextType("Fonts/Lindsey", "Rows:", new Vector2(110, 410), Color.Red, true));
                hud.addText(new TextType("Fonts/Lindsey", "Score:", new Vector2(110, 430), Color.Red, true));
                hud.addText(new TextType("Fonts/Lindsey", "Bonus: x", new Vector2(190, 410), Color.Red, true));
                hud.addText(new TextType("Fonts/Lindsey", "Time:", new Vector2(110, 450), Color.Red, true));
            }
            else
            {
                hud.addText(new TextType("Fonts/Lindsey", "Rows:", new Vector2(110, 410), Color.Red, true));
                hud.addText(new TextType("Fonts/Lindsey", "Score:", new Vector2(110, 430), Color.Red, true));
                hud.addText(new TextType("Fonts/Lindsey", "Bonus: x", new Vector2(190, 410), Color.Red, true));
                hud.addText(new TextType("Fonts/Lindsey", "Time:", new Vector2(110, 450), Color.Red, true));
            }
        }

        void initializeContainers()
        {
            TextContainer container = new TextContainer("Fonts/LindseyLarge");

            container.addEffect(new ScaleEffect(null, new Vector2(60, 140), 0.05f, 0.2f));
            container.addEffect(new ScaleEffect(null, new Vector2(60, 140), -0.05f, 0.15f));
            container.addEffect(new PauseEffect(null, 1));
            container.addEffect(new FadeEffect(null, 0.5f, -1));

            hud.addContainer(container, "Next Level");

            container = new TextContainer("Fonts/LindseyMedium");

            container.addEffect(new ScaleEffect(null, new Vector2(90, 180), 0.05f, 0.2f));
            container.addEffect(new ScaleEffect(null, new Vector2(90, 180), -0.05f, 0.15f));
            container.addEffect(new PauseEffect(null, 1));
            container.addEffect(new FadeEffect(null, 0.5f, -1));

            hud.addContainer(container, "Bonus");

            container = new TextContainer("Fonts/LindseyMedium");

            container.addEffect(new FadeEffect(null, 0.5f, 1));
            container.addEffect(new PauseEffect(null, 2));
            container.addEffect(new FadeEffect(null, 0.5f, -1));

            hud.addContainer(container, "Bonus Expired");

            hud.addContainer(new TextContainer("Fonts/LindseyMedium", new FadeEffect(null, 0.40f, -1)), "Draw Score");

            hud.addContainer(new GraphicsContainer(Game.GraphicsDevice, 200, 20, Color.White, new FadeEffect(null, 0.40f, -1)), "Draw Flash");
        }

        void initializeBindText()
        {
            if (gameType.ScoreValue == ScoreValue.score)
            {
                ValueTextType<int> dynamicText = new ValueTextType<int>(1, "Fonts/Lindsey", new Vector2(160, 410), Color.Red, true);
                dynamicText.ChangeValue += UpdateLevel;
                hud.addText(dynamicText);

                dynamicText = new ValueTextType<int>(0, "Fonts/Lindsey", new Vector2(160, 430), Color.Red, true);
                dynamicText.ChangeValue += UpdateScore;
                dynamicText.addEffect(new CountingEffect(null, 0.5f));
                hud.addText(dynamicText);

                dynamicText = new ValueTextType<int>(1, "Fonts/Lindsey", new Vector2(250, 410), Color.Red, true);
                dynamicText.ChangeValue += UpdateBonus;
                hud.addText(dynamicText);
            }
            else if (gameType.ScoreValue == ScoreValue.time)
            {
                ValueTextType<int> dynamicText = new ValueTextType<int>(gameType.RowsToAdvance, "Fonts/Lindsey", new Vector2(160, 410), Color.Red, true);
                dynamicText.ChangeValue += UpdateRowsToDelete;
                hud.addText(dynamicText);

                dynamicText = new ValueTextType<int>(0, "Fonts/Lindsey", new Vector2(160, 430), Color.Red, true);
                dynamicText.ChangeValue += UpdateScore;
                dynamicText.addEffect(new CountingEffect(null, 0.5f));
                hud.addText(dynamicText);

                dynamicText = new ValueTextType<int>(1, "Fonts/Lindsey", new Vector2(250, 410), Color.Red, true);
                dynamicText.ChangeValue += UpdateBonus;
                hud.addText(dynamicText);

                ValueTextType<string> dynamicTime = new ValueTextType<string>("0:00", "Fonts/Lindsey", new Vector2(160, 450), Color.Red, true);
                dynamicTime.ChangeValue += UpdateTime;
                hud.addText(dynamicTime);
            }
            else if (gameType.ScoreValue == ScoreValue.rows)
            {
                ValueTextType<int> dynamicText = new ValueTextType<int>(0, "Fonts/Lindsey", new Vector2(160, 410), Color.Red, true);
                dynamicText.ChangeValue += UpdateRowsDeleted;
                hud.addText(dynamicText);

                dynamicText = new ValueTextType<int>(0, "Fonts/Lindsey", new Vector2(160, 430), Color.Red, true);
                dynamicText.ChangeValue += UpdateScore;
                dynamicText.addEffect(new CountingEffect(null, 0.5f));
                hud.addText(dynamicText);

                dynamicText = new ValueTextType<int>(1, "Fonts/Lindsey", new Vector2(250, 410), Color.Red, true);
                dynamicText.ChangeValue += UpdateBonus;
                hud.addText(dynamicText);

                ValueTextType<string> dynamicTime = new ValueTextType<string>("0:00", "Fonts/Lindsey", new Vector2(160, 450), Color.Red, true);
                dynamicTime.ChangeValue += UpdateTime;
                hud.addText(dynamicTime);
            }
        }
        #endregion

        #region Class Functions
        public Texture2D getBackground()
        {
            RenderTarget2D renderTarget = new RenderTarget2D(GraphicsDevice, 272, 480, 1, SurfaceFormat.Color);

            GraphicsDevice.SetRenderTarget(0, renderTarget);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(), Color.White);

            mainGrid.draw(spriteBatch);
            previewGrid.draw(spriteBatch);
            controls.draw(spriteBatch);

            spriteBatch.End();

            hud.draw();

            GraphicsDevice.SetRenderTarget(0, null);

            return renderTarget.GetTexture();
        }

        BlockType randomize()
        {
            BlockType randomBlock;

            switch (random.Next(1, 8))
            {
                case 1:
                    randomBlock = new OBlock();
                    break;

                case 2:
                    randomBlock = new IBlock();
                    break;

                case 3:
                    randomBlock = new TBlock();
                    break;

                case 4:
                    randomBlock = new LBlock();
                    break;

                case 5:
                    randomBlock = new JBlock();
                    break;

                case 6:
                    randomBlock = new SBlock();
                    break;

                case 7:
                    randomBlock = new ZBlock();
                    break;

                default:
                    randomBlock = new LBlock();
                    break;
            }

            randomBlock.load(Game.Content);

            return randomBlock;
        }

        void spawn()
        {
            previewBlock.clearGrid(previewGrid);

            mainBlock = previewBlock;

            if (Collision.overlapCollision(mainBlock.SpawnPoint, mainGrid))
            {
                mainBlock.normal(mainGrid);

                previewBlock = randomize();
                previewBlock.preview(previewGrid);

                gameOver("Overflow!");
            }
            else
            {
                mainBlock.normal(mainGrid);

                previewBlock = randomize();
                previewBlock.preview(previewGrid);
            }
        }

        void rotate()
        {
            Rectangle hitBox = mainBlock.BoundingBox;
            hitBox.X = hitBox.X * 20 + 36;
            hitBox.Y = hitBox.Y * 20 + 14;
            hitBox.Width *= 20;
            hitBox.Height *= 20;

            //If the press is active, rotate the block
            if (controls.rotate(input, hitBox))
            {
                //If a rotation will not result in a collision
                if (!Collision.rotationCollision(mainBlock, mainGrid))
                    mainBlock.rotate(mainGrid);

                SoundManager.playSound("Audio/Bloop");
            }
        }

        void drop()
        {
            while (!Collision.floorCollision(mainBlock) && !Collision.blockCollision(mainBlock, mainGrid))
                mainBlock.drop(mainGrid);

            score += 25 * bonus;
        }

        void checkScore()
        {
            if (rowsDeleted >= gameType.RowsToAdvance)
            {
                rowsDeleted = 0;

                level++;

                if (dropCounter.Interval - 5 > gameType.MaxSpeed)
                    dropCounter.increaseInterval(-5);

                dropCounter.reset();

                if (gameType.ScoreValue == ScoreValue.score)
                    hud.displayText("Next Level", "Next Level", new Vector2(60, 140), Color.Yellow);
                else
                    hud.displayText("Next Level", "Speeding Up!", new Vector2(60, 140), Color.Yellow);
            }
        }

        void checkBonus()
        {
            if (increase > 0)
            {
                if (increaseBonus)
                {
                    bonus++;
                    hud.displayText("Bonus", "Bonus!", new Vector2(90, 180), Color.Cyan);
                    bonusCounter.reset();
                }
                else
                    increaseBonus = true;
            }
            else
                increaseBonus = false;
        }
        #endregion

        #region Events
        void Drop(object sender, EventArgs e)
        {
            if (!Collision.floorCollision(mainBlock) && !Collision.blockCollision(mainBlock, mainGrid))
                mainBlock.drop(mainGrid);
        }

        void AccelDrop(object sender, EventArgs e)
        {
            if (!Collision.floorCollision(mainBlock) && !Collision.blockCollision(mainBlock, mainGrid))
                mainBlock.drop(mainGrid);
        }

        void Move(object sender, EventArgs e)
        {
            //If the correct input is pressed, move the block according to the direction
            if (controls.moveLeft(input, mainBlock.BoundingBox))
            {
                if (!Collision.blockCollision(mainBlock, mainGrid, Direction.left))
                    mainBlock.move(mainGrid, Direction.left);
            }
            else if (controls.moveRight(input, mainBlock.BoundingBox))
            {
                if (!Collision.blockCollision(mainBlock, mainGrid, Direction.right))
                    mainBlock.move(mainGrid, Direction.right);
            }
        }

        void Update(object sender, EventArgs e)
        {
            increase = mainGrid.deleteRow(ref scoreVector);

            if (increase > 0)
            {
                hud.displayGraphic("Draw Flash", new Vector2(scoreVector.X, scoreVector.Y), Color.White);
                rowsDeleted += (increase / 100);
                totalDeleted += (increase / 100);

                SoundManager.playSound("Audio/Row Deleted");
            }

            checkBonus();

            score += increase * bonus;

            if (increase > 0)
                hud.displayText("Draw Score", (increase * bonus).ToString(), new Vector2(115, scoreVector.Y - 10), Color.Blue);

            movementCounter.reset();
            checkScore();

            spawn();
        }

        void ReduceBonus(object sender, EventArgs e)
        {
            bonus--;

            hud.displayText("Bonus Expired", "Bonus Expired", new Vector2(40, 180), new Color(0, 255, 255, 0));
        }
        #endregion

        #region Binds
        void UpdateLevel(object sender, ref int hudLevel)
        {
            hudLevel = level;
        }

        void UpdateScore(object sender, ref int hudScore)
        {
            IScreenObject text = sender as IScreenObject;

            if (text != null && hudScore != score)
                text.startEffect();

            hudScore = score;
        }

        void UpdateBonus(object sender, ref int hudBonus)
        {
            hudBonus = bonus;
        }

        void UpdateRowsToDelete(object sender, ref int hudRows)
        {
            hudRows = gameType.MaxRows - totalDeleted;
        }

        void UpdateRowsDeleted(object sender, ref int hudRows)
        {
            hudRows = totalDeleted;
        }

        void UpdateTime(object sender, ref string hudTime)
        {
            if (time.Seconds > 9)
                hudTime = time.Minutes.ToString() + ":" + time.Seconds.ToString();
            else
                hudTime = time.Minutes.ToString() + ":0" + time.Seconds.ToString();
        }
        #endregion
    }
}
