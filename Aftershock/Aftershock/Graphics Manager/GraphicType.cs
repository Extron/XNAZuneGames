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
using Aftershock.Effects;
using Aftershock.Delegates;
using Microsoft.Xna.Framework.Content;

namespace Aftershock.Graphics
{
    public class GraphicType : IScreenObject
    {
        StoryboardType effects;
        Texture2D graphic;
        Vector2 position;
        Vector2 origin;
        Color color;
        Color[] array;
        string asset;
        float scale;
        float rotation;
        bool drawGraphic;
        int width;
        int height;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public string Text { get; set; }

        public GraphicType(string textureAsset, Vector2 location, bool draw)
        {
            asset = textureAsset;
            position = location;
            drawGraphic = draw;

            effects = new StoryboardType();
            color = Color.White;
            origin = new Vector2();
            scale = 1f;
            rotation = 0f;
        }

        public GraphicType(string textureAsset, Vector2 location, Vector2 textureOrigin, bool draw)
            : this(textureAsset, location, draw)
        {
            origin = textureOrigin;
        }

        public GraphicType(GraphicsDevice graphics, int textureWidth, int textureHeight, Vector2 location, Color textureColor, bool draw)
        {
            width = textureWidth;
            height = textureHeight;
            position = location;
            color = textureColor;
            drawGraphic = draw;

            graphic = new Texture2D(graphics, width, height);
            effects = new StoryboardType();
            origin = new Vector2();
            scale = 1f;
            rotation = 0f;
        }

        public GraphicType(Texture2D texture, Vector2 location, Color tint, StoryboardType newEffects)
        {
            graphic = texture;
            position = location;
            color = tint;
            effects = newEffects;

            drawGraphic = true;
            origin = new Vector2();
            scale = 1f;
            rotation = 0f;
        }

        public void initialize()
        {
            if (width != 0 && height != 0)
            {
                array = new Color[width * height];

                for (int i = 0; i < array.Length; i++)
                    array[i] = color;
            }

            effects.Complete += StopGraphic;
        }

        public void load(ContentManager content)
        {
            if (asset != null)
                graphic = content.Load<Texture2D>(asset);
            else if (width != 0 && height != 0)
                graphic.SetData<Color>(array);
        }

        public void unload()
        {
            effects.unload();
            graphic = null;
            array = null;
        }

        public void update(GameTime gameTime)
        {
            if (effects != null)
                effects.update(gameTime, this);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (drawGraphic || effects.IsRunning)
                spriteBatch.Draw(graphic, position, null, color, rotation, origin, scale, SpriteEffects.None, 0.5f); 
        }

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
            return origin;
        }

        public void StopGraphic(IScreenObject screenObject)
        {
            drawGraphic = false;
        }
    }
}
