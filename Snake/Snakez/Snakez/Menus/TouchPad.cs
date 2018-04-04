using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMenus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class TouchPad : SmallMenuType
    {
        public TouchPad(MenuType menu)
            : base(menu, "Touch Pad")
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
            TitleComponent title = new TitleComponent("Touch Pad", "Lindsey", Color.Yellow, new Vector2(120, 210), TextAlignment.center);

            TransitionWrapper transition;

            transition = new BaseTransition(15, TransitionState.intro);
            transition = new FloatTitle(transition, new Vector2(0, 15));
            transition = new FadeTitle(transition);
            transition.setTransition(title);
            title.Intro = transition;

            transition = new BaseTransition(15, TransitionState.selected);
            transition = new FloatTitle(transition, new Vector2(0, -15));
            transition = new FadeTitle(transition);
            title.Select = transition;

            transition.State = TransitionState.exit;
            title.Exit = transition;

            addComponent(title);
        }

        private void setBackground()
        {
            BackgroundComponent background = new BackgroundComponent("Menus/Small Menu", new Vector2(25, 210), Color.White);

            TransitionWrapper transition;

            transition = new BaseTransition(15, TransitionState.intro);
            transition = new FloatBackground(transition, new Vector2(0, 15));
            transition = new FadeBackground(transition);
            transition.setTransition(background);
            background.Intro = transition;

            transition = new BaseTransition(15, TransitionState.selected);
            transition = new FloatBackground(transition, new Vector2(0, -15));
            transition = new FadeBackground(transition);
            background.Select = transition;

            transition.State = TransitionState.exit;
            background.Exit = transition;

            addComponent(background);
        }

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent(7);

            OptionType option = new OptionType("On", "Lindsey", Color.Red, new Vector2(120, 240), OptionAction.previous, true, new Rectangle(120, 240, 120, 30), "Menus/Center Highlight");
            option.Alignment = TextAlignment.center;
            option.ActivateTransition = false;
            options.add(option);

            option = new OptionType("Off", "Lindsey", Color.Red, new Vector2(120, 270), OptionAction.previous, true, new Rectangle(120, 270, 120, 30), "Menus/Center Highlight");
            option.Alignment = TextAlignment.center;
            option.ActivateTransition = false;
            options.add(option);

            TransitionWrapper transition = new BaseTransition(15, TransitionState.intro);
            transition = new FloatOptions(transition, new Vector2(0, 15));
            transition = new FadeOptions(transition);
            transition.setTransition(options);
            options.Intro = transition;

            transition = new BaseTransition(15, TransitionState.selected);
            transition = new FloatOptions(transition, new Vector2(0, 15));
            transition = new FadeOptions(transition);
            options.Select = transition;

            transition.State = TransitionState.exit;
            options.Exit = transition;

            addComponent(options);
        }
    }
}
