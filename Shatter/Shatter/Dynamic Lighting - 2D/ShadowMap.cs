using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shatter.Lighting.Sources;
using Microsoft.Xna.Framework.Graphics;

namespace Shatter.Lighting
{
    public class ShadowMap
    {
        AmbientSource ambience;

        Dictionary<string, ILightSource> lightSources;

        public ShadowMap()
        {
            lightSources = new Dictionary<string,ILightSource>();
        }

        public Texture2D shadowTexture(GraphicsDevice device, Texture2D texture)
        {
            Texture2D shadowedTexture = texture;

            if (ambience != null)
                shadowedTexture = ambience.illuminate(device, texture);

            foreach (ILightSource source in lightSources.Values)
            {
                shadowedTexture = source.illuminate(device, shadowedTexture);
            }

            return shadowedTexture;
        }

        public void addAmbience(AmbientSource source)
        {
            ambience = source;
        }

        public void addSource(ILightSource source, string key)
        {
            lightSources.Add(key, source);
        }

        public ILightSource getSource(string key)
        {
            return lightSources[key];
        }
    }
}
