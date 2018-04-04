using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZHandler;
using Microsoft.Xna.Framework.Content;

namespace CombatEffective.Weapons
{
    class AutoRifle : WeaponType
    {
        public const int MAX_AMMO = 30;

        public AutoRifle()
        {
            textureAsset = @"Weapons\Automatic Rifle 1";
        }

        public override void initialize()
        {
            ammoCount = MAX_AMMO;
            origin = new Vector2(0, 10);
            color = Color.White;
            bullet = new BulletType(new Vector2(-15, -6), 0.05f);
        }

        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureAsset);
        }

        public override void update(GameTime gameTime, InputHandlerComponent input, Vector2 moveVector)
        {
            vector = moveVector;

            bullet.update(gameTime, moveVector);

            if (input.getButton(ButtonType.b, true))
                reload();

            if (input.getButton(ButtonType.a, false))
                shoot(gameTime);
            else if (input.getReleased(ButtonType.a, true))
                bullet.deactivate();
        }

        public override void draw(SpriteBatch spriteBatch, Vector2 sourceVector, float textureRotation)
        {
            Vector2 drawVector = vector - sourceVector;

            rotation = textureRotation;

            bullet.draw(spriteBatch, drawVector, rotation);

            spriteBatch.Draw(texture, drawVector, null, color, rotation, origin, 1f, SpriteEffects.None, 0f);
        }

        public override void shoot(GameTime gameTime)
        {
            //If the gun has ammo, fire, else reload
            if (ammoCount > 0)
            {
                //Note: Calculate the bullet path and draw the bullet here (not implemented yet)
                if (!bullet.Active)
                    bullet.activate(320, gameTime);

                //Reduce the ammo in the clip
                ammoCount--;
            }
            else
                reload();
        }

        public override void reload()
        {
            //Note: Play the reload animation here (not implemented yet)

            //Reduce the number of rounds in the inventory by the number of rounds needed to fill the clip
            inventoryCount -= MAX_AMMO - ammoCount;

            //Refill the clip of the weapon
            ammoCount = MAX_AMMO;
        }
    }
}
