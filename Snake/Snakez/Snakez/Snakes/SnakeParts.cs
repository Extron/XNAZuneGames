/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of Snakez.
 * 
 * Snakez is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Snakez is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Snakez.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class SnakePart
    {
        protected Vector2 vector;
        protected OrientationType orientation;

        Texture2D texture;
        string asset;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        public OrientationType Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public SnakePart(Texture2D partTexture)
        {
            texture = partTexture;
        }

        public SnakePart(string textureAsset)
        {
            asset = textureAsset;
        }

        public virtual void initialize(SpawnData spawnPoint)
        {
            orientation = spawnPoint.orientation;
        }

        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>(asset);
        }

        public  virtual void fillGrid(GameGrid grid, Color color)
        {
            float rotation = MovementFunctions.calculateRotation(orientation);

            grid.GetCell(vector).fillCell(CellContent.snake, texture, rotation, color);
        }

        public void deepCopy(SnakePart part)
        {
            texture = part.texture;
            vector = part.vector;
            orientation = part.orientation;
        }
    }
    //Each snake part has its own class, containing variables and functions
    class HeadType : SnakePart
    {
        public HeadType()
            : base(@"Snake\Snake 1")
        {
        }

        public HeadType(string textureAsset)
            : base(textureAsset)
        {
        }

        public override void initialize(SpawnData spawnPoint)
        {
            vector = spawnPoint.coordinate;

            base.initialize(spawnPoint);
        }
    }

    class BodyType : SnakePart
    {
        //This number signifies the number the segment is at in the snake.  It is used to calculate the initial position
        int segmentNumber;

        public int SegmentNumber
        {
            set { segmentNumber = value; }
        }

        public BodyType(int partPosition)
            : base(@"Snake\Snake Section 1")
        {
            segmentNumber = partPosition;
        }

        public BodyType(string textureAsset, int partPosition)
            : base(textureAsset)
        {
            segmentNumber = partPosition;
        }

        public override void initialize(SpawnData spawnPoint)
        {
            int xDisplacement = 0;
            int yDisplacement = 0;

            switch (spawnPoint.orientation)
            {
                case OrientationType.up:
                    yDisplacement = segmentNumber - 1;
                    break;

                case OrientationType.down:
                    yDisplacement = -(segmentNumber - 1);
                    break;

                case OrientationType.left:
                    xDisplacement = segmentNumber - 1;
                    break;

                case OrientationType.right:
                    xDisplacement = -(segmentNumber - 1);
                    break;
            }

            Vector2 displacement = new Vector2(xDisplacement, yDisplacement);

            vector = spawnPoint.coordinate + displacement;

            base.initialize(spawnPoint);
        }
    }

    class TailType : SnakePart
    {
        public TailType()
            : base(@"Snake\Tail 1")
        {
        }

        public TailType(string textureAsset)
            : base(textureAsset)
        {
        }

        public override void initialize(SpawnData spawnPoint)
        {
            int xDisplacement = 0;
            int yDisplacement = 0;

            switch (spawnPoint.orientation)
            {
                case OrientationType.up:
                    yDisplacement = 5;
                    break;

                case OrientationType.down:
                    yDisplacement = -5;
                    break;

                case OrientationType.left:
                    xDisplacement = 5;
                    break;

                case OrientationType.right:
                    xDisplacement = -5;
                    break;
            }

            Vector2 displacement = new Vector2(xDisplacement, yDisplacement);

            vector = spawnPoint.coordinate + displacement;

            base.initialize(spawnPoint);
        }
    }

    class CurveType : SnakePart
    {
        float rotation;

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public CurveType()
            : base(@"Snake\Snake Curve 1")
        {
        }

        public CurveType(string textureAsset)
            : base(textureAsset)
        {
        }

        public override void initialize(SpawnData spawnPoint)
        {
            vector = new Vector2();

            base.initialize(spawnPoint);
        }

        public override void fillGrid(GameGrid grid, Color color)
        {
            grid.GetCell(vector).fillCell(CellContent.snake, Texture, rotation, color);
        }
    }
}
