#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of Aftershock.
 * 
 * Aftershock is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * Aftershock is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with Aftershock.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aftershock.Text
{
    public struct ScreenObjectValues
    {
        Vector2 position;
        Vector2 origin;
        Color color;
        float scale;
        float rotation;

        public void setDefaults(IScreenObject screenObject)
        {
            position = screenObject.Position;
            origin = screenObject.Origin;
            color = screenObject.Color;
            scale = screenObject.Scale;
            rotation = screenObject.Rotation;
        }

        public void resetscreenObject(IScreenObject screenObject)
        {
            screenObject.Position = position;
            screenObject.Origin = origin;
            screenObject.Color = color;
            screenObject.Scale = scale;
            screenObject.Rotation = rotation;
        }
    }
}