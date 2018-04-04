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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Aftershock.Graphics
{
    class GraphicsManager
    {
        Dictionary<string, GraphicsContainer> containers;
        List<GraphicType> graphics;

        public GraphicsManager()
        {
            containers = new Dictionary<string, GraphicsContainer>();
            graphics = new List<GraphicType>();
        }

        public void initialize()
        {
            foreach (GraphicsContainer container in containers.Values)
                container.intialize();

            foreach (GraphicType graphic in graphics)
                graphic.initialize();
        }

        public void load(ContentManager content)
        {
            foreach (GraphicsContainer container in containers.Values)
                container.load(content);

            foreach (GraphicType graphic in graphics)
                graphic.load(content);
        }

        public void unload()
        {
            foreach (GraphicsContainer container in containers.Values)
                container.unload();

            containers.Clear();

            foreach (GraphicType graphic in graphics)
                graphic.unload();

            graphics.Clear();
        }

        public void update(GameTime gameTime)
        {
            foreach (GraphicsContainer container in containers.Values)
                container.update(gameTime);

            foreach (GraphicType graphic in graphics)
                graphic.update(gameTime);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (GraphicsContainer container in containers.Values)
                container.draw(spriteBatch);

            foreach (GraphicType graphic in graphics)
                graphic.draw(spriteBatch);
        }

        public void addGraphic(GraphicType graphic)
        {
            graphics.Add(graphic);
        }

        public void addContainer(GraphicsContainer container, string key)
        {
            containers.Add(key, container);
        }

        public void displayGraphic(string containerKey, Vector2 vector, Color color)
        {
            GraphicsContainer container;

            if (containers.TryGetValue(containerKey, out container))
                container.displayGraphic(vector, color);
        }
    }
}