using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snakez
{
    class RabbitType : FoodType
    {
        public RabbitType()
            : base(FoodClassification.smart)
        {
            score = 400;
            growth = 4;
        }
    }
}
