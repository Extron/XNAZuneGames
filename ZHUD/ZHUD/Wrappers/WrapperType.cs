using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ZHUD
{
    public abstract class WrapperType : BaseTextType
    {
        protected BaseTextType textType;

        #region Overridden Properties
        public override string Text
        {
            get
            {
                return textType.Text;
            }
            set
            {
                textType.Text = value;
            }
        }

        public override SpriteFont Font
        {
            get
            {
                return textType.Font;
            }
            set
            {
                textType.Font = value;
            }
        }

        public override Vector2 Vector
        {
            get
            {
                return textType.Vector;
            }
            set
            {
                textType.Vector = value;
            }
        }

        public override Color Color
        {
            get
            {
                return textType.Color;
            }
            set
            {
                textType.Color = value;
            }
        }

        public override bool DrawText
        {
            get
            {
                return textType.DrawText;
            }
            set
            {
                textType.DrawText = value;
            }
        }
        #endregion

        public override void load(ContentManager content)
        {
            textType.load(content);
        }

        public override void update(GameTime time)
        {
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

        public override void changeText(string str)
        {
            textType.changeText(str);
        }

        public override void addToText(string str)
        {
            textType.addToText(str);
        }

        public override void addToNumber(int n)
        {
            textType.addToNumber(n);
        }

        public override void changeNumber(int n)
        {
            textType.changeNumber(n);
        }
    }
}
