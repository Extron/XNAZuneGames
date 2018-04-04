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
using Microsoft.Xna.Framework.Graphics;
using ZStorage;
using QuatrixHD.Blocks;

namespace QuatrixHD.Storage
{
    /// <summary>
    /// Manages the storage of the block color data.
    /// </summary>
    static class ColorData
    {
        static DataType<byte> data;

        static List<Color> colors;

        public static void load(StorageComponent storage)
        {
            colors = new List<Color>();

            data = storage.LoadData<byte>("colors.lst");

            if (data == null)
            {
                data = new DataType<byte>(21);

                data.fillList(0);

                resetColors();

                saveColors();

                storage.SaveData<byte>(data, "colors.lst");
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    colors.Add(new Color(data.list[i], data.list[i + 7], data.list[i + 14]));
                }
            }
        }

        public static void save(StorageComponent storage)
        {
            for (int i = 0; i < colors.Count; i++)
            {
                data.list[i] = colors[i].R;
                data.list[i + 7] = colors[i].G;
                data.list[i + 14] = colors[i].B;
            }

            storage.SaveData<byte>(data, "colors.lst");
        }

        public static void saveColor(BlockType block)
        {
            if (block is OBlock)
                colors[0] = OBlock.color;
            else if (block is IBlock)
                colors[1] = IBlock.color;
            else if (block is TBlock)
                colors[2] = TBlock.color;
            else if (block is JBlock)
                colors[3] = JBlock.color;
            else if (block is LBlock)
                colors[4] = LBlock.color;
            else if (block is SBlock)
                colors[5] = SBlock.color;
            else if (block is ZBlock)
                colors[6] = ZBlock.color;
        }

        public static void saveColors()
        {
            if (colors.Count == 0)
            {
                for (int i = 0; i < 7; i++)
                    colors.Add(Color.White);
            }

            colors[0] = OBlock.color;
            colors[1] = IBlock.color;
            colors[2] = TBlock.color;
            colors[3] = JBlock.color;
            colors[4] = LBlock.color;
            colors[5] = SBlock.color;
            colors[6] = ZBlock.color;
        }

        public static void setColor(BlockType block)
        {
            if (block is OBlock)
                OBlock.color = colors[0];
            else if (block is IBlock)
                IBlock.color = colors[1];
            else if (block is TBlock)
                TBlock.color = colors[2];
            else if (block is JBlock)
                JBlock.color = colors[3];
            else if (block is LBlock)
                LBlock.color = colors[4];
            else if (block is SBlock)
                SBlock.color = colors[5];
            else if (block is ZBlock)
                ZBlock.color = colors[6];
        }

        public static void setColors()
        {
            OBlock.color = colors[0];
            IBlock.color = colors[1];
            TBlock.color = colors[2];
            JBlock.color = colors[3];
            LBlock.color = colors[4];
            SBlock.color = colors[5];
            ZBlock.color = colors[6];
        }

        public static void resetColors()
        {
            OBlock.resetColor();
            IBlock.resetColor();
            TBlock.resetColor();
            JBlock.resetColor();
            LBlock.resetColor();
            SBlock.resetColor();
            ZBlock.resetColor();
        }
    }
}
