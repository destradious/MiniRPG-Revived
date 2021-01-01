﻿using BasicRPGTest_Mono.Engine.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RPGEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace BasicRPGTest_Mono.Engine.Entities
{
    public class ItemSwing
    {
        public Timer swingTimer;
        public Direction direction;
        public Player player;
        public Item item;
        public Graphic graphic;
        public Vector2 swingPos;
        public float rotation;
        public int rotationFrame;
        public Rectangle hitBox;
        public Rectangle itemBox;

        public ItemSwing(Direction direction, int swingTime, Player player, Item item)
        {
            this.player = player;
            this.direction = direction;
            this.item = item;
            graphic = item.graphic;
            itemBox = item.hitbox;
            itemBox.X = itemBox.Width / 2;
            itemBox.Y = itemBox.Height / 2;

            swingTimer = new Timer(swingTime / 7);
            swingTimer.Elapsed += Update;
            swingTimer.Start();
        }

        public void Update(Object source, ElapsedEventArgs args)
        {
            if (rotationFrame == 6)
            {
                Stop();
                return;
            }
            rotationFrame++;
            rotation += 0.25f;
        }
        public void Draw(SpriteBatch batch, Vector2 position)
        {
            swingPos = new Vector2(position.X, position.Y);

            int offset = itemBox.Height - itemBox.Y - 16;
            if (offset < 0) offset = 0;

            float angle = 0;
            Vector2 origin = new Vector2(graphic.texture.Width/4, graphic.texture.Height + (graphic.texture.Height / 4));
            int modx = 0;
            int mody = 0;

            switch (direction)
            {
                case Direction.Up:
                    angle = 0;
                    swingPos.Y -= 28;
                    modx = 4;
                    mody = 36;
                    hitBox = new Rectangle((int)swingPos.X - itemBox.X, (int)swingPos.Y - itemBox.Y - offset, itemBox.Width, itemBox.Height);
                    break;
                case Direction.Left:
                    angle = -1.58f;
                    swingPos.X -= 28;
                    modx = 36;
                    mody = -4;
                    hitBox = new Rectangle((int)swingPos.X - itemBox.Y - offset, (int)swingPos.Y - itemBox.X, itemBox.Width - (itemBox.Width - itemBox.Height), itemBox.Height + (itemBox.Width - itemBox.Height));
                    break;
                case Direction.Down:
                    angle = -3.15f;
                    swingPos.Y += 28;
                    modx = -4;
                    mody = -36;
                    hitBox = new Rectangle((int)swingPos.X - itemBox.X, (int)swingPos.Y - itemBox.Y + offset, itemBox.Width, itemBox.Height);
                    break;
                case Direction.Right:
                    angle = -4.72f;
                    swingPos.X += 28;
                    modx = -36;
                    mody = 4;
                    hitBox = new Rectangle((int)swingPos.X - itemBox.Y + offset, (int)swingPos.Y - itemBox.X, itemBox.Width - (itemBox.Width - itemBox.Height), itemBox.Height + (itemBox.Width - itemBox.Height));
                    break;
            }
            batch.Begin();
            batch.DrawRectangle(hitBox, Color.White);
            batch.End();

            angle -= rotation;

            graphic.draw(batch, new Vector2(swingPos.X + modx, swingPos.Y + mody), angle, origin, 1.5f);
        }
        public void Stop()
        {
            swingTimer.Stop();
            player.itemSwing = null;
        }
    }
}
