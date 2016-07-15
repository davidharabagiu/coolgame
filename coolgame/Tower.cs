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
    public class Tower : Entity
    {
        private LaserGun laserGun;

        public Tower(ContentManager content, int groundLevel) : base(content)
        {
            SetTexture(content, "tower");
            Width = texture.Width;
            Height = texture.Height;
            X = Game.GAME_WIDTH / 2 - Width / 2;
            Y = groundLevel - Height;
            EnableHealthBar = true;
            laserGun = new LaserGun(content, X + 10, Y + 10);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            laserGun.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            laserGun.Draw(spriteBatch);
        }
    }
}