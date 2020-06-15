using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public abstract class HitCube : Block
    {
        public HitCube(Color clr, float x, float y, int width, int height, bool hit) : base(clr, x, y, width, height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Hit = hit;
        }

        public override void Draw()
        {
            SplashKit.FillRectangle(Color, X, Y, Width, Height);
        }

        public bool Hit { get; set; }
    }
}
