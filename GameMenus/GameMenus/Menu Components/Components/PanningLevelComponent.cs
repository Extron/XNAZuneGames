using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class PanningLevelComponent : IComponent//, ISelectLevel
    {
        public const string identifier = "Panning Select Level";

        public EventHandler<EventArgs> Selected;
        public int level;

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        ArrowButtonComponent arrows;
        StringType text;
        SpriteFont font;
        string asset;
        int max;

        #region Implemented Properties
        public TransitionWrapper Intro
        {
            set { intro = value; }
        }

        public TransitionWrapper Select
        {
            set { select = value; }
        }

        public TransitionWrapper Exit
        {
            set { exit = value; }
        }

        public bool HasIntro
        {
            get
            {
                return (intro != null);
            }
        }

        public bool HasSelect
        {
            get
            {
                return (select != null);
            }
        }

        public bool HasExit
        {
            get
            {
                return (exit != null);
            }
        }

        public string Identifier
        {
            get { return identifier; }
        }
        #endregion

        #region Constructors
        public PanningLevelComponent(string fontAsset, Vector2 textVector, Color textColor, int maxLevel)
        {
            arrows = new ArrowButtonComponent();
            text = new StringType("", textVector, textColor);
            asset = fontAsset;
            max = maxLevel;
        }
        #endregion

        #region Implemented Functions
        public void initialize()
        {
            arrows.initialize();
            level = 1;

            text.text = level.ToString();
        }

        public void load()
        {
            font = AssetManager.getFont(asset);

            arrows.load();
        }

        public void update(InputHandlerComponent i)
        {
            arrows.update(i);

            if (i.getButton("Select", true))
            {
                if (Selected != null)
                    Selected(this, new EventArgs());
            }
        }

        public void reset()
        {
        }


        public void draw(SpriteBatch spriteBatch)
        {
            arrows.draw(spriteBatch);
            text.draw(spriteBatch, font);
        }

        public void updateTransitions(TransitionState state)
        {
            switch (state)
            {
                case TransitionState.intro:
                    if (intro != null)
                        intro.update(this);
                    break;

                case TransitionState.selected:
                    if (select != null)
                        select.update(this);
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        exit.update(this);
                    break;
            }
        }

        public void drawTransitions(SpriteBatch spriteBatch, TransitionState state)
        {
            switch (state)
            {
                case TransitionState.intro:
                    if (intro != null)
                        intro.draw(spriteBatch, this);
                    break;

                case TransitionState.selected:
                    if (select != null)
                        select.draw(spriteBatch, this);
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        exit.draw(spriteBatch, this);
                    break;
            }
        }

        public bool completedTransitions(TransitionState state)
        {
            bool temp = false;

            switch (state)
            {
                case TransitionState.intro:
                    temp = intro.isComplete();
                    break;

                case TransitionState.selected:
                    temp = select.isComplete();
                    break;

                case TransitionState.exit:
                    temp = exit.isComplete();
                    break;
            }

            return temp;
        }
        #endregion

        #region Class Functions
        public void setArrows(ArrowType leftArrow, ArrowType rightArrow)
        {
            rightArrow.Selected += PressArrows;
            leftArrow.Selected += PressArrows;

            arrows.add(rightArrow, ArrowDirection.right);
            arrows.add(leftArrow, ArrowDirection.left);
        }

        public void PressArrows(object sender, EventArgs e)
        {
            ArrowType arrow = (ArrowType)sender;

            switch (arrow.direction)
            {
                case ArrowDirection.left:
                    if (level > 1)
                        level--;
                    break;

                case ArrowDirection.right:
                    if (level < max)
                        level++;
                    break;
            }

            text.text = level.ToString();
        }
        #endregion
    }
}