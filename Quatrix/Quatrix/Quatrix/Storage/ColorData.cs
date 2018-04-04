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
using Microsoft.Xna.Framework.Graphics;

namespace Quatrix
{
    class ColorData
    {
        public ColorDataType data = new ColorDataType();

        public void loadData()
        {
            data.loadData();

            setDefault();

            assignColors();
        }

        public void setColors()
        {
            data.setColor(OBlock.color, 0);

            data.setColor(TBlock.color, 1);

            data.setColor(IBlock.color, 2);

            data.setColor(ZBlock.color, 3);

            data.setColor(SBlock.color, 4);

            data.setColor(LBlock.color, 5);

            data.setColor(JBlock.color, 6);
        }

        public void assignColors()
        {
            OBlock.color = data.getColor(0);

            TBlock.color = data.getColor(1);

            IBlock.color = data.getColor(2);

            ZBlock.color = data.getColor(3);
            
            SBlock.color = data.getColor(4);

            LBlock.color = data.getColor(5);

            JBlock.color = data.getColor(6);
        }

        public void setDefault()
        {
            OBlock.resetColor();

            TBlock.resetColor();

            IBlock.resetColor();

            ZBlock.resetColor();

            SBlock.resetColor();

            LBlock.resetColor();

            JBlock.resetColor();

            setColors();
        }
    }

        [Serializable]
        class ColorDataType
        {
            public DataType<int> redData;
            public DataType<int> greenData;
            public DataType<int> blueData;

           
            public void loadData()
            {
                for (int i = 0; i < redData.max; i++)
                {
                    int temp = 0;

                    redData.list.Add(temp);

                    greenData.list.Add(temp);

                    blueData.list.Add(temp);
                }
            }

            public Color getColor(int index)
            {
                Color temp = new Color();

                temp.R = (byte)redData.list[index];
                temp.G = (byte)greenData.list[index];
                temp.B = (byte)blueData.list[index];
                temp.A = 255;

                return temp;
            }

            public void setColor(Color color, int index)
            {
                redData.list[index] = color.R;
                greenData.list[index] = color.G;
                blueData.list[index] = color.B;
            }
        }
}
