using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ZHUD
{
    public class InGameMessage
    {
        SpriteFont font;
        Vector2 vector;
        Color color;
        string text;
        string asset;
        bool display;

        public InGameMessage(string message, string fontAsset, Vector2 textVector, Color textColor)
        {
            text = message;
            asset = fontAsset;
            vector = textVector;
            color = textColor;
            font = null;
            display = true;
        }

        public void load(ContentManager content)
        {
            font = content.Load<SpriteFont>(asset);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (display)
                spriteBatch.DrawString(font, text, vector, color);
        }

        public void displayMessage(bool state)
        {
            display = state;
        }
    }

    public class MessageType
    {
        Dictionary<string, InGameMessage> messages;

        public MessageType()
        {
            messages = new Dictionary<string, InGameMessage>();
        }

        public void load(ContentManager content)
        {
            foreach (InGameMessage message in messages.Values)
                message.load(content);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (InGameMessage message in messages.Values)
                message.draw(spriteBatch);
        }

        public void addMessage(InGameMessage message, string identifier)
        {
            messages.Add(identifier, message);
        }

        public void displayMessage(string identifier, bool state)
        {
            try
            {
                messages[identifier].displayMessage(state);
            }
            catch
            {
                return;
            }
        } 
    }
}