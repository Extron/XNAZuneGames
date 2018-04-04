using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class FadeBackground : TransitionWrapper
    {
        TransitionWrapper transition;
        bool initialized;
        int direction;
        int alpha;
        int defaultAlpha;

        #region Properties
        public override TransitionState State
        {
            get
            {
                return transition.State;
            }
            set
            {
                transition.State = value;
            }
        }

        public override int Counter
        {
            get
            {
                return transition.Counter;
            }
        }

        public override int Interval
        {
            get
            {
                return transition.Interval;
            }
        }
        #endregion

        #region Constructors
        public FadeBackground(TransitionWrapper baseTransition)
        {
            transition = baseTransition;
            defaultAlpha = -1;
            initialized = false;
        }
        #endregion

        #region Overridden Functions
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

            int x = 255 - alpha;
            int y = direction / transition.Interval;

            if (x >= y)
                alpha += direction / transition.Interval;

            setAlpha(background);

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
        #endregion

        #region Private Functions
        private void initializeBackground(BackgroundComponent background)
        {
            defaultAlpha = background.BackgroundColor.A;

            //Set the fade to fade in
            if (transition.State == TransitionState.intro)
            {
                direction = 255;
                alpha = 0;

                setAlpha(background);
            }
            //Else set the fade to fade out;
            else if (transition.State == TransitionState.exit || transition.State == TransitionState.selected)
            {
                direction = -255;
                alpha = 255;

                setAlpha(background);
            }
        }

        private void resetBackground(BackgroundComponent background)
        {
            if (defaultAlpha > 0)
            {
                alpha = defaultAlpha;

                setAlpha(background);
            }
        }

        private void setAlpha(BackgroundComponent background)
        {
            Color temp = background.BackgroundColor;

            temp.A = (byte)alpha;

            background.BackgroundColor = temp;
        }
        #endregion
    }
}
