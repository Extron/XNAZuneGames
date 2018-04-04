/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of Quatrix.
 * 
 * Quatrix is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Quatrix is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Quatrix.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZStorage;

namespace Quatrix
{
    class HighScoreData
    {
        public DataType<int> data;

        public int getHighScore()
        {
            int highScore = 0;

            data.list.Sort();

            if (data.list.Count > 0)
            {
                highScore = data.list[data.list.Count - 1];
            }

            return highScore;
        }

        public bool addHighScore(int score)
        {
            bool added = false;

            data.list.Sort();

            if (data.list.Count == 0)
            {
                data.list.Add(score);

                added = true;
            }
            else if (data.list.Count < data.max)
            {
                data.list.Add(score);

                data.list.Sort();

                added = true;
            }
            else
            {
                if (score > data.list[0])
                {
                    data.list[0] = score;

                    data.list.Sort();

                    added = true;
                }
            }

            return added;
        }

        public void clearHighScores()
        {
            data.list.Clear();
        }
    }
}
