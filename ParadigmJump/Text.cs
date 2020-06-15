using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Text : GameObject
    {
        public Text(string text, Color clr, string font, int fontsize, float x, float y) : base(clr)
        {
            X = x;
            Y = y;
            Font = font;
            FontSize = fontsize;
            Value = text;
        }

        public override void Draw()
        {
            SplashKit.DrawText(Value, Color, Font, FontSize, X, Y);
        }

        public override bool IsAt(Point2D pt2D)
        {
            throw new NotImplementedException();
        }

        public string Value { get; set; }

        public string Font { get; set; }

        public int FontSize { get; set; }
    }
}
