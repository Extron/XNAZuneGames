using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;

namespace Reverb.Components.InGame
{
    public class InGameComponent : ComponentType, IMenuComponent, IInGame
    {
        Texture2D background;
        Color tint;

        public Vector2 Position { get; set; }

        public Vector2 Origin { get; set; }

        public Color Color
        {
            get { return tint; }
            set { tint = value; }
        }

        public Rectangle BoundingBox { get; set; }

        public string Text { get; set; }

        public float Scale { get; set; }

        public InGameComponent()
        {
            identifier = "In Game";

            tint = Color.White;
        }

        public InGameComponent(Color textureTint)
        {
            identifier = "In Game";

            tint = textureTint;
        }

        public override void initialize()
        {
        }

        public override void load(ContentManager content)
        {
        }

        public override void update(InputHandler input)
        {
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(), tint);
        }

        public void setBackground(Texture2D backgroundTexture)
        {
            background = backgroundTexture;
        }
    }
}
