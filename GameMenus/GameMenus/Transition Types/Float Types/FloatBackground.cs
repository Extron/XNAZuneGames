using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class FloatBackground : TransitionWrapper
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

        public FloatBackground(TransitionWrapper baseTransition, Vector2 backgroundOffset)
        {
            transition = baseTransition;
            offset = backgroundOffset;
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
            BackgroundComponent background = component as BackgroundComponent;

            if (!initialized)
            {
                resetBackground(background);
                initializeBackground(background);
                initialized = true;
            }

            Vector2 temp = new Vector2();

            temp.X = (distance.X / transition.Interval);
            temp.Y = (distance.Y / transition.Interval);

            int x = (int)(defaultVector.X - background.Vector.X);
            int y = (int)(defaultVector.Y - background.Vector.Y);

            if (((Math.Abs(x) < Math.Abs(temp.X)) || (Math.Abs(y) < Math.Abs(temp.Y))) && transition.State == TransitionState.intro)
                background.Vector = defaultVector;
            else
                background.Vector += temp;

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
            BackgroundComponent background = component as BackgroundComponent;

            initializeBackground(background);
            initialized = true;

            transition.setTransition(component);
        }

        private void initializeBackground(BackgroundComponent background)
        {
            if (transition.State == TransitionState.intro)
            {
                defaultVector = background.Vector;

                Vector2 temp = new Vector2();

                temp = background.Vector + offset;

                background.Vector = temp;

                distance = defaultVector - temp;
            }
            else if (transition.State == TransitionState.exit || transition.State == TransitionState.selected)
            {
                defaultVector = background.Vector;

                Vector2 temp = new Vector2();

                temp = defaultVector + offset;

                distance = defaultVector - temp;
            }
        }

        private void resetBackground(BackgroundComponent background)
        {
            if (!float.IsNaN(defaultVector.X) && !float.IsNaN(defaultVector.Y))
                background.Vector = defaultVector;
        }
    }
}