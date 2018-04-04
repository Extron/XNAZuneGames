using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DyLight2D
{
    public class DynamicLightComponent : DrawableGameComponent
    {
        AmbientLightType ambientLight;
        SpriteBatch spriteBatch;
        GamePadState pad;
        GamePadState oldPad;

        public DynamicLightComponent(Game game)
            : base(game)
        {
            ambientLight = new AmbientLightType();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            pad = GamePad.GetState(PlayerIndex.One);

            if (pad.Buttons.X == ButtonState.Pressed && oldPad.Buttons.X == ButtonState.Released)
            {
                if (ambientLight.on)
                    ambientLight.turnOff();  
                else
                    ambientLight.turnOn();
            }

            ambientLight.update();

            oldPad = pad;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            ambientLight.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void getSpriteBatch(SpriteBatch sb)
        {
            spriteBatch = sb;
        }

        public void load(ContentManager content)
        {
            ambientLight.load(content, "Blank Shader");
        }
    }
}
