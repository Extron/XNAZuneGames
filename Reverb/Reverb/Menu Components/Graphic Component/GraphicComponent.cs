using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;

namespace Reverb.Components.Graphics
{
    public class GraphicComponent : ComponentType, IMenuComponent
    {
        Texture2D graphic;
        Rectangle boundingBox;
        Vector2 origin;
        Color color;
        string asset;
        float rotation;

        public Vector2 Position
        {
            get { return new Vector2(boundingBox.X, boundingBox.Y); }
            set { boundingBox.Location = new Point((int)value.X, (int)value.Y); }
        }

        public Vector2 Origin { get; set; }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public string Text { get; set; }

        public float Scale { get; set; }

        public GraphicComponent(string textureAsset, Rectangle graphicRectangle)
        {
            asset = textureAsset;
            boundingBox = graphicRectangle;

            identifier = "Menu Graphic";
            origin = new Vector2();
            color = Color.White;
            rotation = 0f;
        }

        public override void initialize()
        {
        }

        public override void load(ContentManager content)
        {
            graphic = content.Load<Texture2D>(asset);
        }

        public override void update(InputHandler input)
        {
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(graphic, boundingBox, null, color, rotation, origin, SpriteEffects.None, 0.5f);
        }
    }
}
