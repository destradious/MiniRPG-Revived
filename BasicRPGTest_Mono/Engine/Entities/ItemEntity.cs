﻿using BasicRPGTest_Mono.Engine.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRPGTest_Mono.Engine.Entities
{
    public class ItemEntity : Entity, IDisposable
    {
        public Map map;

        private Rectangle box = new Rectangle(0, 0, 24, 24);
        private Item item;
        public ItemEntity(Map map, Item item, Vector2 pos) : base(item.graphic, new Rectangle((int)pos.X, (int)pos.Y, item.graphic.width, item.graphic.height))
        {
            this.item = item;
            Position = new Vector2((int)pos.X, (int)pos.Y);

            List<ItemEntity> nearbyItems = getNearbyItems(50);
            foreach (ItemEntity nearbyItem in nearbyItems)
            {
                item.quantity += nearbyItem.item.quantity;
                nearbyItem.remove();
                // Add code for increasing the quantity of this new item here.
            }

            Core.items.Add(Position, this);
        }

        public List<ItemEntity> getNearbyItems(int pixelRadius)
        {
            List<ItemEntity> items = new List<ItemEntity>();

            Vector2 startingPos = new Vector2(Position.X - pixelRadius, Position.Y - pixelRadius);
            Vector2 endingPos = new Vector2(Position.X + pixelRadius, Position.Y + pixelRadius);

            Vector2 targetPos = new Vector2();
            for (int x = (int)startingPos.X; x < endingPos.X; x++)
            {
                for (int y = (int)startingPos.Y; y < endingPos.Y; y++)
                {
                    targetPos.X = x;
                    targetPos.Y = y;
                    if (Core.items.ContainsKey(targetPos))
                    {
                        items.Add(Core.items[targetPos]);
                        System.Diagnostics.Debug.WriteLine($"Found item in merge radius!");
                    }
                }

            }

            return items;
        }

        public void pickUp(Player player)
        {
            int slot = player.inventory.getFirstEmpty();
            if (slot == -1) return;

            player.inventory.addItem(item);

            remove();

        }

        public void remove()
        {
            Core.items.Remove(Position);
            Dispose();
        }

        public override void draw(SpriteBatch batch)
        {
            if (!Camera.camera.BoundingRectangle.Intersects(boundingBox)) return;
            float scale = (float)box.Width / item.graphic.texture.Width;
            graphic.draw(batch, Position, Color.White, 0f, Vector2.Zero, scale);
            batch.DrawString(Core.itemFont, $"{item.quantity}", new Vector2(Position.X + 12, Position.Y + 12), Color.Black);
        }

        public void Dispose()
        {
        }
    }
}
