using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Reverb.Transitions;

namespace Reverb.Components
{
    public abstract class ComponentType
    {
        public string identifier;

        ITransition intro;
        ITransition revert;
        ITransition select;
        ITransition exit;

        #region Properties
        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public bool HasIntro
        {
            get { return (intro != null); }
        }

        public bool HasRevert
        {
            get { return (revert != null); }
        }

        public bool HasSelect
        {
            get { return (select != null); }
        }

        public bool HasExit
        {
            get { return (exit != null); }
        }
        #endregion

        #region Abstract Functions
        public abstract void initialize();

        public abstract void load(ContentManager content);

        public abstract void update(InputHandler input);

        public abstract void draw(SpriteBatch spriteBatch);
        #endregion

        #region Class Functions
        /// <summary>
        /// If the component has any transitions, reset the values of teh component, based on the default values of that 
        /// transition.
        /// </summary>
        public virtual void reset()
        {
            if (intro != null)
                intro.reset(this as IMenuComponent);

            if (revert != null)
                revert.reset(this as IMenuComponent);

            if (select != null)
                select.reset(this as IMenuComponent);

            if (exit != null)
                exit.reset(this as IMenuComponent);
        }

        /// <summary>
        /// Initialize the transition, setting any initial values that it needs to have to run.
        /// </summary>
        /// <param name="transition">The transition that needs to be run</param>
        public void setTransition(TransitionState transition)
        {
            switch (transition)
            {
                case TransitionState.intro:
                    if (intro != null)
                        intro.setTransition(this as IMenuComponent);
                    break;

                case TransitionState.revert:
                    if (revert != null)
                        revert.setTransition(this as IMenuComponent);
                    break;

                case TransitionState.select:
                    if (select != null)
                        select.setTransition(this as IMenuComponent);
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        exit.setTransition(this as IMenuComponent);
                    break;
            }
        }

        /// <summary>
        /// Initializes all non-null transitions with default values and object initializations.
        /// </summary>
        public void initializeTransitions()
        {
            if (intro != null)
                intro.initialize(this as IMenuComponent);

            if (revert != null)
                revert.initialize(this as IMenuComponent);

            if (select != null)
                select.initialize(this as IMenuComponent);

            if (exit != null)
                exit.initialize(this as IMenuComponent);
        }

        /// <summary>
        /// Updates any transition logic of the current transition.
        /// </summary>
        /// <param name="state">The current transition state.</param>
        public void updateTransitions(TransitionState state)
        {
            switch (state)
            {
                case TransitionState.intro:
                    if (intro != null)
                        intro.update(this as IMenuComponent);
                    break;

                case TransitionState.revert:
                    if (revert != null)
                        revert.update(this as IMenuComponent);
                    break;

                case TransitionState.select:
                    if (select != null)
                        select.update(this as IMenuComponent);
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        exit.update(this as IMenuComponent);
                    break;
            }
        }

        /// <summary>
        /// Determines if the transitions are complete.
        /// </summary>
        /// <param name="state">The current transition state.</param>
        /// <returns>A value representing wether the transition has finished running.</returns>
        public bool completedTransitions(TransitionState state)
        {
            bool temp = false;

            switch (state)
            {
                case TransitionState.intro:
                    if (intro != null)
                        temp = intro.IsComplete;
                    else
                        temp = true;
                    break;

                case TransitionState.revert:
                    if (revert != null)
                        temp = revert.IsComplete;
                    else
                        temp = true;
                    break;

                case TransitionState.select:
                    if (select != null)
                        temp = select.IsComplete;
                    else
                        temp = true;
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        temp = exit.IsComplete;
                    else
                        temp = true;
                    break;

                case TransitionState.none:
                    temp = true;
                    break;
            }

            return temp;
        }

        public void setTransitions(ITransition introTransition, ITransition exitTransition)
        {
            intro = introTransition;
            revert = introTransition;
            select = exitTransition;
            exit = exitTransition;
        }

        public void setTransitions(ITransition introTransition, ITransition revertTransition, ITransition selectTransition, ITransition exitTransition)
        {
            intro = introTransition;
            revert = revertTransition;
            select = selectTransition;
            exit = exitTransition;
        }
        #endregion
    }
}
