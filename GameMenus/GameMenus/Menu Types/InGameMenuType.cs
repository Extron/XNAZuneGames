using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameMenus
{
    public class InGameMenuType : MenuType
    {
        public BackgroundType menuBackground;

        public Texture2D Background
        {
            get { return menuBackground.texture; }
            set { menuBackground.texture = value; }
        }

        public Color Shade
        {
            get { return menuBackground.color; }
            set { menuBackground.color = value; }
        }

        public InGameMenuType(string state)
            : base(state)  
        {
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (Background != null)
                menuBackground.draw(spriteBatch);

            base.draw(spriteBatch);
        } 

        public void setBackground(Texture2D texture, Color color)
        {
            Background = texture;

            Shade = color;
        }
    }
}
