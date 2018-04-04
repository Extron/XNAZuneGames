using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class FloatTitle : TransitionWrapper
    {
        TransitionWrapper transition;
        Vector2 distance;
        Vector2 offset;
        Vector2 defaultVector;
        bool initialized;

        #region Properties
        public override TransitionState State
        {
            get { return transition.State; }
            set { transition.State = value; }
        }

        public override int Counter
        {
            get { return transition.Counter; }
        }

        public override int Interval
        {
            get { return transition.Interval; }
        }
        #endregion

        public FloatTitle(TransitionWrapper baseTransition, Vector2 titleOffset)
        {
            transition = baseTransition;
            offset = titleOffset;
            defaultVector.X = float.NaN;
            defaultVector.Y = float.NaN;
        }

        public override void initialize()
        {
            transition.initialize();
        }

        public override void load()
        {
            transition.load();
        }

        public override void update(IComponent component)
        {
            TitleComponent title = component as TitleComponent;

            if (!initialized)
            {
                resetTitle(title);
                initializeTitle(title);
                initialized = true;
            }

            Vector2 temp = new Vector2();

            temp.X = (distance.X / transition.Interval);
            temp.Y = (distance.Y / transition.Interval);

            int x = (int)(defaultVector.X - title.TitleVector.X);
            int y = (int)(defaultVector.Y - title.TitleVector.Y);

            if (((Math.Abs(x) < Math.Abs(temp.X)) || (Math.Abs(y) < Math.Abs(temp.Y))) && transition.State == TransitionState.intro)
                title.TitleVector = defaultVector;
            else
                title.TitleVector += temp;

            transition.update(component);
        }

        public override void draw(SpriteBatch spriteBatch, IComponent component)
        {
            transition.draw(spriteBatch, component);
        }

        public override bool isComplete()
        {
            if (transition.isComplete())
            {
                initialized = false;
            }

            return transition.isComplete();
        }

        public override void setTransition(IComponent component)
        {
            TitleComponent title = component as TitleComponent;

            initializeTitle(title);
            initialized = true;

            transition.setTransition(component);
        }

        private void initializeTitle(TitleComponent title)
        {
            if (transition.State == TransitionState.intro)
            {
                defaultVector = title.TitleVector;

                Vector2 temp = new Vector2();

                temp = title.TitleVector + offset;

                title.TitleVector = temp;

                distance = defaultVector - temp;
            }
            else if (transition.State == TransitionState.exit || transition.State == TransitionState.selected)
            {
                defaultVector = title.TitleVector;

                Vector2 temp = new Vector2();

                temp = defaultVector + offset;

                distance = defaultVector - temp;
            }
        }

        private void resetTitle(TitleComponent title)
        {
            if (!float.IsNaN(defaultVector.X) && !float.IsNaN(defaultVector.Y))
                title.TitleVector = defaultVector;
        }
    }
}
