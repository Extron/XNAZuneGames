using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Enumerations;

namespace Reverb.Components.Text
{
    public class TextComponent : ComponentType, IMenuComponent
    {
        SpriteFont font;
        TextAlignment alignment;
        Vector2 position;
        Vector2 origin;
        Color color;
        string text;
        string asset;
        float scale;
        float rotation;

        #region Properties
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

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                                       (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);
            }
            set { }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        #endregion

        public TextComponent(string fontAsset, string title, Vector2 titlePosition, Color textColor)
        {
            identifier = "Text";

            asset = fontAsset;
            text = title;
            position = titlePosition;
            color = textColor;

            origin = new Vector2();
            scale = 1f;
            rotation = 0f;
        }

        public override void initialize()
        {
            scale = 1;
        }

        public override void load(ContentManager content)
        {
            font = content.Load<SpriteFont>(asset);

            loadAlignment();
        }

        public override void update(InputHandler input)
        {
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        public void setAlignment(TextAlignment textAlignment)
        {
            alignment = textAlignment;
        }

        void loadAlignment()
        {
            switch (alignment)
            {
                case TextAlignment.right:
                    alignRight();
                    break;

                case TextAlignment.center:
                    alignCenter();
                    break;

                case TextAlignment.left:
                    alignLeft();
                    break;
            }
        }

        void alignRight()
        {
            origin = Vector2.Zero;
        }

        void alignCenter()
        {
            origin.X = font.MeasureString(text).X / 2;
        }

        void alignLeft()
        {
            origin.X = font.MeasureString(text).X;
        }
    }
}
