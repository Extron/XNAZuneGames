using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZHandler;

namespace GameMenus
{
    public class PreviewComponent : IComponent, IPreview
    {
        public const string identifier = "Preview";

        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        Texture2D texture;
        Vector2 vector;
        Color color;
        string asset;

        #region Properties
        public Color PreviewColor
        {
            get { return color; }
            set { color = value; }
        }
        #endregion

        #region Implemented Properties
        public TransitionWrapper Intro
        {
            set { intro = value; }
        }

        public TransitionWrapper Select
        {
            set { select = value; }
        }

        public TransitionWrapper Exit
        {
            set { exit = value; }
        }

        public bool HasIntro
        {
            get
            {
                return (intro != null);
            }
        }

        public bool HasSelect
        {
            get
            {
                return (select != null);
            }
        }

        public bool HasExit
        {
            get
            {
                return (exit != null);
            }
        }

        public string Identifier
        {
            get { return identifier; }
        }
        #endregion

        #region Constructors
        public PreviewComponent(string textureAsset, Vector2 textureVector, Color textureColor)
        {
            asset = textureAsset;
            vector = textureVector;
            color = textureColor;
        }
        #endregion

        #region IComponent Implemented Functions
        public void initialize()
        {          
        }

        public void load()
        {
            texture = AssetManager.getTexture(asset);
        }

        public void update(InputHandlerComponent i)
        {
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, color);
        }

        public void reset()
        {
            color = Color.White;
        }

        public void updateTransitions(TransitionState state)
        {
            switch (state)
            {
                case TransitionState.intro:
                    if (intro != null)
                        intro.update(this);
                    break;

                case TransitionState.selected:
                    if (select != null)
                        select.update(this);
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        exit.update(this);
                    break;
            }
        }

        public void drawTransitions(SpriteBatch spriteBatch, TransitionState state)
        {
            switch (state)
            {
                case TransitionState.intro:
                    if (intro != null)
                        intro.draw(spriteBatch, this);
                    else
                        draw(spriteBatch);
                    break;

                case TransitionState.selected:
                    if (select != null)
                        select.draw(spriteBatch, this);
                    else
                        draw(spriteBatch);
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        exit.draw(spriteBatch, this);
                    else
                        draw(spriteBatch);
                    break;
            }
        }

        public bool completedTransitions(TransitionState state)
        {
            bool temp = false;

            switch (state)
            {
                case TransitionState.intro:
                    if (intro != null)
                        temp = intro.isComplete();
                    else
                        temp = true;
                    break;

                case TransitionState.selected:
                    if (select != null)
                        temp = select.isComplete();
                    else
                        temp = true;
                    break;

                case TransitionState.exit:
                    if (exit != null)
                        temp = exit.isComplete();
                    else
                        temp = true;
                    break;
            }

            return temp;
        }
        #endregion

        #region IPreview Implemented Functions
        public void changeColor(Color newColor)
        {
            color = newColor;
        }

        public void changeTexture(Texture2D newTexture)
        {
            texture = newTexture;
        }

        public void changeTexture(string newAsset)
        {
            asset = newAsset;

            texture = AssetManager.getTexture(asset);
        }

        public void changeVector(Vector2 newVector)
        {
            vector = newVector;
        }

        public void changeVector(int x, int y)
        {
            vector = new Vector2(x, y);
        }
        #endregion
    }
}
