using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shatter.Lighting.Sources
{
    /// <summary>
    /// Creates a circular light source radiating from one point.  A point source light can be defined using three 
    /// pieces of information: location, intensity, and tint.  Based on the set location, the light will illumiate 
    /// based on that point.  The intensity defines the radius and brightness of the light, i.e. a dimmer light will
    /// illuminate in a smaller circle.  The tint determines the shading color of the light.
    /// </summary>
    public class PointSource: ILightSource
    {
        /// <summary>
        /// A vector that defines the light source's current location within the game world.
        /// </summary>
        Vector2 location;

        /// <summary>
        /// The color of the light source.
        /// </summary>
        Color tint;

        /// <summary>
        /// A value between 0.0 (0% light emitted) and 1.0 (100% light emitted) for the intensity of the light.  
        /// It is assumed that the pre-rendered texture data is colored as if the ambient light was at a 100% intensity.
        /// </summary>
        float intensity;

        /// <summary>
        /// A value representing the total magnatude of the light source.  A higher magnatude means a longer falloff distance.
        /// The actual effect on the source is a square root, i.e. a mangatude of 9 generates a light with a 3 pixel radius.
        /// </summary>
        float magnitude;

        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        public PointSource(Vector2 lightLocation, Color lightTint, float lightIntensity, float lightMagnitude)
        {
            location = lightLocation;
            tint = lightTint;
            intensity = lightIntensity;
            magnitude = lightMagnitude;
        }

        public PointSource(Vector2 lightLocation, Color lightTint, float lightIntensity)
        {
            location = lightLocation;
            tint = lightTint;
            intensity = lightIntensity;
        }

        public Texture2D illuminate(GraphicsDevice device, Texture2D texture)
        {
            Texture2D shadowedTexture = texture;

            Color[] pixels = new Color[shadowedTexture.Width * shadowedTexture.Height];

            shadowedTexture.GetData<Color>(pixels);

            if (magnitude == 0)
                magnitude = (texture.Width * texture.Height) / 2;

            //Distance away from the light source, in pixels
            int distance = 1;

            //The intensity at the given distance
            float currentIntensity = intensity - (distance / magnitude);

            int indexOfLocation = (int)(location.Y * texture.Width + location.X);

            int indexOfBox;

            int pixelIndex;

            //For every integer distance from the source, apply the color changes until the currentIntensity is zero
            do
            {

                //Calculate the actual tint of the light
                Color actualTint = new Color((byte)(tint.R * currentIntensity), (byte)(tint.G * currentIntensity), (byte)(tint.B * currentIntensity));

                //Find the array index of the box of pixels being checked.
                indexOfBox = indexOfLocation - (int)(distance * texture.Width + distance);

                //Fore every pixel within the box, check its distance from the source
                for (int i = 0; i < (2 * distance + 1); i++)
                {
                    for (int j = 0; j < (2 * distance + 1); j++)
                    {
                        //Get the pixel index
                        pixelIndex = indexOfBox + (i * texture.Width + j);

                        //If the pixel to be edited is "off the screen", (This pixel does not actually exist), go to the next iteration
                        if (pixelIndex < 0 || pixelIndex >= pixels.Length)
                            continue;

                        //Get the pixel vector and the relative vector of the light source
                        Vector2 pixelVector = new Vector2(j, i);
                        Vector2 relativeLocation = new Vector2(distance);

                        float pixelDistance;

                        //Find the distance from the two
                        Vector2.Distance(ref pixelVector, ref relativeLocation, out pixelDistance);

                        //If the number can be rounded down to the distance, apply the color change
                        if (Math.Round(pixelDistance, MidpointRounding.AwayFromZero) == distance)
                        {
                            //Calculate the subtractive color of the pixel
                            byte red, green, blue;

                            if (actualTint.R + pixels[pixelIndex].R > 255)
                                red = 255;
                            else
                                red = (byte)(actualTint.R + pixels[pixelIndex].R);

                            if (actualTint.G + pixels[pixelIndex].G > 255)
                                green = 255;
                            else
                                green = (byte)(actualTint.G + pixels[pixelIndex].G);

                            if (actualTint.B + pixels[pixelIndex].B > 255)
                                blue = 255;
                            else
                                blue = (byte)(actualTint.B + pixels[pixelIndex].B);

                            pixels[pixelIndex] = new Color(red, green, blue);
                        }
                    }
                }

                distance++;

                currentIntensity -= ((float)distance / magnitude);
            }
            while (currentIntensity > 0);

            shadowedTexture.SetData<Color>(pixels);

            return shadowedTexture;
        }
    }
}
