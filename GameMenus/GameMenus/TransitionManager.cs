using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class TransitionManager
    {        
        public EventHandler<EventArgs> FinishTransition;

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

        public void update()
        {
            if (menusToUpdateQueue.Count > 0)
            {
                MenuType currentMenu = getMenu(menusToUpdateQueue.Peek());

                currentMenu.updateTransitions();

                if (currentMenu.CompletedTransitions)
                {
                    switch (currentMenu.TransitionState)
                    {
                        case TransitionState.intro:
                            currentMenu.TransitionState = TransitionState.menu;
                            break;
                            
                        case TransitionState.selected:
                            currentMenu.TransitionState = TransitionState.final;
                            break;

                        case TransitionState.exit:
                            currentMenu.TransitionState = TransitionState.final;
                            break;
                    }

                    menusToUpdateQueue.Dequeue();

                    if (FinishTransition != null)
                        FinishTransition(this, new EventArgs());
                }
            }

            for (int i = 0; i < menusToUpdateList.Count; i++)
            {
                getMenu(menusToUpdateList[i]).updateTransitions();

                if (getMenu(menusToUpdateList[i]).CompletedTransitions)
                {
                    if (FinishTransition != null)
                        FinishTransition(this, new EventArgs());

                    switch (getMenu(menusToUpdateList[i]).TransitionState)
                    {
                        case TransitionState.intro:
                            getMenu(menusToUpdateList[i]).TransitionState = TransitionState.menu;
                            break;

                        case TransitionState.selected:
                            getMenu(menusToUpdateList[i]).TransitionState = TransitionState.final;
                            break;

                        case TransitionState.exit:
                            getMenu(menusToUpdateList[i]).TransitionState = TransitionState.final;
                            break;
                    }

                    menusToUpdateList.Remove(menusToUpdateList[i]);
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (menusToUpdateQueue.Count > 0)
            {
                MenuType currentMenu = getMenu(menusToUpdateQueue.Peek());

                currentMenu.drawTransitions(spriteBatch);
            }

            foreach (string menu in menusToUpdateList)
            {
                getMenu(menu).drawTransitions(spriteBatch);
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

        public void runTransition(TransitionState transition, string menu, bool queueable)
        {
            if (getMenu(menu) != null)
            {
                switch (transition)
                {
                    case TransitionState.intro:
                        if (getMenu(menu).HasIntro)
                        {
                            if (queueable)
                                menusToUpdateQueue.Enqueue(menu);
                            else
                                menusToUpdateList.Add(menu);

                            getMenu(menu).TransitionState = transition;
                        }
                        break;

                    case TransitionState.selected:
                        if (getMenu(menu).HasSelected)
                        {
                            if (queueable)
                                menusToUpdateQueue.Enqueue(menu);
                            else
                                menusToUpdateList.Add(menu);

                            getMenu(menu).TransitionState = transition;
                        }
                        break;

                    case TransitionState.exit:
                        if (getMenu(menu).HasExit)
                        {
                            if (queueable)
                                menusToUpdateQueue.Enqueue(menu);
                            else
                                menusToUpdateList.Add(menu);

                            getMenu(menu).TransitionState = transition;
                        }
                        break;
                }
            }
        }
    }
}
