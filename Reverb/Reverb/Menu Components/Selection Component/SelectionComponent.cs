using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Delegates;
using Reverb.Elements.Buttons;
using Reverb.Enumerations;

namespace Reverb.Components.Selections
{
    public class SelectionComponent : ComponentType, ICollectionComponent
    {
        public ChangeSelection Change;

        List<string> selections;
        ButtonType left;
        ButtonType right;
        SpriteFont font;
        Vector2 position;
        Vector2 origin;
        Color color;
        string asset;
        float scale;
        float rotation;
        int index;

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

                positions.Add(position);
                positions.Add(left.Position);
                positions.Add(right.Position);

                return positions;
            }

            set
            {
                position = value[0];
                left.Position = value[1];
                right.Position = value[2];
            }
        }

        public List<Vector2> Origins
        {
            get
            {
                List<Vector2> origins = new List<Vector2>();

                origins.Add(origin);
                origins.Add(left.Origin);
                origins.Add(right.Origin);

                return origins;
            }

            set
            {
                origin = value[0];
                left.Origin = value[1];
                right.Origin = value[2];
            }
        }

        public List<Color> Colors
        {
            get
            {
                List<Color> colors = new List<Color>();

                colors.Add(color);
                colors.Add(left.Color);
                colors.Add(right.Color);

                return colors;
            }

            set
            {
                color = value[0];
                left.Color = value[1];
                right.Color = value[2];
            }
        }

        public List<Rectangle> BoundingBoxes
        {
            get
            {
                List<Rectangle> boundingBoxes = new List<Rectangle>();

                boundingBoxes.Add(new Rectangle((int)position.X, (int)position.Y, right.BoundingBox.Width, right.BoundingBox.Height));
                boundingBoxes.Add(left.BoundingBox);
                boundingBoxes.Add(right.BoundingBox);

                return boundingBoxes;
            }

            set { }
        }

        public List<string> Texts
        {
            get
            {
                List<string> texts = new List<string>();

                foreach (string selection in selections)
                    texts.Add(selection);

                return selections;
            }

            set
            {
                for (int i = 0; i < selections.Count; i++)
                    selections[i] = value[i];
            }
        }

        public List<float> Scales
        {
            get
            {
                List<float> scales = new List<float>();

                scales.Add(scale);
                scales.Add(left.Scale);
                scales.Add(right.Scale);

                return scales;
            }

            set
            {
                scale = value[0];
                left.Scale = value[1];
                right.Scale = value[2];
            }
        }
        #endregion

        #region Constructors
        public SelectionComponent(string fontAsset, Vector2 textLocation, Color textColor)
        {
            identifier = "Selections";

            asset = fontAsset;
            position = textLocation;
            color = textColor;
            selections = new List<string>();
            scale = 1;
        }
        #endregion

        #region Overridden Functions
        public override void initialize()
        {
            index = 0;

            left.Pressed += MoveLeft;
            right.Pressed += MoveRight;
        }

        public override void load(ContentManager content)
        {
            font = content.Load<SpriteFont>(asset);

            left.load(content);
            right.load(content);
        }

        public override void update(InputHandler input)
        {
            if (left != null)
                left.update(input);

            if (right != null)
                right.update(input);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            left.draw(spriteBatch);
            right.draw(spriteBatch);

            spriteBatch.DrawString(font, selections[index], position, color, rotation, origin, scale, SpriteEffects.None, 0.5f);
        }
        #endregion

        #region Implemented Functions
        public void setEvent(ChangeSelection method)
        {
            Change += method;
        }

        public void setEvent(EventHandler method, int button)
        {
            switch (button)
            {
                case 0:
                    left.Pressed += method;
                    break;

                case 1:
                    right.Pressed += method;
                    break;
            }
        }

        public void setEvents(EventHandler method)
        {
            left.Pressed += method;

            right.Pressed += method;
        }

        public void addButton(ButtonType newButton, int button)
        {
            switch (button)
            {
                case 0:
                    left = newButton;
                    break;

                case 1:
                    right = newButton;
                    break;
            }
        }

        public void addSelection(string selection)
        {
            selections.Add(selection);
        }
        #endregion

        #region Class Functions
        public void setAlignment(TextAlignment textAlignment)
        {
        }
        #endregion

        #region Events
        void MoveLeft(object sender, EventArgs e)
        {
            if (index == 0)
                index = selections.Count - 1;
            else
                index--;

            if (Change != null)
                Change(selections[index]);
        }

        void MoveRight(object sender, EventArgs e)
        {
            if (index == selections.Count - 1)
                index = 0;
            else
                index++;

            if (Change != null)
                Change(selections[index]);
        }
        #endregion
    }
}
