using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reverb.Arguments
{
    public class MenuArgs : EventArgs
    {
        public string identifier;
        public bool activateIntro;
        public bool activateExit;

        public MenuArgs(string menuIdentifier, bool intro, bool exit)
        {
            identifier = menuIdentifier;
            activateIntro = intro;
            activateExit = exit;
        }
    }
}
