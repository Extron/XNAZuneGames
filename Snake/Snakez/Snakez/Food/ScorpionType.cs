using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snakez
{
    class ScorpionType : FoodType
    {
        public ScorpionType()
            : base(FoodClassification.threat)
        {
            growth = 2;
            score = 500;
        }
    }
}