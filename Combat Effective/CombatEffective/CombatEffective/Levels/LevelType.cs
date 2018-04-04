using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ZHandler;

namespace CombatEffective.Levels
{
    class LevelType
    {
        protected string textureAsset;

        Texture2D texture;
        Rectangle sourceRectangle;
        Color color;
        PlayerType player;

        public virtual void initialize()
        {
            player = new PlayerType();

            player.initialize();

            color = Color.White;

            sourceRectangle = new Rectangle(0, 0, 240, 320);
        }

        public virtual void load(ContentManager content)
        {
            player.load(content);

            texture = content.Load<Texture2D>(textureAsset);
        }

        public virtual void update(GameTime gameTime, InputHandlerComponent input)
        {
            player.update(gameTime, input, new Vector2(sourceRectangle.X, sourceRectangle.Y));
            
            pan();
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(), sourceRectangle, color);

            player.draw(spriteBatch, new Vector2(sourceRectangle.X, sourceRectangle.Y));
        }

        private void pan()
        {
            Vector2 sourceVector = new Vector2(sourceRectangle.X, sourceRectangle.Y);

            //If the player is near the right edge, attempt to pan the level
            if (player.DrawVector(sourceVector).X > 160)
            {
                //If the source rectangle is not at the edge of the texture, pan
                if ((sourceRectangle.X + sourceRectangle.Width) < texture.Width)
                    sourceRectangle.X++;
            }
            //Else if the player is near the left edge, attempt to pan the level
            else if (player.DrawVector(sourceVector).X < 80)
            {
                //If the source rectangle is not at the edge of the texture, pan
                if (sourceRectangle.X > 0)
                    sourceRectangle.X--;
            }

            //If the player is near the bottom edge, attempt to pan the level
            if (player.DrawVector(sourceVector).Y > 240)
            {
                //If the source rectangle is not at the edge of the texture, pan
                if ((sourceRectangle.Y + sourceRectangle.Height) < texture.Height)
                    sourceRectangle.Y++;
            }
            //Else if the player is near the top edge, attempt to pan the level
            else if (player.DrawVector(sourceVector).Y < 80)
            {
                //If the source rectangle is not at the edge of the texture, pan
                if (sourceRectangle.Y > 0)
                    sourceRectangle.Y--;
            }
        }
    }
}
