using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Elements;
using Reverb.Enumerations;

namespace Reverb.Components.Switches
{
    public class SwitchComponent : ComponentType, ICollectionComponent
    {
        #region Fields
        /// <summary>
        /// A list of all of the menu's selectable items.
        /// </summary>
        public List<SelectionType> switches;

        /// <summary>
        /// A standard sprite font for all of the menu options.
        /// </summary>
        SpriteFont font;

        /// <summary>
        /// A font asset, used for loading the font.
        /// </summary>
        string asset;

        /// <summary>
        /// The current updatable option.
        /// </summary>
        int index;
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

                foreach (SelectionType selection in switches)
                    positions.Add(selection.Position);

                return positions;
            }

            set
            {
                for (int i = 0; i < switches.Count; i++)
                    switches[i].Position = value[i];
            }
        }

        public List<Vector2> Origins
        {
            get
            {
                List<Vector2> origins = new List<Vector2>();

                foreach (SelectionType selection in switches)
                    origins.Add(selection.Origin);

                return origins;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    switches[i].Origin = value[i];
            }
        }

        public List<Color> Colors
        {
            get
            {
                List<Color> colors = new List<Color>();

                foreach (SelectionType selection in switches)
                    colors.Add(selection.Color);

                return colors;
            }

            set
            {
                for (int i = 0; i < switches.Count; i++)
                    switches[i].Color = value[i];
            }
        }

        public List<Rectangle> BoundingBoxes
        {
            get
            {
                List<Rectangle> boundingBoxes = new List<Rectangle>();

                foreach (SelectionType selection in switches)
                    boundingBoxes.Add(new Rectangle((int)selection.Position.X, (int)selection.Position.Y,
                                                    (int)font.MeasureString(selection.Text).X,
                                                    (int)font.MeasureString(selection.Text).Y));

                return boundingBoxes;
            }

            set { }
        }

        public List<string> Texts
        {
            get
            {
                List<string> text = new List<string>();

                foreach (SelectionType selection in switches)
                    text.Add(selection.Text);

                return text;
            }

            set
            {
                for (int i = 0; i < switches.Count; i++)
                    switches[i].Text = value[i];
            }
        }

        public List<float> Scales
        {
            get
            {
                List<float> scales = new List<float>();

                foreach (SelectionType selection in switches)
                    scales.Add(selection.Scale);

                return scales;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    switches[i].Scale = value[i];
            }
        }
        #endregion

        #region Constructors
        public SwitchComponent(string fontAsset)
        {
            identifier = "Switches";

            asset = fontAsset;
            switches = new List<SelectionType>();
        }
        #endregion

        #region Overridden Functions
        public override void initialize()
        {
            index = 0;

            switches[index].reset(true);

            foreach (SelectionType selection in switches)
                selection.Selected += Select;
        }

        public override void load(ContentManager content)
        {
            font = content.Load<SpriteFont>(asset);

            foreach (SelectionType selection in switches)
                selection.load(content, font);
        }

        public override void update(InputHandler input)
        {
            foreach (SelectionType selection in switches)
                selection.update(input);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            foreach (SelectionType selection in switches)
                selection.draw(spriteBatch, font);
        }
        #endregion

        #region Implemented Functions
        public void setEvent(EventHandler method, int selectionIndex)
        {
            switches[selectionIndex].Selected += method;
        }

        public void setEvents(EventHandler method)
        {
            foreach (SelectionType selection in switches)
                selection.Selected += method;
        }

        public void addSelection(SelectionType selection)
        {
            switches.Add(selection);
        }
        #endregion

        #region Class Functions
        public void setAlignment(TextAlignment textAlignment)
        {
            foreach (SelectionType selection in switches)
                selection.setAlignment(textAlignment);
        }
        #endregion

        #region Events
        void Select(object sender, EventArgs e)
        {
            SelectionType selection = sender as SelectionType;

            switches[index].reset(false);

            index = switches.IndexOf(selection);
        }
        #endregion
    }
}
