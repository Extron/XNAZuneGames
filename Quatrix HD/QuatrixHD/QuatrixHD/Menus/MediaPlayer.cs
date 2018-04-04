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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shockwave.Components.Player;
using Microsoft.Xna.Framework.Media;
using Reverb;
using Reverb.Components.Background;
using Reverb.Transitions;
using Reverb.Components.Graphics;
using Reverb.Components.Text;
using Reverb.Components.Slides;
using Reverb.Components.Options;
using Reverb.Elements;
using Reverb.Enumerations;
using Reverb.Screens;
using Reverb.Elements.Buttons;


namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a media player interface.
    /// </summary>
    class Player : MenuType
    {
        public Player()
            : base("Media Player")
        {
            Queue = false;
        }

        public override void initialize()
        {
            setBackground();

            setPlayer();

            base.initialize();
        }

        private void setBackground()
        {
            BackgroundComponent background = new BackgroundComponent("Menus/Standard Menu");

            background.setTransitions(new FadeComponent(15, 1), new FadeComponent(15, -1));

            addComponent(background);

            GraphicComponent bar = new GraphicComponent("Media Player/Media Buttons Frame", new Rectangle(52, 240, 160, 35));

            bar.setTransitions(new MoveComponent(new Vector2(-200, 0), true, 15), new MoveComponent(new Vector2(200, 0), false, 15));
            addComponent(bar);
        }

        private void setPlayer()
        {
            MediaPlayerComponent player = new MediaPlayerComponent("Media Player/No Art", "Fonts/MaturaOptions", new Rectangle(52, 60, 160, 160), Color.White);

            player.setText(new Vector2(52, 310), new Vector2(52, 340), new Vector2(52, 280), Color.Red);
            player.setPlayPause(new StateButtonType("Media Player/Play", "Media Player/Pause", new Vector2(102, 242)));
            player.setStop(new ButtonType("Media Player/Stop", "Media Player/Stop Pressed", new Vector2(135, 242)));
            player.setShuffle(new StateButtonType("Media Player/Shuffle Button", "Media Player/Shuffle Pressed", new Vector2(18, 243)));
            player.setRepeat(new StateButtonType("Media Player/Repeat Button", "Media Player/Repeat Pressed", new Vector2(220, 241)));
            player.setNext(new ButtonType("Media Player/Next", "Media Player/Next Pressed", new Vector2(172, 242)));
            player.setPrevious(new ButtonType("Media Player/Previous", "Media Player/Previous Pressed", new Vector2(60, 242)));

            player.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            addComponent(player);

            TextComponent text = new TextComponent("Fonts/MaturaOptions", "Volume:", new Vector2(52, 370), Color.Red);
            text.setTransitions(new MoveComponent(new Vector2(-200, 0), true, 15), new MoveComponent(new Vector2(200, 0), false, 15));
            addComponent(text);

            NumberSlideComponent slide = new NumberSlideComponent("Menus/Meter", "Menus/Knob", new Vector2(50, 400), new Rectangle(59, 400, 142, 25));
            slide.ChangeValue += ChangeVolume;
            slide.SetValue += SetVolume;

            slide.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(slide);

            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");
            options.addOption(new OptionType("Back", new Vector2(136, 430), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 430)));
            options.setAlignment(TextAlignment.center);
            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            addComponent(options);
        }

        private void SetVolume(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            slide.Value = MediaPlayer.Volume;
        }

        private void ChangeVolume(object sender, EventArgs e)
        {
            NumberSlideComponent slide = sender as NumberSlideComponent;

            MediaPlayer.Volume = slide.Value;
        }
    }
}
