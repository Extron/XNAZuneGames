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

namespace Reflex.Motions
{
    /// <summary>
    /// Checks for a press in a specific area of the screen.
    /// </summary>
    public class Press : TouchMotionType
    {
        bool edgeDetect;
        bool hasPressed;

        public Press(Rectangle pressHitBox)
        {
            hitBox = pressHitBox;
            edgeDetect = false;
            hasPressed = false;
        }

        public Press(Rectangle pressHitBox, bool useEdgeDetect)
        {
            hitBox = pressHitBox;
            edgeDetect = useEdgeDetect;
            hasPressed = false;
        }

        #if ZUNE
        public override bool activated(TouchCollection touchState, GameTime gameTime)
        {
            bool returnValue = false;

            if (touchState.Count > 0)
            {
                foreach (TouchLocation locationState in touchState)
                {
                    switch (locationState.State)
                    {
                        case TouchLocationState.Moved:
                            if (hitBox.Contains((int)locationState.Position.X, (int)locationState.Position.Y))
                            {
                                if (edgeDetect)
                                {
                                    if (!hasPressed)
                                    {
                                        returnValue = true;
                                        hasPressed = true;
                                    }
                                }
                                else
                                    returnValue = true;
                            }
                            break;
                    }
                }
            }
            else
            {
                if (edgeDetect)
                    hasPressed = false;
            }

            return returnValue;
        }
        #endif

        #if WINDOWS
        public override bool activated(MouseState currentState, MouseState oldState, GameTime gameTime)
        {
            bool returnValue = false;

            if (currentState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                startVector = new Vector2(currentState.X, currentState.Y);

                currentVector = startVector;

                if (hitBox.Contains((int)currentVector.X, (int)currentVector.Y))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }
        #endif

    }
}
