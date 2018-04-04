using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameMenus
{
    public static class AssetManager
    {
        public static List<string> FontAssets;
        public static List<string> TextureAssets;

        public static Dictionary<string, SpriteFont> Fonts;
        public static Dictionary<string, Texture2D> Textures;

        public static string FontFile = "Fonts";

        static AssetManager()
        {
            FontAssets = new List<string>();
            TextureAssets = new List<string>();

            Fonts = new Dictionary<string,SpriteFont>();
            Textures = new Dictionary<string,Texture2D>();
        }

        public static void LoadAssets(ContentManager content)
        {
            foreach (string fontAsset in FontAssets)
            {
                SpriteFont font = content.Load<SpriteFont>(FontFile + "/" + fontAsset);

                Fonts.Add(fontAsset, font);
            }

            foreach (string textureAsset in TextureAssets)
            {
                Texture2D texture = content.Load<Texture2D>(textureAsset);

                Textures.Add(textureAsset, texture);
            }
        }

        public static void addFontAsset(string fileName)
        {
            FontAssets.Add(fileName);
        }

        public static void addTextureAsset(string fileName)
        {
            TextureAssets.Add(fileName);
        }

        public static SpriteFont getFont(string fileName)
        {
            try 
            {
                return Fonts[fileName];
            }
            catch
            {
                return null;
            }
        }

        public static Texture2D getTexture(string fileName)
        {
            try
            {
                return Textures[fileName];
            }
            catch
            {
                return null;
            }
        }
    }
}
