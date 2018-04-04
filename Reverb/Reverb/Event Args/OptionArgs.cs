using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb.Enumerations;

namespace Reverb.Arguments
{
    public class OptionArgs : EventArgs
    {
        public OptionAction action;
        public string menuLink;
        public string text;
        public bool activateInro;
        public bool activateSelect;
        public int index;

        public OptionArgs(OptionAction optionAction, string link, string optionText, bool intro, bool select, int optionIndex)
        {
            action = optionAction;
            menuLink = link;
            activateInro = intro;
            activateSelect = select;
            index = optionIndex;
            text = optionText;
        }
    }
}
