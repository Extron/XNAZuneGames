using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Elements.Highlighters;

namespace Reverb.Elements
{
    public class PreviewType
    {
        public EventHandler Selected;

        HighlighterType highlighter;
        HighlighterType selector;
        Texture2D texture;
        Rectangle hitBox;
        Vector2 position;
        Vector2 origin;
        Color color;
        string asset;
        float scale;
        float rotation;
        bool isSelected;
        bool isHighlighted;
        int interval;
        int counter;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                Vector2 distance = value - position;

                position = value;

                highlighter.Position += distance;

                hitBox.Location = new Point(hitBox.Location.X + (int)Math.Round(distance.X), hitBox.Location.Y + (int)Math.Round(distance.Y));
            }
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

        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public PreviewType(string textureAsset, Vector2 texturePosition, Color tint, string highlighterAsset, string selectorAsset, Vector2 highlighterVector)
        {
            asset = textureAsset;
            position = texturePosition;
            color = tint;

            highlighter = new HighlighterType(highlighterAsset, highlighterVector);
            selector = new HighlighterType(selectorAsset, highlighterVector);

            origin = new Vector2();
            rotation = 0;
            scale = 1;
        }

        public virtual void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(asset);

            highlighter.load(content);

            selector.load(content);

            if (hitBox.IsEmpty)
            {
                if (highlighter == null)
                    hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                else
                    hitBox = highlighter.BoundingBox;
            }
        }

        public virtual void update(InputHandler input)
        {
            if (isHighlighted)
            {
                highlighter.update();
                select(input);
            }

            highlight(input);

            counter++;
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (isHighlighted)
                highlighter.draw(spriteBatch);

            if (isSelected)
                selector.draw(spriteBatch);

            spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 0.5f);
        }

        public virtual void highlight(InputHandler input)
        {
#if WINDOWS
            if (hitBox.Contains((int)input.Location.X, (int)input.Location.Y))
                isHighlighted = true;
            else
                isHighlighted = false;
#endif

#if ZUNE
            if (input.pressActivated(hitBox, false))
                isHighlighted = true;
            else
                isHighlighted = false;
#endif
        }

        public virtual void select(InputHandler input)
        {
            if (input.pressActivated(hitBox))
                interval = counter;

            if (input.pressActivated(hitBox, true) && counter - interval < 10 && !isSelected)
            {
                Selected(this, new EventArgs());
                isSelected = true;
            }
            else
                isHighlighted = false;
        }

        public virtual void reset()
        {
            isHighlighted = false;
            isSelected = false;
        }

        public void deselect()
        {
            isSelected = false;
        }

        public void select()
        {
            isSelected = true;
        }
    }
}
