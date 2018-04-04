using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class FadeOptions : TransitionWrapper
    {
        TransitionWrapper transition;
        string identifier;
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
        public FadeOptions(TransitionWrapper baseTransition)
        {
            transition = baseTransition;
            identifier = "Options";
            defaultAlpha = -1;
            initialized = false;
        }

        public FadeOptions(TransitionWrapper baseTransition, string optionIdentifier)
        {
            transition = baseTransition;
            identifier = optionIdentifier;
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
            OptionsComponent options = component as OptionsComponent;

            if (!initialized)
            {
                resetOptions(options);
                initializeOptions(options);
                initialized = true;
            }

            int x = 255 - alpha;
            int y = direction / transition.Interval;

            if (x >= y)
                alpha += direction / transition.Interval;

            setAlpha(options);

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
            OptionsComponent optionComponent = component as OptionsComponent;

            initializeOptions(optionComponent);
            initialized = true; 
            
            transition.setTransition(component);
        }
        #endregion

        #region Private Functions
        private void initializeOptions(OptionsComponent optionComponent)
        {
            defaultAlpha = optionComponent.options[0].TextColor.A;

            //Set the fade to fade in
            if (transition.State == TransitionState.intro)
            {
                direction = 255;
                alpha = 0;

                setAlpha(optionComponent);
            }
            //Else set the fade to fade out;
            else if (transition.State == TransitionState.exit || transition.State == TransitionState.selected)
            {
                direction = -255;
                alpha = 255;

                setAlpha(optionComponent);
            }
        }

        private void resetOptions(OptionsComponent optionComponent)
        {
            if (defaultAlpha > 0)
            {
                alpha = defaultAlpha;

                setAlpha(optionComponent);
            }
        }

        private void setAlpha(OptionsComponent optionComponent)
        {
            foreach (OptionType option in optionComponent.options)
            {
                Color temp = option.TextColor;

                temp.A = (byte)alpha;

                option.TextColor = temp;
            }
        }
        #endregion
    }
}
