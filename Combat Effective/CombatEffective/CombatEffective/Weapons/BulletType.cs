using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Shatter;

namespace CombatEffective.Weapons
{
    class BulletType
    {
        Texture2D texture;
        Vector2 vector;
        Vector2 direction;
        Vector2 origin;
        Color color;
        bool drawBullet;
        bool activated;
        float interval;
        float activationTime;
        float startTime;

        public bool Active
        {
            get { return activated; }
        }

        public BulletType(Vector2 bulletOrigin)
        {
            color = Color.White;

            origin = bulletOrigin;

            drawBullet = false;
        }

        public BulletType(Vector2 origin, float rateOfFire)
            : this(origin)
        {
            interval = rateOfFire;
        }

        public BulletType(Vector2 origin, float rateOfFire, float time)
            : this(origin, rateOfFire)
        {
            activationTime = time;
        }

        public void update(GameTime gameTime, Vector2 moveVector)
        {
            if (activated)
            {
                float elapstedTime = 0;

                if (startTime != 0)
                    elapstedTime = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

                vector = moveVector;

                if (interval == 0)
                    drawBullet = true;
                else if (elapstedTime > interval)
                {
                    drawBullet = !drawBullet;
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                }

                if (elapstedTime > activationTime && activationTime != 0)
                {
                    activated = false;
                    drawBullet = false;
                    startTime = 0;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch, Vector2 drawVector, float rotation)
        {
            if (drawBullet) 
                spriteBatch.Draw(texture, drawVector, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }

        public void activate(int length, GameTime gameTime)
        {
            activated = true;
            startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            
            texture = TextureGenerator.createLine(color, length, 1);
        }

        public void deactivate()
        {
            activated = false;
            drawBullet = false;
            startTime = 0;
        }
    }
}
