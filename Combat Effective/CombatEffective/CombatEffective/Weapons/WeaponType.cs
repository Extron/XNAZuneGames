using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZHandler;
using Microsoft.Xna.Framework.Content;

namespace CombatEffective.Weapons
{
    abstract class WeaponType
    {
        #region Variables
        /// <summary>
        /// The gun's bullet object.
        /// </summary>
        protected BulletType bullet;

        /// <summary>
        /// The graphic used to draw the weapon.
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// The location of the texture within the level
        /// </summary>
        protected Vector2 vector;

        /// <summary>
        /// The origin of the texture.
        /// </summary>
        protected Vector2 origin;

        /// <summary>
        /// The tint of the texture.
        /// </summary>
        protected Color color;

        /// <summary>
        /// The asset used to load the texture.
        /// </summary>
        protected string textureAsset;

        /// <summary>
        /// The rotation of the texture.
        /// </summary>
        protected float rotation;

        /// <summary>
        /// The number of rounds in the clip.
        /// </summary>
        protected int ammoCount;

        /// <summary>
        /// The number of rounds the player is carrying.
        /// </summary>
        protected int inventoryCount;
        #endregion

        public Vector2 Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        public abstract void initialize();

        public abstract void load(ContentManager content);

        public abstract void update(GameTime gameTime, InputHandlerComponent input, Vector2 moveVector);

        public abstract void draw(SpriteBatch spriteBatch, Vector2 sourceVector, float textureRotation);

        public abstract void shoot(GameTime gameTime);

        public abstract void reload();
    }
}
