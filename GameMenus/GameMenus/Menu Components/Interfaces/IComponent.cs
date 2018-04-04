using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ZHandler;

namespace GameMenus
{
    /// <summary>
    /// This component is the standard interface for any items that would be put into a game menu screen.  It provides 
    /// functions for initialize, load, update, and draw, as well as transitions.
    /// </summary>
    public interface IComponent
    {
        #region Properties
        /// <summary>
        /// Contains the data for the intro transition
        /// </summary>
        TransitionWrapper Intro { set; }

        /// <summary>
        /// Contains the data for the select transition
        /// </summary>
        TransitionWrapper Select { set; }

        /// <summary>
        /// Contains the data for the exit transition
        /// </summary>
        TransitionWrapper Exit { set; }

        /// <summary>
        /// Contains the unique constant string used to individually identify the components
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Determines if the component has an intro transition initialized
        /// </summary>
        bool HasIntro { get; }

        /// <summary>
        /// Determines if the component has a select transition initialized
        /// </summary>
        bool HasSelect { get; }

        /// <summary>
        /// Determines if the component has an exit transition initialized
        /// </summary>
        bool HasExit { get; }
        #endregion

        #region Functions
        void initialize();

        void load();

        void update(InputHandlerComponent input);

        void draw(SpriteBatch spriteBatch);

        void reset();

        void updateTransitions(TransitionState state);

        void drawTransitions(SpriteBatch spriteBatch, TransitionState state);

        bool completedTransitions(TransitionState state);
        #endregion 
    }
}
