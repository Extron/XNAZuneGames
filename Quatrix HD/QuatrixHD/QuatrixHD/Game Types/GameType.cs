using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aftershock;
using QuatrixHD.Grid;
using QuatrixHD.Blocks;
using QuatrixHD.Quatrix;
using Microsoft.Xna.Framework;
using Reflex;
using Microsoft.Xna.Framework.Graphics;
using Mach;
using QuatrixHD.GameTypes;
using Microsoft.Xna.Framework.Content;
using QuatrixHD.Storage;
using Reflex.Motions;
using Aftershock.Text;
using Aftershock.Effects;
using Aftershock.Graphics;

namespace QuatrixHD.GameTypes
{
    class GameType
    {
        #region Fields
        /// <summary>
        /// This event is raised when the game has reached a game over state.
        /// </summary>
        public EventHandler GameOver;

        /// <summary>
        /// The game type's HUD component.  Each game type as a unique HUD.
        /// </summary>
        protected HUDComponent hud;

        /// <summary>
        /// The game type's controls, which can be different depending on the game type.
        /// </summary>
        protected IControls controls;

        /// <summary>
        /// An object that contains numerical data on the specific values of the game type.
        /// </summary>
        protected GameData data;

        /// <summary>
        /// An object that stores data pertinent to the high score list.
        /// </summary>
        protected ScoreData scoreData;

        /// <summary>
        /// The game type's unique background.
        /// </summary>
        protected Texture2D background;

        /// <summary>
        /// The main grid of the game.
        /// </summary>
        protected GridType mainGrid;

        /// <summary>
        /// The preview grid of the game.
        /// </summary>
        protected GridType previewGrid;

        /// <summary>
        /// The main Tetris block.
        /// </summary>
        protected BlockType mainBlock;

        /// <summary>
        /// The block that is displayed in the preview grid.
        /// </summary>
        protected BlockType previewBlock;

        /// <summary>
        /// A random number generator, used to choose the next block.
        /// </summary>
        protected Random random;

        /// <summary>
        /// The counter for the block's fall.
        /// </summary>
        protected CounterType fallCounter;

        /// <summary>
        /// The counter for the block's accelerated fall.
        /// </summary>
        protected CounterType accelFallCounter;

        /// <summary>
        /// The counter for the block's lateral movement.
        /// </summary>
        protected CounterType movementCounter;

        /// <summary>
        /// The counter for the update loop, which checks for row deletion and updates the score.
        /// </summary>
        protected CounterType updateCounter;

        /// <summary>
        /// The counter that determines the length of the bonus.
        /// </summary>
        protected CounterType bonusCounter;

        /// <summary>
        /// The vector of the row that was deleted.
        /// </summary>
        protected Vector2 scoreVector;

        /// <summary>
        /// A time span variable that keeps track of the game's elapsed time.
        /// </summary>
        protected TimeSpan time;

        /// <summary>
        /// Determines whether to increase the bonus or not.
        /// </summary>
        protected bool increaseBonus;

        /// <summary>
        /// The player's score.
        /// </summary>
        protected int score;

        /// <summary>
        /// The player's current level.
        /// </summary>
        protected int level;

        /// <summary>
        /// The player's current bonus.
        /// </summary>
        protected int bonus;

        /// <summary>
        /// The total number of rows the player has deleted.
        /// </summary>
        protected int totalDeleted;

        /// <summary>
        /// The number of rows the player has deleted within the level.
        /// </summary>
        protected int rowsDeleted;

        /// <summary>
        /// The increase of the score after a row is deleted.
        /// </summary>
        protected int increase;

        /// <summary>
        /// A reference to the game manager's input handler.
        /// </summary>
        private InputHandler input;
        #endregion

        #region Constructors
        public GameType(GameData gameData)
        {
            data = gameData;
        }
        #endregion

        #region Public Methods
        public void initialize(Game game, InputHandler inputHandler, ControlType controlType)
        {
            input = inputHandler;

            input.Initialize();
            hud.Initialize();

            initializeControls(controlType);

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

            initializeHUD(game.GraphicsDevice);

            fallCounter = new CounterType(data.StartSpeed);
            fallCounter.setEvent(Drop);

            accelFallCounter = new CounterType(5);
            accelFallCounter.setEvent(AccelDrop);

            movementCounter = new CounterType(5);
            movementCounter.setEvent(Move);

            updateCounter = new CounterType(30);
            updateCounter.setEvent(Update);

            bonusCounter = new CounterType(900);
            bonusCounter.setEvent(ReduceBonus);

            game.Components.Add(hud);
        }
        #endregion

        #region Virtual Methods
        public virtual void initializeControls(ControlType controlType)
        {
            switch (controlType)
            {
                case ControlType.buttons:
                    controls = new ButtonControls();
                    break;

                case ControlType.motion:
                    controls = new TapControls();
                    break;
            }
        }

        public void initializeGame()
        {
        }
        #endregion

        #region Game Methods
        public virtual void initialize(Game game, InputHandler inputHandler, ControlType controlType)
        {
            input = inputHandler;

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

            initializeHUD(game.GraphicsDevice);

            fallCounter = new CounterType(data.StartSpeed);
            fallCounter.setEvent(Drop);

            accelFallCounter = new CounterType(5);
            accelFallCounter.setEvent(AccelDrop);

            movementCounter = new CounterType(5);
            movementCounter.setEvent(Move);

            updateCounter = new CounterType(30);
            updateCounter.setEvent(Update);

            bonusCounter = new CounterType(900);
            bonusCounter.setEvent(ReduceBonus);

            game.Components.Add(hud);
        }

        public virtual void load(ContentManager content)
        {
            background = content.Load<Texture2D>("Game/Quatrix Background");

            mainBlock.load(content);
            previewBlock.load(content);

            mainBlock.normal(mainGrid);

            previewBlock.preview(previewGrid);

            hud.load();

            controls.load(content);
        }

        public virtual void unload()
        {
            hud.unload();

            background = null;
            random = null;
            mainGrid.unload();
            previewGrid.unload();
            mainBlock.unload();
            previewBlock.unload();

            fallCounter = null;
            accelFallCounter = null;
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
        }

        public virtual void update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime;

            if (controls.fall(input))
                accelFallCounter.update();
            else
                fallCounter.update();

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

            if (data.TimeLimit > TimeSpan.Zero && time >= data.TimeLimit)
                gameOver("Time Limit\n    Reached");

            if (data.MaxRows > 0 && totalDeleted >= data.MaxRows)
                gameOver("All rows\n deleted. Nice!");
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(), Color.White);

            mainGrid.draw(spriteBatch);
            previewGrid.draw(spriteBatch);
        }

        public virtual void gameOver(string message)
        {
            scoreData.setData(message, time, score, totalDeleted, level);

            HighScoreData.addScore(scoreData);

            GameOver(this, new EventArgs());
        }

        public virtual void reset()
        {
            mainGrid.clearGrid();
            previewGrid.clearGrid();

            score = 0;
            level = 1;
            bonus = 1;
            increase = 0;

            movementCounter.reset();
            updateCounter.reset();
            accelFallCounter.reset();
            fallCounter.reset();
            bonusCounter.reset();

            fallCounter.changeInterval(45);

            increaseBonus = false;
        }
        #endregion

        #region Class Methods
        protected virtual void initializeHUD(GraphicsDevice graphics)
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

            hud.addContainer(new GraphicsContainer(graphics, 200, 20, Color.White, new FadeEffect(null, 0.40f, -1)), "Draw Flash");
        }

        protected virtual BlockType randomize()
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

        protected virtual void spawn()
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

        protected virtual void rotate()
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

        protected virtual void drop()
        {
            while (!Collision.floorCollision(mainBlock) && !Collision.blockCollision(mainBlock, mainGrid))
                mainBlock.drop(mainGrid);

            score += 25 * bonus;
        }

        protected virtual void checkScore()
        {
            if (rowsDeleted >= data.RowsToAdvance)
            {
                rowsDeleted = 0;

                level++;

                if (fallCounter.Interval - 5 > data.MaxSpeed)
                    fallCounter.increaseInterval(-5);

                fallCounter.reset();

                hud.displayText("Next Level", data.LevelUpMessage, new Vector2(60, 140), Color.Yellow);
            }
        }

        protected virtual void checkBonus()
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
        protected virtual void Drop(object sender, EventArgs e)
        {
            if (!Collision.floorCollision(mainBlock) && !Collision.blockCollision(mainBlock, mainGrid))
                mainBlock.drop(mainGrid);
        }

        protected virtual void AccelDrop(object sender, EventArgs e)
        {
            if (!Collision.floorCollision(mainBlock) && !Collision.blockCollision(mainBlock, mainGrid))
                mainBlock.drop(mainGrid);
        }

        protected virtual void Move(object sender, EventArgs e)
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

        protected virtual void Update(object sender, EventArgs e)
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

        protected virtual void ReduceBonus(object sender, EventArgs e)
        {
            bonus--;

            hud.displayText("Bonus Expired", "Bonus Expired", new Vector2(40, 180), new Color(0, 255, 255, 0));
        }
        #endregion

        #region Binds
        protected virtual void UpdateLevel(object sender, ref int hudLevel)
        {
            hudLevel = level;
        }

        protected virtual void UpdateScore(object sender, ref int hudScore)
        {
            IScreenObject text = sender as IScreenObject;

            if (text != null && hudScore != score)
                text.startEffect();

            hudScore = score;
        }

        protected virtual void UpdateBonus(object sender, ref int hudBonus)
        {
            hudBonus = bonus;
        }

        protected virtual void UpdateRowsToDelete(object sender, ref int hudRows)
        {
            hudRows = data.MaxRows - totalDeleted;
        }

        void UpdateRowsDeleted(object sender, ref int hudRows)
        {
            hudRows = totalDeleted;
        }

        protected virtual void UpdateTime(object sender, ref string hudTime)
        {
            if (time.Seconds > 9)
                hudTime = time.Minutes.ToString() + ":" + time.Seconds.ToString();
            else
                hudTime = time.Minutes.ToString() + ":0" + time.Seconds.ToString();
        }
        #endregion
    }

    class GameData
    {
        ScoreValue scoreValue;
        TimeSpan timeLimit;
        string levelUpMessage;
        int maximumRows;
        int rowsToAdvance;
        int startSpeed;
        int maximumSpeed;

        public GameData(ScoreValue value, TimeSpan limit, string levelMessage, int maxRows, int advanceRows, int speed, int maxSpeed)
        {
            scoreValue = value;
            timeLimit = limit;
            levelUpMessage = levelMessage;
            maximumRows = maxRows;
            rowsToAdvance = advanceRows;
            startSpeed = speed;
            maximumSpeed = maxSpeed;
        }

        public ScoreValue ScoreValue
        {
            get { return scoreValue; }
        }

        public TimeSpan TimeLimit
        {
            get { return timeLimit; }
        }

        public string LevelUpMessage
        {
            get { return levelUpMessage; }
        }

        public int MaxRows
        {
            get { return maximumRows; }
        }

        public int RowsToAdvance
        {
            get { return rowsToAdvance; }
        }

        public int StartSpeed
        {
            get { return startSpeed; }
        }

        public int MaxSpeed
        {
            get { return maximumSpeed; }
        }
    }
}
