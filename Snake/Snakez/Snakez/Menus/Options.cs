using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMenus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class Options : MenuType
    {
        public Options()
            : base("Options")
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
            TitleComponent title = new TitleComponent("Options", "LindseyLarge", Color.Yellow, new Vector2(25, 0));

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

            OptionType option = new OptionType("Touch Pad", "Lindsey", Color.Red, new Vector2(24, 60), "Touch Pad", new Rectangle(24, 60, 120, 30), "Menus/Highlight");
            option.ActivateSelected = false;
            options.add(option);

            option = new OptionType("Sound", "Lindsey", Color.Red, new Vector2(24, 90), "Sound", new Rectangle(24, 90, 120, 30), "Menus/Highlight");
            option.ActivateSelected = false;
            options.add(option);

            option = new OptionType("Snake Options", "Lindsey", Color.Red, new Vector2(24, 120), "Snake Options", new Rectangle(24, 120, 120, 30), "Menus/Highlight");
            options.add(option);

            option = new OptionType("High Scores", "Lindsey", Color.Red, new Vector2(24, 150), "High Scores", new Rectangle(24, 150, 120, 30), "Menus/Highlight");
            options.add(option);

            option = new OptionType("How to Play", "Lindsey", Color.Red, new Vector2(24, 180), "How to Play", new Rectangle(24, 180, 120, 30), "Menus/Highlight");
            options.add(option);

            option = new OptionType("About", "Lindsey", Color.Red, new Vector2(24, 210), "About", new Rectangle(24, 210, 120, 30), "Menus/Highlight");
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

