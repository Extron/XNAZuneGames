#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of QuatrixHD.
 * 
 * QuatrixHD is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * QuatrixHD is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with QuatrixHD.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reflex;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace QuatrixHD.Quatrix
{
    /// <summary>
    /// An interface that allows classes to implement functionality to access the different game controls, such
    /// as moving, rotating, falling, and dropping.
    /// </summary>
    interface IControls
    {
        void initializeInput(InputHandler input);

        void load(ContentManager content);

        bool moveLeft(InputHandler input, Rectangle hitBox);

        bool moveRight(InputHandler input, Rectangle hitBox);

        bool drop(InputHandler input);

        bool fall(InputHandler input);

        bool rotate(InputHandler input, Rectangle hitBox);

        void draw(SpriteBatch spriteBatch);
    }
}
