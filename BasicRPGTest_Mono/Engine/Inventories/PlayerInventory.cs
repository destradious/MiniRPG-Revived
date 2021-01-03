﻿using BasicRPGTest_Mono.Engine.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPGTest_Mono.Engine.Inventories
{
    public class PlayerInventory : Inventory
    {
        public Item mainhand;
        public Item offhand;
        public Hotbar hotbarPrimary;
        public Hotbar hotbarSecondary;

        public PlayerInventory()
        {
            hotbarPrimary = new Hotbar();
            hotbarSecondary = new Hotbar();

            maxItems = 80;
            for (int i = 0; i < maxItems; i++)
            {
                items.TryAdd(i, null);
            }
        }

    }
}