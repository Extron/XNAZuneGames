using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class ArrowType
    {
        public EventHandler<EventArgs> Selected;

        public Vector2 vector;
        public Texture2D texture;
        public Texture2D pressed;
        public Color color;
        public ArrowDirection direction;
        public string asset;
        public string pressedAsset;
        public bool selected;
        public int counter;
        public int interval;
        float rotation;

        public ArrowType(string textureAsset, string pressedTextureAsset, Vector2 textureVector, ArrowDirection arrowDirection, float arrowRotation)
        {
            vector = textureVector;
            asset = textureAsset;
            pressedAsset = pressedTextureAsset;
            direction = arrowDirection;
            rotation = arrowRotation;
            color = Color.White;
            interval = 5;
            counter = 0;
            selected = false;
            texture = null;
            pressed = null;
            Selected = null;
        }

        public void initialize()
        {
        }

        public void load()
        {
            texture = AssetManager.getTexture(asset);
            pressed = AssetManager.getTexture(pressedAsset);
        }

        public void update()
        {
            if (selected && counter == interval)
            {
                selected = false;
                counter = 0;
            }

            if (selected)
                counter++;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (selected)
                spriteBatch.Draw(pressed, vector, null, color, rotation, new Vector2(0), 1.0f, SpriteEffects.None, 1.0f);
            else
                spriteBatch.Draw(texture, vector, null, color, rotation, new Vector2(0), 1.0f, SpriteEffects.None, 1.0f);
        }

        public void press()
        {
            selected = true;

            if (Selected != null)
                Selected(this, new EventArgs());
        }
    }
}