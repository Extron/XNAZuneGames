using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameMenus
{
    public class DirectionArgs : EventArgs
    {
        public Direction direction;

        public DirectionArgs(Direction d)
        {
            direction = d;
        }
    }
}
