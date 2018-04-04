using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Aftershock.Text;
using Microsoft.Xna.Framework;
using Aftershock.Effects;

namespace QuatrixHD.GameTypes
{
    class Infinity : GameType
    {
        public Infinity()
            : base(new GameData(ScoreValue.score, TimeSpan.Zero, "Next Level",  0, 10, 45, 5))
        {
        }

        protected override void initializeHUD(GraphicsDevice graphics)
        {
            hud.addText(new TextType("Fonts/Lindsey", "Level:", new Vector2(110, 410), Color.Red, true));
            hud.addText(new TextType("Fonts/Lindsey", "Score:", new Vector2(110, 430), Color.Red, true));
            hud.addText(new TextType("Fonts/Lindsey", "Bonus: x", new Vector2(190, 410), Color.Red, true));

            ValueTextType<int> dynamicText = new ValueTextType<int>(1, "Fonts/Lindsey", new Vector2(160, 410), Color.Red, true);
            dynamicText.ChangeValue += UpdateLevel;
            hud.addText(dynamicText);

            dynamicText = new ValueTextType<int>(0, "Fonts/Lindsey", new Vector2(160, 430), Color.Red, true);
            dynamicText.ChangeValue += UpdateScore;
            dynamicText.addEffect(new CountingEffect(null, 0.5f));
            hud.addText(dynamicText);

            dynamicText = new ValueTextType<int>(1, "Fonts/Lindsey", new Vector2(250, 410), Color.Red, true);
            dynamicText.ChangeValue += UpdateBonus;
            hud.addText(dynamicText);

            base.initializeHUD(graphics);
        }
    }
}
