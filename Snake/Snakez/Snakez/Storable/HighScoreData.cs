using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZStorage;

namespace Snakez
{
    class HighScoreData
    {
        static DataType<int> highScores;

        public static List<int> List
        {
            get
            {
                return highScores.list;
            }
        }

        public DataType<int> Scores
        {
            get { return highScores; }
        }

        public HighScoreData()
        {
            highScores = new DataType<int>(10);
        }

        public bool populate(DataType<int> scores)
        {
            //If the saved score data is null, return false, else copy the data and return true
            if (scores == null)
            {
                return false;
            }
            else
            {
                highScores = scores;
                highScores.list.Sort();
                return true;
            }
        }

        public void addScore(int score)
        {
            //If the list is not full, add the new score and sort, else remove the lowest score, add the new score, and sort
            if (highScores.list.Count < highScores.max)
            {
                highScores.list.Add(score);
                highScores.list.Sort();
            }
            else
            {
                highScores.list.Remove(highScores.list.Min());
                highScores.list.Add(score);
                highScores.list.Sort();
            }
        }

        public void clearScores()
        {
            highScores.list.Clear();
        }
    }
}
