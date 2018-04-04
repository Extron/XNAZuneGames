using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class ArrowButtonComponent : IComponent
    {
        public const string identifier = "Arrow Buttons";

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        Dictionary<ArrowDirection, ArrowType> arrows;

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
        public ArrowButtonComponent()
        {
            arrows = new Dictionary<ArrowDirection, ArrowType>();
        }
        #endregion

        #region Implemented Functions
        public void initialize()
        {
        }

        public void load()
        {
            foreach (ArrowType arrow in arrows.Values)
                arrow.load();
        }

        public void update(InputHandlerComponent i)
        {
            ArrowDirection direction = ArrowDirection.left;

            foreach (ArrowType arrow in arrows.Values)
                arrow.update();

            if (i.getButton("Left", true))
            {
                direction = ArrowDirection.left;
            }
            else if (i.getButton("Right", true))
            {
                direction = ArrowDirection.right;
            }
            else if (i.getButton("Up", true))
            {
                direction = ArrowDirection.up;
            }
            else if (i.getButton("Down", true))
            {
                direction = ArrowDirection.down;
            }
            else 
                return;

            try
            {
                getArrow(direction).press();
            }
            catch
            {
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (ArrowType arrow in arrows.Values)
                arrow.draw(spriteBatch);
        }

        public void reset()
        {
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
                    else
                        draw(spriteBatch);
                    break;

                case TransitionState.selected:
                    if (select != null)
                        select.draw(spriteBatch, this);
                    else
                        draw(spriteBatch);
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        exit.draw(spriteBatch, this);
                    else
                        draw(spriteBatch);
                    break;
            }
        }

        public bool completedTransitions(TransitionState state)
        {
            bool temp = false;

            switch (state)
            {
                case TransitionState.intro:
                    if (intro != null)
                        temp = intro.isComplete();
                    else
                        temp = true;
                    break;

                case TransitionState.selected:
                    if (select != null)
                        temp = select.isComplete();
                    else
                        temp = true;
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        temp = exit.isComplete();
                    else
                        temp = true;
                    break;
            }

            return temp;
        }
        #endregion

        #region Class Functions
        public void add(ArrowType arrow, ArrowDirection direction)
        {
            arrows.Add(direction, arrow);
        }

        public ArrowType getArrow(ArrowDirection direction)
        {
                return arrows[direction];
        }
        #endregion
    }
}