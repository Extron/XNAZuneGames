using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb.Arguments;

namespace Reverb.Delegates
{
    public delegate void Select(OptionArgs args);

    public delegate void Back(MenuArgs args);

    public delegate void ChangeSelection(string selection);
}
