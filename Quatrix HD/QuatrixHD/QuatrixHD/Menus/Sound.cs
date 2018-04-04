#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of QuatrixHD.
 * 
 * QuatrixHD is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * QuatrixHD is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with QuatrixHD.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb;
using Reverb.Components.Titles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Components.Graphics;
using Reverb.Components.Background;
using Reverb.Elements;
using Reverb.Enumerations;
using Reverb.Components.Switches;
using Mach;
using Reverb.Components.Slides;
using Reverb.Components.Options;
using Reverb.Components.Text;
using Reverb.Transitions;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu to access and manipulate the sound settings.
    /// </summary>
    class Sound : MenuType
    {
        public Sound()
            : base("Sound")
        {
            Queue = false;
        }

        public override void initialize()
        {
            setBackground();

            setFrame();

            setTitle();

            setOptions();

            base.initialize();
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("Fonts/MaturaTitle", "Sound", new Vector2(136, 50), Color.Red);

            title.setAlignment(TextAlignment.center);

            title.setTransitions(new MoveComponent(null, new Vector2(-200, 0), true, 15), new MoveComponent(null, new Vector2(200, 0), false, 15));


            addComponent(title);
        }

        private void setBackground()
        {
            BackgroundComponent background = new BackgroundComponent("Menus/Standard Menu");

            background.setTransitions(new FadeComponent(15, 1), new FadeComponent(15, -1));

            addComponent(background);
        }

        private void setFrame()
        {
            GraphicComponent frame = new GraphicComponent("Menus/Option Frame", new Rectangle(39, 105, 196, 360));

            frame.setTransitions(new FadeComponent(15, 1), new FadeComponent(15, -1));

            addComponent(frame);
        }

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            options.addOption(new OptionType("Back", new Vector2(136, 420), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 420)));
            options.setAlignment(TextAlignment.center);
            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            addComponent(options);

            SwitchComponent selections = new SwitchComponent("Fonts/MaturaOptions");

            selections.addSelection(new SelectionType("On", new Vector2(100, 120), Color.Red, TextAlignment.center, "Menus/Small Highlighter", "Menus/Selector"));
            selections.addSelection(new SelectionType("Off", new Vector2(180, 120), Color.Red, TextAlignment.center, "Menus/Small Highlighter", "Menus/Selector"));

            selections.setEvent(SoundOn, 0);
            selections.setEvent(SoundOff, 1);

            selections.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(null, new Vector2(200, 0), 15, false));

            addComponent(selections);

            TextComponent text = new TextComponent("Fonts/MaturaOptions", "Volume:", new Vector2(50, 165), Color.Red);

            text.setTransitions(new MoveComponent(null, new Vector2(-200, 0), true, 15), new MoveComponent(null, new Vector2(200, 0), false, 15));
            addComponent(text);

            NumberSlideComponent slide = new NumberSlideComponent("Menus/Meter", "Menus/Knob", new Vector2(50, 190), new Rectangle(59, 190, 142, 25));
            slide.ChangeValue += ChangeVolume;
            slide.SetValue += SetVolume;

            slide.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(slide);
        }

        private void SoundOn(object sender, EventArgs e)
        {
            SoundManager.IsActive = true;
        }

        private void SoundOff(object sender, EventArgs e)
        {
            SoundManager.IsActive = false;
        }

        private void SetVolume(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            slide.Value = SoundManager.Volume;
        }

        private void ChangeVolume(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            SoundManager.Volume = slide.Value;
        }
    }
}
