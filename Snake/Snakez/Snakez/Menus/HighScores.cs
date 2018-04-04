using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMenus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class HighScores : MenuType
    {
        public HighScores()
            : base("High Scores")
        {
        }

        public override void initialize()
        {
            setBackground();

            setTitle();

            setOptions();

            setDisplayList();

            base.initialize();
        }

        public override void load()
        {
            loadDisplayList();

            base.load();
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("High Scores", "LindseyLarge", Color.Yellow, new Vector2(25, 0));

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

        private void setBackground()
        {
            BackgroundComponent background = new BackgroundComponent("Menus/Menu Background", new Vector2(), Color.White);

            addComponent(background);
        }

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent(7);

            OptionType option = new OptionType("Clear Scores", "Lindsey", Color.Red, new Vector2(24, 60), OptionAction.previous, false, new Rectangle(24, 60, 120, 30), "Menus/Highlight");
            option.ActivateSelected = false;
            options.add(option);

            option = new OptionType("Back", "Lindsey", Color.Red, new Vector2(24, 90), OptionAction.previous, true, new Rectangle(24, 90, 120, 30), "Menus/Highlight");
            option.ActivateSelected = true;
            options.add(option);

            TransitionWrapper transition = new BaseTransition(15, TransitionState.intro);
            transition = new FloatOptions(transition, new Vector2(-20, 0));
            transition = new FadeOptions(transition);
            transition.setTransition(options);
            options.Intro = transition;

            transition = new BaseTransition(15, TransitionState.selected);
            transition = new FloatOptions(transition, new Vector2(20, 0));
            transition = new FadeOptions(transition);
            options.Select = transition;

            transition.State = TransitionState.exit;
            options.Exit = transition;

            addComponent(options);
        }

        private void setDisplayList()
        {
            DisplayListComponent list = new DisplayListComponent("Lindsey", Color.Blue);

            list.Direction = SortDirection.down;

            addComponent(list);
        }

        public void loadDisplayList()
        {
            DisplayListComponent list;

            try
            {
                list = components["Display List"] as DisplayListComponent;
            }
            catch
            {
                return;
            }

            list.Direction = SortDirection.down;

            list.setText<int>(HighScoreData.List);


            list.setVectors(165, 50, 20);

        }
    }
}
