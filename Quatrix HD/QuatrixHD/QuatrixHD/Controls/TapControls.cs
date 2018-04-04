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
using Reflex.Motions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace QuatrixHD.Quatrix
{
    /// <summary>
    /// A controls setting that creates motions to activate with the touch screen.
    /// </summary>
    class TapControls : IControls
    {
        public void initializeInput(InputHandler input)
        {
            input.addMotion("Rotate", new Press(new Rectangle(136, 14, 40, 40), true));
            input.addMotion("Move Left", new PressHold(new Rectangle(0, 0, 136, 396)));
            input.addMotion("Move Right", new PressHold(new Rectangle(136, 0, 136, 396)));
            input.addMotion("Accel Drop", new PressHold(new Rectangle(0, 396, 272, 84)));
            input.addMotion("Drop", new Swipe(new Vector2(0, 100), 0.5f));
        }

        public void load(ContentManager content)
        {
        }

        public bool moveLeft(InputHandler input, Rectangle hitBox)
        {
            input.getMotion("Move Left").HitBoxSize = new Vector2(hitBox.X * 20 + 36, 396);

            return input.motionActivated("Move Left");
        }

        public bool moveRight(InputHandler input, Rectangle hitBox)
        {
            input.getMotion("Move Right").Location = new Vector2(hitBox.Right * 20 + 36, 0);
            input.getMotion("Move Right").HitBoxSize = new Vector2(hitBox.Right * 20 + 36, 396);

            return input.motionActivated("Move Right");
        }

        public bool rotate(InputHandler input, Rectangle hitBox)
        {
            input.getMotion("Rotate").HitBox = hitBox;

            return input.motionActivated("Rotate");
        }

        public bool drop(InputHandler input)
        {
            return input.motionActivated("Drop");
        }

        public bool fall(InputHandler input)
        {
            return input.motionActivated("Accel Drop");
        }

        public void draw(SpriteBatch spriteBatch)
        {
        }
    }
}
