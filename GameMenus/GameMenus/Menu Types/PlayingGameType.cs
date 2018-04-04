using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using ZHandler;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class PlayingGameType : MenuType
    {
        #region Contructors
        public PlayingGameType(string gameState)
            : base(gameState)
        {
        }

        public PlayingGameType()
            : base("Playing Game")
        {
        }
        #endregion

        #region Overridden Functions
        public override void initialize()
        {
        }

        public override void update(InputHandlerComponent input)
        {
        }

        public override void draw(SpriteBatch spriteBatch)
        {
        }
        #endregion
    }
}
