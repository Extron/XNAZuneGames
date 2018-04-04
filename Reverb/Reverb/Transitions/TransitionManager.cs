using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Screens;

namespace Reverb.Transitions
{
    public class TransitionManager
    {
        public EventHandler FinishTransition;

        Dictionary<string, MenuType> menus;
        List<string> menusToUpdateList;
        Queue<string> menusToUpdateQueue;

        public bool HasTransitions
        {
            get
            {
                bool temp = false;

                if (menusToUpdateList.Count > 0)
                    temp = true;

                if (menusToUpdateQueue.Count > 0)
                    temp = true;

                return temp;
            }
        }

        public TransitionManager()
        {
            menus = new Dictionary<string, MenuType>();
            menusToUpdateList = new List<string>();
            menusToUpdateQueue = new Queue<string>();
        }

        public void initialize()
        {
            foreach (MenuType menu in menus.Values)
                menu.initializeTransitions();
        }

        public void update()
        {
            //If there are menus whose transitions are not complete, update those transitions
            if (menusToUpdateQueue.Count > 0)
            {
                MenuType currentMenu = getMenu(menusToUpdateQueue.Peek());

                currentMenu.updateTransitions();

                //If the menu has completed those transitions, reset the transitions and remove the menu from the list
                if (currentMenu.CompletedTransitions)
                {
                    currentMenu.TransitionState = TransitionState.none;

                    currentMenu.reset();

                    menusToUpdateQueue.Dequeue();

                    if (FinishTransition != null)
                    {
                        //Run all events that have been set to run once the transition completes
                        FinishTransition(this, new EventArgs());

                        //Clear all of the events
                        FinishTransition = null;
                    }
                }
            }

            for (int i = 0; i < menusToUpdateList.Count; i++)
            {
                MenuType currentMenu = getMenu(menusToUpdateList[i]);

                currentMenu.updateTransitions();

                if (currentMenu.CompletedTransitions)
                {
                    currentMenu.TransitionState = TransitionState.none;

                    currentMenu.reset();

                    menusToUpdateList.Remove(menusToUpdateList[i]);

                    if (FinishTransition != null)
                    {
                        //Run all events that have been set to run once the transition completes
                        FinishTransition(this, new EventArgs());

                        //Clear all of the events
                        FinishTransition = null;
                    }
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (menusToUpdateQueue.Count > 0)
            {
                getMenu(menusToUpdateQueue.Peek()).draw(spriteBatch);
            }

            if (menusToUpdateList.Count > 0)
            {
                foreach (string menu in menusToUpdateList)
                {
                    getMenu(menu).draw(spriteBatch);
                }
            }
        }

        public void addMenu(string menuName, MenuType menu)
        {
            menus.Add(menuName, menu);
        }

        public MenuType getMenu(string menuName)
        {
            try
            {
                return menus[menuName];
            }
            catch
            {
                return null;
            }
        }

        public void runTransition(TransitionState transition, string menu)
        {
            MenuType menuItem = getMenu(menu);

            //If there is a menu listed in the transition manager's list, add that menu to the update list
            if (menuItem != null)
            {
                if (menuItem.hasTransition(transition))
                {
                    menuItem.setTransitions(transition);

                    if (menuItem.Queue)
                        menusToUpdateQueue.Enqueue(menu);
                    else
                        menusToUpdateList.Add(menu);

                    menuItem.TransitionState = transition;
                }
            }
        }
    }
}
