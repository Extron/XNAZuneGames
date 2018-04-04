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
using QuatrixHD.Quatrix;

namespace QuatrixHD.Storage
{
    /// <summary>
    /// Contains data on the high scores, such as the score, the level reached, the rows deleted, and the time took.
    /// </summary>
    class HighScoreInformation
    {
        public int score;
        public int level;
        public int rows;
        public int time;

        public HighScoreInformation(int finalScore, int levelReached, int rowsRemoved, int timeTook)
        {
            score = finalScore;
            level = levelReached;
            rows = rowsRemoved;
            time = timeTook;
        }

        public HighScoreInformation(ScoreData gameData)
        {
            score = gameData.score;
            level = gameData.levelReached;
            rows = gameData.rowsDeleted;
            time = (gameData.time.Minutes * 100) + gameData.time.Seconds;
        }

        public void clear()
        {
            score = 0;
            level = 0;
            rows = 0;
            time = 0;
        }
    }
}
