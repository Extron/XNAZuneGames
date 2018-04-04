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
using Microsoft.Xna.Framework;

namespace Aftershock.Text
{
    public class TextManager
    {
        Dictionary<string, TextContainer> containers;
        List<TextType> text;

        public TextManager()
        {
            containers = new Dictionary<string, TextContainer>();
            text = new List<TextType>();
        }

        public void initialize()
        {
            foreach (TextContainer container in containers.Values)
                container.intialize();

            foreach (TextType item in text)
                item.initialize();
        }

        public void load(ContentManager content)
        {
            foreach (TextContainer container in containers.Values)
                container.load(content);

            foreach (TextType item in text)
               item.load(content);
        }

        public void unload()
        {
            foreach (TextContainer container in containers.Values)
                container.unload();

            containers.Clear();

            foreach (TextType item in text)
                item.unload();

            text.Clear();
        }


        public void update(GameTime gameTime)
        {
            foreach (TextContainer container in containers.Values)
                container.update(gameTime);

            foreach (TextType item in text)
                item.update(gameTime);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (TextContainer container in containers.Values)
                container.draw(spriteBatch);

            foreach (TextType item in text)
                item.draw(spriteBatch);
        }

        public void addText(TextType item)
        {
            text.Add(item);
        }

        public void addContainer(TextContainer container, string key)
        {
            containers.Add(key, container);
        }

        public void displayText(string containerKey, string newText, Vector2 vector, Color color)
        {
            TextContainer container;

            if (containers.TryGetValue(containerKey, out container))
                container.displayText(newText, vector, color);
        }
    }
}
