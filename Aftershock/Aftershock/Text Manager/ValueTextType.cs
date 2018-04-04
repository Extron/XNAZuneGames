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
using Aftershock.Delegates;

namespace Aftershock.Text
{
    /// <summary>
    /// Allows for text for values other than string values, as well as dynamically changing values, including string values.
    /// </summary>
    /// <typeparam name="Type">The type of value to be displayed.</typeparam>
    public class ValueTextType<Type> : TextType
    {
        public Change<Type> ChangeValue;

        Type value;

        public ValueTextType(Type initialValue, string fontAsset, Vector2 textVector, Color textColor, bool isVisible)
            : base(fontAsset, initialValue.ToString(), textVector, textColor, isVisible)
        {
            value = initialValue;
        }

        public override void update(GameTime gameTime)
        {
            ChangeValue(this, ref value);

            text = value.ToString();

            base.update(gameTime);
        }
    }
}
