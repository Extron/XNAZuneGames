using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class MoveComponent : TransitionWrapper, ITransition
    {
        Vector2 defaultVector;
        Vector2 direction;
        bool toVector;
        int interval;
        int counter;

        public bool IsComplete
        {
            get { return (counter >= interval); }
        }

        public MoveComponent(Vector2 floatDirection, bool travelToVector, int length)
        {
            direction = floatDirection;
            toVector = travelToVector;
            interval = length;
        }

        public MoveComponent(ITransition transition, Vector2 floatDirection, bool travelToVector, int length)
            : base(transition)
        {
            direction = floatDirection;
            toVector = travelToVector;
            interval = length;
        }

        public override void initialize(IMenuComponent component)
        {
            defaultVector = component.Position;

            base.initialize(component);
        }

        public override void update(IMenuComponent component)
        {
            if (counter < interval)
            {
                component.Position += (direction / new Vector2(interval));

                counter++;
            }

            base.update(component);
        }

        public override void reset(IMenuComponent component)
        {
            component.Position = defaultVector;

            counter = 0;

            base.reset(component);
        }

        public override void setTransition(IMenuComponent component)
        {
            if (toVector)
            {
                component.Position -= direction;
            }

            base.setTransition(component);
        }
    }
}
