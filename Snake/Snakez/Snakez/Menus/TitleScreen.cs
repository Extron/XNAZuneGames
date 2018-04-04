using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMenus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class TitleScreen : MenuType
    {
        public TitleScreen()
            : base("Title Screen")
        {
        }

        public override void initialize()
        {
            setBackground();

            setTitle();

            setOptions();

            base.initialize();
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("SnakeZ", "LindseyLarge", Color.Yellow, new Vector2(20, 25));

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
            BackgroundComponent background = new BackgroundComponent("Menus/Title Screen", new Vector2(), Color.White);

            addComponent(background);
        }

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent(7);

            OptionType option = new OptionType("Start Game", "Lindsey", Color.Red, new Vector2(24, 100), OptionAction.startGame, true, new Rectangle(24, 100, 120, 30), "Menus/Highlight");
            option.ActivateTransition = false;
            options.add(option);

            option = new OptionType("Select Level", "Lindsey", Color.Red, new Vector2(24, 130), "Select Level", new Rectangle(24, 130, 120, 30), "Menus/Highlight");
            options.add(option);

            option = new OptionType("Options", "Lindsey", Color.Red, new Vector2(24, 160), "Options", new Rectangle(24, 160, 120, 30), "Menus/Highlight");
            options.add(option);

            option = new OptionType("Exit", "Lindsey", Color.Red, new Vector2(24, 190), OptionAction.exit, false, new Rectangle(24, 190, 120, 30), "Menus/Highlight");
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
    }
}
