using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class ExtendComponent : TransitionWrapper, ITransition
    {
        Rectangle defaultRectangle;
        Vector2 extend;
        bool toVector;
        int interval;
        int counter;
        int corner;

        public bool IsComplete
        {
            get { return counter == interval; }
        }

        public ExtendComponent(Vector2 rectangleExtend, int extendedCorner, int length, bool travelToVector)
        {
            extend = rectangleExtend;
            corner = extendedCorner;
            interval = length;
            toVector = travelToVector;
        }

        public override void initialize(IMenuComponent component)
        {
            defaultRectangle = component.BoundingBox;

            base.initialize(component);
        }

        public override void update(IMenuComponent component)
        {
            if (counter < interval)
            {
                Rectangle newRectangle = component.BoundingBox;

                switch (corner)
                {
                    case 1:
                        newRectangle.X += (int)(extend.X / interval);
                        newRectangle.Width += (int)(-extend.X / interval);

                        newRectangle.Y += (int)(extend.Y / interval);
                        newRectangle.Height += (int)(-extend.Y / interval);
                        break;

                    case 2:
                        newRectangle.Y += (int)(extend.Y / interval);
                        newRectangle.Height += (int)(-extend.Y / interval);

                        newRectangle.Width += (int)(extend.X / interval);
                        break;

                    case 3:
                        newRectangle.Height += (int)(extend.Y / interval);
                        newRectangle.Width += (int)(extend.X / interval);
                        break;

                    case 4:
                        newRectangle.X += (int)(extend.X / interval);
                        newRectangle.Width += (int)(-extend.X / interval);

                        newRectangle.Height += (int)(extend.Y / interval);
                        break;
                }

                component.BoundingBox = newRectangle;

                counter++;
            }

            base.update(component);
        }

        public override void reset(IMenuComponent component)
        {
            component.BoundingBox = defaultRectangle;

            counter = 0;

            base.reset(component);
        }

        public override void setTransition(IMenuComponent component)
        {
            if (toVector)
            {
                Rectangle newRectangle = component.BoundingBox;

                switch (corner)
                {
                    case 1:
                        newRectangle.X -= (int)extend.X;
                        newRectangle.Y -= (int)extend.Y;

                        newRectangle.Width -= (int)extend.X;
                        newRectangle.Height -= (int)extend.Y;
                        break;

                    case 2:
                        newRectangle.Y -= (int)extend.Y;

                        newRectangle.Width -= (int)extend.X;
                        newRectangle.Height -= (int)extend.Y;
                        break;

                    case 3:
                        newRectangle.Width -= (int)extend.X;
                        newRectangle.Height -= (int)extend.Y;
                        break;

                    case 4:
                        newRectangle.X -= (int)extend.X;

                        newRectangle.Width -= (int)extend.X;
                        newRectangle.Height -= (int)extend.Y;
                        break;
                }

                component.BoundingBox = newRectangle;
            }

            base.setTransition(component);
        }
    }
}
