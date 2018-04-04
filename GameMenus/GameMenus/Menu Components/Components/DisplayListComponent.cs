using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZHandler;

namespace GameMenus
{
    public class DisplayListComponent : IComponent, IList
    {
        public const string identifier = "Display List";

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        List<string> list;
        List<Vector2> vectors;
        SortDirection direction;
        SpriteFont font;
        Color color;
        string asset;

        #region Properties
        public SortDirection Direction
        {
            set { direction = value; }
        }
        #endregion

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
        public DisplayListComponent(string fontAsset, Color fontColor)
        {
            vectors = new List<Vector2>();
            list = new List<string>();
            asset = fontAsset;
            color = fontColor;
        }
        #endregion

        #region IComponent Implemented Functions
        public void initialize()
        {
            direction = SortDirection.up;
        }

        public void load()
        {
            font = AssetManager.getFont(asset);
        }

        public void update(InputHandlerComponent i)
        {
        }

        public void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < list.Count; i++)
            {
                spriteBatch.DrawString(font, list[i], vectors[i], color);
            }
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

        #region IList Implemented Functions
        public void setText<Type>(List<Type> listToDisplay)
        {
            list.Clear();

            if (direction == SortDirection.up)
            {
                for (int i = 0; i < listToDisplay.Count; i++)
                {
                    string temp = listToDisplay[i].ToString();

                    list.Add(temp);
                }
            }
            else
            {
                for (int i = listToDisplay.Count - 1; i >= 0; i--)
                {
                    string temp = listToDisplay[i].ToString();

                    list.Add(temp);
                }
            }
        }

        public void setVectors(int x, int y, int factor)
        {
            vectors.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                Vector2 temp = new Vector2(x, y + (factor * i));

                vectors.Add(temp);
            }
        }

        public void resetList()
        {
            list.Clear();
            vectors.Clear();
        }
        #endregion
    }

    public enum SortDirection
    {
        up,
        down
    }
}
