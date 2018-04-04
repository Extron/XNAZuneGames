#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of Aftershock.
 * 
 * Aftershock is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * Aftershock is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with Aftershock.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Aftershock.Effects;
using Microsoft.Xna.Framework;

namespace Aftershock.Text
{
    /// <summary>
    /// Creates a container for non-static text to be drawn at any time during runtime.  A container is an object 
    /// that holds information that is used to draw the object.  A TextContainer contains data like fonts and effects
    /// that are applied to each text object that is drawn to the container.
    /// </summary>
    public class TextContainer
    {
        #region Fields
        /// <summary>
        /// The list of text to draw within the container.
        /// </summary>
        List<TextType> text;

        /// <summary>
        /// The font which all text objects are drawn with.
        /// </summary>
        SpriteFont font;

        /// <summary>
        /// The list of effects that are applied to all added text objects.
        /// </summary>
        StoryboardType effects;

        /// <summary>
        /// An optional area on the screen that the text can be drawn to.
        /// </summary>
        Rectangle boundingBox;

        /// <summary>
        /// The name of the SpriteFont to load the font from.
        /// </summary>
        string asset;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a basic container with no initial effects.
        /// </summary>
        /// <param name="fontAsset">The name of the SpriteFont file used to load the font.</param>
        public TextContainer(string fontAsset)
        {
            asset = fontAsset;

            text = new List<TextType>();
            effects = new StoryboardType();
        }

        /// <summary>
        /// Creates a basic container with an innitial effect added to the storyboard.
        /// </summary>
        /// <param name="fontAsset">The name of the SpriteFont file used to load the font.</param>
        /// <param name="effect">The effect to add to the storyboard.</param>
        public TextContainer(string fontAsset, IEffect effect)
            : this(fontAsset)
        {
            effects.addEffect(effect);
        }

        /// <summary>
        /// Creates a basic container with a bounding box.
        /// </summary>
        /// <param name="fontAsset">The name of the SpriteFont file used to load the font.</param>
        /// <param name="textBoundingBox">The bounding box that objects are drawn in.</param>
        public TextContainer(string fontAsset, Rectangle textBoundingBox)
            : this(fontAsset)
        {
            boundingBox = textBoundingBox;
        }
        #endregion

        #region Game Methods
        public void intialize()
        {
            effects.Complete += RemoveText;
        }

        public void load(ContentManager content)
        {
            font = content.Load<SpriteFont>(asset);
        }

        public void unload()
        {
            for (int i = 0; i < text.Count; i++)
                text[i] = null;

            text.Clear();

            text = null;

            font = null;

            effects.unload();

            asset = "";
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < text.Count; i++)
            {
                text[i].update(gameTime);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (TextType item in text)
            {
                item.draw(spriteBatch);
            }
        }
        #endregion

        #region Class Methods
        public void displayText(string newText, Vector2 vector, Color color)
        {
            TextType newTextType = new TextType(font, newText, vector, color, effects);

            newTextType.startEffect();

            text.Add(newTextType);
        }

        public void addEffect(IEffect newEffect)
        {
            effects.addEffect(newEffect);
        }

        public void RemoveText(IScreenObject screenObject)
        {
            TextType textToRemove = screenObject as TextType;

            text.Remove(textToRemove);
        }
        #endregion
    }
}
