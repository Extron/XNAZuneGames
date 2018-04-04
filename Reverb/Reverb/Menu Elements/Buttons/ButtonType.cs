using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;

namespace Reverb.Elements.Buttons
{
    public class ButtonType
    {
        public EventHandler Pressed;

        SpriteEffects effect;
        Texture2D button;
        Texture2D pressed;
        Rectangle hitBox;
        Vector2 position;
        Vector2 origin;
        Color color;
        string buttonAsset;
        string pressedAsset;
        float rotation;
        float scale;
        bool isPressed;
        bool updateCounter;
        int interval;
        int counter;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, button.Width, button.Height); }
        }

        public ButtonType(string buttonTextureAsset, string pressedTextureAsset, Vector2 buttonLocation, Rectangle buttonHitBox, SpriteEffects buttonEffect)
        {
            buttonAsset = buttonTextureAsset;
            pressedAsset = pressedTextureAsset;
            position = buttonLocation;
            hitBox = buttonHitBox;
            effect = buttonEffect;

            color = Color.White;
            origin = new Vector2();
            rotation = 0;
            scale = 1;
            interval = 5;
            isPressed = false;
            updateCounter = false;
        }

        public ButtonType(string buttonTextureAsset, string pressedTextureAsset, Vector2 buttonLocation)
        {
            buttonAsset = buttonTextureAsset;
            pressedAsset = pressedTextureAsset;
            position = buttonLocation;

            color = Color.White;
            origin = new Vector2();
            rotation = 0;
            scale = 1;
            interval = 5;
            isPressed = false;
            updateCounter = false;
            effect = SpriteEffects.None;
        }

        public void initialize()
        {
        }

        public void load(ContentManager content)
        {
            button = content.Load<Texture2D>(buttonAsset);
            pressed = content.Load<Texture2D>(pressedAsset);

            if (hitBox.IsEmpty)
                hitBox = new Rectangle((int)position.X, (int)position.Y, button.Width, button.Height);
        }

        public void update(InputHandler input)
        {
            if (isPressed && updateCounter)
            {
                counter++;

                if (counter >= interval)
                {
                    isPressed = false;
                    updateCounter = false;
                    counter = 0;
                }
            }

            press(input);

        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (isPressed)
                spriteBatch.Draw(pressed, position, null, color, rotation, origin, scale, effect, 0.5f);
            else
                spriteBatch.Draw(button, position, null, color, rotation, origin, scale, effect, 0.5f);
        }

        void press(InputHandler input)
        {
            if (input.pressActivated(hitBox, false))
                isPressed = true;

            if (input.pressActivated(hitBox, true))
            {
                Pressed(this, new EventArgs());

                isPressed = true;
                updateCounter = true;
            }
        }
    }
}
