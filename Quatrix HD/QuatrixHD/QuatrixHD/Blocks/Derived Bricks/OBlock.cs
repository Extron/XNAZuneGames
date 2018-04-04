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
using Microsoft.Xna.Framework;
using QuatrixHD.Grid;
using Microsoft.Xna.Framework.Content;

namespace QuatrixHD.Blocks
{
    /// <summary>
    /// A block with the O block configurations.
    /// </summary>
    class OBlock : BlockType
    {
        public static Color color = new Color(65, 65, 255);

        public OBlock()
            : base()
        {
            spawn.brick1Vector = new Vector2(5, 0);
            spawn.brick2Vector = new Vector2(6, 0);
            spawn.brick3Vector = new Vector2(5, 1);
            spawn.brick4Vector = new Vector2(6, 1);

            previewSpawn.brick1Vector = new Vector2(1, 0);
            previewSpawn.brick2Vector = new Vector2(2, 0);
            previewSpawn.brick3Vector = new Vector2(1, 1);
            previewSpawn.brick4Vector = new Vector2(2, 1);

            spawnRotation = RotationState.second;

            numberOfRotations = 0;

            setColor(color);
        }

        #region Class Functions
        public void initialize()
        {
            setColor(color);
        }

        public override void changeColor(Color c)
        {
            color = c;

            setColor(c);
        }

        public override Color getColor()
        {
            return color;
        }

        public static void resetColor()
        {
            color = new Color(65, 65, 255);
        }
        #endregion
    }
}
