using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reverb.Elements.Highlighters;
using Reverb.Delegates;
using Reflex;
using Reverb.Enumerations;
using Reverb.Arguments;

namespace Reverb.Elements
{
    public class OptionType
    {
        public Select Selected;

        OptionAction action;
        Rectangle hitBox;
        HighlighterType highlighter;
        TextAlignment alignment;

        Vector2 position;
        Vector2 origin;
        Color color;
        string text;
        string link;
        float scale;
        float rotation;
        bool isHighlighted;
        bool activateIntro;
        bool activateSelect;
        int interval;
        int counter;
        int index;

        public string Text
        {
            get { return text; }
            set { text = value; }
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

        public bool ActivateIntro
        {
            get { return activateIntro; }
            set { activateIntro = value; }
        }

        public bool ActivateSelect
        {
            get { return activateSelect; }
            set { activateSelect = value; }
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        #region Constructors
        public OptionType(string optionText, Vector2 vector, Color textColor, OptionAction optionAction, string menuLink, bool intro, bool select, string highlighterAsset)
        {
            text = optionText;
            position = vector;
            color = textColor;
            action = optionAction;
            link = menuLink;
            activateIntro = intro;
            activateSelect = select;

            highlighter = new HighlighterType(highlighterAsset, this);
            origin = new Vector2();
            scale = 1f;
            rotation = 0f;
        }

        public OptionType(string optionText, Vector2 vector, Color textColor, OptionAction optionAction, bool intro, bool select, string highlighterAsset, Vector2 highlighterLocation)
        {
            text = optionText;
            position = vector;
            color = textColor;
            action = optionAction;
            link = "";
            activateIntro = intro;
            activateSelect = select;

            highlighter = new HighlighterType(highlighterAsset, highlighterLocation);
            origin = new Vector2();
            scale = 1f;
            rotation = 0f;
        }

        public OptionType(string optionText, Vector2 vector, Color textColor, OptionAction optionAction, string menuLink, string highlighterAsset, Vector2 highlighterLocation)
        {
            text = optionText;
            position = vector;
            color = textColor;
            action = optionAction;
            link = menuLink;

            highlighter = new HighlighterType(highlighterAsset, highlighterLocation);
            origin = new Vector2();
            activateIntro = true;
            activateSelect = true;
            scale = 1f;
            rotation = 0f;
        }

        public OptionType(string optionText, Vector2 vector, Color textColor, Rectangle optionHitBox, OptionAction optionAction, string highlighterAsset, Vector2 highlighterLocation)
        {
            text = optionText;
            position = vector;
            color = textColor;
            action = optionAction;
            hitBox = optionHitBox;

            highlighter = new HighlighterType(highlighterAsset, highlighterLocation);
            origin = new Vector2();
            activateIntro = true;
            activateSelect = true;
            scale = 1f;
            rotation = 0f;
        }

        public OptionType(string optionText, Vector2 vector, Color textColor, Rectangle optionHitBox, OptionAction optionAction, string menuLink, string highlighterAsset, Vector2 highlighterLocation)
        {
            text = optionText;
            position = vector;
            color = textColor;
            action = optionAction;
            hitBox = optionHitBox;
            link = menuLink;

            highlighter = new HighlighterType(highlighterAsset, highlighterLocation);
            origin = new Vector2();
            activateIntro = true;
            activateSelect = true;
            scale = 1f;
            rotation = 0f;
        }

        public OptionType(OptionType option)
        {
            text = option.text;
            position = option.position;
            color = option.color;
            action = option.action;
            hitBox = option.hitBox;
            link = option.link;

            highlighter = new HighlighterType(option.highlighter);
            origin = option.origin;
            activateIntro = option.activateIntro;
            activateSelect = option.activateSelect;
            scale = option.scale;
            rotation = option.rotation;
        }
        #endregion

        public virtual void load(ContentManager content, SpriteFont font)
        {
            highlighter.load(content);

            loadAlignment(font);

            if (hitBox.IsEmpty)
            {
                if (highlighter == null)
                    hitBox = new Rectangle((int)position.X, (int)position.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);
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

        public virtual void draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (isHighlighted)
                highlighter.draw(spriteBatch);

            try
            {
                spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0f);
            }
            catch
            {
            }
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

            if (input.pressActivated(hitBox, true) && counter - interval < 10)
                Selected(new OptionArgs(action, link, text, activateIntro, activateSelect, index));
            else
                isHighlighted = false;
        }

        public virtual void reset()
        {
            isHighlighted = false;
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
                    break;

                case TextAlignment.center:
                    alignCenter(font);
                    highlighter.alignCenter();
                    break;

                case TextAlignment.left:
                    alignLeft(font);
                    highlighter.alignLeft();
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
