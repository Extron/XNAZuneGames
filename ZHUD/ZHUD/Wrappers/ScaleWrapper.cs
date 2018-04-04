using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZHUD
{
    public class ScaleWrapper : WrapperType
    {
        Vector2 origin;
        float scale;
        float factor;
        float length;
        float period;
        float speed;
        float oldTime;
        float stopTime;

        public ScaleWrapper(BaseTextType text, Vector2 textOrigin, float scaleFactor, float effectLength, float time, int pulseSpeed)
        {
            textType = text;
            origin = textOrigin;
            textType.DrawText = false;
            scale = 1;
            factor = scaleFactor;
            length = effectLength;
            period = 0;
            stopTime = time;
            speed = (float)(pulseSpeed) / 100.0f;
        }

        public ScaleWrapper(BaseTextType text, float scaleFactor, float effectLength, float time)
        {
            textType = text;
            textType.DrawText = false;
            scale = 1;
            factor = scaleFactor;
            length = effectLength;
            period = 0;
            stopTime = time;
            speed = 0;
        }

        public ScaleWrapper(BaseTextType text, Vector2 textOrigin, float effectLength, float time, int pulseSpeed)
        {
            textType = text;
            origin = textOrigin;
            textType.DrawText = false;
            scale = 1;
            factor = 1;
            length = effectLength;
            period = 0;
            stopTime = time;
            speed = (float)(pulseSpeed) / 100.0f;
        }

        public override void update(GameTime time)
        {
            float timePassed = (float)time.TotalGameTime.TotalSeconds - oldTime;

            if (timePassed > length)
            {
                textType.DrawText = false;
                
                scale = 1;

                period = 0;
            }

            if (textType.DrawText && timePassed < stopTime)
            {
                if (factor != 1)
                    scale = scale * factor;
                else
                {
                    scale = Math.Abs((float)Math.Sin(period)) + 1;

                    period = period + (float)(Math.PI * speed);
                }
            }

            textType.update(time);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            textType.drawScale(spriteBatch, origin, scale);
        }

        public override void drawScale(SpriteBatch spriteBatch, Vector2 origin, float scale)
        {
            textType.drawScale(spriteBatch, origin, scale);
        }

        public override void addToText(string str)
        {
            textType.addToText(str);
        }

        public override void changeText(string str)
        {
            textType.changeText(str);
        }

        public override void startText(GameTime gameTime)
        {
            if (origin == null || (origin.X == 0 && origin.Y == 0))
                origin = centerText(textType.Text);

            if (!textType.DrawText)
            {
                textType.DrawText = true;
                oldTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            textType.startText(gameTime);
        }

        public override void addToNumber(int n)
        {
            textType.addToNumber(n);
        }

        public override void changeNumber(int n)
        {
            textType.changeNumber(n);
        }

        public override Vector2 centerText(string text)
        {
            return textType.centerText(text);
        }
    }
}
