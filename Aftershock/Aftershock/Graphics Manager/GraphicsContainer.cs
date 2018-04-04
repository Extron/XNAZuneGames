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
using Aftershock.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Aftershock.Graphics
{
    public class GraphicsContainer
    {
        List<GraphicType> graphics;
        StoryboardType effects;
        Rectangle boundingBox;
        Texture2D graphic;
        Color color;
        Color[] array;
        string asset;

        public GraphicsContainer(string graphicAsset)
        {
            asset = graphicAsset;

            graphics = new List<GraphicType>();
            effects = new StoryboardType();
        }

        public GraphicsContainer(string graphicAsset, IEffect effect)
            : this(graphicAsset)
        {
            effects.addEffect(effect);
        }

        public GraphicsContainer(GraphicsDevice graphicsDevice, int textureWidth, int textureHeight, Color textureColor, IEffect effect)
        {
            effects = new StoryboardType();

            effects.addEffect(effect);

            color = textureColor;
            array = new Color[textureWidth * textureHeight];

            graphic = new Texture2D(graphicsDevice, textureWidth, textureHeight);
            graphics = new List<GraphicType>();
        }

        public GraphicsContainer(string graphicAsset, Rectangle textBoundingBox)
            : this(graphicAsset)
        {
            boundingBox = textBoundingBox;
        }

        public void intialize()
        {
            effects.Complete += RemoveGraphic;

            if (graphic != null)
            {
                for (int i = 0; i < array.Length; i++)
                    array[i] = color;
            }
        }

        public void load(ContentManager content)
        {
            if (graphic == null)
                graphic = content.Load<Texture2D>(asset);
            else
                graphic.SetData<Color>(array);
        }


        public void unload()
        {
            foreach (GraphicType item in graphics)
                item.unload();

            graphics.Clear();

            graphics = null;

            effects.unload();

            graphic.Dispose();
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < graphics.Count; i++)
            {
                graphics[i].update(gameTime);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (GraphicType item in graphics)
            {
                item.draw(spriteBatch);
            }
        }

        public void displayGraphic(Vector2 vector, Color color)
        {
            GraphicType newGraphic = new GraphicType(graphic, vector, color, effects);

            newGraphic.startEffect();

            graphics.Add(newGraphic);
        }

        public void addEffect(IEffect newEffect)
        {
            effects.addEffect(newEffect);
        }

        public void RemoveGraphic(IScreenObject screenObject)
        {
            GraphicType graphicToRemove = screenObject as GraphicType;

            graphics.Remove(graphicToRemove);
        }
    }
}
