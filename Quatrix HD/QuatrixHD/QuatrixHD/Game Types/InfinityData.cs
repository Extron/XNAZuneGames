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

namespace QuatrixHD.GameTypes
{
    /// <summary>
    /// The settings of the Infinity game type.
    /// </summary>
    class InfinityData : GameTypeData
    {
        public InfinityData()
        {
            scoreValue = ScoreValue.score;
            timeLimit = TimeSpan.Zero;
            maximumRows = 0;
            rowsToAdvance = 10;
            startSpeed = 45;
            maximumSpeed = 5;
        }
    }
}
