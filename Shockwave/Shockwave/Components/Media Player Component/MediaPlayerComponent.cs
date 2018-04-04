#region Legal Statements
/* Copyright (c) 2010 Extron Productions.
 * 
 * This file is part of Shockwave.
 * 
 * Shockwave is free software: you can redistribute it and/or modify
 * it under the terms of the New BSD License as vetted by
 * the Open Source Initiative.
 * 
 * Shockwave is distributed in the hope that it will be useful,
 * but WITHOUT WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * New BSD License for more details.
 * 
 * You should have received a copy of the New BSD License
 * along with Shockwave.  If not, see <http://www.opensource.org/licenses/bsd-license.php>.
 * */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reverb;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Reflex;
using Microsoft.Xna.Framework.Media;
using Reverb.Components;
using Reverb.Elements.Buttons;

namespace Shockwave.Components.Player
{
    public class MediaPlayerComponent : ComponentType, ICollectionComponent
    {
        Texture2D albumArt;
        Texture2D noArt;
        Rectangle boundingBox;
        Vector2 origin;
        Color color;
        float rotation;
        string textureAsset;

        StateButtonType playPause;
        ButtonType stop;
        ButtonType next;
        ButtonType previous;
        StateButtonType shuffle;
        StateButtonType repeat;

        SpriteFont font;
        Color textColor;
        Vector2 artistVector;
        Vector2 albumVector;
        Vector2 songVector;
        string artist;
        string album;
        string song;
        string fontAsset;

        #region Properties
        public Vector2 Position { get; set; }

        public Vector2 Origin { get; set; }

        public Color Color { get; set; }

        public Rectangle BoundingBox { get; set; }

        public string Text { get; set; }

        public float Scale { get; set; }

        public List<Vector2> Positions
        {
            get
            {
                List<Vector2> positions = new List<Vector2>();

                
                positions.Add(new Vector2(boundingBox.Location.X, boundingBox.Y));
                positions.Add(playPause.Position);
                positions.Add(stop.Position);
                positions.Add(next.Position);
                positions.Add(previous.Position);
                positions.Add(shuffle.Position);
                positions.Add(repeat.Position);
                positions.Add(artistVector);
                positions.Add(albumVector);
                positions.Add(songVector);

                return positions;
            }

            set
            {
                boundingBox.Location = new Point((int)value[0].X, (int)value[0].Y);
                playPause.Position = value[1];
                stop.Position = value[2];
                next.Position = value[3];
                previous.Position = value[4];
                shuffle.Position = value[5];
                repeat.Position = value[6];
                artistVector = value[7];
                albumVector = value[8];
                songVector = value[9];
            }
        }

        public List<Vector2> Origins
        {
            get
            {
                List<Vector2> origins = new List<Vector2>();

                origins.Add(origin);
                origins.Add(playPause.Origin);
                origins.Add(stop.Origin);
                origins.Add(next.Origin);
                origins.Add(previous.Origin);
                origins.Add(shuffle.Origin);
                origins.Add(repeat.Origin);

                return origins;
            }

            set
            {
                origin = value[0];
                playPause.Origin = value[1];
                stop.Origin = value[2];
                next.Origin = value[3];
                previous.Origin = value[4];
                shuffle.Origin = value[5];
                repeat.Origin = value[6];
            }
        }

        public List<Color> Colors
        {
            get
            {
                List<Color> colors = new List<Color>();

                colors.Add(color);
                colors.Add(playPause.Color);
                colors.Add(stop.Color);
                colors.Add(next.Color);
                colors.Add(previous.Color);
                colors.Add(shuffle.Color);
                colors.Add(repeat.Color);
                colors.Add(textColor);

                return colors;
            }

            set
            {
                color = value[0];
                playPause.Color = value[1];
                stop.Color = value[2];
                next.Color = value[3];
                previous.Color = value[4];
                shuffle.Color = value[5];
                repeat.Color = value[6];
                textColor = value[7];
            }
        }

        public List<Rectangle> BoundingBoxes
        {
            get
            {
                List<Rectangle> boundingBoxes = new List<Rectangle>();

                boundingBoxes.Add(boundingBox);

                return boundingBoxes;
            }

            set 
            {
                boundingBox = value[0];
            }
        }

        public List<string> Texts
        {
            get
            {
                List<string> text = new List<string>();

                text.Add(artist);
                text.Add(album);
                text.Add(song);

                return text;
            }

            set
            {
                artist = value[0];
                album = value[1];
                song = value[3];
            }
        }

        public List<float> Scales
        {
            get
            {
                List<float> scales = new List<float>();

                scales.Add(playPause.Scale);
                scales.Add(stop.Scale);
                scales.Add(next.Scale);
                scales.Add(previous.Scale);
                scales.Add(shuffle.Scale);
                scales.Add(repeat.Scale);


                return scales;
            }

            set
            {
                playPause.Scale = value[0];
                stop.Scale = value[1];
                next.Scale = value[2];
                previous.Scale = value[3];
                shuffle.Scale = value[4];
                repeat.Scale = value[5];
            }
        }
        #endregion

        public MediaPlayerComponent(string noArtTextureAsset, string infoTextFontAsset, Rectangle artBox, Color artColor)
        {
            identifier = "Media Player";

            textureAsset = noArtTextureAsset;
            boundingBox = artBox;
            color = artColor;
            fontAsset = infoTextFontAsset;

            origin = new Vector2();
            rotation = 0;
        }

        public override void initialize()
        {
            playPause.initialize();
            stop.initialize();
            next.initialize();
            previous.initialize();
            shuffle.initialize();
            repeat.initialize();

            artist = "<Artist>";
            album = "<Album>";
            song = "<Song>";
        }

        public override void load(ContentManager content)
        {
            noArt = content.Load<Texture2D>(textureAsset);
            font = content.Load<SpriteFont>(fontAsset);

            playPause.load(content);
            stop.load(content);
            next.load(content);
            previous.load(content);
            shuffle.load(content);
            repeat.load(content);
        }

        public override void update(InputHandler input)
        {
            playPause.update(input);
            stop.update(input);
            next.update(input);
            previous.update(input);
            shuffle.update(input);
            repeat.update(input);

            if (MediaComponent.playing)
            {
                MediaComponent.getInfo(out artist, out album, out song);
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (albumArt == null)
                spriteBatch.Draw(noArt, boundingBox, null, color, rotation, origin, SpriteEffects.None, 0.5f);
            else
                spriteBatch.Draw(noArt, boundingBox, null, color, rotation, origin, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, artist, artistVector, textColor);
            spriteBatch.DrawString(font, album, albumVector, textColor);
            spriteBatch.DrawString(font, song, songVector, textColor);

            playPause.draw(spriteBatch);
            stop.draw(spriteBatch);
            next.draw(spriteBatch);
            previous.draw(spriteBatch);
            shuffle.draw(spriteBatch);
            repeat.draw(spriteBatch);
        }

        public void setText(Vector2 artistTextVector, Vector2 albumTextVector, Vector2 songTextVector, Color infoTextColor)
        {
            artistVector = artistTextVector;
            albumVector = albumTextVector;
            songVector = songTextVector;
            textColor = infoTextColor;
        }

        public void setPlayPause(StateButtonType button)
        {
            playPause = button;

            playPause.Pressed += Play;

            playPause.Released += Pause;
        }

        public void setStop(ButtonType button)
        {
            stop = button;

            stop.Pressed += Stop;
        }

        public void setPrevious(ButtonType button)
        {
            previous = button;

            previous.Pressed += Next;
        }

        public void setNext(ButtonType button)
        {
            next = button;

            next.Pressed += Next;
        }

        public void setShuffle(StateButtonType button)
        {
            shuffle = button;

            shuffle.Pressed += ShuffleOn;

            shuffle.Released += ShuffleOff;
        }

        public void setRepeat(StateButtonType button)
        {
            repeat = button;

            repeat.Pressed += RepeatOn;

            repeat.Released += RepeatOff;
        }

        public void Play(object sender, EventArgs e)
        {
            MediaComponent.start();

            MediaComponent.getInfo(out artist, out album, out song);

            albumArt = MediaComponent.getArt();
        }

        public void Pause(object sender, EventArgs e)
        {
            MediaComponent.pause();
        }

        public void Stop(object sender, EventArgs e)
        {
            playPause.revert();

            artist = "<Artist>";
            album = "<Album>";
            song = "<Song>";

            MediaComponent.stop();

            albumArt = null;
        }

        public void Next(object sender, EventArgs e)
        {
            MediaComponent.moveNextSong();

            MediaComponent.getInfo(out artist, out album, out song);

            albumArt = MediaComponent.getArt();
        }

        public void Previous(object sender, EventArgs e)
        {
            MediaComponent.movePreviousSong();

            MediaComponent.getInfo(out artist, out album, out song);

            albumArt = MediaComponent.getArt();
        }

        public void ShuffleOn(object sender, EventArgs e)
        {
            MediaPlayer.IsShuffled = true;
        }

        public void ShuffleOff(object sender, EventArgs e)
        {
            MediaPlayer.IsShuffled = false;
        }

        public void RepeatOn(object sender, EventArgs e)
        {
            MediaPlayer.IsRepeating= true;
        }

        public void RepeatOff(object sender, EventArgs e)
        {
            MediaPlayer.IsRepeating = false;
        }
    }
}