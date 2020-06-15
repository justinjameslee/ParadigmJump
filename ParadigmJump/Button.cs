using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Button : Block
    {
        public Button(Color clr, float x, float y, int width, int height) : base (clr, x, y, width, height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Toggle = false;
        }

        public override void Draw()
        {
            if(Hover) SplashKit.FillRectangle(Color, X, Y, Width, Height);
            else SplashKit.DrawRectangle(Color, X, Y, Width, Height);
        }

        public bool Hover { get; set; } = false;

        public bool Toggle { get; set; }
    }
}
