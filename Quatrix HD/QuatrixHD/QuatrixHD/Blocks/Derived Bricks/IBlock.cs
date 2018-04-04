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
    /// A block with the I block configuration.
    /// </summary>
    class IBlock : BlockType
    {
        public static Color color = new Color(0, 215, 70);

        public IBlock()
            : base()
        {
            spawn.brick1Vector = new Vector2(4, 0);
            spawn.brick2Vector = new Vector2(5, 0);
            spawn.brick3Vector = new Vector2(6, 0);
            spawn.brick4Vector = new Vector2(7, 0);

            previewSpawn.brick1Vector = new Vector2(0, 0);
            previewSpawn.brick2Vector = new Vector2(1, 0);
            previewSpawn.brick3Vector = new Vector2(2, 0);
            previewSpawn.brick4Vector = new Vector2(3, 0);

            spawnRotation = RotationState.second;

            rotation1Data.brick1Location = new Vector2(-1, 1);
            rotation1Data.brick3Location = new Vector2(1, -1);
            rotation1Data.brick4Location = new Vector2(2, -2);

            rotation2Data.brick1Location = new Vector2(1, -1);
            rotation2Data.brick3Location = new Vector2(-1, 1);
            rotation2Data.brick4Location = new Vector2(-2, 2);

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
            color = new Color(0, 215, 70);
        }
        #endregion
    }
}
