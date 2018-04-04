#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of ZStorage.
 * 
 * ZStorage is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * ZStorage is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with ZStorage.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZStorage
{
    /// <summary>
    /// A serializable class that contains a list of data to be stored to a .lst file.
    /// </summary>
    /// <typeparam name="Data">The data type of the information to be stored.
    /// NOTE: Only data types with serializable capabilities can be stored with this class.</typeparam>
    [Serializable]
    public class DataType<Data>
    {
        public List<Data> list;

        public int max;

        public DataType()
        {
            list = new List<Data>();
        }

        public DataType(int maxCount)
        {
            list = new List<Data>(maxCount);

            max = maxCount;
        }

        public void fillList(Data value)
        {
            for (int i = 0; i < max; i++)
                list.Add(value);
        }
    }
}
