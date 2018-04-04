using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameMenus
{
    public struct BackgroundType
    {
        public Texture2D texture;
        public Vector2 vector;
        public Color color;
        public string textureAsset;

        public BackgroundType(string asset, Vector2 textureVector, Color textureColor)
        {
            textureAsset = asset;
            vector = textureVector;
            color = textureColor;
            texture = null;
        }

        public void setTexture(Texture2D t)
        {
            texture = t;
        }

        public void setTexture(string fileName)
        {
            textureAsset = fileName;
        }

        public void setVector(Vector2 v)
        {
            vector = v;
        }

        public void setColor(Color c)
        {
            color = c;
        }

        public void load()
        {
            texture = AssetManager.getTexture(textureAsset);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, vector, color);
            else
                spriteBatch.GraphicsDevice.Clear(color);
        }
    }
}
