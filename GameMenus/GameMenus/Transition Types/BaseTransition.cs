using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class BaseTransition : TransitionWrapper
    {
        public BaseTransition(int speed, TransitionState transitionState)
        {
            interval = speed;
            state = transitionState;
        }

        public override void initialize()
        {
        }

        public override void load()
        {
        }

        public override void update(IComponent component)
        {
            if (counter < interval)
                counter++;
            else
                counter = 0;
        }

        public override void draw(SpriteBatch spriteBatch, IComponent component)
        {
            component.draw(spriteBatch);
        }

        public override bool isComplete()
        {
            return (counter == interval);
        }

        public override void setTransition(IComponent component)
        {
        }
    }
}
