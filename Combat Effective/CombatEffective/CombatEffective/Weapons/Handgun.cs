using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZHandler;
using Shatter;

namespace CombatEffective.Weapons
{
    class Handgun : WeaponType
    {
        public const int MAX_AMMO = 10;

        public Handgun()
        {
            textureAsset = @"Weapons\Handgun 1";
        }

        public override void initialize()
        {
            ammoCount = MAX_AMMO;
            origin = new Vector2(0, 10);
            color = Color.White;
            bullet = new BulletType(new Vector2(-20, 0), 0, 0.05f);
        }

        public override void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureAsset);
        }

        public override void update(GameTime gameTime, InputHandlerComponent input, Vector2 moveVector)
        {
            vector = moveVector;

            if (input.getButton(ButtonType.b, true))
                reload();

            if (input.getButton(ButtonType.a, true))
                shoot(gameTime);

            bullet.update(gameTime, vector);
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
