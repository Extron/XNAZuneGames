﻿#region Legal Statements
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
using QuatrixHD.Blocks;

namespace QuatrixHD.Storage
{
    /// <summary>
    /// Manages the storage of the texture data.
    /// </summary>
    static class TextureData
    {
        static DataType<string> data;

        static string texture;

        public static void load(StorageComponent storage)
        {
            data = storage.LoadData<string>("texture.lst");

            if (data == null)
            {
                data = new DataType<string>(1);

                data.fillList("Game/Bricks/Cat-Eye Brick");

                storage.SaveData<string>(data, "texture.lst");
            }

            texture = data.list[0];
        }

        public static void save(StorageComponent storage)
        {
            data.list[0] = texture;

            storage.SaveData<string>(data, "texture.lst");
        }

        public static void saveTexture()
        {
            texture = BlockType.textureAsset;
        }

        public static void setTexture()
        {
            BlockType.textureAsset = texture;
        }
    }
}
