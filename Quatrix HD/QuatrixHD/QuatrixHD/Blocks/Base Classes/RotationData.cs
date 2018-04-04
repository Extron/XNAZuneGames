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
using Microsoft.Xna.Framework;

namespace QuatrixHD.Blocks
{
    /// <summary>
    /// Contains information for one state of the rotation of the block, such as the new locations of the bricks.
    /// </summary>
    struct RotationData
    {
        public Vector2 brick1Location;
        public Vector2 brick2Location;
        public Vector2 brick3Location;
        public Vector2 brick4Location;

        public void rotate(BlockType block)
        {
            block.Brick1.Vector += brick1Location;

            block.Brick2.Vector += brick2Location;

            block.Brick3.Vector += brick3Location;

            block.Brick4.Vector += brick4Location;
        }
    }
}
