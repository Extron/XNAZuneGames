using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Elements;

namespace Reverb.Components.Previews
{
    public class PreviewListComponent : ComponentType, ICollectionComponent
    {
        #region Fields
        /// <summary>
        /// A list of all of the menu's selectable items.
        /// </summary>
        public List<PreviewType> previews;

        /// <summary>
        /// The current updatable option.
        /// </summary>
        int previewIndex;
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

                foreach (PreviewType preview in previews)
                    positions.Add(preview.Position);

                return positions;
            }

            set
            {
                for (int i = 0; i < previews.Count; i++)
                    previews[i].Position = value[i];
            }
        }

        public List<Vector2> Origins
        {
            get
            {
                List<Vector2> origins = new List<Vector2>();

                foreach (PreviewType preview in previews)
                    origins.Add(preview.Origin);

                return origins;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    previews[i].Origin = value[i];
            }
        }

        public List<Color> Colors
        {
            get
            {
                List<Color> colors = new List<Color>();

                foreach (PreviewType preview in previews)
                    colors.Add(preview.Color);

                return colors;
            }

            set
            {
                for (int i = 0; i < previews.Count; i++)
                    previews[i].Color = value[i];
            }
        }

        public List<Rectangle> BoundingBoxes
        {
            get
            {
                List<Rectangle> boundingBoxes = new List<Rectangle>();

                foreach (PreviewType preview in previews)
                    boundingBoxes.Add(new Rectangle((int)preview.Position.X, (int)preview.Position.Y, preview.Texture.Width, preview.Texture.Height));

                return boundingBoxes;
            }

            set { }
        }

        public List<string> Texts { get; set; }

        public List<float> Scales
        {
            get
            {
                List<float> scales = new List<float>();

                foreach (PreviewType preview in previews)
                    scales.Add(preview.Scale);

                return scales;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    previews[i].Scale = value[i];
            }
        }
        #endregion

        #region Constructors
        public PreviewListComponent()
        {
            identifier = "Preview List";

            previews = new List<PreviewType>();
        }
        #endregion

        #region Overridden Functions
        public override void initialize()
        {
            previewIndex = 0;

            previews[previewIndex].select();

            foreach (PreviewType preview in previews)
                preview.Selected += Select;
        }

        public override void load(ContentManager content)
        {
            foreach (PreviewType preview in previews)
                preview.load(content);
        }

        public override void update(InputHandler input)
        {
            foreach (PreviewType preview in previews)
                preview.update(input);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            foreach (PreviewType preview in previews)
                preview.draw(spriteBatch);
        }
        #endregion

        #region Implemented Functions
        public void setEvent(EventHandler method, int previewIndex)
        {
            previews[previewIndex].Selected += method;
        }

        public void setEvents(EventHandler method)
        {
            foreach (PreviewType preview in previews)
                preview.Selected += method;
        }

        public void addSelection(PreviewType selection)
        {
            previews.Add(selection);
        }
        #endregion

        #region Class Functions
        public void addPreview(PreviewType preview)
        {
            previews.Add(preview);
        }
        #endregion

        #region Events
        void Select(object sender, EventArgs e)
        {
            PreviewType preview = sender as PreviewType;

            previews[previewIndex].deselect();

            previewIndex = previews.IndexOf(preview);
        }
        #endregion
    }
}
