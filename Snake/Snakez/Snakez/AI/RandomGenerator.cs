using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snakez
{
    class RandomGenerator
    {
        public static Random generator = new Random();

        public static int randomNumber(int lower, int upper)
        {
            return generator.Next(lower, upper);
        }
    }
}
