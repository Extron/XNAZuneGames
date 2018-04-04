using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class ScaleComponent : TransitionWrapper, ITransition
    {
        Vector2 scaleOrigin;
        Vector2 defaultOrigin;
        float defaultScale;
        float factor;
        bool toScale;
        int interval;
        int counter;

        public bool IsComplete
        {
            get { return (counter >= interval); }
        }

        public ScaleComponent(Vector2 originOfScale, float scaleFactor, int duration, bool travelToScale)
        {
            scaleOrigin = originOfScale;
            factor = scaleFactor;
            interval = duration;
            toScale = travelToScale;
        }

        public ScaleComponent(ITransition transition, Vector2 originOfScale, float scaleFactor, int duration, bool travelToScale)
            : base(transition)
        {
            scaleOrigin = originOfScale;
            factor = scaleFactor;
            interval = duration;
            toScale = travelToScale;
        }

        public override void initialize(IMenuComponent component)
        {
            defaultScale = component.Scale;

            defaultOrigin = component.Origin;

            base.initialize(component);
        }

        public override void update(IMenuComponent component)
        {
            if (counter < interval)
            {
                component.Scale += (factor / (float)interval);

                counter++;
            }

            base.update(component);
        }

        public override void reset(IMenuComponent component)
        {
            component.Scale = defaultScale;

            component.Origin = defaultOrigin;

            component.Position -= scaleOrigin;

            counter = 0;

            base.reset(component);
        }

        public override void setTransition(IMenuComponent component)
        {
            if (toScale)
                component.Scale -= factor;

            component.Origin = scaleOrigin;

            component.Position += scaleOrigin;

            base.setTransition(component);
        }
    }
}
