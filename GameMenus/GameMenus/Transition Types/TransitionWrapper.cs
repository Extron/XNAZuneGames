using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public abstract class TransitionWrapper
    {
        protected TransitionState state;
        protected bool complete;
        protected int counter;
        protected int interval;

        #region Properties
        public virtual TransitionState State
        {
            get { return state; }
            set { state = value; }
        }

        public virtual int Counter
        {
            get { return counter; }
        }

        public virtual int Interval
        {
            get { return interval; }
        }
        #endregion

        public abstract void initialize();

        public abstract void load();

        public abstract void update(IComponent component);

        public abstract void draw(SpriteBatch spriteBatch, IComponent component);

        public abstract bool isComplete();

        public abstract void setTransition(IComponent component);
    }
}
