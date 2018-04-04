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
using Reverb.Transitions;
using Reverb.Components.InGame;
using Reverb.Components.Background;
using Reverb.Elements;
using Reverb.Enumerations;
using Reverb.Components.Options;
using Reverb.Components.Text;
using Reverb.Components;
using QuatrixHD.Quatrix;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu for the game over state.
    /// </summary>
    class GameOver : MenuType
    {
        public GameOver()
            : base("Game Over")
        {
        }

        public override void initialize()
        {
            setBackground();

            setOptions();

            setText();

            base.initialize();
        }

        public override void dynamicInitialize()
        {
            ICollectionComponent list = components["Text List"] as ICollectionComponent;

            List<string> newText = new List<string>();

            newText.Add(GameManager.data.gameOverMessage);
            newText.Add("Score:");
            newText.Add(GameManager.data.score.ToString());
            newText.Add("Level:");
            newText.Add(GameManager.data.levelReached.ToString());
            newText.Add("Removed:");
            newText.Add(GameManager.data.rowsDeleted.ToString() + " Rows");
            newText.Add("Time:");

            if (GameManager.data.time.Seconds > 9)
                newText.Add(GameManager.data.time.Minutes.ToString() + ":" + GameManager.data.time.Seconds.ToString());
            else
                newText.Add(GameManager.data.time.Minutes.ToString() + ":0" + GameManager.data.time.Seconds.ToString());

            list.Texts = newText;
        }

        private void setBackground()
        {
            InGameComponent inGameBackground = new InGameComponent();

            addComponent(inGameBackground);


            BackgroundComponent background = new BackgroundComponent("Menus/Game Over");

            background.Position = new Vector2(36, 50);

            background.setTransitions(new ScaleComponent(new FadeComponent(15, 1), new Vector2(100, 180), 0.75f, 15, true), new MoveComponent(new Vector2(-200, 0), true, 15),
                                      new ScaleComponent(new FadeComponent(15, -1), new Vector2(100, 180), -0.75f, 15, false), new MoveComponent(new Vector2(200, 0), false, 15));

            addComponent(background);

        }

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            options.addOption(new OptionType("Restart", new Vector2(136, 240), Color.Red, OptionAction.startGame, false, true, "Menus/Highlighter", new Vector2(136, 240)));
            options.addOption(new OptionType("Quit", new Vector2(136, 270), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(136, 270)));

            options.setAlignment(TextAlignment.center);

            options.setTransitions(new FadeCollection(15, 1), new MoveCollection(new Vector2(-200, 0), 15, true),
                                   new FadeCollection(15, -1), new MoveCollection(new Vector2(200, 0), 15, false));
            addComponent(options);
        }

        private void setText()
        {
            TextListComponent textList = new TextListComponent("Fonts/MaturaOptions");

            textList.addText("", new Vector2(80, 65), Color.Red);
            textList.addText("Score:", new Vector2(60, 130), Color.Red);
            textList.addText("", new Vector2(140, 130), Color.Red);
            textList.addText("Level:", new Vector2(60, 150), Color.Red);
            textList.addText("", new Vector2(140, 150), Color.Red);
            textList.addText("Removed:", new Vector2(60, 170), Color.Red);
            textList.addText(" Rows", new Vector2(150, 170), Color.Red);
            textList.addText("Time:", new Vector2(60, 190), Color.Red);
            textList.addText("", new Vector2(140, 190), Color.Red);

            textList.setTransitions(new FadeCollection(15, 1), new MoveCollection(new Vector2(-200, 0), 15, true),
                                    new FadeCollection(15, -1), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(textList);
        }
    }
}
