using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZHUD
{
    public class FadeWrapper : WrapperType
    {
        float length;
        float interval;
        float oldTime;
        bool circular;
        int alpha;
        int fadeDirection;

        public FadeWrapper(BaseTextType text, float textLength)
        {
            textType = text;
            length = textLength;
            interval = length / 3;
            alpha = 0;
            circular = true;
            fadeDirection = 1;
            DrawText = false;
        }

        public FadeWrapper(BaseTextType text, float textLength, int direction)
        {
            textType = text;
            length = textLength;
            interval = length;
            circular = false;

            if (direction > 0)
                alpha = 0;
            else
                alpha = 255;

            fadeDirection = direction;
            DrawText = false;
        }

        public override void update(GameTime time)
        {
            float timePassed = (float)time.TotalGameTime.TotalSeconds - oldTime;

            if (timePassed > length)
            {
                textType.DrawText = false;

                if (fadeDirection > 0)
                    alpha = 0;
                else
                    alpha = 255;

                if (circular)
                    fadeDirection = 1;
            }

            if (textType.DrawText)
            {
                if (circular)
                    circularFade(timePassed);
                else
                    directionalFade(timePassed);
            }

            textType.update(time);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            Color color = textType.Color;
            color.A = (byte)alpha;
            textType.Color = color;

            textType.draw(spriteBatch);
        }

        public override void drawScale(SpriteBatch spriteBatch, Vector2 origin, float scale)
        {
            Color color = textType.Color;
            color.A = (byte)alpha;
            textType.Color = color;

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
            oldTime = (float)gameTime.TotalGameTime.TotalSeconds;

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

        private void circularFade(float timePassed)
        {
            if (timePassed < interval)
                fadeDirection = 1;
            else if ((timePassed >= interval) && (timePassed < (interval * 2)))
                fadeDirection = 0;
            else if (timePassed >= (interval * 2))
                fadeDirection = -1;

            alpha = alpha + ((255 / (int)Math.Ceiling(interval * 30)) * fadeDirection);

            if (alpha > 255)
                alpha = 255;

            if (alpha < 0)
                alpha = 0;
        }

        private void directionalFade(float timePassed)
        {
            int alphaInterval = (int)(255 / (interval * 30));
            int currentInterval;

            if (fadeDirection > 0)
               currentInterval = 255 - alpha;
            else
                currentInterval = alpha;

            if (currentInterval < alphaInterval)
            {
                if (fadeDirection > 0)
                    alpha = 255;
                else
                    alpha = 0;
            }
            else
                alpha += (alphaInterval * fadeDirection);
                
        }
    }
}
