using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Cube : HitCube
    {
        public Cube(Color clr, float x, float y, int width, int height, bool hit, bool bomb) : base(clr, x, y, width, height, hit)
        {
            Bomb = bomb;
        }

        public override void Draw()
        {
            if (Bomb) SplashKit.FillRectangle(Color, X, Y, Width, Height);
            else SplashKit.DrawRectangle(Color, X, Y, Width, Height);
        }

        public bool Bomb { get; set; }
    }
}
