using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;

namespace Reverb.Components.Slides
{
    public class NumberSlideComponent : ComponentType, ICollectionComponent
    {
        public EventHandler ChangeValue;
        public EventHandler SetValue;

        Texture2D meter;
        Texture2D knob;
        Rectangle slideArea;
        Vector2 location;
        Vector2 knobLocation;
        Color color;
        Color knobColor;
        string meterAsset;
        string knobAsset;
        float meterValue;

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

                positions.Add(location);
                positions.Add(knobLocation);

                return positions;
            }

            set
            {
                location = value[0];
                knobLocation = value[1];
            }
        }

        public List<Vector2> Origins { get; set; }

        public List<Color> Colors
        {
            get
            {
                List<Color> colors = new List<Color>();

                colors.Add(color);
                colors.Add(knobColor);

                return colors;
            }

            set
            {
                color = value[0];
                knobColor = value[1];
            }
        }

        public List<Rectangle> BoundingBoxes
        {
            get
            {
                List<Rectangle> boundingBoxes = new List<Rectangle>();

                boundingBoxes.Add(new Rectangle((int)location.X, (int)location.Y, meter.Width, meter.Height));
                boundingBoxes.Add(new Rectangle((int)knobLocation.X, (int)knobLocation.Y, knob.Width, knob.Height));

                return boundingBoxes;
            }
            set { }
        }

        public List<string> Texts { get; set; }

        public List<float> Scales { get; set; }

        public float Value
        {
            get { return meterValue; }
            set
            {
                meterValue = value;
                setKnob();
            }
        }
        #endregion

        #region Constructors
        public NumberSlideComponent(string meterTextureAsset, string knobTextureAsset, Vector2 meterLocation)
        {
            identifier = "Number Slide";

            meterAsset = meterTextureAsset;
            knobAsset = knobTextureAsset;
            location = meterLocation;
            knobLocation = meterLocation;
            meterValue = 0f;
            color = Color.White;
            knobColor = Color.White;
        }

        public NumberSlideComponent(string meterTextureAsset, string knobTextureAsset, Vector2 meterLocation, Rectangle slideAreaRectangle)
        {
            identifier = "Number Slide";

            meterAsset = meterTextureAsset;
            knobAsset = knobTextureAsset;
            location = meterLocation;
            slideArea = slideAreaRectangle;

            knobLocation = new Vector2(slideArea.X, slideArea.Y);
            color = Color.White;
            knobColor = Color.White;
        }
        #endregion

        public override void initialize()
        {
            meterValue = 1.0f;
        }

        public override void load(ContentManager content)
        {
            meter = content.Load<Texture2D>(meterAsset);
            knob = content.Load<Texture2D>(knobAsset);

            if (slideArea.IsEmpty)
                slideArea = new Rectangle((int)location.X, (int)location.Y, meter.Width, meter.Height);

            if (SetValue != null)
                SetValue(this, new EventArgs());
        }

        public override void update(InputHandler input)
        {
            if (input.pressActivated(new Rectangle(slideArea.X, slideArea.Y, slideArea.Width - knob.Width, slideArea.Height), false))
            {
                knobLocation.X = input.Location.X;

                meterValue = (float)(knobLocation.X - slideArea.X) / (float)(slideArea.Width - knob.Width);

                ChangeValue(this, new EventArgs());
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(meter, location, color);

            spriteBatch.Draw(knob, knobLocation, knobColor);
        }

        public override void reset()
        {
            base.reset();

            SetValue(this, new EventArgs());
        }

        public void setKnob()
        {
            knobLocation.X = meterValue * (float)(slideArea.Width - knob.Width) + slideArea.X;
        }
    }
}
