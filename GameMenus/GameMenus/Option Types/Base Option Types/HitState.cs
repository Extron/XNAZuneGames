using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameMenus
{
    public class HitState : HighlightState
    {
        int counter;
        int interval;

        #region Properties
        public bool HasDrawn
        {
            get
            {
                return (counter == interval);
            }
        }
        #endregion

        #region Constructors
        public HitState(HighlightState highlightState)
            : base (highlightState.Standard)
        {
            highlight = highlightState.Highlighter;
            counter = 0;
            interval = 5;
        }

        public HitState(HighlightState highlightState, int pressedLength)
            : base(highlightState.Standard)
        {
            highlight = highlightState.Highlighter;
            counter = 0;
            interval = pressedLength;
        }

        public HitState(StandardState state)
            : base (state)
        {
            counter = 0;
            interval = 5;
        }
        #endregion

        #region Overridden Functions
        public override void update()
        {
            counter++;

            base.update();
        }
        #endregion
    }
}
