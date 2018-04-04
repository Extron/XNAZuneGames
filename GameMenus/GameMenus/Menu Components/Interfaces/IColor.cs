using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameMenus
{
    public interface IColor
    {
        void SetRed(object sender, EventArgs e);

        void SetGreen(object sender, EventArgs e);

        void SetBlue(object sender, EventArgs e);
    }
}
