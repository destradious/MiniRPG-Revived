﻿using BasicRPGTest_Mono.Engine.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RPGEngine;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BasicRPGTest_Mono.Engine
{
    public class Entity
    {
        public string name { get; set; }
        public int id { get; set; }
        public int instanceId { get; set; }
        public Graphic graphic { get; set; }
        public Rectangle boundingBox { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TilePosition
        {
            get
            {
                Vector2 pos = new Vector2(Position.X, Position.Y);

                pos.X = pos.X / TileManager.dimensions;
                pos.Y = pos.Y / TileManager.dimensions;

                return pos;
            }
        }
        public Vector2 CenteredPosition
        {
            get
            {
                return new Vector2(Position.X + (graphic.texture.Width / 2), Position.Y + (graphic.texture.Height / 2));
            }
        }

        private Color _tintColor = Color.White;
        public Color tintColor
        {
            get { return _tintColor; }
            set
            {
                _tintColor = value;
            }
        }


        public Entity(Graphic graphic) : this(graphic, new Rectangle(0, 0, graphic.width, graphic.height)) { }
        public Entity(Graphic graphic, Rectangle box)
        {
            this.graphic = graphic;
            boundingBox = box;
            if (GetType() == typeof(Entity)) id = EntityManager.entities.Count;


            //EntityManager.add(this);
        }

        public bool isCollidingWith(Rectangle box)
        {
            if (boundingBox.Intersects(box)) return true;
            return false;
        }


        public virtual void update()
        {
        }
        public virtual void draw(SpriteBatch batch)
        {
            if (!Camera.camera.BoundingRectangle.Intersects(boundingBox)) return;
            graphic.draw(batch, Position, tintColor);

        }


        public virtual Rectangle getBox(Vector2 pos)
        {
            int x = (int)(pos.X - (boundingBox.Width - (boundingBox.Width / 2)));
            int y = (int)(pos.Y - (boundingBox.Height - (graphic.texture.Height / 2)));

            Rectangle box = new Rectangle(x, y, boundingBox.Width, boundingBox.Height);

            return box;
        }

    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
}
