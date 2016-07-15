﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace coolgame
{
    public class HealthBar
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private Color color;
        private float val;
        private int maxWidth;

        public int Width
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
                rectangle.Width = (int)(value * val);
            }
        }

        public int Height
        {
            get { return rectangle.Height; }
            set { rectangle.Height = value; }
        }

        public float Value
        {
            get { return val; }
            set
            {
                val = value;
                rectangle.Width = (int)(maxWidth * val);
                color = new Color(Math.Min(255, (int)(500 * (1 - val))), Math.Min(255, (int)(500 * val)), 0);
            }
        }

        public int X
        {
            get { return rectangle.X; }
            set { rectangle.X = value; }
        }

        public int Y
        {
            get { return rectangle.Y; }
            set { rectangle.Y = value; }
        }

        public HealthBar(ContentManager content)
        {
            texture = content.Load<Texture2D>("hpbar");
            rectangle = new Rectangle();
            Width = 50;
            Height = 5;
            Value = 1.0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}