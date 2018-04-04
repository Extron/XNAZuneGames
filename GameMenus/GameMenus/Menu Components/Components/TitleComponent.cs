using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZHandler;

namespace GameMenus
{
    public class TitleComponent : IComponent
    {
        public const string identifier = "Title";

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        TitleType title;

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

        #region  Class Properties
        public Color TitleColor
        {
            get { return title.TextColor; }
            set { title.TextColor = value; }
        }

        public Vector2 TitleVector
        {
            get { return title.Vector; }
            set { title.Vector = value; }
        }
        #endregion

        #region Contructors
        public TitleComponent(TitleType componentTitle)
        {
            title = componentTitle;
        }

        public TitleComponent(string text, string fontAsset, Color titleColor, Vector2 titleVector)
        {
            title = new TitleType(text, fontAsset, titleColor, titleVector);
        }

        public TitleComponent(string text, string fontAsset, Color titleColor, Vector2 titleVector, TextAlignment alignment)
        {
            title = new TitleType(text, fontAsset, titleColor, titleVector, alignment);
        }
        #endregion

        #region Implemented Functions
        public void initialize()
        {
        }

        public void load()
        {
            title.load();
        }

        public void update(InputHandlerComponent i)
        {
        }

        public void draw(SpriteBatch spriteBatch)
        {
            title.draw(spriteBatch);
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