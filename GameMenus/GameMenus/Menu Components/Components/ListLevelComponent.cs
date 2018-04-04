using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZHandler;

namespace GameMenus
{
    public class ListLevelComponent : IComponent, IOptions
    {
        public const string identifier = "List Select Level";

        public EventHandler<SelectLevelArgs> Selected;
        public ScrollingOptionsComponent levels;
        public int level;

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        #region Implemented Properties
        public int Index
        {
            get { return levels.Index; }
        }

        public int Count
        {
            get { return levels.Count; }
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

        public string Identifier
        {
            get { return identifier; }
        }
        #endregion

        #region Constructors
        public ListLevelComponent(ScrollingOptionsComponent list)
        {
            levels = list;
        }
        #endregion

        #region IComponent Implemented Functions
        public void initialize()
        {
            levels.initialize();

            levels.setEvents(SelectLevel);
        }

        public void load()
        {
            levels.load();
        }

        public void update(InputHandlerComponent i)
        {
            levels.update(i);
        }

        public void reset()
        {
            levels.reset();
        }


        public void draw(SpriteBatch spriteBatch)
        {
            levels.draw(spriteBatch);
        }

        public void updateTransitions(TransitionState state)
        {
            levels.updateTransitions(state);

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
            levels.drawTransitions(spriteBatch, state);

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
        }

        public void add(OptionType option)
        {
            levels.add(option);
        }

        public void setEvent(Select method, int index)
        {
            levels.setEvent(method, index);
        }

        public void setEvents(Select method)
        {
            levels.setEvents(method);
        }
        #endregion

        #region Class Functions
        public void dynamicInitialize(ScrollingOptionsComponent list)
        {
            levels = list;

            levels.initialize();

            levels.load();
        }

        public void setSelectEvent(EventHandler<SelectLevelArgs> method)
        {
            Selected += method;
        }
        #endregion

        #region Events
        private void SelectLevel(object sender, EventArgs e)
        {
            level = levels.Index + 1;

            if (Selected != null)
                Selected(this, new SelectLevelArgs(level));
        }
        #endregion
    }
}
