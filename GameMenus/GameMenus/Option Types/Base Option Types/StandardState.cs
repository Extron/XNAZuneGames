using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class StandardState
    {
        StringType text;
        SpriteFont font;
        string fontAsset;

        #region Properties
        public string Text
        {
            get { return text.text; }
            set { text.text = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public Vector2 Vector
        {
            get { return text.vector; }
            set { text.vector = value; }
        }

        public Vector2 TextSize
        {
            get { return font.MeasureString(text.text); }
        }

        public Color TextColor
        {
            get { return text.color; }
            set { text.color = value; }
        }
        #endregion

        #region Constructors
        public StandardState(StringType optionText)
        {
            text = optionText;
        }

        public StandardState(StringType optionText, string fontName) 
        {
            text = optionText;
            fontAsset = fontName;
        }
        #endregion

        #region Virtual Functions
        public virtual void load()
        {
            font = AssetManager.getFont(fontAsset);
        }

        public virtual void update()
        {
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            text.draw(spriteBatch, font);
        }
        #endregion

        #region Class Functions
        public void right()
        {
            text.origin = new Vector2();
        }
        public void center()
        {
            Vector2 temp = font.MeasureString(text.text);
            text.origin.X = temp.X / 2;
        }

        public void left()
        {
            Vector2 temp = font.MeasureString(text.text);
            text.origin.X = temp.X;
        }
        #endregion
    }
}
