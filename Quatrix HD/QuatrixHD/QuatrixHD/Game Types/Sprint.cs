using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Aftershock.Text;
using Microsoft.Xna.Framework;
using Aftershock.Effects;
using Aftershock.Graphics;

namespace QuatrixHD.GameTypes
{
    class Sprint : GameType
    {
        public Sprint()
            : base(new GameData(ScoreValue.time, TimeSpan.Zero, 30, 10, 30, 5))
        {
        }

        protected override void initializeHUD(GraphicsDevice graphics)
        {
            hud.addText(new TextType("Fonts/Lindsey", "Rows:", new Vector2(110, 410), Color.Red, true));
            hud.addText(new TextType("Fonts/Lindsey", "Score:", new Vector2(110, 430), Color.Red, true));
            hud.addText(new TextType("Fonts/Lindsey", "Bonus: x", new Vector2(190, 410), Color.Red, true));
            hud.addText(new TextType("Fonts/Lindsey", "Time:", new Vector2(110, 450), Color.Red, true));

            TextContainer container = new TextContainer("Fonts/LindseyLarge");

            container.addEffect(new ScaleEffect(null, new Vector2(60, 140), 0.05f, 0.2f));
            container.addEffect(new ScaleEffect(null, new Vector2(60, 140), -0.05f, 0.15f));
            container.addEffect(new PauseEffect(null, 1));
            container.addEffect(new FadeEffect(null, 0.5f, -1));

            hud.addContainer(container, "Next Level");

            container = new TextContainer("Fonts/LindseyMedium");

            container.addEffect(new ScaleEffect(null, new Vector2(90, 180), 0.05f, 0.2f));
            container.addEffect(new ScaleEffect(null, new Vector2(90, 180), -0.05f, 0.15f));
            container.addEffect(new PauseEffect(null, 1));
            container.addEffect(new FadeEffect(null, 0.5f, -1));

            hud.addContainer(container, "Bonus");

            container = new TextContainer("Fonts/LindseyMedium");

            container.addEffect(new FadeEffect(null, 0.5f, 1));
            container.addEffect(new PauseEffect(null, 2));
            container.addEffect(new FadeEffect(null, 0.5f, -1));

            hud.addContainer(container, "Bonus Expired");

            hud.addContainer(new TextContainer("Fonts/LindseyMedium", new FadeEffect(null, 0.40f, -1)), "Draw Score");

            hud.addContainer(new GraphicsContainer(graphics, 200, 20, Color.White, new FadeEffect(null, 0.40f, -1)), "Draw Flash");

            ValueTextType<int> dynamicText = new ValueTextType<int>(gameType.RowsToAdvance, "Fonts/Lindsey", new Vector2(160, 410), Color.Red, true);
            dynamicText.ChangeValue += UpdateRowsToDelete;
            hud.addText(dynamicText);

            dynamicText = new ValueTextType<int>(0, "Fonts/Lindsey", new Vector2(160, 430), Color.Red, true);
            dynamicText.ChangeValue += UpdateScore;
            dynamicText.addEffect(new CountingEffect(null, 0.5f));
            hud.addText(dynamicText);

            dynamicText = new ValueTextType<int>(1, "Fonts/Lindsey", new Vector2(250, 410), Color.Red, true);
            dynamicText.ChangeValue += UpdateBonus;
            hud.addText(dynamicText);

            ValueTextType<string> dynamicTime = new ValueTextType<string>("0:00", "Fonts/Lindsey", new Vector2(160, 450), Color.Red, true);
            dynamicTime.ChangeValue += UpdateTime;
            hud.addText(dynamicTime);
        }
    }
}
