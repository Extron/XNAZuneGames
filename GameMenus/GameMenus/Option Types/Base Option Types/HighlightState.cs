using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class HighlightState
    {
        protected HighlightType highlight;
        protected StandardState option;
        protected string highlightAsset;

        #region Properties
        public Texture2D Texture
        {
            get { return highlight.text; }
            set { highlight.text = value; }
        }

        public Vector2 Highlight
        {
            get { return highlight.Vector; }
            set { highlight.Vector = value; }
        }

        public SpriteFont Font
        {
            get { return option.Font; }
            set { option.Font = value; }
        }

        public HighlightType Highlighter
        {
            get { return highlight; }
            set { highlight = value; }
        }

        public StandardState Standard
        {
            get { return option; }
            set { option = value; }
        }
        #endregion

        #region Constructors
        public HighlightState(StandardState state, Rectangle rectangle, string highlightName)
        {
            highlight = new HighlightType(rectangle);
            highlightAsset = highlightName;
            option = state;
        }
        public HighlightState(StandardState state, string highlightName)
        {
            highlight = new HighlightType(new Rectangle());
            highlightAsset =  highlightName;
            option = state;
        }

        public HighlightState(StandardState state)
        {
            highlight = null;
            option = state;
        }
        #endregion

        #region Virtual Functions
        public virtual void load()
        {
            highlight.setTexture(AssetManager.getTexture(highlightAsset));
        }

        public virtual void update()
        {
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (highlight != null)
                highlight.draw(spriteBatch);

            option.draw(spriteBatch);
        }
        #endregion

        #region Class Functions
        public void right()
        {
            highlight.right();
            option.right();
        }

        public void center()
        {
            highlight.center();
            option.center();
        }

        public void left()
        {
            highlight.left();
            option.left();
        }

        public void wrapText()
        {
            highlight.rect.Width = (int)(option.TextSize.X * 1.25);
            highlight.rect.Height = (int)(option.TextSize.Y);
        }
        #endregion
    }
}
