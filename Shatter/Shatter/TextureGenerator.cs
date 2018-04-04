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

namespace Shatter
{
    public static class TextureGenerator
    {
        public static GraphicsDevice graphicsDevice;

        public static void setGraphicsDevice(GraphicsDevice graphics)
        {
            graphicsDevice = graphics;
        }

        public static Texture2D createLine(Color lineColor, int width, int height)
        {
            Color[] colorArray = new Color[width * height];

            for (int i = 0; i < colorArray.Length; i++)
                colorArray[i] = lineColor;

            Texture2D line = new Texture2D(graphicsDevice, width, height);

            line.SetData<Color>(colorArray);

            return line;
        }
    }
}
