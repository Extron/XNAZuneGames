using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Reflex;

namespace Reverb.Screens
{
    public class SplashScreen
    {
#if WINDOWS
        VideoPlayer player;
        Video splash;
        string asset;
#endif

#if ZUNE
        Texture2D splash;
        Color color;
        string asset;
        int interval;
        int counter;
        int direction;
#endif

        public bool IsComplete
        {
#if WINDOWS
            get { return player.State == MediaState.Stopped; }
#endif

#if ZUNE
            get { return (counter == interval) && (color.A == 0); }
#endif
        }

        public SplashScreen(string videoAsset)
        {
#if WINDOWS
            asset = videoAsset;
#endif
        }

        public SplashScreen(string textureAsset, int pauseInterval)
        {
#if ZUNE
            asset = textureAsset;
            interval = pauseInterval;
#endif
        }

        public void initialize()
        {
#if WINDOWS
            player = new VideoPlayer();
#endif

#if ZUNE
            color = new Color(255, 255, 255, 0);
            direction = 1;
#endif
        }

        public void load(ContentManager content)
        {
#if WINDOWS
            splash = content.Load<Video>(asset);
#endif

#if ZUNE
            splash = content.Load<Texture2D>(asset);
#endif
        }

        public void update(InputHandler input, bool isLoaded)
        {
            if (isLoaded && input.pressActivated(new Rectangle(0, 0, 272, 480), true))
            {
#if WINDOWS
                stop();
#endif

#if ZUNE
                counter = interval;
                color.A = 0;
#endif
            }

#if ZUNE
            if (color.A != 255)
                color.A += (byte)((255 / 51) * direction);
            else if (color.A == 255)
                counter++;

            if (counter == interval)
            {
                direction = -1;
                color.A += (byte)((255 / 51) * direction);
            }
#endif
        }

        public void draw(SpriteBatch spriteBatch)
        {
#if WINDOWS
            Texture2D currentFrame = player.GetTexture();

            spriteBatch.Draw(currentFrame, new Vector2(-180, 0), Color.White);
#endif

#if ZUNE
            spriteBatch.Draw(splash, new Vector2(), Color.White);
#endif
        }

        public void start()
        {
#if WINDOWS
            player.Play(splash);
#endif
        }

        public void stop()
        {
#if WINDOWS
            player.Stop();
#endif
        }
    }
}
