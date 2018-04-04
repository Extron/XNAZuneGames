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
    /// Checks for a swipe motion.  Activates if the user has swiped in the approximate direction and length.  Activates on release.
    /// </summary>
    public class Swipe : TouchMotionType
    {
        Vector2 direction;
        int vectorID;

        public Swipe(Vector2 swipeDirection, float motionSpeed)
        {
            direction = swipeDirection;
            speed = motionSpeed;
        }

        #if ZUNE
        public override bool activated(TouchCollection touchState, GameTime gameTime)
        {
            bool returnValue = false;

            //If there is at least one input location, check that location.
            if (touchState.Count > 0)
            {
                foreach (TouchLocation locationState in touchState)
                {
                    switch (locationState.State)
                    {
                        case TouchLocationState.Pressed:
                            startVector = locationState.Position;

                            vectorID = locationState.Id;

                            if (speed > 0)
                                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                            else
                                startTime = 0;
                            break;

                        case TouchLocationState.Released:
                            if (locationState.Id == vectorID)
                            {
                                float totalTime = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

                                //Determine the distance travelled from start to end.
                                Vector2 travel = locationState.Position - startVector;

                                //If both of the travel values are zero, the input did not move, so do not check the distance
                                if ((travel.X != 0 || travel.Y != 0) && (Math.Abs(travel.X) >= Math.Abs(direction.X) && Math.Abs(travel.Y) >= Math.Abs(direction.Y)))
                                {
                                    //If the absolute value of the negative distance minus the travel is equal to the sum of the absolute values, return true 
                                    bool xValue = Math.Abs(-direction.X - travel.X) == Math.Abs(direction.X) + Math.Abs(travel.X);

                                    bool yValue = Math.Abs(-direction.Y - travel.Y) == Math.Abs(direction.Y) + Math.Abs(travel.Y);

                                    returnValue = (xValue && yValue) && (totalTime <= speed);
                                }
                            }
                            break;
                    }
                }
            }

            return returnValue;
        }
        #endif

        #if WINDOWS
        public override bool activated(MouseState currentState, MouseState oldState, GameTime gameTime)
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
                float totalTime = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

                Vector2 travel = currentVector - startVector;

                //If both of the travel values are zero, the input did not move, so do not check the distance
                if ((travel.X != 0 || travel.Y != 0) && (Math.Abs(travel.X) >= Math.Abs(direction.X) && Math.Abs(travel.Y) >= Math.Abs(direction.Y)))
                {
                    //If the absolute value of the negative distance minus the travel is equal to the sum of the absolute values, return true 
                    bool xValue = Math.Abs(-direction.X - travel.X) == Math.Abs(direction.X) + Math.Abs(travel.X);

                    bool yValue = Math.Abs(-direction.Y - travel.Y) == Math.Abs(direction.Y) + Math.Abs(travel.Y);

                    returnValue = (xValue && yValue) && (totalTime <= speed);
                }
            }

            return returnValue;
        }
        #endif
    }
}
