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

namespace Reverb.Components.Options
{
    public class OptionsComponent : ComponentType, ICollectionComponent, IOptions
    {
        #region Fields
        /// <summary>
        /// A list of all of the menu's selectable items.
        /// </summary>
        public List<OptionType> options;

        /// <summary>
        /// A standard sprite font for all of the menu options.
        /// </summary>
        SpriteFont font;

        /// <summary>
        /// A font asset, used for loading the font.
        /// </summary>
        string asset;
        #endregion

        #region Properties
        public Vector2 Position { get; set; }

        public Vector2 Origin { get; set; }

        public Color Color { get; set; }

        public Rectangle BoundingBox { get; set; }

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
        #endregion

        #region Constructors
        public OptionsComponent(string fontAsset)
        {
            identifier = "Options";

            asset = fontAsset;
            options = new List<OptionType>();
        }
        #endregion

        #region Overridden Functions
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
            foreach (OptionType option in options)
                option.update(input);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            foreach (OptionType option in options)
                option.draw(spriteBatch, font);
        }
        #endregion

        #region Implemented Functions
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
            options.Add(option);
        }
        #endregion

        #region Class Functions
        public void setAlignment(TextAlignment textAlignment)
        {
            foreach (OptionType option in options)
                option.setAlignment(textAlignment);
        }
        #endregion
    }
}
