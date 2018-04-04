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
    public class CountingEffect : WrapperType, IEffect
    {
        float interval;
        float time;
        int oldValue;
        int newValue;
        int currentValue;

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

        public CountingEffect(IEffect effect, float speed)
            : base(effect)
        {
            interval = speed;
            time = 0;
        }

        public override void update(GameTime gameTime, IScreenObject screenObject)
        {
            ValueTextType<int> text = screenObject as ValueTextType<int>;

            if (time == 0)
            {
                if (text != null)
                {
                    newValue = Int32.Parse(text.Text);
                    currentValue = oldValue;
                }
            }
 
            if (time < interval)
            {
                if (text != null)
                {
                    int difference = newValue - oldValue;

                    if ((int)(difference / (interval / gameTime.ElapsedGameTime.TotalSeconds)) < newValue - currentValue)
                        currentValue += (int)Math.Round((difference / (interval / gameTime.ElapsedGameTime.TotalSeconds)));
                    else
                        currentValue = newValue;

                    text.Text = currentValue.ToString();
                }

                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.update(gameTime, screenObject);
        }

        public override void reset(IScreenObject screenObject)
        {
            ValueTextType<int> text = screenObject as ValueTextType<int>;

            if (text != null)
            {
                oldValue = Int32.Parse(text.Text);
            }

            time = 0;

            base.reset(screenObject);
        }
    }
}
