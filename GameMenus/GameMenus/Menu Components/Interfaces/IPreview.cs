using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public interface IPreview
    {
        void changeColor(Color color);

        void changeTexture(Texture2D texture);

        void changeTexture(string asset);

        void changeVector(Vector2 vector);

        void changeVector(int x, int y);
    }
}
