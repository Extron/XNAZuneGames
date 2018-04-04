#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of Mach.
 * 
 * Mach is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * Mach is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with Mach.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Mach
{
    public class InstanceType
    {
        SoundEffectInstance instance;
        SoundType instanceType;
        bool active;

        public float Volume
        {
            get { return instance.Volume; }
            set { instance.Volume = value; }
        }

        public InstanceType(SoundEffectInstance soundInstance, SoundType soundType, float volume, bool soundActive)
        {
            instance = soundInstance;
            instanceType = soundType;
            active = soundActive;

            switch (instanceType)
            {
                case SoundType.instance:
                    instance.IsLooped = false;
                    instance.Volume = volume;
                    break;

                case SoundType.loop:
                    instance.IsLooped = true;
                    instance.Volume = volume;
                    break;

                case SoundType.background:
                    instance.IsLooped = false;
                    instance.Volume = volume * 0.5f;
                    break;

                case SoundType.music:
                    instance.IsLooped = true;
                    instance.Volume = volume * 0.5f;
                    break;
            }
        }

        public void playInstance()
        {
            if (active && instance.State != SoundState.Playing)
            {
                if (instance.State == SoundState.Paused)
                    instance.Resume();
                else 
                    instance.Play();
            }
        }

        public void playInstance(float pitch, float pan)
        {
            if (active && instance.State != SoundState.Playing)
            {
                instance.Pitch = pitch;
                instance.Pan = pan;

                if (instance.State == SoundState.Paused)
                    instance.Resume();
                else 
                    instance.Play();
            }
        }

        public void pauseInstance()
        {
            if (active && instance.State == SoundState.Playing)
            {
                instance.Pause();
            }
        }

        public void stopInstance()
        {
            if (active && instance.State != SoundState.Stopped)
            {
                instance.Stop();
            }
        }

        public void stopInstance(bool immediate)
        {
            if (active && instance.State != SoundState.Stopped)
            {
                if (instance.IsLooped)
                {
                    instance.Stop(immediate);
                }
                else
                {
                    if (immediate)
                        instance.Stop();
                }
            }
        }
    }
}
