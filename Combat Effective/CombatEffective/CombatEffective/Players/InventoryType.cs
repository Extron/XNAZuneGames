using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatEffective.Weapons;
using Microsoft.Xna.Framework.Content;
using ZHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatEffective.Players
{
    class InventoryType
    {
        WeaponType primaryWeapon;
        WeaponType secondaryWeapon;

        WeaponType currentWeapon;
        WeaponSlot slot;

        public InventoryType()
        {
            primaryWeapon = new AutoRifle();
            secondaryWeapon = new Handgun();
        }

        public void initialize()
        {
            primaryWeapon.initialize();
            secondaryWeapon.initialize();

            currentWeapon = primaryWeapon;
            slot = WeaponSlot.primary;
        }

        public void load(ContentManager content)
        {
            primaryWeapon.load(content);
            secondaryWeapon.load(content);
        }

        public void update(GameTime gameTime, InputHandlerComponent input, Vector2 moveVector)
        {
            currentWeapon.update(gameTime, input, moveVector);

            if (input.getButton(ButtonType.back, true))
                swap(moveVector);
        }

        public void draw(SpriteBatch spriteBatch, Vector2 sourceVector, float textureRotation)
        {
            currentWeapon.draw(spriteBatch, sourceVector, textureRotation);
        }

        public void swap(Vector2 moveVector)
        {
            if (slot == WeaponSlot.primary)
            {
                currentWeapon = secondaryWeapon;
                slot = WeaponSlot.secondary;
            }
            else
            {
                currentWeapon = primaryWeapon;
                slot = WeaponSlot.primary;
            }

            currentWeapon.Vector = moveVector;
        }

        public void exchange(WeaponType newWeapon)
        {
            if (slot == WeaponSlot.primary)
            {
                primaryWeapon = newWeapon;
                currentWeapon = primaryWeapon;
            }
            else
            {
                secondaryWeapon = newWeapon;
                currentWeapon = secondaryWeapon;
            }
        }
    }

    enum WeaponSlot
    {
        primary,
        secondary
    }
}
