using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reverb.Transitions
{
    /// <summary>
    /// Provides values for identifying the current transition state of a given menu or component.  Transitions run in pairs
    /// based on these pairs.  If the user activates a menu option, the select transition runs on the exiting menu while the 
    /// intro transition runs on the entering menu.  Inversely, if a user activates a return to a previous menu, via either by
    /// the back event or a returning option, the exiting menu runs the exit transition while the entering menu runs the revert 
    /// transition.
    /// </summary>
    public enum TransitionState
    {
        intro,
        revert,
        select,
        exit,
        none
    }
}
