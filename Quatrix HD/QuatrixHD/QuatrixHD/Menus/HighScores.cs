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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reverb.Transitions;
using Reverb.Components.Titles;
using Reverb.Components.Background;
using Reverb.Components.Options;
using Reverb.Elements;
using Reverb.Enumerations;
using Reverb.Arguments;
using QuatrixHD.Storage;
using Reverb.Components.Text;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu to list the stored high scores.
    /// </summary>
    class HighScores : MenuType
    {
        public HighScores()
            : base("High Scores")
        {
            Queue = false;
        }

        public override void initialize()
        {
            setBackground();

            setTitle();

            setOptions();

            setList();

            base.initialize();
        }

        public override void dynamicInitialize()
        {
            TextListComponent list = components["Text List"] as TextListComponent;

            List<HighScoreInformation> information = HighScoreData.HighScores;

            List<string> newList = new List<string>();

            newList.Add("Score:  Level:  Rows:  Time:");

            for (int i = 0; i < information.Count; i++)
            {
                int score = information[i].score;
                int level = information[i].level;
                int rows = information[i].rows;
                int seconds = information[i].time % 100;
                int minutes = information[i].time / 100;


                newList.Add(score.ToString());
                newList.Add(level.ToString());
                newList.Add(rows.ToString());

                if (seconds > 9)
                    newList.Add(minutes.ToString() + ":" + seconds.ToString());
                else
                    newList.Add(minutes.ToString() + ":0" + seconds.ToString());
            }

            list.Texts = newList;

            base.dynamicInitialize();
        }

        private void setTitle()
        {
            TitleComponent title = new TitleComponent("Fonts/MaturaTitle", "High Scores", new Vector2(136, 10), Color.Red);

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

        private void setOptions()
        {
            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            OptionType option = new OptionType("Clear List", new Vector2(136, 400), Color.Red, OptionAction.nothing, false, false, "Menus/Highlighter", new Vector2(137, 400));
            option.Selected += ClearScores;
            options.addOption(option);

            options.addOption(new OptionType("Back", new Vector2(136, 430), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 430)));

            options.setAlignment(TextAlignment.center);

            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(options);
        }

        private void setList()
        {
            List<HighScoreInformation> information = HighScoreData.HighScores;

            TextListComponent list = new TextListComponent("Fonts/MaturaOptions");

            list.addText("Score: Level: Rows: Time:", new Vector2(5, 70), Color.Red);

            for (int i = 0; i < information.Count; i++)
            {
                int score = information[i].score;
                int level = information[i].level;
                int rows = information[i].rows;
                int seconds = information[i].time % 100;
                int minutes = information[i].time - seconds;

                list.addText(score.ToString(), new Vector2(20, 100 + i * 30), Color.Red);
                list.addText(level.ToString(), new Vector2(100, 100 + i * 30), Color.Red);
                list.addText(rows.ToString(), new Vector2(160, 100 + i * 30), Color.Red);

                if (seconds > 9)
                    list.addText(minutes.ToString() + ":" + seconds.ToString(), new Vector2(210, 100 + i * 30), Color.Red);
                else
                    list.addText(minutes.ToString() + ":0" + seconds.ToString(), new Vector2(210, 100 + i * 30), Color.Red);
            }

            list.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            addComponent(list);
        }

        private void ClearScores(OptionArgs args)
        {
            HighScoreData.clearScores();

            dynamicInitialize();
        }
    }
}
