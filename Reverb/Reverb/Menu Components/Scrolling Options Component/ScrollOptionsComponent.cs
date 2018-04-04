using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Delegates;
using Reverb.Elements;
using Reverb.Enumerations;
using Reverb.Components.Options;

namespace Reverb.Components.Scrolling
{
    public class ScrollOptionsComponent : ComponentType, ICollectionComponent, IOptions
    {
        List<OptionType> options;
        SpriteFont font;
        Rectangle boundingBox;
        string asset;
        float deceleration;
        int counter;
        int swipeInterval;

        #region Properties
        public Vector2 Position { get; set; }

        public Vector2 Origin { get; set; }

        public Color Color { get; set; }

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public string Text { get; set; }

        public float Scale { get; set; }

        public List<Vector2> Positions
        {
            get
            {
                List<Vector2> positions = new List<Vector2>();

                foreach (OptionType option in options)
                    positions.Add(option.Position);

                return positions;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    options[i].Position = value[i];
            }
        }

        public List<Vector2> Origins
        {
            get
            {
                List<Vector2> origins = new List<Vector2>();

                foreach (OptionType option in options)
                    origins.Add(option.Origin);

                return origins;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    options[i].Origin = value[i];
            }
        }

        public List<Color> Colors
        {
            get
            {
                List<Color> colors = new List<Color>();

                foreach (OptionType option in options)
                    colors.Add(option.Color);

                return colors;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    options[i].Color = value[i];
            }
        }

        public List<Rectangle> BoundingBoxes
        {
            get
            {
                List<Rectangle> boundingBoxes = new List<Rectangle>();

                foreach (OptionType option in options)
                    boundingBoxes.Add(new Rectangle((int)option.Position.X, (int)option.Position.Y,
                                                    (int)font.MeasureString(option.Text).X,
                                                    (int)font.MeasureString(option.Text).Y));

                return boundingBoxes;
            }

            set { }
        }

        public List<string> Texts
        {
            get
            {
                List<string> text = new List<string>();

                foreach (OptionType option in options)
                    text.Add(option.Text);

                return text;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    options[i].Text = value[i];
            }
        }

        public List<float> Scales
        {
            get
            {
                List<float> scales = new List<float>();

                foreach (OptionType option in options)
                    scales.Add(option.Scale);

                return scales;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    options[i].Scale = value[i];
            }
        }

        public int Count
        {
            get { return options.Count; }
        }
        #endregion

        public ScrollOptionsComponent(string fontAsset, Rectangle optionsBoundingBox)
        {
            identifier = "Scroll Options";

            asset = fontAsset;
            boundingBox = optionsBoundingBox;

            options = new List<OptionType>();
        }

        public override void initialize()
        {
        }

        public override void load(ContentManager content)
        {
            font = content.Load<SpriteFont>(asset);

            foreach (OptionType option in options)
                option.load(content, font);
        }

        public override void update(InputHandler input)
        {
            if (input.pressActivated(boundingBox))
                swipeInterval = counter;

            if (input.pressActivated(boundingBox, false))
            {
                if (options[0].Position.Y + -input.Distance.Y <= boundingBox.Y && options[options.Count - 1].Position.Y + -input.Distance.Y >= boundingBox.Bottom)
                {
                    foreach (OptionType option in options)
                    {
                        Vector2 newPosition = new Vector2(option.Position.X, option.Position.Y + -input.Distance.Y);

                        option.Position = newPosition;
                    }
                }
            }

            Vector2 magnitude;

            if (input.swipeActivated(boundingBox, new Vector2(0, 50), out magnitude) && counter - swipeInterval < 20)
            {
                deceleration = -magnitude.Y / 3;
            }

            if (input.swipeActivated(boundingBox, new Vector2(0, -50), out magnitude) && counter - swipeInterval < 20)
            {
                deceleration = -magnitude.Y / 3;
            }

            if (options[0].Position.Y + deceleration <= boundingBox.Y && options[options.Count - 1].Position.Y + deceleration >= boundingBox.Bottom)
            {
                foreach (OptionType option in options)
                {
                    Vector2 newPosition = new Vector2(option.Position.X, option.Position.Y + deceleration);

                    option.Position = newPosition;
                }
            }

            deceleration *= 0.9f;

            for (int i = 0; i < options.Count; i++)
            {
                if (boundingBox.Contains((int)options[i].Position.X, (int)options[i].Position.Y))
                    options[i].update(input);
            }

            counter++;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            foreach (OptionType option in options)
            {
                if (boundingBox.Contains((int)option.Position.X, (int)option.Position.Y))
                    option.draw(spriteBatch, font);
            }
        }

        public override void reset()
        {
            Vector2 distance = new Vector2(boundingBox.X, boundingBox.Y) - options[0].Position;

            foreach (OptionType option in options)
                option.Position += distance;

            base.reset();
        }

        #region Class Functions
        public void setEvent(Select method, int optionIndex)
        {
            options[optionIndex].Selected += method;
        }

        public void setEvents(Select method)
        {
            foreach (OptionType option in options)
                option.Selected += method;
        }

        public void addOption(OptionType option)
        {
            option.Index = options.Count;

            options.Add(option);
        }

        public OptionType getOption(int index)
        {
            return options[index];
        }

        public void clearOptions(int index)
        {
            options.RemoveRange(index, (options.Count - 1) - index);
        }

        public void clearOptions(int index, bool clearAll)
        {
            if (clearAll)
                options.Clear();
            else
                options.RemoveRange(index, options.Count - 1);
        }

        public void setAlignment(TextAlignment textAlignment)
        {
            foreach (OptionType option in options)
                option.setAlignment(textAlignment);
        }
        #endregion
    }
}
