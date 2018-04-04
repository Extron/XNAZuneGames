using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMenus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snakez
{
    class SelectLevel : MenuType
    {
        EventHandler<SelectLevelArgs> Select;

        public SelectLevel(EventHandler<SelectLevelArgs> method)
            : base("Select Level")
        {
            Select = method;
        }

        public override void initialize()
        {
            setBackground();

            setTitle();

            setList();

            base.initialize();
        }

        public override void dynamicInitialize()
        {
            ListLevelComponent selectLevel = components["List Select Level"] as ListLevelComponent;

            for (int i = selectLevel.levels.Count; i < LevelProgressData.CurrentLevel; i++)
            {
                OptionType option = selectLevel.levels.options[0];
                option.Text = "Level " + (i + 1).ToString();

                selectLevel.levels.add(option);
            }

            base.dynamicInitialize();
        }

        private void setBackground()
        {
            BackgroundComponent background = new BackgroundComponent("Menus/Menu Background", new Vector2(), Color.White);

            addComponent(background);
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("Select Level", "LindseyLarge", Color.Yellow, new Vector2(25, 0));

            TransitionWrapper transition;

            transition = new BaseTransition(15, TransitionState.intro);
            transition = new FloatTitle(transition, new Vector2(0, -20));
            transition = new FadeTitle(transition);
            transition.setTransition(title);
            title.Intro = transition;

            transition = new BaseTransition(15, TransitionState.selected);
            transition = new FloatTitle(transition, new Vector2(0, 20));
            transition = new FadeTitle(transition);
            title.Select = transition;

            transition.State = TransitionState.exit;
            title.Exit = transition;

            addComponent(title);
        }

        private void setList()
        {
            ScrollingOptionsComponent list = new ScrollingOptionsComponent(new Vector2(24, 60), 7, 30, 7);

            for (int i = 0; i < LevelProgressData.CurrentLevel; i++)
            {
                OptionType option = new OptionType("Level " + (i + 1).ToString(), "Lindsey", Color.Red, new Vector2(24, 60 + 30 * i), OptionAction.startGame, false, new Rectangle(24, 60 + i * 30, 120, 30), "Menus/Highlight");
                list.add(option);
            }

            ListLevelComponent level = new ListLevelComponent(list);

            level.setSelectEvent(Select);

            addComponent(level);
        }
    }
}
