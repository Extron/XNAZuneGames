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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Reflex
{
    /// <summary>
    /// Manages both the Zune input and an emulator input for Windows development.  Contains a list of specific customizable 
    /// "motions", which can be monitored independantly from one another.  Also contains a list of functions for most basic
    /// types of input, as a way to access input information without having to define motions for each situation.
    /// </summary>
    public class InputHandler : GameComponent
    {
        #region Fields
        /// <summary>
        /// A list of unique independant motions for the input handler to check.
        /// </summary>
        Dictionary<string, TouchMotionType> motions;

        /// <summary>
        /// A starting location for input, allowing the handler to track how far the input motion travelled.
        /// </summary>
        Vector2 startLocation;

        /// <summary>
        /// A variable holding the game's current game time.
        /// </summary>
        GameTime time;

        #if ZUNE
        /// <summary>
        /// The current Zune touch state.
        /// </summary>
        TouchCollection touchState;

        /// <summary>
        /// The previous Zune touch state.
        /// </summary>
        TouchCollection oldState;
        #endif

        #if WINDOWS
        MouseState mouseState;
        MouseState oldState;
        KeyboardState oldKeyboard;
        #endif
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current location of the input, or a zero-value vector if there isn't any input.
        /// </summary>
        public Vector2 Location
        {
            get 
            {
                #if WINDOWS
                return new Vector2(mouseState.X, mouseState.Y); 
                #endif

                #if ZUNE
                if (touchState.Count > 0)
                {
                    return touchState[touchState.Count - 1].Position;
                }
                else
                    return new Vector2();
                #endif
            }
        }

        /// <summary>
        /// Gets a vector, representing the distance traveled, in signed values from the last update of the input.
        /// </summary>
        public Vector2 Distance
        {
            get
            {
                #if WINDOWS
                return (new Vector2(oldState.X, oldState.Y) - new Vector2(mouseState.X, mouseState.Y));
                #endif

                #if ZUNE
                if (touchState.Count > 0)
                {
                    TouchLocation location;

                    if (touchState[0].TryGetPreviousLocation(out location))
                        return (location.Position - touchState[0].Position);
                    else
                        return new Vector2();
                }
                else
                    return new Vector2();
                #endif
            }
        }

        /// <summary>
        /// Gets the amount travelled from the start of the input, in signed values originating from the start location.
        /// </summary>
        public Vector2 Travel
        {
            get
            {
                #if WINDOWS
                return (startLocation - new Vector2(mouseState.X, mouseState.Y));
                #endif

                #if ZUNE
                if (touchState.Count > 1)
                {
                    return (startLocation - touchState[0].Position);
                }
                else
                    return new Vector2();
                #endif
            }
        }
        #endregion

        #region Constructors
        public InputHandler(Game game)
            : base(game)
        {
            motions = new Dictionary<string, TouchMotionType>();
        }
        #endregion

        #region Game Methods
        public override void Initialize()
        {
            #if ZUNE
            oldState = TouchPanel.GetState();
            touchState = TouchPanel.GetState();
            #endif

            #if WINDOWS
            mouseState = Mouse.GetState();
            oldState = Mouse.GetState();
            #endif

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            time = gameTime;

            #if ZUNE
            oldState = touchState;
            touchState = TouchPanel.GetState();
            #endif

            #if WINDOWS
            oldState = mouseState;
            mouseState = Mouse.GetState();
            oldKeyboard = Keyboard.GetState();
            #endif

            base.Update(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            motions.Clear();

            base.Dispose(disposing);
        }
        #endregion

        /// <summary>
        /// Forces input update before the main game's next iteration of the update method.
        /// </summary>
        public void updateInput()
        {
            #if ZUNE
            oldState = touchState;
            touchState = TouchPanel.GetState();
            #endif

            #if WINDOWS
            oldState = mouseState;
            mouseState = Mouse.GetState();
            oldKeyboard = Keyboard.GetState();
            #endif
        }

        public void unload()
        {
            motions.Clear();
        }

        /// <summary>
        /// Adds a unique motion to the list of motions.
        /// </summary>
        /// <param name="identifier">The identifier which the motion can be accessed.</param>
        /// <param name="motion">The motion to be added to the list.</param>
        public void addMotion(string identifier, TouchMotionType motion)
        {
            motions.Add(identifier, motion);
        }

        /// <summary>
        /// Gets a motion by its identifier.
        /// </summary>
        /// <param name="identifier">The unique identifier of the motion.</param>
        /// <returns>Returns the motion referenced by the identifier.  If no motion is found, returns null.</returns>
        public TouchMotionType getMotion(string identifier)
        {
            TouchMotionType motion;

            if (motions.TryGetValue(identifier, out motion))
            {
                return motion;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Determines if a motion has been activated.
        /// </summary>
        /// <param name="identifier">The unique identification of the motion</param>
        /// <returns>Returns whether the motion has been activated by the user or not.</returns>
        public bool motionActivated(string identifier)
        {
            TouchMotionType motion;

            if (motions.TryGetValue(identifier, out motion))
            {
                #if ZUNE
                return motion.activated(touchState, time);
                #endif

                #if WINDOWS
                return motion.activated(mouseState, oldState, time);
                #endif
            }
            else
                return false;
        }

        /// <summary>
        /// Determines if there has been a tap on the screen, within a specific area on the screen.
        /// </summary>
        /// <param name="hitBox">The area to check for the tap.</param>
        /// <param name="onRelease">If this value is true, the method will only return true if the user has pressed and
        /// the released.  Otherwise it returns true if the user simply presses.</param>
        /// <returns>Returns a value indicating wether the user has tapped the screen in a designated area.</returns>
        public bool pressActivated(Rectangle hitBox, bool onRelease)
        {
            bool returnValue = false;

            #if ZUNE
            //If the touchState reports a touch on the screen, check that location
            if (touchState.Count > 0)
            {
                //If the first tap on the screen is within the rectangle, check its state
                if (hitBox.Contains((int)touchState[0].Position.X, (int)touchState[0].Position.Y))
                {
                    //If it is looking for on release, check the state for a Released state
                    if (onRelease)
                        returnValue = touchState[0].State == TouchLocationState.Released;
                    //Else check for a state other than the released state
                    else
                        returnValue = touchState[0].State != TouchLocationState.Released;
                }
            }
            #endif

            #if WINDOWS
            if (hitBox.Contains(mouseState.X, mouseState.Y))
            {
                if (onRelease)
                    returnValue = (mouseState.LeftButton == ButtonState.Released) && (oldState.LeftButton == ButtonState.Pressed);
                else
                    returnValue = mouseState.LeftButton == ButtonState.Pressed;
            }
            #endif

            return returnValue;
        }

        /// <summary>
        /// Determines if the a tap has been made within the update frame.
        /// </summary>
        /// <param name="hitBox">The area of the screen where the tap needs to be made.</param>
        /// <returns>Returns true if the user has tapped the screen within the update frame, i.e. the previous update frame
        /// reported no input.  Returns false otherwise.</returns>
        public bool pressActivated(Rectangle hitBox)
        {
            bool returnValue = false;

            #if ZUNE
            //If there is input reported on the screen, check the input
            if (touchState.Count > 0 && oldState.Count > 0)
            {
                //If the input location is within the designated rectangle, check its state.
                if (hitBox.Contains((int)touchState[0].Position.X, (int)touchState[0].Position.Y))
                {
                    returnValue = touchState[0].State == TouchLocationState.Moved && oldState[0].State == TouchLocationState.Pressed;
                }
            }
            #endif

            #if WINDOWS
            if (hitBox.Contains(mouseState.X, mouseState.Y))
                returnValue = (mouseState.LeftButton == ButtonState.Pressed) && (oldState.LeftButton == ButtonState.Released);
            #endif

            return returnValue;
        }

        public bool backActivated(bool edgeDetect)
        {
            bool returnValue = false;

            if (edgeDetect)
            {
                #if WINDOWS
                returnValue = (Keyboard.GetState().IsKeyDown(Keys.Escape) && oldKeyboard.IsKeyUp(Keys.Escape));
                #endif
            }
            else
            {
                #if WINDOWS
                returnValue = (Keyboard.GetState().IsKeyDown(Keys.Escape));
                #endif
            }

            #if WINDOWS
            oldKeyboard = Keyboard.GetState();
            #endif

            return returnValue;
        }

        /// <summary>
        /// Determines if a swipe motion has been activated within a given area of the screen.
        /// </summary>
        /// <param name="boundingBox">The area of the screen the motion must be activated in.</param>
        /// <param name="direction">The direction of the swipe, including minimum magnitude.</param>
        /// <returns>Returns true if the user has swiped a distance greater or equal to the direction within the area, 
        /// otherwise returns false.</returns>
        public bool swipeActivated(Rectangle boundingBox, Vector2 direction)
        {
            bool returnValue = false;

            #if ZUNE
            //If there is at least one input location, check that location.
            if (touchState.Count > 0)
            {
                foreach (TouchLocation locationState in touchState)
                {
                    switch (locationState.State)
                    {
                        case TouchLocationState.Pressed:
                            startLocation = locationState.Position;
                            break;

                        case TouchLocationState.Released:
                            if (boundingBox.Contains((int)startLocation.X, (int)startLocation.Y) && boundingBox.Contains((int)locationState.Position.X, (int)locationState.Position.Y))
                            {
                                //Determine the distance travelled from start to end.
                                Vector2 travel = locationState.Position - startLocation;

                                //If both of the travel values are zero, the input did not move, so do not check the distance
                                if ((travel.X != 0 || travel.Y != 0) && (Math.Abs(travel.X) >= Math.Abs(direction.X) && Math.Abs(travel.Y) >= Math.Abs(direction.Y)))
                                {
                                    //If the absolute value of the negative distance minus the travel is equal to the sum of the absolute values, return true 
                                    bool xValue = Math.Abs(-direction.X - travel.X) == Math.Abs(direction.X) + Math.Abs(travel.X);

                                    bool yValue = Math.Abs(-direction.Y - travel.Y) == Math.Abs(direction.Y) + Math.Abs(travel.Y);

                                    returnValue = (xValue && yValue);
                                }
                            }
                            break;
                    }
                }
            }
            #endif

            #if WINDOWS
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
            {
                if (boundingBox.Contains(mouseState.X, mouseState.Y) && boundingBox.Contains(oldState.X, oldState.Y))
                {
                    Vector2 travel = startLocation - new Vector2(mouseState.X, mouseState.Y);

                    //If both of the travel values are zero, the input did not move, so do not check the distance
                    if (travel.X != 0 || travel.Y != 0)
                    {
                        //If the absolute value of the negative distance minus the travel is equal to the sum of the absolute values, return true 
                        bool xValue = Math.Abs(-direction.X - travel.X) == Math.Abs(direction.X) + Math.Abs(travel.X);

                        bool yValue = Math.Abs(-direction.Y - travel.Y) == Math.Abs(direction.Y) + Math.Abs(travel.Y);

                        returnValue = xValue && yValue;
                    }
                }
            }
            else if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                startLocation = new Vector2(oldState.X, oldState.Y);
            }
            else if (mouseState.LeftButton == ButtonState.Released)
                startLocation = new Vector2(mouseState.X, mouseState.Y);
#endif

            return returnValue;
        }

        /// <summary>
        /// Determines if a swipe motion has been activated within a given area of the screen.
        /// </summary>
        /// <param name="boundingBox">The area of the screen the motion must be activated in.</param>
        /// <param name="direction">The direction of the swipe, including minimum magnitude.</param>
        /// <param name="magnitude">A value representing the amount that the swipe was over the given distance.</param>
        /// <returns>Returns true if the user has swiped a distance greater or equal to the direction within the area, 
        /// otherwise returns false.</returns>
        public bool swipeActivated(Rectangle boundingBox, Vector2 direction, out Vector2 magnitude)
        {
            bool returnValue = false;

            magnitude = new Vector2();

            #if ZUNE
            //If there is at least one input location, check that location.
            if (touchState.Count > 0)
            {
                //If the touch location has a state of released, check to see if the total motion satisfies the swipe parameters
                if (touchState[0].State == TouchLocationState.Released)
                {
                    //If both the starting location and the ending location are within the designated hit location, check its distance.
                    if (boundingBox.Contains((int)startLocation.X, (int)startLocation.Y) && boundingBox.Contains((int)touchState[0].Position.X, (int)touchState[0].Position.Y))
                    {
                        //Determine the distance travelled from start to end.
                        Vector2 travel = touchState[0].Position - startLocation;

                        //If both of the travel values are zero, the input did not move, so do not check the distance
                        if (travel.X != 0 || travel.Y != 0)
                        {
                            //If the absolute value of the negative distance minus the travel is equal to the sum of the absolute values, return true 
                            bool xValue = Math.Abs(-direction.X - travel.X) == Math.Abs(direction.X) + Math.Abs(travel.X);

                            bool yValue = Math.Abs(-direction.Y - travel.Y) == Math.Abs(direction.Y) + Math.Abs(travel.Y);

                            returnValue = xValue && yValue;
                        }

                        magnitude = travel - direction;
                    }
                }
            }
            #endif

            #if WINDOWS
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
            {
                if (boundingBox.Contains(mouseState.X, mouseState.Y) && boundingBox.Contains(oldState.X, oldState.Y))
                {
                    Vector2 travel = startLocation - new Vector2(mouseState.X, mouseState.Y);

                    //If both of the travel values are zero, the input did not move, so do not check the distance
                    if (travel.X != 0 || travel.Y != 0)
                    {
                        //If the absolute value of the negative distance minus the travel is equal to the sum of the absolute values, return true 
                        bool xValue = Math.Abs(-direction.X - travel.X) == Math.Abs(direction.X) + Math.Abs(travel.X);

                        bool yValue = Math.Abs(-direction.Y - travel.Y) == Math.Abs(direction.Y) + Math.Abs(travel.Y);

                        returnValue = xValue && yValue;
                    }

                    magnitude = travel - direction;
                }
            }
            else if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                startLocation = new Vector2(oldState.X, oldState.Y);
            }
            else if (mouseState.LeftButton == ButtonState.Released)
                startLocation = new Vector2(mouseState.X, mouseState.Y);
#endif

            return returnValue;
        }
    }
}
