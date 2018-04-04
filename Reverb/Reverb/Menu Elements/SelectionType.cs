using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Elements.Highlighters;
using Reverb.Enumerations;

namespace Reverb.Elements
{
    public class SelectionType
    {
        public EventHandler Selected;

        HighlighterType highlighter;
        HighlighterType selector;
        TextAlignment alignment;
        Rectangle hitBox;
        Vector2 position;
        Vector2 origin;
        Color color;
        string text;
        float rotation;
        float scale;
        bool isHighlighted;
        bool isSelected;

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

        public SelectionType(string displayText, Vector2 textLocation, Color textColor, string highlighterAsset, string selectorAsset, Vector2 highlighterLocation)
        {
            text = displayText;
            position = textLocation;
            color = textColor;

            highlighter = new HighlighterType(highlighterAsset, highlighterLocation);
            selector = new HighlighterType(selectorAsset, highlighterLocation);
            alignment = TextAlignment.left;
            origin = new Vector2();
            rotation = 0f;
            scale = 1f;
        }

        public SelectionType(string displayText, Vector2 textLocation, Color textColor, TextAlignment textAlignment, string highlighterAsset, string selectorAsset, Vector2 highlighterLocation)
        {
            text = displayText;
            position = textLocation;
            color = textColor;
            alignment = textAlignment;

            highlighter = new HighlighterType(highlighterAsset, highlighterLocation);
            selector = new HighlighterType(selectorAsset, highlighterLocation);
            origin = new Vector2();
            rotation = 0f;
            scale = 1f;
        }

        public SelectionType(string displayText, Vector2 textLocation, Color textColor, TextAlignment textAlignment, string highlighterAsset, string selectorAsset)
        {
            text = displayText;
            position = textLocation;
            color = textColor;
            alignment = textAlignment;

            highlighter = new HighlighterType(highlighterAsset, textLocation);
            selector = new HighlighterType(selectorAsset, textLocation);
            origin = new Vector2();
            rotation = 0f;
            scale = 1f;
        }

        public void load(ContentManager content, SpriteFont font)
        {
            highlighter.load(content);
            selector.load(content);

            if (hitBox.IsEmpty)
            {
                if (highlighter == null)
                    hitBox = new Rectangle((int)position.X, (int)position.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);
                else
                    hitBox = highlighter.BoundingBox;
            }

            loadAlignment(font);
        }

        public void update(InputHandler input)
        {
            if (isHighlighted)
            {
                highlighter.update();
                select(input);
            }

            highlight(input);
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (isSelected)
                selector.draw(spriteBatch);

            if (isHighlighted)
                highlighter.draw(spriteBatch);

            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        public void highlight(InputHandler input)
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

        public void select(InputHandler input)
        {
            if (input.pressActivated(hitBox, true))
            {
                Selected(this, new EventArgs());
                isSelected = true;
            }
        }

        public void reset(bool select)
        {
            isSelected = select;
        }

        public void setAlignment(TextAlignment textAlignment)
        {
            alignment = textAlignment;
        }

        void loadAlignment(SpriteFont font)
        {
            switch (alignment)
            {
                case TextAlignment.right:
                    alignRight();
                    highlighter.alignRight();
                    selector.alignRight();
                    break;

                case TextAlignment.center:
                    alignCenter(font);
                    highlighter.alignCenter();
                    selector.alignCenter();
                    break;

                case TextAlignment.left:
                    alignLeft(font);
                    highlighter.alignLeft();
                    selector.alignLeft();
                    break;
            }
        }

        void alignRight()
        {
            origin = Vector2.Zero;
        }

        void alignCenter(SpriteFont font)
        {
            origin.X = font.MeasureString(text).X / 2;
        }

        void alignLeft(SpriteFont font)
        {
            origin.X = font.MeasureString(text).X;
        }
    }
}
