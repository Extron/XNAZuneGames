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
using Microsoft.Xna.Framework.Content;
using QuatrixHD.Grid;
using Microsoft.Xna.Framework;

namespace QuatrixHD.Blocks
{
    /// <summary>
    /// Creates a standard block class, composed of 4 bricks, as well as data on what configuration those bricks
    /// are arranged in.
    /// </summary>
    class BlockType
    {
        public static string textureAsset;

        protected static Texture2D texture;

        protected RotationState rotation = RotationState.first;

        protected SpawnData spawn;
        protected SpawnData previewSpawn;

        protected RotationData rotation1Data;
        protected RotationData rotation2Data;
        protected RotationData rotation3Data;
        protected RotationData rotation4Data;

        protected BrickType brick1;
        protected BrickType brick2;
        protected BrickType brick3;
        protected BrickType brick4;

        protected RotationState spawnRotation;
        protected int numberOfRotations;

        #region Properties
        public BrickType Brick1
        {
            get { return brick1; }
            set { brick1 = value; }
        }

        public BrickType Brick2
        {
            get { return brick2; }
            set { brick2 = value; }
        }

        public BrickType Brick3
        {
            get { return brick3; }
            set { brick3 = value; }
        }

        public BrickType Brick4
        {
            get { return brick4; }
            set { brick4 = value; }
        }

        public Rectangle BoundingBox
        {
            get 
            {
                Rectangle brick1Box = new Rectangle((int)brick1.Vector.X, (int)brick1.Vector.Y, 1, 1);
                Rectangle brick2Box = new Rectangle((int)brick2.Vector.X, (int)brick2.Vector.Y, 1, 1);
                Rectangle brick3Box = new Rectangle((int)brick3.Vector.X, (int)brick3.Vector.Y, 1, 1);
                Rectangle brick4Box = new Rectangle((int)brick4.Vector.X, (int)brick4.Vector.Y, 1, 1);

                Rectangle boundingBox = Rectangle.Union(brick1Box, brick2Box);
                boundingBox = Rectangle.Union(boundingBox, brick3Box);
                boundingBox = Rectangle.Union(boundingBox, brick4Box);

                return boundingBox;
            }
        }

        public Vector2 Vector
        {
            get
            {
                Vector2 newVector;

                if (brick1.Vector.X <= brick2.Vector.X && brick1.Vector.Y <= brick2.Vector.Y)
                {
                    if (brick1.Vector.X <= brick3.Vector.X && brick1.Vector.Y <= brick3.Vector.Y)
                    {
                        if (brick1.Vector.X <= brick4.Vector.X && brick1.Vector.Y <= brick4.Vector.Y)
                            newVector = brick1.Vector;
                        else
                            newVector = brick4.Vector;
                    }
                    else
                    {
                        if (brick3.Vector.X <= brick4.Vector.X && brick3.Vector.Y <= brick4.Vector.Y)
                            newVector = brick3.Vector;
                        else
                            newVector = brick4.Vector;
                    }
                }
                else
                {
                    if (brick2.Vector.X <= brick3.Vector.X && brick2.Vector.Y <= brick3.Vector.Y)
                    {
                        if (brick2.Vector.X <= brick4.Vector.X && brick2.Vector.Y <= brick4.Vector.Y)
                            newVector = brick2.Vector;
                        else
                            newVector = brick4.Vector;
                    }
                    else
                    {
                        if (brick3.Vector.X <= brick4.Vector.X && brick3.Vector.Y <= brick4.Vector.Y)
                            newVector = brick3.Vector;
                        else
                            newVector = brick4.Vector;
                    }
                }

                return newVector;
            }
        }

        public RotationState Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public SpawnData SpawnPoint
        {
            get { return spawn; }
            set { spawn = value; }
        }
        #endregion

        #region Constructors
        public BlockType()
        {
            brick1 = new BrickType(textureAsset);

            brick2 = new BrickType(textureAsset);

            brick3 = new BrickType(textureAsset);

            brick4 = new BrickType(textureAsset);
        }
        #endregion

        #region Virtual Functions
        public virtual void load(ContentManager content)
        {
            brick1.load(content);

            brick2.load(content);

            brick3.load(content);

            brick4.load(content);
        }

        public virtual void preview(GridType grid)
        {
            previewSpawn.spawn(this);

            fillGrid(grid);
        }

        public virtual void normal(GridType grid)
        {
            spawn.spawn(this);

            rotation = spawnRotation;

            fillGrid(grid);
        }

        public virtual void rotate(GridType grid)
        {
            clearGrid(grid);

            switch (rotation)
            {
                case RotationState.first:
                    rotation1Data.rotate(this);

                    if (numberOfRotations > 1)
                        rotation = RotationState.second;
                    else
                        rotation = RotationState.first;

                    break;

                case RotationState.second:
                    rotation2Data.rotate(this);

                    if (numberOfRotations > 2)
                        rotation = RotationState.third;
                    else 
                        rotation = RotationState.first;

                    break;

                case RotationState.third:
                    rotation3Data.rotate(this);

                    if (numberOfRotations > 3)
                        rotation = RotationState.fourth;
                    else
                        rotation = RotationState.first;

                    break;

                case RotationState.fourth:
                    rotation4Data.rotate(this);
                    rotation = RotationState.first;
                    break;
            }

            fillGrid(grid);
        }

        public virtual void changeColor(Color color)
        {
            setColor(color);
        }

        public virtual Color getColor()
        {
            return Color.White;
        }
        #endregion

        #region Class Functions
        public void move(GridType grid, Direction direction)
        {
            clearGrid(grid);

            brick1.move(grid, direction);

            brick2.move(grid, direction);

            brick3.move(grid, direction);

            brick4.move(grid, direction);
        }

        public void drop(GridType grid)
        {
            clearGrid(grid);

            brick1.drop(grid);

            brick2.drop(grid);

            brick3.drop(grid);

            brick4.drop(grid);
        }

        public void fillGrid(GridType grid)
        {
            brick1.fillGrid(grid);

            brick2.fillGrid(grid);

            brick3.fillGrid(grid);

            brick4.fillGrid(grid);
        }

        public void clearGrid(GridType grid)
        {
            brick1.clearGrid(grid);

            brick2.clearGrid(grid);

            brick3.clearGrid(grid);

            brick4.clearGrid(grid);
        }

        public void unload()
        {
            brick1.unload();
            brick2.unload();
            brick3.unload();
            brick4.unload();
        }

        public void setColor(Color newColor)
        {
            brick1.Color = newColor;
            brick2.Color = newColor;
            brick3.Color = newColor;
            brick4.Color = newColor;
        }

        public void setTexture(ContentManager content, string newAsset)
        {
            textureAsset = newAsset;

            texture = content.Load<Texture2D>(textureAsset);

            brick1.Texture = texture;

            brick2.Texture = texture;

            brick3.Texture = texture;

            brick4.Texture = texture;
        }

        public RotationData getRotation(RotationState state)
        {
            RotationData returnData = new RotationData();

            switch (state)
            {
                case RotationState.first:
                    returnData = rotation1Data;
                    break;

                case RotationState.second:
                    returnData = rotation2Data;
                    break;

                case RotationState.third:
                    returnData = rotation3Data;
                    break;

                case RotationState.fourth:
                    returnData = rotation4Data;
                    break;
            }

            return returnData;
        }
        #endregion
    }
}
