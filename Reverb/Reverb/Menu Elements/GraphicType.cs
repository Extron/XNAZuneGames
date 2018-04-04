using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Reverb.Elements
{
    public class GraphicType
    {
        Texture2D texture;
        Vector2 position;
        Vector2 origin;
        Color color;
        string asset;
        float scale;
        float rotation;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public GraphicType(string textureAsset, Vector2 texturePosition)
        {
            asset = textureAsset;
            position = texturePosition;

            color = Color.White;
            origin = new Vector2();
            scale = 1;
            rotation = 0;
        }

        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(asset);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 0.5f);
        }
    }
}
