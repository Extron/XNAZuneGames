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
    class SBlock : GameBlockNew
    {
        public static Color color = Color.Red;

        #region Overridden Functions
        public override void load(ContentManager content)
        {
            setColor(color);
            base.load(content);
        }

        protected override GameBlockNew Create()
        {
            return new SBlock();
        }

        public override void Preview()
        {
            Brick1.Rect.Location = new Point(201, 41);
            Brick2.Rect.Location = new Point(217, 41);
            Brick3.Rect.Location = new Point(185, 57);
            Brick4.Rect.Location = new Point(201, 57);
        }

        public override void Normal()
        {
            Brick1.Rect.Location = new Point(73, 10);
            Brick2.Rect.Location = new Point(89, 10);
            Brick3.Rect.Location = new Point(57, 26);
            Brick4.Rect.Location = new Point(73, 26);
        }

        public override void Rotate()
        {
            if (rState == RotationState.first)
            {
                Brick1.Rect.Y -= 16;
                Brick1.Coordinate -= 10;
                Brick3.Rect.X += 16;
                Brick3.Rect.Y -= 16;
                Brick3.Coordinate -= 9;
                Brick4.Rect.X += 16;
                Brick4.Coordinate++;
                rState = RotationState.second;
            }
            else if (rState == RotationState.second)
            {
                if (MovementFunctions.BounderyCoordinate(Brick1.Coordinate) > 0
                    && MovementFunctions.LeftBounderyCollision(this))
                {
                    Brick1.Rect.Y += 16;
                    Brick1.Coordinate += 10;
                    Brick3.Rect.X -= 16;
                    Brick3.Rect.Y += 16;
                    Brick3.Coordinate += 9;
                    Brick4.Rect.X -= 16;
                    Brick4.Coordinate--;
                    rState = RotationState.first;
                }
            }
        }
        #endregion

        #region Class Functions
        public void changeColor(Color c)
        {
            color = c;

            setColor(c);
        }

        public static void resetColor()
        {
            color = Color.Red;
        }
        #endregion
    }
}
