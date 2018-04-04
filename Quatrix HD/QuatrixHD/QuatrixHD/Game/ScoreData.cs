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

namespace QuatrixHD.Quatrix
{
    /// <summary>
    /// Contains data about the game score, set once the game is over.
    /// </summary>
    struct ScoreData
    {
        public string gameOverMessage;
        public TimeSpan time;
        public int score;
        public int rowsDeleted;
        public int levelReached;

        public void setData(string message, TimeSpan finalTime, int finalScore, int finalRows, int finalLevel)
        {
            gameOverMessage = message;
            time = finalTime;
            score = finalScore;
            rowsDeleted = finalRows;
            levelReached = finalLevel;
        }
    }
}
