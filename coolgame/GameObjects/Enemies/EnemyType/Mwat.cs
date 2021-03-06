﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace coolgame
{
    public class Mwat : RangedEnemy
    {
        public Mwat(ContentManager Content) : base(Content)
        {
            SetTexture("mwat");
            Width = 79;
            Height = 91;
            EnableAnimation = true;
            AnimationSpeed = 0.03f;
            spriteFacing = EnemyDirection.ToRight;

            healthBar.MaxHealth = 1000;
            movingSpeed = 11f;
            attackSpeed = 1.5f;
            attackPower = 150;

            spaceCash = 40;
            


            burstFire = true;
            burstFireAmmount = 3;
            burstFireSpeed = 10f;
            precision = ((float)GameManager.RNG.NextDouble() * 10f + 10f);
            Range = GameManager.RNG.Next(50, 150);

            attackSound = "enemylaser";
            hitSound = "mwathit";
            laserAssetName = "mwatlaser";
        }

        protected override void MosCraciun(bool idle)
        {
            if (idle)
            {
                AnimationSpeed = 0;
                EnableAnimation = false;
                SetTexture("mwat_idle");
                //Width = 67;
                //Height = 92;
                Y = GameManager.Ground.Top - Height;
            }
            else
            {
                AnimationSpeed = 0.03f;
                EnableAnimation = true;
                SetTexture("mwat");
                Width = 79;
                Height = 91;
                Y = GameManager.Ground.Top - Height;
            }
        }
    }
}
