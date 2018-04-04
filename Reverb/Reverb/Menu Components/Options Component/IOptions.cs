using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb.Delegates;
using Reverb.Elements;

namespace Reverb.Components.Options
{
    interface IOptions
    {
        void setEvent(Select method, int optionIndex);

        void setEvents(Select method);

        void addOption(OptionType option);
    }
}
