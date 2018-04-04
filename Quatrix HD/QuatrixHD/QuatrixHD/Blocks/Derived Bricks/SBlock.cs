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
    /// A block with the S block configurations.
    /// </summary>
    class SBlock : BlockType
    {
        public static Color color = Color.Red;

        public SBlock()
            : base()
        {
            spawn.brick1Vector = new Vector2(4, 1);
            spawn.brick2Vector = new Vector2(5, 1);
            spawn.brick3Vector = new Vector2(5, 0);
            spawn.brick4Vector = new Vector2(6, 0);

            previewSpawn.brick1Vector = new Vector2(0, 1);
            previewSpawn.brick2Vector = new Vector2(1, 1);
            previewSpawn.brick3Vector = new Vector2(1, 0);
            previewSpawn.brick4Vector = new Vector2(2, 0);

            spawnRotation = RotationState.first;

            rotation1Data.brick1Location = new Vector2(2, 0);
            rotation1Data.brick2Location = new Vector2(1, -1);
            rotation1Data.brick4Location = new Vector2(-1, -1);

            rotation2Data.brick1Location = new Vector2(-2, 0);
            rotation2Data.brick2Location = new Vector2(-1, 1);
            rotation2Data.brick4Location = new Vector2(1, 1);

            numberOfRotations = 2;
           
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
            color = Color.Red;
        }
        #endregion
    }
}
