using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameMenus
{
    public class SelectLevelArgs : EventArgs
    {
        public int level;

        public SelectLevelArgs(int selectedLevel)
        {
            level = selectedLevel;
        }
    }
}
