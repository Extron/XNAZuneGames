using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class ObjectArgs
    {
        Dictionary<Vector2, ObjectType> objects;

        public ObjectArgs(List<ObjectType> list)
        {
            objects = new Dictionary<Vector2, ObjectType>();

            foreach (ObjectType item in list)
            {
                objects.Add(item.Location, item);
            }
        }

        public ObjectArgs(ObjectType item)
        {
            objects.Add(item.Location, item);
        }

        public ObjectType getObject(Vector2 vector)
        {
            ObjectType item;
            if (objects.TryGetValue(vector, out item))
                return item;
            else
                return null;
        }
    }
}
