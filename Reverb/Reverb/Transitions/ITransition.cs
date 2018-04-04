using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb.Components;

namespace Reverb.Transitions
{
    public interface ITransition
    {
        bool IsComplete { get; }

        void initialize(IMenuComponent component);

        void update(IMenuComponent component);

        void reset(IMenuComponent component);

        void setTransition(IMenuComponent component);
    }
}
