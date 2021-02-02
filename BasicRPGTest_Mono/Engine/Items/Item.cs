﻿using BasicRPGTest_Mono.Engine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPGTest_Mono.Engine.Items
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public Graphic graphic { get; set; }
        public string description { get; set; }

        // Attack data
        public float swingDist;
        public SwingStyle swingStyle;
        public Rectangle hitbox;
        public double damage;

        // Instance variables
        public bool isInstance = false;
        public Item parent;
        public int quantity { get; set; }

        public Item(string displayName, Texture2D texture) : this(displayName, new Graphic(texture)) { }
        public Item(string displayName, Graphic graphic)
        {
            id = ItemManager.items.Count;
            name = Utility.Util.cleanString(displayName).ToLower();
            this.displayName = displayName;
            this.graphic = graphic;


            if (GetType() == typeof(Item)) swingDist = 0.785f;
            if (GetType() == typeof(Item)) swingStyle = SwingStyle.Slash;
            if (GetType() == typeof(Item)) hitbox = new Rectangle(0, 0, 24, 24);
            if (GetType() == typeof(Item)) damage = 1;
        }
        public Item(Item parent, int quantity = 1)
        {
            this.parent = parent;

            id = parent.id;
            name = parent.name;
            displayName = parent.displayName;
            graphic = parent.graphic;

            swingDist = parent.swingDist;
            swingStyle = parent.swingStyle;
            hitbox = parent.hitbox;
            damage = parent.damage;

            isInstance = true;
            this.quantity = quantity;

        }
    }
}
