using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Snakez
{
    class Objects
    {
        Dictionary<string, ObjectType> objects;

        Dictionary<Vector2, ObjectType> spawnedObjects;

        public Objects()
        {
            objects = new Dictionary<string, ObjectType>();
            spawnedObjects = new Dictionary<Vector2, ObjectType>();
        }

        public void initialize()
        {
            objects.Add("Key", new KeyType());
            objects.Add("Diet Pill", new PillType());
            objects.Add("Extra Life", new ExtraLifeType());
        }

        public void load(ContentManager content)
        {
            foreach (ObjectType item in objects.Values)
                item.load(content);
        }

        public void addObject(ObjectType item)
        {
            objects.Add(item.Identifier, item);
        }

        public void spawnObject(GameGrid grid, string identifier)
        {
            ObjectType item;

            if (objects.TryGetValue(identifier, out item))
            {
                item.spawn(grid);

                spawnedObjects.Add(item.Location, item);
            }
        }

        public void activateObject(GameGrid grid, Vector2 location)
        {
            ObjectType item;

            if (spawnedObjects.TryGetValue(location, out item))
            {
                item.activate(grid);

                spawnedObjects.Remove(location);
            }
        }

        public void setEvent(EventHandler<EventArgs> method, string identifier)
        {
            ObjectType item;

            if (objects.TryGetValue(identifier, out item))
            {
                item.Activate += method;
            }
        }

        public ObjectType getObject(string identifier)
        {
            ObjectType item;

            if (objects.TryGetValue(identifier, out item))
                return item;
            else
                return null;
        }

        public ObjectType getObject(Vector2 vector)
        {
            ObjectType item;

            if (spawnedObjects.TryGetValue(vector, out item))
                return item;
            else
                return null;
        }
    }
}
