using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class FloatOptions : TransitionWrapper
    {
        TransitionWrapper transition;
        List<Vector2> distances;
        List<Vector2> optionVectors;
        Vector2 startVector;
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

        #region Constructors
        public FloatOptions(TransitionWrapper baseTransition, Vector2 offset)
        {
            transition = baseTransition;
            startVector = offset;
            distances = new List<Vector2>();
            optionVectors = new List<Vector2>();
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

            setOptions(options);

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
            OptionsComponent options = component as OptionsComponent;

            initializeOptions(options);
            initialized = true;

            transition.setTransition(component);
        }
        #endregion

        #region Class Functions
        private void initializeOptions(OptionsComponent optionComponent)
        {
            optionVectors.Clear();

            distances.Clear();

            for (int i = 0; i < optionComponent.options.Count; i++)
            {
                if (transition.State == TransitionState.intro)
                {
                    optionVectors.Add(optionComponent.options[i].TextVector);

                    Vector2 temp = optionComponent.options[i].TextVector + startVector;

                    Vector2 distance = optionComponent.options[i].TextVector - temp;

                    optionComponent.options[i].TextVector = temp;

                    distances.Add(distance);
                }
                else if (transition.State == TransitionState.exit || transition.State == TransitionState.selected)
                {
                    Vector2 temp = optionComponent.options[i].TextVector + startVector;

                    Vector2 distance = temp - optionComponent.options[i].TextVector;

                    optionVectors.Add(optionComponent.options[i].TextVector);

                    distances.Add(distance);
                }
            }
        }

        private void resetOptions(OptionsComponent optionComponent)
        {
            for (int i = 0; i < optionComponent.options.Count; i++)
            {
                try
                {
                    optionComponent.options[i].TextVector = optionVectors[i];
                }
                catch
                {
                }
            }
        }

        private void setOptions(OptionsComponent optionComponent)
        {
            Vector2 temp = new Vector2();

            for (int i = 0; i < optionComponent.options.Count; i++)
            {
                temp.X = (distances[i].X / transition.Interval);
                temp.Y = (distances[i].Y / transition.Interval);

                int x = (int)(optionVectors[i].X - optionComponent.options[i].TextVector.X);
                int y = (int)(optionVectors[i].Y - optionComponent.options[i].TextVector.Y);

                if (((Math.Abs(x) < Math.Abs(temp.X)) || (Math.Abs(y) < Math.Abs(temp.Y))) && transition.State == TransitionState.intro)
                    optionComponent.options[i].TextVector = optionVectors[i];
                else
                    optionComponent.options[i].TextVector += temp;
            }
        }
        #endregion
    }
}
