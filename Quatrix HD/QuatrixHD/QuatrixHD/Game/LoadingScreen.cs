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
using QuatrixHD.Blocks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using QuatrixHD.Grid;
using Microsoft.Xna.Framework;

namespace QuatrixHD.Quatrix
{
    /// <summary>
    /// Manages the loading screen of the game.
    /// </summary>
    class LoadingScreen
    {
        GridType loadingGrid;
        TBlock block;
        SpriteFont font;
        string asset;
        int counter;

        public LoadingScreen(string fontAsset)
        {
            asset = fontAsset;
        }

        public void initialize()
        {
            loadingGrid = new GridType(new Vector2(100, 220), new Vector2(20), 3, 3);

            loadingGrid.initialize();

            block = new TBlock();

            SpawnData spawnPoint = new SpawnData();

            spawnPoint.brick1Vector = new Vector2(0, 1);
            spawnPoint.brick2Vector = new Vector2(1, 1);
            spawnPoint.brick3Vector = new Vector2(2, 1);
            spawnPoint.brick4Vector = new Vector2(1, 2);

            block.SpawnPoint = spawnPoint;

            block.initialize();

            block.normal(loadingGrid);
        }

        public void load(ContentManager content)
        {
            block.load(content);

            font = content.Load<SpriteFont>(asset);
        }

        public void update()
        {
            if (counter >= 10)
            {
                block.rotate(loadingGrid);
                counter = 0;
            }

            counter++;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            loadingGrid.draw(spriteBatch);

            spriteBatch.DrawString(font, "Loading...", new Vector2(80, 160), Color.Red);
        }
    }
}
