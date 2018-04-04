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
    /// A base class for game types that contains information on the settings of the game.
    /// </summary>
    class GameTypeData
    {
        protected ScoreValue scoreValue;
        protected TimeSpan timeLimit;
        protected int maximumRows;
        protected int rowsToAdvance;
        protected int startSpeed;
        protected int maximumSpeed;

        public ScoreValue ScoreValue
        {
            get { return scoreValue; }
        }

        public TimeSpan TimeLimit
        {
            get { return timeLimit; }
        }

        public int MaxRows
        {
            get { return maximumRows; }
        }

        public int RowsToAdvance
        {
            get { return rowsToAdvance; }
        }

        public int StartSpeed
        {
            get { return startSpeed; }
        }

        public int MaxSpeed
        {
            get { return maximumSpeed; }
        }
    }
}
