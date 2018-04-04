/* Copyright (c) 2009 Extron Productions.
 *  
 * This file is part of GameMenus.
 * 
 * GameMenus is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * GameMenus is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GameMenus.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ZHandler;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameMenus
{
    public class ControlMenuType : MenuType
    {
        public List<ControlType> controls;
        public InputHandlerComponent gameInput;

        bool assignKey;

        public ControlMenuType(string current)
            : base(current)
        {
            controls = new List<ControlType>();

            assignKey = false;
        }

        public override void initialize()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].Selected += ChangeButton;
            }

            base.initialize();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].draw(spriteBatch);
            }
        }

        public override void update(InputHandlerComponent input)
        {
            if (!assignKey)
            {
                base.update(input);

                for (int i = 0; i < controls.Count; i++)
                    controls[i].update(input);
            }
            else
            {
                gameInput.OldPad = gameInput.Pad;

                gameInput.Pad = GamePad.GetState(PlayerIndex.One);

                /*
                if (gameInput.changeButton(controls[Position].buttonType))
                {
                    assignKey = false;

                    controls[Position].button = gameInput.getAssignedButton(controls[Position].buttonType);

                    controls[Position].Text = controls[Position].setText();
                }
                */ 
            }
        }

        public /*override*/ void movement(InputHandlerComponent input)
        {
            /*
            if (!assignKey)
            {
                if (input.getButton(ButtonType.down, false) && Position != options.Count - 1 && options.Count != 0 && Counter > 10)
                {
                    controls[Position].State = OptionState.standard;

                    base.movement(input);

                    if (Position < controls.Count)
                        controls[Position].State = OptionState.highlighted;

                    return;
                }
                else if (input.getButton(ButtonType.up, false) && Position != 0 && options.Count != 0 && Counter > 10)
                {
                    if (Position < controls.Count)
                        controls[Position].State = OptionState.standard;

                    base.movement(input);

                    controls[Position].State = OptionState.highlighted;
                }
            }
            */
        }

        public /*override*/ void reset()
        {
            //if (!assignKey)
                //base.reset();
        }

        public void ChangeButton(object sender, EventArgs e)
        {
            assignKey = true;

            gameInput.Pad = GamePad.GetState(PlayerIndex.One);
        }
    }
}
