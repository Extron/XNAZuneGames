using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZStorage;

namespace Snakez
{
    class LevelProgressData
    {
        static DataType<int> data;

        public static int CurrentLevel
        {
            get { return data.list[0]; }
        }

        public DataType<int> CurrentLevelData
        {
            get { return data; }
        }

        public LevelProgressData()
        {
            data = new DataType<int>(1);
        }

        public bool populate(DataType<int> currentLevel)
        {
            if (currentLevel == null)
            {
                return false;
            }
            else
            {
                data = currentLevel;
                return true;
            }
        }

        public void setCurrentLevel(int level)
        {
            data.list[0] = level;
        }

        public void clearCurrentLevel()
        {
            data.list[0] = 1;
        }
    }
}
