#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of Reflex HD.
 * 
 * Reflex HD is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * Reflex HD is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with Reflex HD.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Reflex
{
    /// <summary>
    /// A base class for touch motions, containing values to store a hit box, the current vector of the 
    /// touch, the starting vector of the motion, the speed of the motion, and the start time of the motion.
    /// </summary>
    public abstract class TouchMotionType
    {
        protected Rectangle hitBox;
        protected Vector2 currentVector;
        protected Vector2 startVector;
        protected float speed;
        protected float startTime;

        public Rectangle HitBox
        {
            get { return hitBox; }
            set { hitBox = value; }
        }

        public Vector2 HitBoxSize
        {
            get 
            {
                return new Vector2(hitBox.Width, hitBox.Height); 
            }
            set
            {
                hitBox.Width = (int)value.X;
                hitBox.Height = (int)value.Y;
            }
        }
        public Vector2 Location
        {
            get
            {
                return new Vector2(hitBox.X, hitBox.Y);
            }
            set
            {
                hitBox.Location = new Point((int)value.X, (int)value.Y);
            }
        }

        public Vector2 Position
        {
            get { return currentVector; }
        }

        #if ZUNE
        public abstract bool activated(TouchCollection touchState, GameTime gameTime);
        #endif

        #if WINDOWS
        public abstract bool activated(MouseState currentState, MouseState oldState, GameTime gameTime);
        #endif
    }
}
