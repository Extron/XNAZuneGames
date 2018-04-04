using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class MoveCollection : TransitionWrapper, ITransition
    {
        List<Vector2> defaultVectors;
        Vector2 direction;
        bool toVector;
        int interval;
        int counter;

        public bool IsComplete
        {
            get { return (counter >= interval); }
        }

        public MoveCollection(Vector2 floatDirection, int duration, bool travelToVector)
        {
            interval = duration;
            direction = floatDirection;
            toVector = travelToVector;

            defaultVectors = new List<Vector2>();
        }

        public MoveCollection(ITransition transition, Vector2 floatDirection, int duration, bool travelToVector)
            : base(transition)
        {
            interval = duration;
            direction = floatDirection;
            toVector = travelToVector;

            defaultVectors = new List<Vector2>();
        }

        public override void initialize(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            foreach (Vector2 position in collection.Positions)
            {
                defaultVectors.Add(position);
            }

            base.initialize(component);
        }

        public override void update(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<Vector2> newPositions = collection.Positions;

            if (counter < interval)
            {
                for (int i = 0; i < collection.Positions.Count; i++)
                {
                    newPositions[i] += direction / new Vector2(interval);
                }

                counter++;
            }

            collection.Positions = newPositions;

            base.update(component);
        }

        public override void reset(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<Vector2> newPositions = collection.Positions;

            for (int i = 0; i < collection.Positions.Count; i++)
            {
                newPositions[i] = defaultVectors[i];
            }

            collection.Positions = newPositions;

            counter = 0;

            base.reset(component);
        }

        public override void setTransition(IMenuComponent component)
        {
            ICollectionComponent collection = component as ICollectionComponent;

            List<Vector2> newPositions = collection.Positions;

            if (toVector)
            {
                for (int i = 0; i < collection.Positions.Count; i++)
                {
                    newPositions[i] -= direction;
                }
            }

            collection.Positions = newPositions;

            base.setTransition(component);
        }
    }
}
