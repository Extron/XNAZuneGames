using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DyLight2D
{
    public static class LightFunctions
    {
        static Random random = new Random();

        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
