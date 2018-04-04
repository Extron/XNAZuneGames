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
using Aftershock.Delegates;
using Microsoft.Xna.Framework;

namespace Aftershock.Effects
{
    public class StoryboardType
    {
        public EffectComplete Complete;

        List<IEffect> effects;
        ScreenObjectValues defaultValues;
        bool run;
        int currentEffect;

        public bool IsRunning
        {
            get { return run; }
        }

        public StoryboardType()
        {
            effects = new List<IEffect>();
        }

        public void update(GameTime gameTime, IScreenObject screenObject)
        {
            if (run)
            {
                if (effects.Count > 0)
                {
                    effects[currentEffect].update(gameTime, screenObject);

                    if (effects[currentEffect].IsComplete)
                    {
                        if (currentEffect < effects.Count - 1)
                            currentEffect++;
                        else
                        {
                                if (Complete != null)
                                    Complete(screenObject);

                                currentEffect = 0;

                                foreach (IEffect effect in effects)
                                    effect.reset(screenObject);

                                defaultValues.resetscreenObject(screenObject);

                                run = false;
                        }
                    }
                }
            }
        }

        public void unload()
        {
            if (effects != null)
            {
                for (int i = 0; i < effects.Count; i++)
                    effects[i] = null;

                effects.Clear();

                effects = null;

                currentEffect = 0;
            }
        }

        public void addEffect(IEffect effect)
        {
            effects.Add(effect);
        }

        public void start(IScreenObject screenObject)
        {
            defaultValues.setDefaults(screenObject);

            run = true; 
        }

        public void stop(IScreenObject screenObject)
        {
            defaultValues.resetscreenObject(screenObject);

            run = false;
        }
    }
}
