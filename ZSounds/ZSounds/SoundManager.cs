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

namespace ZSounds
{
    public static class SoundManager
    {
        static Dictionary<string, SoundEffect> sounds = new Dictionary<string,SoundEffect>();
        static List<string> soundAssets = new List<string>();
        static bool isEnabled = true;

        public static bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }

        public static float Volume
        {
            get { return SoundEffect.MasterVolume; }
            set 
            { 
                if (value >= 0 && value <= 1)
                    SoundEffect.MasterVolume = value; 
            }
        }

        public static void addSound(string fileName)
        {
            soundAssets.Add(fileName);
        }

        public static void load(ContentManager content, string file)
        {
            foreach (string soundAsset in soundAssets)
            {
                SoundEffect sound = content.Load<SoundEffect>(file + "/" + soundAsset);
                sounds.Add(soundAsset, sound);
            }
        }

        public static void playSound(string fileName)
        {
            if (isEnabled)
            {
                SoundEffect sound;

                if (sounds.TryGetValue(fileName, out sound))
                    sound.Play();
            }
        }
    }
}
