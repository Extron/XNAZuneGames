using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class FadeCollection : TransitionWrapper, ITransition
    {
        byte defaultAlpha;
        int direction;
        int interval;
        int counter;

        public bool IsComplete
        {
            get { return (counter >= interval); }
        }

        public FadeCollection(int duration, int fadeDirection)
        {
            interval = duration;
            direction = fadeDirection;
        }

        public FadeCollection(ITransition transition, int duration, int fadeDirection)
            : base(transition)
        {
            interval = duration;
            direction = fadeDirection;
        }

        public override void initialize(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            defaultAlpha = collection.Colors[0].A;

            base.initialize(component);
        }

        public override void update(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<Color> newColors = collection.Colors;

            if (counter < interval)
            {
                for (int i = 0; i < collection.Colors.Count; i++)
                {
                    Color newColor = newColors[i];

                    newColor.A += (byte)((255 / interval) * direction);

                    newColors[i] = newColor;
                }

                counter++;
            }

            collection.Colors = newColors;

            base.update(component);
        }

        public override void reset(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<Color> newColors = collection.Colors;

            for (int i = 0; i < collection.Colors.Count; i++)
            {
                Color newColor = newColors[i];


                newColor.A = defaultAlpha;

                newColors[i] = newColor;
            }

            counter = 0;

            collection.Colors = newColors;

            base.reset(component);
        }

        public override void setTransition(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<Color> newColors = collection.Colors;

            byte startAlpha = 0;

            if (direction > 0)
                startAlpha = (byte)(255 - collection.Color.A);
            else if (direction < 0)
                startAlpha = collection.Color.A;

            for (int i = 0; i < collection.Colors.Count; i++)
            {
                Color newColor = newColors[i];

                newColor.A = startAlpha;

                newColors[i] = newColor;
            }

            collection.Colors = newColors;

            base.setTransition(collection);
        }
    }
}
