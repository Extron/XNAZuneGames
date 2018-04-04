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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Aftershock.Effects;
using Aftershock.Delegates;

namespace Aftershock.Text
{
    /// <summary>
    /// Manages a text based object that is displayed with the HUD, overlaying the main game.
    /// </summary>
    public class TextType : IScreenObject
    {
        #region Fields
        /// <summary>
        /// A list of effects to run on the text.
        /// </summary>
        protected StoryboardType effects;

        /// <summary>
        /// The font to draw the text with.
        /// </summary>
        protected SpriteFont font;

        /// <summary>
        /// The position of the text within the HUD.
        /// </summary>
        protected Vector2 vector;

        /// <summary>
        /// The origin of the text.
        /// </summary>
        protected Vector2 origin;

        /// <summary>
        /// The color of the text.
        /// </summary>
        protected Color color;

        /// <summary>
        /// The scale of the text.
        /// </summary>
        protected float scale;

        /// <summary>
        /// The rotation of the text.
        /// </summary>
        protected float rotation;

        /// <summary>
        /// The text to be displayed.
        /// </summary>
        protected string text;

        /// <summary>
        /// The file name of the font to load from.
        /// </summary>
        protected string asset;

        /// <summary>
        /// Determines if the text is only drawn when the effects are running.
        /// </summary>
        protected bool drawText;
        #endregion

        #region Properties
        /// <summary>
        /// Provides a hook to the object's position (Implemented from IScreenObject).
        /// </summary>
        public Vector2 Position
        {
            get { return vector; }
            set { vector = value; }
        }

        /// <summary>
        /// Provides a hook to the object's origin (Implemented from IScreenObject).
        /// </summary>
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// Provides a hook to the object's color (Implemented from IScreenObject).
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Provides a hook to the object's text (Implemented from IScreenObject).
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// Provides a hook to the object's scale (Implemented from IScreenObject).
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Provides a hook to the object's rotation (Implemented from IScreenObject).
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a text-based object to display as part of the HUD.
        /// </summary>
        /// <param name="fontAsset">The name of the SpriteFont file to load the font from.</param>
        /// <param name="displayText">The text to display.</param>
        /// <param name="textVector">The position within the HUD to display from.</param>
        /// <param name="textColor">The text's color.</param>
        /// <param name="isVisibleConstantly">Allows the object to be visible even when the effects are not running.  Otherwise,
        /// the object will only be visible when the effects are running.</param>
        public TextType(string fontAsset, string displayText, Vector2 textVector, Color textColor, bool isVisibleConstantly)
        {
            asset = fontAsset;
            text = displayText;
            vector = textVector;
            color = textColor;
            effects = new StoryboardType();
            drawText = isVisibleConstantly;

            rotation = 0;
            scale = 1;
            origin = new Vector2();
        }

        /// <summary>
        /// Creates a text-based object to display as part of the HUD.
        /// </summary>
        /// <param name="fontAsset">The name of the SpriteFont file to load the font from.</param>
        /// <param name="displayText">The text to display.</param>
        /// <param name="textVector">The position within the HUD to display from.</param>
        /// <param name="textColor">The text's color.</param>
        /// <param name="isVisibleConstantly">Allows the object to be visible even when the effects are not running.  Otherwise,
        /// the object will only be visible when the effects are running.</param>
        /// <param name="textEffect">Adds the effect to the storyboard.</param>
        public TextType(string fontAsset, string displayText, Vector2 textVector, Color textColor, bool isVisibleConstantly, IEffect textEffect)
            : this(fontAsset, displayText, textVector, textColor, isVisibleConstantly)
        {
            effects.addEffect(textEffect);
        }

        /// <summary>
        /// Creates a text-based object to display as part of the HUD.
        /// </summary>
        /// <param name="font">The SpriteFont to draw the text with.</param>
        /// <param name="displayText">The text to display.</param>
        /// <param name="textVector">The position within the HUD to display from.</param>
        /// <param name="textColor">The text's color.</param>
        /// <param name="timeline">The storyboard of effects to run.</param>
        public TextType(SpriteFont textFont, string displayText, Vector2 textVector, Color textColor, StoryboardType timeline)
            : this("", displayText, textVector, textColor, true)
        {
            effects = timeline;
            font = textFont;
        }
        #endregion

        #region Game Methods
        public virtual void initialize()
        {
        }

        public virtual void load(ContentManager content)
        {
            font = content.Load<SpriteFont>(asset);
        }

        public virtual void unload()
        {
            effects.unload();

            font = null;
        }

        public virtual void update(GameTime gameTime)
        {
            if (effects != null)
                effects.update(gameTime, this);
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (drawText || effects.IsRunning)
                spriteBatch.DrawString(font, text, vector, color, rotation, origin, scale, SpriteEffects.None, 0f);
        }
        #endregion

        #region Class Methods
        public void addEffect(IEffect effect)
        {
            effects.addEffect(effect);
        }

        public void startEffect()
        {
            effects.start(this);
        }

        public void stopEffect()
        {
            effects.stop(this);
        }

        public Vector2 getCenter()
        {
            Vector2 center = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);

            return center;
        }
        #endregion
    }
}
