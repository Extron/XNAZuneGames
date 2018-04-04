using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Shatter.Lighting.Sources
{
    /// <summary>
    /// Creates an ambient lighting, which modifies the entire texutre with the ambience.  Ambient sources need only two values,
    /// inteisity, measured between 0.0 and 1.0, and tint, represented by a RGB color object.
    /// </summary>
    public class AmbientSource : ILightSource
    {
        Color tint;
        float intensity;

        public Vector2 Location
        {
            get { return new Vector2(); }
            set {}
        }

        public AmbientSource(Color ambientTint, float ambientIntensity)
        {
            tint = ambientTint;
            intensity = ambientIntensity;
        }

        public Texture2D illuminate(GraphicsDevice device, Texture2D texture)
        {
            //Because we want to retain the original texture, create a new copy of the texture, avoiding shallow assignment.
            Texture2D shadowedTexture = new Texture2D(device, texture.Width, texture.Height);

            Color[] pixels = new Color[shadowedTexture.Width * shadowedTexture.Height];

            texture.GetData<Color>(pixels);

            //Calculate the actual tint, based on the intensity
            Color actualTint = new Color((byte)(tint.R * intensity), (byte)(tint.G * intensity), (byte)(tint.B * intensity));

            //For each pixel within the texture, apply a subtractive lighting effect
            for (int i = 0; i < pixels.Length; i++)
            {
                //Determine the pixel's "absorbtion" value
                Color absorbtion = new Color(255 - pixels[i].R, 255 - pixels[i].G, 255 - pixels[i].B);

                //Calculate the subtractive color of the pixel
                byte red, green, blue;

                if (actualTint.R - absorbtion.R < 0)
                    red = 0;
                else
                    red = (byte)(actualTint.R - absorbtion.R);

                if (actualTint.G - absorbtion.G < 0)
                    green = 0;
                else 
                    green = (byte)(actualTint.G - absorbtion.G);

                if (actualTint.B - absorbtion.B < 0)
                    blue = 0;
                else 
                    blue = (byte)(actualTint.B - absorbtion.B);

                pixels[i] = new Color(red, green, blue);
            }

            //Send the new pixels to the texture
            shadowedTexture.SetData<Color>(pixels);

            return shadowedTexture;
        }
    }
}
