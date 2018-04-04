using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Reverb.Components.InGame
{
    interface IInGame
    {
        Color Color { get; set; }

        void setBackground(Texture2D background);
    }
}
