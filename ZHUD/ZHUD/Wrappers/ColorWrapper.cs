using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ZHUD
{
    public class ColorWrapper : WrapperType
    {
        Color startColor;
        Color endColor;
        float speed;
        int lagTime;
        bool reverse;

        public ColorWrapper(BaseTextType text, string fontAsset, Color sColor, Color eColor, float s, int l)
        {
            textType = text;
            startColor = sColor;
            endColor = eColor;
            speed = s;
            lagTime = l;
            reverse = false;
            textType.Color = startColor;
            textType.Asset = fontAsset;
        }

        public override void load(ContentManager content)
        {
            textType.load(content);
        }
        public override void update(GameTime time)
        {
            if (textType.DrawText)
            {
                if (textType.Color == startColor)
                    reverse = false;

                if (textType.Color == endColor)
                    reverse = true;

                if (!reverse)
                    updateColor(startColor, endColor);
                else
                    updateColor(endColor, startColor);
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

        void updateColor(Color startColor, Color endColor)
        {
            Color temp = textType.Color;

            if (startColor.R == endColor.R)
            {
                if (startColor.G == endColor.G)
                {
                    if (startColor.B > endColor.B)
                        temp.B -= (byte)speed;
                    else
                        temp.B += (byte)speed;
                }
                else if (startColor.B == endColor.B)
                {
                    if (startColor.G > endColor.G)
                        temp.G -= (byte)speed;
                    else
                        temp.G += (byte)speed;
                }
                else
                {
                    if (startColor.B > endColor.B)
                        temp.B -= (byte)speed;
                    else
                        temp.B += (byte)speed;

                    if (startColor.G > endColor.G)
                        temp.G -= (byte)speed;
                    else
                        temp.G += (byte)speed;
                }
            }
            else if (startColor.G == endColor.G)
            {
                if (startColor.B == startColor.B)
                {
                    if (startColor.R > endColor.R)
                        temp.R -= (byte)speed;
                    else
                        temp.R += (byte)speed;
                }
                else
                {
                    if (startColor.B > endColor.B)
                        temp.B -= (byte)speed;
                    else
                        temp.B += (byte)speed;
                }
            }
            else if (startColor.B == endColor.B)
            {
                if (startColor.R > endColor.R)
                    temp.R -= (byte)speed;
                else
                    temp.R += (byte)speed;

                if (startColor.G > endColor.G)
                    temp.G -= (byte)speed;
                else
                    temp.G += (byte)speed;
            }
            else
            {
                if (startColor.R > endColor.R)
                    temp.R -= (byte)speed;
                else
                    temp.R += (byte)speed;

                if (startColor.G > endColor.G)
                    temp.G -= (byte)speed;
                else
                    temp.G += (byte)speed;

                if (startColor.R > endColor.R)
                    temp.R -= (byte)speed;
                else
                    temp.R += (byte)speed;

            }

            textType.Color = temp;
        }

        public override Vector2 centerText(string text)
        {
            return textType.centerText(text);
        }
    }
}
