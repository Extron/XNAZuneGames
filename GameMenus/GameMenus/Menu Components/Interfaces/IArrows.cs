using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameMenus
{
    public interface IArrows
    {
        void initialize();

        void load();

        void update();

        void draw();

        void add(ArrowType arrow, ArrowDirection direction);

        ArrowType getArrow(ArrowDirection direction);
    }
}
