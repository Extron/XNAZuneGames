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
    /// allowing for transitions and dynamic value alterations.  This interface is designed for components with only one 
    /// object to be maniputlated.
    /// </summary>
    public interface IMenuComponent
    {
        /// <summary>
        /// The position of the component.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the origin of the component.
        /// </summary>
        Vector2 Origin { get; set; }

        /// <summary>
        /// The color of the component.  For graphics, this alters the texture's tint, and for text it alters the text's 
        /// color.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// The bounding box that completely encompasses the copmonent.  For graphics, this is the bounding box of the graphic.
        /// For text, this is the bouding box that contains the text.
        /// </summary>
        Rectangle BoundingBox { get; set; }

        /// <summary>
        /// Gets the component's text value, if the component is using a text-based object.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the component's scale value.
        /// </summary>
        float Scale { get; set; }
    }
}
