using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;

namespace Reverb.Components.Background
{
    public class BackgroundComponent : ComponentType, IMenuComponent
    {
        Texture2D texture;
        Vector2 vector;
        Vector2 origin;
        Color color;
        string asset;
        float scale;
        float rotation;

        public Vector2 Position
        {
            get { return vector; }
            set { vector = value; }
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

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)vector.X, (int)vector.Y, texture.Width, texture.Height); }
            set { }
        }

        public string Text { get; set; }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public BackgroundComponent(string textureAsset)
        {
            identifier = "Background";

            asset = textureAsset;
            vector = new Vector2();
            color = Color.White;
        }

        public BackgroundComponent(string textureAsset, Vector2 position, Color backgroundColor)
        {
            identifier = "Background";

            asset = textureAsset;
            vector = position;
            color = backgroundColor;
        }

        public override void initialize()
        {
            scale = 1;
        }

        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(asset);
        }

        public override void update(InputHandler input)
        {
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, null, color, rotation, origin, scale, SpriteEffects.None, 1f);
        }
    }
}
