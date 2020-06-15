using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class HP : HitCube
    {
        public HP(Color clr, float x, float y, int width, int height, bool hit, string text) : base(clr, x, y, width, height, hit)
        {
            Text = text;
        }

        public override void Draw()
        {
            SplashKit.FillRectangle(Color, X, Y, Width, Height);
            if(Text == "HP") SplashKit.DrawText(Text, Color.White, Game.getInstance().DefaultFont, 15, X + 15, Y + 5);
            else SplashKit.DrawText(Text, Color.White, Game.getInstance().DefaultFont, 15, X + 7, Y + 5);

        }

        public string Text { get; set; }
    }
}
