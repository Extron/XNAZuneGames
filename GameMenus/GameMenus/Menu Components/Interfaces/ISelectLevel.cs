using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;

namespace GameMenus
{
    public interface ISelectLevel
    {
        int Level { get; set; }

        void select(InputHandlerComponent i);
    }
}
