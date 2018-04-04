using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;

namespace GameMenus
{
    public interface IOptions
    {
        int Index { get; }

        int Count { get; }

        void movement(InputHandlerComponent input);

        void reset();

        void add(OptionType option);

        void setEvent(Select method, int optionIndex);

        void setEvents(Select method);
    }
}
