using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace GameMenus
{
    public class SideToSideSystemType : SystemType
    {
        public SideToSideSystemType(string state)
            : base (state)
        {
        }

        public override void add(MenuType menu)
        {
            SideToSideType temp = menu as SideToSideType;

            if (temp != null)
            {
                temp.Scroll += Scroll;
                base.add(temp);
            }
            else
                base.add(menu);
        }

        public void Scroll(object sender, DirectionArgs args)
        {
            SideToSideType menu = sender as SideToSideType;

            if (args.direction == Direction.left)
            {
                current = menu.Left;
            }
            else if (args.direction == Direction.right)
            {
                current = menu.Right;
            }
        }
    }
}
