using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ZHandler;
using CombatEffective.Players;

namespace CombatEffective
{
    class PlayerType
    {
        Texture2D texture;
        Vector2 vector;
        InventoryType inventory;
        float rotation;

        public Vector2 Vector
        {
            get { return vector; }
        }

        public Vector2 DrawVector(Vector2 sourceVector)
        {
            return vector - sourceVector;
        }

        public void initialize()
        {
            inventory = new InventoryType();

            inventory.initialize();

            vector = new Vector2(120, 160);
        }

        public void load(ContentManager content)
        {
            inventory.load(content);

            texture = content.Load<Texture2D>(@"Avatars\Player 1");
        }

        public void update(GameTime gameTime, InputHandlerComponent input, Vector2 sourceVector)
        {
            move(input);
            rotate(input, sourceVector);

            inventory.update(gameTime, input, vector);
        }

        public void draw(SpriteBatch spriteBatch, Vector2 sourceVector)
        {
            //Calculate the actual vector to draw to based on the source vector from the level and the vector of the player.
            Vector2 drawVector = vector - sourceVector;

            inventory.draw(spriteBatch, sourceVector, rotation);

            spriteBatch.Draw(texture, drawVector, null, Color.White, rotation, new Vector2(10), 1.0f, SpriteEffects.None, 0f); 
        }

        private void move(InputHandlerComponent input)
        {
            int x = 0;
            int y = 0;

            //Get the y variable of the displacement
            if (input.getButton(ButtonType.up, false))
                y = -1;
            else if (input.getButton(ButtonType.down, false))
                y = 1;

            //Get the x variable of the displacement
            if (input.getButton(ButtonType.left, false))
                x = -1;
            else if (input.getButton(ButtonType.right, false))
                x = 1;

            //Create the displacement variable using the x and y values
            Vector2 displacement = new Vector2(x, y);

            //Modify the character vector by adding it to the displacement
            vector += displacement;
        }

        private void rotate(InputHandlerComponent input, Vector2 sourceVector)
        {
            //Get the vector of the touch pad
            Vector2 inputVector = input.getVector(vector - sourceVector);

            //If the y coordinate is positive, invert the rotation
            if (inputVector.Y > 0)
                rotation = -(float)(Math.Acos(inputVector.X));
            else
                rotation = (float)Math.Acos(inputVector.X);
        }
    }
}
