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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Aftershock.Effects;

namespace Aftershock
{
    public interface IScreenObject
    {
        Vector2 Position { get; set; }

        Vector2 Origin { get; set; }

        Color Color { get; set; }

        string Text { get; set; }

        float Scale { get; set; }

        float Rotation { get; set; }
 
        void initialize();

        void load(ContentManager content);

        void update(GameTime gameTime);

        void draw(SpriteBatch spriteBatch);

        void addEffect(IEffect effect);

        void startEffect();

        void stopEffect();

        Vector2 getCenter();
    }
}
