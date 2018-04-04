using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Snakez
{
    class ObjectType
    {
        public EventHandler<EventArgs> Activate;

        Texture2D texture;
        Vector2 location;
        Color color;
        string textureAsset;
        string identifier;

        public Vector2 Location
        {
            get { return location; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public ObjectType(string asset, string objectIdentifier)
        {
            textureAsset = asset;
            color = Color.White;
            location = new Vector2();
            identifier = objectIdentifier;
        }

        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureAsset);
        }

        public void activate(GameGrid grid)
        {
            if (Activate != null)
                Activate(this, new EventArgs());

            grid.GetCell(location).emptyCell();
        }

        public void spawn(GameGrid grid)
        {
            int x = 0, y = 0;

            do
            {
                x = RandomGenerator.randomNumber(0, 24);
                y = RandomGenerator.randomNumber(0, 32);
            }
            while (grid.GetCell(x, y).content != CellContent.empty);

            location = new Vector2(x, y);

            fillGrid(grid);
        }

        public void fillGrid(GameGrid grid)
        {
            grid.GetCell(location).fillCell(CellContent.item, texture, 0, color);
        }
    }
}
