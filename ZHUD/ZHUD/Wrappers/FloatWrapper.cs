using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZHUD
{
    public class FloatWrapper : WrapperType
    {
        Vector2 position;
        Vector2 direction;
        float length;
        float oldTime;
        int lagTime;

        public FloatWrapper(BaseTextType text, Vector2 d, float l)
        {
            textType = text;
            position = textType.Vector;
            textType.DrawText = false;
            direction = d;
            length = l;
            lagTime = 5;
        }

        public override void update(GameTime time)
        {
            float timePassed = (float)time.TotalGameTime.TotalSeconds - oldTime;

            if (timePassed > length)
            {
                textType.DrawText = false;
                textType.Vector = position;
            }

            if (textType.DrawText)
            {
                textType.Vector += direction * (1 / (timePassed + lagTime));
            }

            textType.update(time);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            textType.draw(spriteBatch);
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
    }
}
