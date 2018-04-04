using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Reverb.Components
{
    /// <summary>
    /// This interface allows other objects to manipulate the values within a given component that is using the interface,
    /// allowing for transitions and dynamic value alterations.  This interface is designed for components that contain 
    /// lists or groups of objects that need to be manipulated independantly from one another.
    /// </summary>
    public interface ICollectionComponent : IMenuComponent
    {
        /// <summary>
        /// Gets or sets a list of the positions of the objects within the component.
        /// </summary>
        List<Vector2> Positions { get; set; }

        /// <summary>
        /// Gets or sets a list of the origins of the objects within the component.
        /// </summary>
        List<Vector2> Origins { get; set; }

        /// <summary>
        /// Gets or sets a list of the colors of the objects within the component.
        /// </summary>
        List<Color> Colors { get; set; }

        /// <summary>
        /// Gets or sets a list of the bounding boxes of the objects within the component.
        /// </summary>
        List<Rectangle> BoundingBoxes { get; set; }

        /// <summary>
        /// Gets or sets a list of the text of the text-based objects within the component.
        /// </summary>
        List<string> Texts { get; set; }

        /// <summary>
        /// Gets or sets a list of the scales of the objects within the component.
        /// </summary>
        List<float> Scales { get; set; }
    }
}
