using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Platform : Block
    {
        public Platform(Color clr, float x, float y, int width, int height) : base (clr, x, y, width, height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public Platform() : this ( Color.Black, 0,0, 600, 10 ) { }

        public override void Draw()
        {
            SplashKit.FillRectangle(Color, X, Y, Width, Height);
        }
    }
}
