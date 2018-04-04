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
using Aftershock.Text;

namespace Aftershock.Effects
{
    public class FloatEffect : WrapperType, IEffect
    {
        Vector2 direction;
        float interval;
        float time;

        public float Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        public bool IsComplete
        {
            get
            {
                return (time >= interval);
            }
        }

        public FloatEffect(IEffect effect, Vector2 floatDirection, float speed)
            : base (effect)
        {
            direction = floatDirection;
            interval = speed;
        }

        public override void update(GameTime gameTime, IScreenObject screenObject)
        {
            if (time < interval)
            {
                screenObject.Position += direction;

                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.update(gameTime, screenObject);
        }

        public override void reset(IScreenObject screenObject)
        {
            time = 0;

            base.reset(screenObject);
        }
    }
}
