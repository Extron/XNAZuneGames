/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of ZHUD.
 * 
 * ZHUD is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * ZHUD is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with ZHUD.  If not, see <http://www.gnu.org/licenses/>.
 */


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

namespace ZHUD
{
    public class HUDComponent : DrawableGameComponent
    {
        Dictionary<string, BaseTextType> text;
        SpriteBatch spriteBatch;

        public HUDComponent(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            text = new Dictionary<string, BaseTextType>();
        }

        #region Overrided Functions
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (BaseTextType item in text.Values)
                item.load(Game.Content);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (BaseTextType entry in text.Values)
                entry.update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (BaseTextType entry in text.Values)
                entry.draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Add Functions
        public void addText(BaseTextType t)
        {
            text.Add(t.Text, t);
        }
        #endregion

        public BaseTextType getText(string str)
        {
            try
            {
                return text[str];
            }
            catch
            {
                return null;
            }
        }
    }
}
