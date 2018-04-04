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
using Reverb.Components.Titles;
using Reverb.Transitions;
using Reverb.Components.Background;
using Reverb.Components.Switches;
using Reverb.Elements;
using QuatrixHD.Blocks;
using Reverb.Components.Options;
using Reverb.Arguments;
using QuatrixHD.Storage;
using Reverb.Enumerations;
using Reverb.Components.Graphics;
using Reverb.Components.Previews;
using Reverb.Screens;

namespace QuatrixHD.Menus
{
    /// <summary>
    /// Creates a menu to select the brick texture of the blocks.
    /// </summary>
    class BrickTextures : MenuType
    {
        public BrickTextures()
            : base("Brick Textures")
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
            TitleComponent title = new TitleComponent("Fonts/MaturaTitle", "Textures", new Vector2(136, 50), Color.Red);

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
            PreviewListComponent previewList = new PreviewListComponent();

            PreviewType preview = new PreviewType("Game/Bricks/Cat-Eye Preview", new Vector2(60, 120), Color.Yellow, "Menus/Brick Highlighter", "Menus/Brick Selector", new Vector2(55, 115));
            preview.Selected += SetCatEye;
            previewList.addPreview(preview);

            preview = new PreviewType("Game/Bricks/Dragon-Eye Preview", new Vector2(155, 120), Color.Yellow, "Menus/Brick Highlighter", "Menus/Brick Selector", new Vector2(150, 115));
            preview.Selected += SetDragonEye;
            previewList.addPreview(preview);

            preview = new PreviewType("Game/Bricks/Round Preview", new Vector2(60, 205), Color.Yellow, "Menus/Brick Highlighter", "Menus/Brick Selector", new Vector2(55, 200));
            preview.Selected += SetRoundEye;
            previewList.addPreview(preview);

            preview = new PreviewType("Game/Bricks/Glass Preview", new Vector2(155, 205), Color.Yellow, "Menus/Brick Highlighter", "Menus/Brick Selector", new Vector2(150, 200));
            preview.Selected += SetGlassEye;
            previewList.addPreview(preview);

            previewList.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));

            addComponent(previewList);

            OptionsComponent options = new OptionsComponent("Fonts/MaturaOptions");

            OptionType option = new OptionType(new OptionType("Save", new Vector2(136, 390), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 390)));
            option.Selected += SaveTexture;
            options.addOption(option);

            option = new OptionType(new OptionType("Cancel", new Vector2(136, 420), Color.Red, OptionAction.previous, true, true, "Menus/Highlighter", new Vector2(137, 420)));
            option.Selected += DiscardTexture;
            options.addOption(option);

            options.setAlignment(TextAlignment.center);
            options.setTransitions(new MoveCollection(new Vector2(-200, 0), 15, true), new MoveCollection(new Vector2(200, 0), 15, false));
            addComponent(options);
        }

        private void SetCatEye(object sender, EventArgs e)
        {
            BlockType.textureAsset = "Game/Bricks/Cat-Eye Brick";
        }

        private void SetDragonEye(object sender, EventArgs e)
        {
            BlockType.textureAsset = "Game/Bricks/Dragon-Eye Brick";
        }

        private void SetRoundEye(object sender, EventArgs e)
        {
            BlockType.textureAsset = "Game/Bricks/Round Brick";
        }

        private void SetGlassEye(object sender, EventArgs e)
        {
            BlockType.textureAsset = "Game/Bricks/Glass Brick";
        }

        private void SaveTexture(OptionArgs args)
        {
            TextureData.saveTexture();
        }

        private void DiscardTexture(OptionArgs args)
        {
            TextureData.setTexture();
        }
    }
}
