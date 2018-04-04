using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public interface IInGame
    {
        void setBackground(Texture2D background, Color tint);
    }
}
