using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameMenus
{
    public interface IList
    {
        void setText<Type>(List<Type> list);

        void setVectors(int x, int y, int factor);

        void resetList();
    }
}
