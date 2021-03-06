﻿using BasicRPGTest_Mono.Engine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPGTest_Mono.Engine.Items
{
    public class ParentTool : ParentItem
    {
        public Dictionary<DamageType, double> damageTypes;
        public ParentTool(string displayName, Texture2D texture, Rectangle hitbox, double damage, SwingStyle style = SwingStyle.Slash, float swingDist = 0.785f) : base(displayName, texture)
        {
            if (hitbox == null) this.hitbox = new Rectangle(0, 0, 24, 24);
            else this.hitbox = hitbox;

            this.swingDist = swingDist;
            this.swingStyle = style;

            this.damage = damage;
            damageTypes = new Dictionary<DamageType, double>();
        }

    }


    public enum DamageType
    {
        Slashing,
        Piercing,
        Bashing,
        Mining,
        Chopping,
        Digging
    }
}
