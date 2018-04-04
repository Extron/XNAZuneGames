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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Mach
{
    public class SoundManager
    {
        static Dictionary<string, SoundEffect> audio;
        static List<string> audioAssets;
        static float volume;
        static bool active;

        public static float Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public static bool IsActive
        {
            get { return active; }
            set { active = value; }
        }

        public SoundManager()
        {
            audio = new Dictionary<string, SoundEffect>();
            audioAssets = new List<string>();
            volume = 1;
            active = true;
        }

        public void load(ContentManager content)
        {
            foreach (string asset in audioAssets)
                audio.Add(asset, content.Load<SoundEffect>(asset));
        }

        public void addSound(string asset)
        {
            audioAssets.Add(asset);
        }

        public static void playSound(string asset)
        {
            SoundEffect effect;

            if (audio.TryGetValue(asset, out effect))
            {
                if (active)
                    effect.Play(volume, 0f, 0f);
            }
        }

        public static void playSound(string asset, float pitch, float pan)
        {
            SoundEffect effect;

            if (audio.TryGetValue(asset, out effect))
            {
                if (active)
                    effect.Play(volume, pitch, pan);
            }
        }

        public static InstanceType getInstance(string asset, SoundType soundType)
        {
            SoundEffect effect;

            if (audio.TryGetValue(asset, out effect))
                return new InstanceType(effect.CreateInstance(), soundType, volume, active);
            else
                return null;
        }
    }
}
