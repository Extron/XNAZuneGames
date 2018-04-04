using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZHandler;
using Microsoft.Xna.Framework.Graphics;

namespace GameMenus
{
    public class ColorChangeComponent : IComponent, IOptions
    {
        public const string identifier = "Color Changer";
        
        TransitionWrapper intro;
        TransitionWrapper select;
        TransitionWrapper exit;

        PreviewComponent preview;
        List<SlideType> slides;
        OptionType option;
        Color color;

        int index;
        int counter;
        int interval;

        #region Properties
        public Color NewColor
        {
            get { return color; }
        }
        #endregion

        #region Implemented Properties
        public int Index
        {
            get { return index; }
        }

        public int Count
        {
            get { return 1; }
        }

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

        #region Contructors
        public ColorChangeComponent(PreviewComponent previewComponent, OptionType saveOption, int scrollSpeed)
        {
            preview = previewComponent;
            slides = new List<SlideType>();
            option = saveOption;
            interval = scrollSpeed;
        }
        #endregion

        #region IComponent Implemented Functions
        public void initialize()
        {
            color = Color.White;

            preview.initialize();

            slides[0].State = OptionState.highlighted;
        }

        public void load()
        {
            foreach (SlideType slide in slides)
                slide.load();

            preview.load();

            option.load();
        }

        public void update(InputHandlerComponent i)
        {
            movement(i);

            foreach (SlideType slide in slides)
            {
                slide.update(i);
            }

            calculateColor();

            preview.changeColor(color);

            preview.update(i);

            option.update(i);

            counter++;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (SlideType slide in slides)
            {
                slide.draw(spriteBatch);
            }

            preview.draw(spriteBatch);

            option.draw(spriteBatch);
        }

        public void reset()
        {
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

        #region IOptions Implemented Functions
        public void movement(InputHandlerComponent i)
        {
            if (i.getButton("Down", false) && index < slides.Count - 1 && counter > interval)
            {
                slides[index].State = OptionState.standard;
                index++;
                slides[index].State = OptionState.highlighted;
                counter = 0;
                return;
            }
            else if (i.getButton("Down", false) && (index == slides.Count - 1) && (counter > interval))
            {
                slides[index].State = OptionState.standard;
                index++;
                option.State = OptionState.highlighted;
                counter = 0;
                return;
            }
            else if (i.getButton("Up", false) && (index == slides.Count) && (counter > interval))
            {
                option.State = OptionState.standard;
                index--;
                slides[index].State = OptionState.highlighted;
                counter = 0;
                return;
            }
            else if (i.getButton("Up", false) && index > 0 && counter > interval)
            {
                slides[index].State = OptionState.standard;
                index--;
                slides[index].State = OptionState.highlighted;
                counter = 0;
                return;
            }
        }

        public void add(OptionType option)
        {
        }

        public void setEvent(Select method, int index)
        {
        }

        public void setEvents(Select method)
        {
            option.Selected += method;
        }
        #endregion

        #region Class Functions
        public void calculateColor()
        {
            color.R = (byte)slides[0].Number;
            color.G = (byte)slides[1].Number;
            color.B = (byte)slides[2].Number;
        }

        public void setSave(Select save)
        {
            option.Selected += save;
        }

        public void addSlider(SlideType slide)
        {
            if (slides.Count < 3)
                slides.Add(slide);
        }
        #endregion
    }
}
