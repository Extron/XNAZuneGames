using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class FadeComponent : TransitionWrapper, ITransition
    {
        byte defaultAlpha;
        int direction;
        int interval;
        int counter;

        public bool IsComplete
        {
            get { return (counter >= interval); }
        }

        public FadeComponent(int duration, int fadeDirection)
        {
            interval = duration;
            direction = fadeDirection;
        }

        public FadeComponent(ITransition transition, int duration, int fadeDirection)
            : base(transition)
        {
            interval = duration;
            direction = fadeDirection;
        }

        public override void initialize(IMenuComponent component)
        {
            defaultAlpha = component.Color.A;

            base.initialize(component);
        }

        public override void update(IMenuComponent component)
        {
            if (counter < interval)
            {
                Color newColor = component.Color;

                newColor.A += (byte)((255 / interval) * direction);

                component.Color = newColor;

                counter++;
            }

            base.update(component);
        }

        public override void reset(IMenuComponent component)
        {
            Color newColor = component.Color;

            newColor.A = defaultAlpha;

            component.Color = newColor;

            counter = 0;

            base.reset(component);
        }

        public override void setTransition(IMenuComponent component)
        {
            byte startAlpha = 0;

            if (direction > 0)
                startAlpha = (byte)(255 - component.Color.A);
            else if (direction < 0)
                startAlpha = component.Color.A;

            Color newColor = component.Color;

            newColor.A = startAlpha;

            component.Color = newColor;

            base.setTransition(component);
        }
    }
}
