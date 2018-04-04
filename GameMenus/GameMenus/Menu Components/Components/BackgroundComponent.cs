using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class BackgroundComponent : IComponent
    {
        public const string identifier = "Background";

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        BackgroundType background;

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

        #region Class Properties
        public Vector2 Vector
        {
            get { return background.vector; }
            set { background.vector = value; }
        }

        public Color BackgroundColor
        {
            get { return background.color; }
            set { background.color = value; }
        }
        #endregion

        #region Constructors
        public BackgroundComponent(BackgroundType menuBackground)
        {
            background = menuBackground;
        }

        public BackgroundComponent(string backgroundAsset, Vector2 backgroundVector, Color backgroundColor)
        {
            background = new BackgroundType(backgroundAsset, backgroundVector, backgroundColor);
        }
        #endregion

        #region Implemented Functions
        public void initialize()
        {
        }

        public void load()
        {
            background.load();
        }

        public void update(InputHandlerComponent i)
        {
        }

        public void draw(SpriteBatch spriteBatch)
        {
            background.draw(spriteBatch);
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


    }
}
