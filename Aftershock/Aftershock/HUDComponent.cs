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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Aftershock.Text;
using Aftershock.Graphics;

namespace Aftershock
{
    public class HUDComponent : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        TextManager text;
        GraphicsManager graphics;

        public HUDComponent(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            text = new TextManager();

            graphics = new GraphicsManager();
        }

        public override void Initialize()
        {
            text.initialize();

            graphics.initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            text.load(Game.Content);

            graphics.load(Game.Content);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            text.update(gameTime);

            graphics.update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            graphics.draw(spriteBatch);

            text.draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            text.unload();

            graphics.unload();

            base.UnloadContent();
        }

        public void load()
        {
            text.load(Game.Content);

            graphics.load(Game.Content);
        }

        public void draw()
        {
            spriteBatch.Begin();

            graphics.draw(spriteBatch);

            text.draw(spriteBatch);

            spriteBatch.End();
        }

        public void unload()
        {
            text.unload();

            graphics.unload();
        }

        public void addText(TextType newText)
        {
            text.addText(newText);
        }

        public void addContainer(TextContainer container, string key)
        {
            text.addContainer(container, key);
        }

        public void addContainer(GraphicsContainer container, string key)
        {
            graphics.addContainer(container, key);
        }

        public void addGraphic(GraphicType graphic)
        {
            graphics.addGraphic(graphic);
        }

        public void displayText(string containerKey, string newText, Vector2 vector, Color color)
        {
            text.displayText(containerKey, newText, vector, color);
        }

        public void displayGraphic(string containerKey, Vector2 vector, Color color)
        {
            graphics.displayGraphic(containerKey, vector, color);
        }
    }
}
