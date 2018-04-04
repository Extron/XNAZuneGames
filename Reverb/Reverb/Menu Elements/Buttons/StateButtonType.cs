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
    public class StateButtonType
    {
        public EventHandler Pressed;
        public EventHandler Released;

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

        public Vector2 Position
        {
            get { return position; }

            set
            {
                Vector2 distance = value - position;

                position = value;

                hitBox.Location = new Point(hitBox.Location.X + (int)distance.X, hitBox.Location.Y + (int)distance.Y);
            }
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

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public StateButtonType(string buttonTextureAsset, string pressedTextureAsset, Vector2 buttonLocation, Rectangle buttonHitBox, SpriteEffects buttonEffect)
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
        }

        public StateButtonType(string buttonTextureAsset, string pressedTextureAsset, Vector2 buttonLocation)
        {
            buttonAsset = buttonTextureAsset;
            pressedAsset = pressedTextureAsset;
            position = buttonLocation;

            color = Color.White;
            origin = new Vector2();
            rotation = 0;
            scale = 1;
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
            press(input);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (isPressed)
                spriteBatch.Draw(pressed, position, null, color, rotation, origin, scale, effect, 0.5f);
            else
                spriteBatch.Draw(button, position, null, color, rotation, origin, scale, effect, 0.5f);
        }

        public void revert()
        {
            isPressed = false;
        }

        void press(InputHandler input)
        {
            if (input.pressActivated(hitBox, true))
            {
                isPressed = !isPressed;

                if (isPressed)
                    Pressed(this, new EventArgs());
                else
                    Released(this, new EventArgs());
            }

        }
    }
}
