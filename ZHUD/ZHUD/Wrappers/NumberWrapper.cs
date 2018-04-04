using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ZHUD
{
    public class NumberWrapper : WrapperType
    {
        int number;

        public NumberWrapper(BaseTextType text, int n)
        {
            textType = text;

            number = n;
        }

        public override void update(GameTime time)
        {
            textType.update(time);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            string identifier;

            identifier = textType.Text;
            textType.Text = number.ToString();
            textType.draw(spriteBatch);
            textType.Text = identifier;
        }

        public override void drawScale(SpriteBatch spriteBatch, Vector2 origin, float scale)
        {
            string identifier;

            identifier = textType.Text;
            textType.Text = number.ToString();
            textType.drawScale(spriteBatch, origin, scale);
            textType.Text = identifier;
        }

        public override void addToText(string str)
        {
            textType.addToText(str);
        }

        public override void changeText(string str)
        {
            textType.changeText(str);
        }

        public override void changeNumber(int n)
        {
            number = n;
        }

        public override void addToNumber(int n)
        {
            number += n;
        }

        public override void startText(GameTime gameTime)
        {
            textType.startText(gameTime);
        }

        public override Vector2 centerText(string text)
        {
            text = number.ToString();

            return textType.centerText(text);
        }
    }
}
