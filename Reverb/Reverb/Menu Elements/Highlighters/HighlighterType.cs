using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Reverb.Elements.Highlighters
{
    public class HighlighterType
    {
        Texture2D texture;
        Vector2 position;
        Vector2 origin;
        Color color;
        string asset;
        float scale;
        float rotation;

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), texture.Width, texture.Height); }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public HighlighterType(string textureAsset, Vector2 vector)
        {
            asset = textureAsset;
            position = vector;

            origin = new Vector2();
            color = Color.White;
            scale = 1f;
            rotation = 0f;
        }

        public HighlighterType(string textureAsset, OptionType option)
        {
            asset = textureAsset;
            position = option.Position;

            origin = new Vector2();
            color = Color.White;
            scale = 1f;
            rotation = 0f;
        }

        public HighlighterType(HighlighterType highlighter)
        {
            texture = highlighter.texture;
            asset = highlighter.asset;
            position = highlighter.position;
            origin = highlighter.origin;
            color = highlighter.color;
            scale = highlighter.scale;
            rotation = highlighter.rotation;
        }

        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(asset);
        }

        public virtual void update()
        {
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 0.5f);
        }

        public void alignRight()
        {
            origin = Vector2.Zero;
        }

        public void alignCenter()
        {
            origin.X = texture.Width / 2;
        }

        public void alignLeft()
        {
            origin.X = texture.Width;
        }
    }
}
