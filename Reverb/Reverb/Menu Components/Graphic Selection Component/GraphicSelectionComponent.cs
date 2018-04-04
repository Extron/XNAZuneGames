using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Elements;

namespace Reverb.Components.Selections
{
    public class GraphicSelectionComponent : ComponentType, ICollectionComponent
    {
        #region Fields
        /// <summary>
        /// A list of all of the menu's selectable items.
        /// </summary>
        List<GraphicType> graphics;

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

                foreach (GraphicType graphic in graphics)
                    positions.Add(graphic.Position);

                return positions;
            }

            set
            {
                for (int i = 0; i < graphics.Count; i++)
                    graphics[i].Position = value[i];
            }
        }

        public List<Vector2> Origins
        {
            get
            {
                List<Vector2> origins = new List<Vector2>();

                foreach (GraphicType graphic in graphics)
                    origins.Add(graphic.Origin);

                return origins;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    graphics[i].Origin = value[i];
            }
        }

        public List<Color> Colors
        {
            get
            {
                List<Color> colors = new List<Color>();

                foreach (GraphicType graphic in graphics)
                    colors.Add(graphic.Color);

                return colors;
            }

            set
            {
                for (int i = 0; i < graphics.Count; i++)
                    graphics[i].Color = value[i];
            }
        }

        public List<Rectangle> BoundingBoxes { get; set; }

        public List<string> Texts { get; set; }

        public List<float> Scales
        {
            get
            {
                List<float> scales = new List<float>();

                foreach (GraphicType graphic in graphics)
                    scales.Add(graphic.Scale);

                return scales;
            }

            set
            {
                for (int i = 0; i < value.Count; i++)
                    graphics[i].Scale = value[i];
            }
        }
        #endregion

        #region Constructors
        public GraphicSelectionComponent()
        {
            identifier = "Graphic Selections";

            graphics = new List<GraphicType>();
        }
        #endregion

        #region Overridden Functions
        public override void initialize()
        {
            index = 0;
        }

        public override void load(ContentManager content)
        {
            foreach (GraphicType graphic in graphics)
                graphic.load(content);
        }

        public override void update(InputHandler input)
        {
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            graphics[index].draw(spriteBatch);
        }
        #endregion

        #region Class Functions
        public void addGraphic(GraphicType graphic)
        {
            graphics.Add(graphic);
        }

        public void progress()
        {
            if (index < graphics.Count - 1)
                index++;
            else
                index = 0;
        }
        #endregion
    }
}
