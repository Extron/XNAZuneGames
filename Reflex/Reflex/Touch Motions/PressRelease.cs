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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Reflex.Motions
{
    /// <summary>
    /// Checks for a press and then a release.  Activates if the user presses and releases in approximiately the same location.  Activates on release.
    /// </summary>
    public class PressRelease : TouchMotionType
    {
        public PressRelease(Rectangle pressHitBox)
        {
            hitBox = pressHitBox;
        }

        public PressRelease(Rectangle pressHitBox, float pressSpeed)
        {
            hitBox = pressHitBox;
            speed = pressSpeed;
        }

        #if ZUNE
        public override bool activated(TouchCollection touchState, GameTime gameTime)
        {
            bool returnValue = false;

            if (touchState.Count > 0)
            {
                if (hitBox.Contains((int)touchState[0].Position.X, (int)touchState[0].Position.Y))
                {
                    if (touchState[0].State == TouchLocationState.Pressed)
                    {
                        if (speed > 0)
                            startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                        else
                            startTime = 0;
                    }
                    else if (touchState[0].State == TouchLocationState.Released)
                    {
                        float totalTime = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

                        returnValue = totalTime <= speed;
                    }
                }
            }

            return returnValue;
        }
        #endif

        #if WINDOWS
        public override bool  activated(MouseState currentState, MouseState oldState, GameTime gameTime)
        {
            bool returnValue = false;

            if (currentState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
            {
                returnValue = false;
            }
            else if (currentState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                startVector = new Vector2(currentState.X, currentState.Y);

                if (speed > 0)
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                else
                    startTime = 0;

                returnValue = false;
            }
            else if (currentState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Pressed)
            {
                currentVector = new Vector2(currentState.X, currentState.Y);

                returnValue = false;
            }
            else if (currentState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
            {
                currentVector = new Vector2(currentState.X, currentState.Y);

                float totalTime = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

                if (hitBox.Contains((int)currentVector.X, (int)currentVector.Y))
                    returnValue = totalTime <= speed;
            }

            return returnValue;
        }
        #endif
    }
}