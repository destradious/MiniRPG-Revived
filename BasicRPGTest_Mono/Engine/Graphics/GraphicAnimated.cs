﻿using BasicRPGTest_Mono.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace RPGEngine
{

    public class GraphicAnimated : Graphic
    {
        public int rows { get; set; }
        public int columns { get; set; }
        public int currentFrame;
        private int totalFrames;

        private int _framerate;
        private int framerate 
        { 
            get { return _framerate; }
            set
            {
                _framerate = 1000 / value;
                frameTimer.Interval = 1000 / value;
            }
        }
        private Timer frameTimer;


        public GraphicAnimated(Texture2D texture, int rows, int columns, int framerate = 15) : base (texture)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            currentFrame = 0;
            totalFrames = this.rows * this.columns;

            frameTimer = new Timer(framerate);
            this.framerate = framerate;

            frameTimer.Elapsed += update;
            frameTimer.Start();

        }
        public GraphicAnimated(GraphicAnimated graphic) : base (graphic.texture)
        {
            texture = graphic.texture;
            rows = graphic.rows;
            columns = graphic.columns;
            currentFrame = 0;
            totalFrames = rows * columns;

            frameTimer = new Timer(graphic.framerate);
            framerate = graphic.framerate;

            frameTimer.Elapsed += update;
            frameTimer.Start();
        }


        public void update(Object source, ElapsedEventArgs args)
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public override void draw(SpriteBatch spriteBatch, Vector2 location)
        {
            draw(spriteBatch, location, Color.White);
        }
        public override void draw(SpriteBatch spriteBatch, Vector2 location, Color tintColor)
        {
            width = texture.Width / columns;
            height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, tintColor, 0f, new Vector2(width / 2, height / 2), SpriteEffects.None, 0f);

        }

    }
}
