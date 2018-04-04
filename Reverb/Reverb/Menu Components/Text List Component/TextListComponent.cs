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
    public class TextListComponent : ComponentType, ICollectionComponent
    {
        List<string> text;
        List<Vector2> positions;
        List<Color> colors;
        List<Vector2> origins;
        List<float> scales;
        List<float> rotations;
        SpriteFont font;
        TextAlignment alignment;
        string asset;

        #region Properties
        public Vector2 Position { get; set; }

        public Vector2 Origin { get; set; }

        public Color Color { get; set; }

        public Rectangle BoundingBox { get; set; }

        public string Text { get; set; }

        public float Scale { get; set; }

        public List<Vector2> Positions
        {
            get { return positions; }
            set { positions = value; }
        }

        public List<Vector2> Origins
        {
            get { return origins; }
            set { origins = value; }
        }

        public List<Color> Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        public List<Rectangle> BoundingBoxes
        {
            get
            {
                List<Rectangle> boundingBoxes = new List<Rectangle>();

                for (int i = 0; i < text.Count; i++)
                    boundingBoxes.Add(new Rectangle((int)positions[i].X, (int)positions[i].Y,
                                                    (int)font.MeasureString(text[i]).X,
                                                    (int)font.MeasureString(text[i]).Y));

                return boundingBoxes;
            }

            set { }
        }

        public List<string> Texts
        {
            get { return text; }
            set { text = value; }
        }

        public List<float> Scales
        {
            get { return scales; }
            set { scales = value; }
        }
        #endregion

        public TextListComponent(string fontAsset)
        {
            identifier = "Text List";

            asset = fontAsset;

            text = new List<string>();
            positions = new List<Vector2>();
            colors = new List<Color>();
            origins = new List<Vector2>();
            scales = new List<float>();
            rotations = new List<float>();
        }

        public override void initialize()
        {
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
            for (int i = 0; i < text.Count; i++)
                spriteBatch.DrawString(font, text[i], positions[i], colors[i], rotations[i], origins[i], scales[i], SpriteEffects.None, 0f);
        }

        public void addText(string newText, Vector2 textPosition, Color textColor)
        {
            text.Add(newText);
            positions.Add(textPosition);
            colors.Add(textColor);
            origins.Add(new Vector2());
            scales.Add(1f);
            rotations.Add(0f);
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
            for (int i = 0; i < origins.Count; i++)
                origins[i] = Vector2.Zero;
        }

        void alignCenter()
        {
            for (int i = 0; i < origins.Count; i++)
            {
                Vector2 newVector = origins[i];
                newVector.X = font.MeasureString(text[i]).X / 2;
                origins[i] = newVector;
            }
        }

        void alignLeft()
        {
            for (int i = 0; i < origins.Count; i++)
            {
                Vector2 newVector = origins[i];
                newVector.X = font.MeasureString(text[i]).X;
                origins[i] = newVector;
            }
        }
    }
}
