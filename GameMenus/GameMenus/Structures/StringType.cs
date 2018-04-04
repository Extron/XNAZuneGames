using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public struct StringType
    {
        public SpriteEffects effects;
        public Vector2 vector;
        public Vector2 origin;
        public Color color;
        public string text;
        public float rotation;
        public float scale;
        public float depth;

        public StringType(string t, Vector2 v, Color c)
        {
            text = t;
            vector = v;
            color = c;
            origin = new Vector2(0);
            rotation = 0;
            scale = 1;
            depth = 1;
            effects = SpriteEffects.None;
        }

        public void setText(string t)
        {
            text = t;
        }

        public void setVector(Vector2 v)
        {
            vector = v;
        }

        public void setColor(Color c)
        {
            color = c;
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, text, vector, color, rotation, origin, scale, effects, depth); 
        }
    }
}
