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
using Aftershock.Text;
using Microsoft.Xna.Framework;

namespace Aftershock.Effects
{
    public class WrapperType
    {
        protected IEffect baseEffect;

        public WrapperType(IEffect effect)
        {
            baseEffect = effect;
        }

        public virtual void update(GameTime gameTime, IScreenObject screenObject)
        {
            if (baseEffect != null)
                baseEffect.update(gameTime, screenObject);
        }

        public virtual void reset(IScreenObject screenObject)
        {
            if (baseEffect != null)
                baseEffect.reset(screenObject);
        }
    }
}
