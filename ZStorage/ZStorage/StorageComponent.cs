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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;

namespace ZStorage
{
    /// <summary>
    /// Manages saving and loading data from by way of serialization.
    /// </summary>
    public class StorageComponent
    {
        StorageDevice device;
        StorageContainer container;
        string path;
        string gameFileName;

        public StorageComponent(string fileName)
        {
            device.BeginOpenContainer(fileName, GetStorageDevice, new object());
        }

        public bool SaveData<Data>(DataType<Data> data, string fileName)
        {
            bool successful = false;

            Stream stream = container.OpenFile(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataType<Data>));

                serializer.Serialize(stream, data);

                successful = true;
            }
            catch
            {
                successful = false;
            }
            finally
            {
                stream.Close();
            }

            return successful;
        }

        public DataType<Data> LoadData<Data>(string fileName)
        {
            DataType<Data> data = new DataType<Data>();

            path = Path.Combine(gameFileName, fileName);

            Stream stream = container.OpenFile(path, FileMode.OpenOrCreate, FileAccess.Read);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataType<Data>));

                data = (DataType<Data>)serializer.Deserialize(stream);
            }
            catch
            {
                data = null;
            }
            finally
            {
                stream.Close();
            }

            return data;
        }

        void GetStorageDevice(IAsyncResult result)
        {
            device.EndOpenContainer(result);
        }
    }
}
