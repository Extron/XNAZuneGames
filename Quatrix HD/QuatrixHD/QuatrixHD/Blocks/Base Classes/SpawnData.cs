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
using QuatrixHD.Grid;

namespace QuatrixHD.Blocks
{
    /// <summary>
    /// Contains information of the spawn location of each brick within a block.
    /// </summary>
    struct SpawnData
    {
        public Vector2 brick1Vector;
        public Vector2 brick2Vector;
        public Vector2 brick3Vector;
        public Vector2 brick4Vector;

        public void spawn(BlockType block)
        {
            block.Brick1.Vector = brick1Vector;
            block.Brick2.Vector = brick2Vector;
            block.Brick3.Vector = brick3Vector;
            block.Brick4.Vector = brick4Vector;
        }

    }
}
