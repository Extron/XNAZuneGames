using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMenus;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Snakez
{
    class SnakeColor : MenuType
    {
        public SnakeColor()
            : base("Snake Color")
        {
        }

        public override void initialize()
        {
            setBackground();

            setTitle();

            setSlides();
            
            base.initialize();
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("Color", "LindseyLarge", Color.Yellow, new Vector2(25, 0));

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

        private void setSlides()
        {
            PreviewComponent preview = new PreviewComponent("Previews/Snake Preview", new Vector2(180, 50), Color.White);
            OptionType option = new OptionType("Save", "Lindsey", Color.Red, new Vector2(24, 200), OptionAction.previous, true, new Rectangle(24, 200, 120, 30), "Menus/Highlight");

            ColorChangeComponent colorChanger = new ColorChangeComponent(preview, option, 7);

            SliderType slider = new SliderType("Sliders/Slider", new Vector2(90, 110), "Sliders/Slide Nub", new Vector2(90, 110), Color.White);
            HighlightType highlighter = new HighlightType(new Vector2(24, 100), "Menus/Highlight", Color.White);
            colorChanger.addSlider(new SlideType(new StringType("Red", new Vector2(24, 100), Color.Red), highlighter, slider, "Lindsey", 255));
 
            slider = new SliderType("Sliders/Slider", new Vector2(90, 140), "Sliders/Slide Nub", new Vector2(90, 140), Color.White);
            highlighter = new HighlightType(new Vector2(24, 130), "Menus/Highlight", Color.White);
            colorChanger.addSlider(new SlideType(new StringType("Green", new Vector2(24, 130), Color.Red), highlighter, slider, "Lindsey", 255));

            slider = new SliderType("Sliders/Slider", new Vector2(90, 170), "Sliders/Slide Nub", new Vector2(90, 170), Color.White);
            highlighter = new HighlightType(new Vector2(24, 160), "Menus/Highlight", Color.White);
            colorChanger.addSlider(new SlideType(new StringType("Blue", new Vector2(24, 160), Color.Red), highlighter, slider, "Lindsey", 255));

            colorChanger.setSave(SaveColor);

            addComponent(colorChanger);
        }

        private void SaveColor(object sender, EventArgs e)
        {
            ColorChangeComponent component = components["Color Changer"] as ColorChangeComponent;

            if (component != null)
                SnakeType.color = component.NewColor;
        }
    }
}
