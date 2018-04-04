using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMenus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class GameOver : MenuType
    {
        public GameOver()
            : base("Game Over")
        {
        }

        public override void initialize()
        {
            setInGame();

            setBackground();

            setTitle();

            setOptions();

            base.initialize();
        }

        private void setInGame()
        {
            InGameComponent inGame = new InGameComponent();
            addComponent(inGame);
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("Game Over", "Lindsey", Color.Yellow, new Vector2(120, 100), TextAlignment.center);

            TransitionWrapper transition;

            transition = new BaseTransition(15, TransitionState.intro);
            transition = new FloatTitle(transition, new Vector2(15, 0));
            transition = new FadeTitle(transition);
            transition.setTransition(title);
            title.Intro = transition;

            transition = new BaseTransition(15, TransitionState.selected);
            transition = new FloatTitle(transition, new Vector2(-15, 0));
            transition = new FadeTitle(transition);
            title.Select = transition;

            transition.State = TransitionState.exit;
            title.Exit = transition;

            addComponent(title);
        }

        private void setBackground()
        {
            BackgroundComponent background = new BackgroundComponent("Menus/In Game Menu", new Vector2(0, 100), Color.White);

            TransitionWrapper transition;

            transition = new BaseTransition(15, TransitionState.intro);
            transition = new FloatBackground(transition, new Vector2(-15, 0));
            transition = new FadeBackground(transition);
            transition.setTransition(background);
            background.Intro = transition;

            transition = new BaseTransition(15, TransitionState.selected);
            transition = new FloatBackground(transition, new Vector2(15, 0));
            transition = new FadeBackground(transition);
            background.Select = transition;

            transition.State = TransitionState.exit;
            background.Exit = transition;

            addComponent(background);
        }

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent(7);

            OptionType option = new OptionType("Restart", "LindseySmall", Color.Red, new Vector2(120, 135), OptionAction.startGame, true, new Rectangle(120, 135, 120, 25), "Menus/Center Highlight");
            option.ActivateTransition = false;
            option.Alignment = TextAlignment.center;
            options.add(option);

            option = new OptionType("Quit", "LindseySmall", Color.Red, new Vector2(120, 160), OptionAction.endGame, true, new Rectangle(120, 160, 120, 25), "Menus/Center Highlight");
            option.Alignment = TextAlignment.center;
            options.add(option);

            TransitionWrapper transition = new BaseTransition(15, TransitionState.intro);
            transition = new FloatOptions(transition, new Vector2(15, 0));
            transition = new FadeOptions(transition);
            transition.setTransition(options);
            options.Intro = transition;

            transition = new BaseTransition(15, TransitionState.selected);
            transition = new FloatOptions(transition, new Vector2(15, 0));
            transition = new FadeOptions(transition);
            options.Select = transition;

            transition.State = TransitionState.exit;
            options.Exit = transition;

            addComponent(options);
        }
    }
}
