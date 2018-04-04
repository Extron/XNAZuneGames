using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace DyLight2D
{
    public class AmbientLightType
    {
        Texture2D shader;
        Vector2 vector; 
        Color transparency;
        int intensity;
        public bool on;
        bool drawLight;

        public AmbientLightType()
        {
            vector = new Vector2(0);
            transparency = new Color(0, 0, 0, 0);
            intensity = 0;
            on = true;
            drawLight = true;
        }

        public void load(ContentManager content, string fileName)
        {
            shader = content.Load<Texture2D>(fileName);
        }

        public void update()
        {
            transparency.A = (byte)intensity;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (drawLight)
                spriteBatch.Draw(shader, vector, transparency);
        }

        public void turnOff()
        {
            intensity = 255;
            on = false;
        }

        public void turnOn()
        {
            intensity = 0;
            on = true;
        }
    }
}
