/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of Quatrix.
 * 
 * Quatrix is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Quatrix is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Quatrix.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Quatrix
{
    class IBlock : GameBlockNew
    {
        public static Color color = new Color(0, 215, 70);

        #region Overridden Functions
        public override void load(ContentManager content)
        {
            setColor(color);
            base.load(content);
        }

        protected override GameBlockNew Create()
        {
            return new IBlock();
        }

        public override void Preview()
        {
            Brick1.Rect.Location = new Point(200, 13);
            Brick2.Rect.Location = new Point(200, 29);
            Brick3.Rect.Location = new Point(200, 45);
            Brick4.Rect.Location = new Point(200, 61);
        }

        public override void Normal()
        {
            Brick1.Rect.Location = new Point(57, 10);
            Brick2.Rect.Location = new Point(73, 10);
            Brick3.Rect.Location = new Point(89, 10);
            Brick4.Rect.Location = new Point(105, 10);
        }

        public override void Rotate()
        {
            if (rState == RotationState.second)
            {
                if (MovementFunctions.BounderyCoordinate(Brick1.Coordinate) < 9
                    && MovementFunctions.BounderyCoordinate(Brick1.Coordinate) > 0
                    && MovementFunctions.LeftBounderyCollision(this))
                {
                    Brick1.Rect.X -= 16;
                    Brick1.Rect.Y += 16;
                    Brick1.Coordinate += 9;
                    Brick3.Rect.X += 16;
                    Brick3.Rect.Y -= 16;
                    Brick3.Coordinate -= 9;
                    Brick4.Rect.X += 32;
                    Brick4.Rect.Y -= 32;
                    Brick4.Coordinate -= 18;

                    rState = RotationState.first;
                }
            }
            else if (rState == RotationState.first)
            {

                Brick1.Rect.X += 16;
                Brick1.Rect.Y -= 16;
                Brick1.Coordinate -= 9;
                Brick3.Rect.X -= 16;
                Brick3.Rect.Y += 16;
                Brick3.Coordinate += 9;
                Brick4.Rect.X -= 32;
                Brick4.Rect.Y += 32;
                Brick4.Coordinate += 18;

                rState = RotationState.second;
            }
        }
        #endregion

        #region Class Functions
        public void initialize()
        {
            setColor(color);
        }
        public void changeColor(Color c)
        {
            color = c;

            setColor(c);
        }

        public static void resetColor()
        {
            color = new Color(0, 215, 70);
        }
        #endregion
    }
}
