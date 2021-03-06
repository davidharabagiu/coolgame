﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame
{
    class Steve : Enemy
    {
        public Steve(ContentManager Content) :base(Content) {
            SetTexture("enemy2");
            Width = 58;
            Height = 80;
            EnableAnimation = true;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (Direction == EnemyDirection.ToLeft)
                X -= 0.1f * deltaTime;
            else if (Direction == EnemyDirection.ToRight)
                X += 0.1f * deltaTime;
        }
        
    }
}
