using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class ScaleCollection : TransitionWrapper, ITransition
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

        public ScaleCollection(Vector2 originOfScale, float scaleFactor, int duration, bool travelToScale)
        {
            scaleOrigin = originOfScale;
            factor = scaleFactor;
            interval = duration;
            toScale = travelToScale;
        }

        public ScaleCollection(ITransition transition, Vector2 originOfScale, float scaleFactor, int duration, bool travelToScale)
            : base(transition)
        {
            scaleOrigin = originOfScale;
            factor = scaleFactor;
            interval = duration;
            toScale = travelToScale;
        }

        public override void initialize(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            defaultScale = collection.Scales[0];

            defaultOrigin = collection.Origins[0];

            base.initialize(component);
        }

        public override void update(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<float> newScales = collection.Scales;

            if (counter < interval)
            {
                for (int i = 0; i < collection.Scales.Count; i++)
                {
                    float newScale = newScales[i];

                    newScale += (factor / (float)interval);

                    newScales[i] = newScale;
                }

                counter++;
            }

            collection.Scales = newScales;

            base.update(component);
        }

        public override void reset(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<Vector2> newPositions = collection.Positions;
            List<Vector2> newOrigins = collection.Origins;
            List<float> newScales = collection.Scales;

            for (int i = 0; i < collection.Scales.Count; i++)
            {
                newScales[i] = defaultScale;
                newOrigins[i] = defaultOrigin;

                newPositions[i] -= scaleOrigin;
            }

            counter = 0;

            collection.Positions = newPositions;
            collection.Origins = newOrigins;
            collection.Scales = newScales;

            base.reset(component);
        }

        public override void setTransition(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<Vector2> newPositions = collection.Positions;
            List<Vector2> newOrigins = collection.Origins;
            List<float> newScales = collection.Scales;

            for (int i = 0; i < collection.Scales.Count; i++)
            {
                if (toScale)
                    newScales[i] -= factor;

                newOrigins[i] = defaultOrigin;

                newPositions[i] += scaleOrigin;
            }

            collection.Positions = newPositions;
            collection.Origins = newOrigins;
            collection.Scales = newScales;

            base.setTransition(collection);
        }
    }
}
