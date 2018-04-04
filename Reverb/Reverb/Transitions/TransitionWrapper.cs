using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb.Components;

namespace Reverb.Transitions
{
    public class TransitionWrapper
    {
        ITransition baseTransition;

        public TransitionWrapper()
        {
            baseTransition = null;
        }

        public TransitionWrapper(ITransition transition)
        {
            baseTransition = transition;
        }

        public virtual void initialize(IMenuComponent component)
        {
            if (baseTransition != null)
                baseTransition.initialize(component);
        }

        public virtual void update(IMenuComponent component)
        {
            if (baseTransition != null)
                baseTransition.update(component);
        }

        public virtual void reset(IMenuComponent component)
        {
            if (baseTransition != null)
                baseTransition.reset(component);
        }

        public virtual void setTransition(IMenuComponent component)
        {
            if (baseTransition != null)
                baseTransition.setTransition(component);
        }
    }
}
