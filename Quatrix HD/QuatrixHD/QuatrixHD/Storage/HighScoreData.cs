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
using ZStorage;
using QuatrixHD.Quatrix;

namespace QuatrixHD.Storage
{
    /// <summary>
    /// Manages the storage of the high score data.
    /// </summary>
    static class HighScoreData
    {
        static DataType<int> data;

        static List<HighScoreInformation> highScores;

        public static List<HighScoreInformation> HighScores
        {
            get { return highScores; }
        }

        public static void load(StorageComponent storage)
        {
            highScores = new List<HighScoreInformation>();

            data = storage.LoadData<int>("highscores.lst");

            if (data == null)
            {
                data = new DataType<int>(40);

                data.fillList(0);

                storage.SaveData<int>(data, "highscores.lst");
            }

            for (int i = 0; i < 10; i++)
            {
                highScores.Add(new HighScoreInformation(data.list[i], data.list[i + 10],
                                                        data.list[i + 20], data.list[i + 30]));
            }
        }

        public static void save(StorageComponent storage)
        {
            for (int i = 0; i < highScores.Count; i++)
            {
                data.list[i] = highScores[i].score;
                data.list[i + 10] = highScores[i].level;
                data.list[i + 20] = highScores[i].rows;
                data.list[i + 30] = highScores[i].time;
            }

            storage.SaveData<int>(data, "highscores.lst");
        }

        public static void addScore(int finalScore, int levelReached, int rowsRemoved, int timeTook)
        {
            highScores.Sort(new Comparer());

            if (highScores.Count >= 10)
                highScores.RemoveAt(highScores.Count - 1);

            highScores.Add(new HighScoreInformation(finalScore, levelReached, rowsRemoved, timeTook));

            highScores.Sort(new Comparer());
        }

        public static void addScore(ScoreData gameData)
        {
            highScores.Sort(new Comparer());

            if (highScores.Count >= 10)
                highScores.RemoveAt(highScores.Count - 1);

            highScores.Add(new HighScoreInformation(gameData));

            highScores.Sort(new Comparer());
        }

        public static void clearScores()
        {
            for (int i = 0; i < highScores.Count; i++)
            {
                highScores[i].clear();
            }

        }
    }

    /// <summary>
    /// The comparer for the HighScoreInformation class, inversely comparing the score data.
    /// </summary>
    class Comparer : IComparer<HighScoreInformation>
    {
        public int Compare(HighScoreInformation object1, HighScoreInformation object2)
        {
            if (object1.score > object2.score)
                return -1;
            else if (object1.score < object2.score)
                return 1;
            else
                return 0;
        }
    }
}
