using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class ScrollingOptionsComponent : IComponent, IOptions
    {
        public const string identifier = "Options";

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        public List<OptionType> options;

        Vector2 initialVector;
        int index;
        int top;
        int bottom;
        int counter;
        int interval;
        int factor;

        #region Implemented Properties
        public int Index
        {
            get { return index; }
        }

        public int Count
        {
            get { return options.Count; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

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
        #endregion

        #region Constructors
        public ScrollingOptionsComponent(Vector2 vector, int numberOnScreen, int yFactor)
        {
            initialVector = vector;
            options = new List<OptionType>();
            interval = 10;
            top = 0;
            bottom = numberOnScreen;
            factor = yFactor;
        }

        public ScrollingOptionsComponent(Vector2 vector, int numberOnScreen, int yFactor, int scrollSpeed)
            : this(vector, numberOnScreen, yFactor)
        {
            interval = scrollSpeed;
        }
        #endregion

        #region IComponent Implemented Functions
        public void initialize()
        {
            options[0].State = OptionState.highlighted;
        }

        public void load()
        {
            foreach (OptionType option in options)
                option.load();
        }

        public void update(InputHandlerComponent input)
        {
            movement(input);

            for (int i = top; i < bottom; i++)
            {
                options[i].update(input);
            }

            counter++;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            options[top].TextVector = initialVector;
            options[top].Highligher = initialVector;
            options[top].draw(spriteBatch);

            for (int i = top + 1; i < bottom; i++)
            {
                options[i].TextVector = new Vector2(options[i - 1].TextVector.X, options[i - 1].TextVector.Y + factor);
                options[i].Highligher = options[i].TextVector;
                options[i].draw(spriteBatch);
            }
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

        #region IOptions Implemented Functions
        public void movement(InputHandlerComponent i)
        {
            if (i.getButton("Down", false) && index < options.Count - 1 && counter > interval)
            {
                //If the index has progressed past the number of options displayed, progress the list
                if (index + 1 >= bottom)
                {
                    top++;
                    bottom++;
                }

                options[index].State = OptionState.standard;
                options[++index].State = OptionState.highlighted;

                counter = 0;
                return;
            }
            else if (i.getButton("Up", false) && index > 0 && counter > interval)
            {
                if (index <= top)
                {
                    top--;
                    bottom--;
                }

                options[index].State = OptionState.standard;
                options[--index].State = OptionState.highlighted;

                counter = 0;
                return;
            }
        }

        public void reset()
        {
            options[index].State = OptionState.standard;

            options[0].State = OptionState.highlighted;

            bottom -= top;
            top = 0;
            index = 0;
        }

        public void add(OptionType option)
        {
            options.Add(option);
        }

        public void setEvent(Select method, int optionIndex)
        {
            options[optionIndex].Selected += method;
        }

        public void setEvents(Select method)
        {
            foreach (OptionType option in options)
                option.Selected += method;
        }
        #endregion
    }
}