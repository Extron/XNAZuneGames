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
    /// Manages a counter, counting in game "ticks".  Once the counter has reached a designated value, an event will
    /// be raised.
    /// </summary>
    class CounterType
    {
        EventHandler Hit;
        int interval;
        int counter;

        public int Interval
        {
            get { return interval; }
        }

        public CounterType(int counterInterval)
        {
            interval = counterInterval;
        }

        public void update()
        {
            if (counter == interval)
            {
                Hit(this, new EventArgs());
                counter = 0;
            }

            counter++;
        }

        public void setEvent(EventHandler method)
        {
            Hit += method;
        }

        public void changeInterval(int newInterval)
        {
            interval = newInterval;

            counter = 0;
        }

        public void increaseInterval(int factor)
        {
            interval += factor;
        }

        public void reset()
        {
            counter = 0;
        }
    }
}
