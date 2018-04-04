using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class DimComponent : TransitionWrapper, ITransition
    {
        Color defaultColor;
        Vector3 destination;
        int direction;
        int interval;
        int counter;

        public bool IsComplete
        {
            get { return (counter >= interval); }
        }

        public DimComponent(Vector3 destinationTint, int length, int dimDirection)
        {
            destination = destinationTint;
            interval = length;
            direction = dimDirection;
        }

        public DimComponent(ITransition transition, Vector3 destinationTint, int length, int dimDirection)
            : base(transition)
        {
            destination = destinationTint;
            interval = length;
            direction = dimDirection;
        }

        public override void initialize(IMenuComponent component)
        {
            defaultColor = component.Color;

            base.initialize(component);
        }

        public override void update(IMenuComponent component)
        {
            if (counter < interval)
            {
                Vector3 newColor = new Vector3(component.Color.R, component.Color.G, component.Color.B);

                newColor += (destination / new Vector3(interval)) * direction;

                component.Color = new Color((byte)newColor.X, (byte)newColor.Y, (byte)newColor.Z);

                counter++;
            }

            base.update(component);
        }

        public override void reset(IMenuComponent component)
        {
            //component.Color = defaultColor;

            counter = 0;

            base.reset(component);
        }

        public override void setTransition(IMenuComponent component)
        {
            if (direction > 0)
            {
                component.Color = new Color((destination / new Vector3(255)));
            }

            base.setTransition(component);
        }
    }
}
