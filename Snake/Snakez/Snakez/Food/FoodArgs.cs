using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class FoodArgs : EventArgs
    {
        Dictionary<Vector2, FoodType> food;

        public FoodArgs()
        {
            food = new Dictionary<Vector2, FoodType>();
        }

        public void addFood(List<FoodType> list)
        {
            foreach (FoodType item in list)
                food.Add(item.Vector, item);
        }

        public FoodType getFood(Vector2 location)
        {
            if (food.ContainsKey(location))
            {
                return food[location];
            }
            else
            {
                return null;
            }
        }
    }
}
