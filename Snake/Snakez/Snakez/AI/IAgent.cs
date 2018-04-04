using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snakez.AI
{
    interface IAgent
    {
        OrientationType Orientation { get; set; }

        void initialize();

        void move(SnakeType snake, FoodType food, LevelType level);
    }
}
