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
using Microsoft.Xna.Framework.Graphics;
using Reflex;
using Reflex.Motions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace QuatrixHD.Quatrix
{
    /// <summary>
    /// A controls setting that creates pressable buttons on the screen.
    /// </summary>
    class ButtonControls : IControls
    {
        Texture2D leftButton;
        Texture2D rightButton;
        Texture2D rotateButton;
        Texture2D fallButton;
        Texture2D dropButton;

        public void initializeInput(InputHandler input)
        {
            input.addMotion("Left", new PressHold(new Rectangle(3, 240, 30, 30)));
            input.addMotion("Right", new PressHold(new Rectangle(239, 240, 30, 30)));
            input.addMotion("Rotate", new Press(new Rectangle(3, 280, 30, 30), true));
            input.addMotion("Fall", new PressHold(new Rectangle(239, 280, 30, 30)));
            input.addMotion("Drop", new Press(new Rectangle(239, 320, 30, 30), true));
        }

        public void load(ContentManager content)
        {
            leftButton = content.Load<Texture2D>("Game/Buttons/Left Button");
            rightButton = content.Load<Texture2D>("Game/Buttons/Right Button");
            rotateButton = content.Load<Texture2D>("Game/Buttons/Rotate Button");
            fallButton = content.Load<Texture2D>("Game/Buttons/Fall Button");
            dropButton = content.Load<Texture2D>("Game/Buttons/Drop Button");
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(leftButton, new Vector2(3, 240), Color.White);
            spriteBatch.Draw(rightButton, new Vector2(239, 240), Color.White);
            spriteBatch.Draw(rotateButton, new Vector2(3, 280), Color.White);
            spriteBatch.Draw(fallButton, new Vector2(239, 280), Color.White);
            spriteBatch.Draw(dropButton, new Vector2(239, 320), Color.White);
        }

        public bool moveLeft(InputHandler input, Rectangle hitBox)
        {
            return input.motionActivated("Left");
        }

        public bool moveRight(InputHandler input, Rectangle hitBox)
        {
            return input.motionActivated("Right");
        }

        public bool rotate(InputHandler input, Rectangle hitBox)
        {
            return input.motionActivated("Rotate");
        }

        public bool fall(InputHandler input)
        {
            return input.motionActivated("Fall");
        }

        public bool drop(InputHandler input)
        {
            return input.motionActivated("Drop");
        }
    }
}
