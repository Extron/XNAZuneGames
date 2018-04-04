using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class OptionsComponent : IComponent, IOptions
    {
        public const string identifier = "Options";

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        public List<OptionType> options;

        int index;
        int counter;
        int interval;

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
        public OptionsComponent()
        {
            options = new List<OptionType>();
            interval = 10;
        }

        public OptionsComponent(int scrollSpeed)
        {
            options = new List<OptionType>();
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

        public void update(InputHandlerComponent i)
        {
            movement(i);

            foreach (OptionType option in options)
                option.update(i);

            counter++;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (OptionType option in options)
                option.draw(spriteBatch);
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
                options[index].State = OptionState.standard;
                index++;
                options[index].State = OptionState.highlighted;
                counter = 0;
                return;
            }
            else if (i.getButton("Up", false) && index > 0 && counter > interval)
            {
                options[index].State = OptionState.standard;
                index--;
                options[index].State = OptionState.highlighted;
                counter = 0;
                return;
            }
        }

        public void reset()
        {
            options[index].State = OptionState.standard;

            options[0].State = OptionState.highlighted;

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
